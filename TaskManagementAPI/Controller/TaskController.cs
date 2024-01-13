using BusinessLayer.Dtos;
using BusinessLayer.Services.TaskService;
using BusinessLayer.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Task = DataAccessLayer.Entities.Task;


namespace PresentationLayer.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IEmailService _emailService;

        public TaskController(ITaskService taskService, IEmailService emailService)
        {
            _taskService = taskService;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetTaskById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Task> GetTaskById(int id)
        {
            var task = _taskService.GetTask(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet]
        [Route("All", Name = "GetAllTasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<TaskDto>> GetAllTasks()
        {
            return _taskService.GetTasks();
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Task> CreateTask([FromBody] TaskDto task)
        {   
            if (task == null)
            {
                return BadRequest("Task object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            _taskService.CreateTask(task);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Task> UpdateTask(int id, [FromBody] TaskDto task)
        {
            if (task == null)
            {
                return BadRequest("Task object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var dbtask = _taskService.GetTask(id);
            if (!dbtask.Id.Equals(id))
            {
                return NotFound();
            }
            _taskService.UpdateTask(task,id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteTask(int id)
        {
            var dbtask = _taskService.GetTask(id);
            if (!dbtask.Id.Equals(id))
            {
                return NotFound();
            }
            _taskService.DeleteTask(dbtask,id);
            return NoContent();
        }

        [HttpGet]
        [Route("GenerateExcel")]
        public async Task<IActionResult> GenerateExcel()
        {
            var tasks = _taskService.GetTasks();
            string emailtemplatepath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate//TaskReport.html");
            string htmldata = System.IO.File.ReadAllText(emailtemplatepath);
            string excelstring = "";
            foreach (var task in tasks)
            {
                excelstring += "<tr><td>" + task.Title + "</td><td>" + task.Description + "</td><td>" + task.Assignee + "</td></tr>";
            }
            htmldata = htmldata.Replace("@@ActualData", excelstring);
            string StoredFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles", DateTime.Now.Ticks.ToString() + ".xls");
            System.IO.File.AppendAllText(StoredFilePath, htmldata);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(StoredFilePath, out var contettype))
            {
                contettype = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(StoredFilePath);

            return File(bytes, contettype, Path.Combine(StoredFilePath));
        }

        [HttpPost]
        [Route("SendEmail")]
        public IActionResult SendEmail(string email)
        {
            if (email == null)
            {
                return BadRequest("Email is required");
            }
            _emailService.SendEmail(email);
            return Ok();
        }

    }
}

