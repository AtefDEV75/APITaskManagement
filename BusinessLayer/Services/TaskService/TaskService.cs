
using AutoMapper;
using BusinessLayer.Dtos;
using BusinessLayer.Services.EmailService;
using DataAccessLayer.Repositories.Contracts;
using Task = DataAccessLayer.Entities.Task;

namespace BusinessLayer.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository<Task> _repository;
        private  Mapper _Taskmapper;
        private readonly IEmailService _emailService;

        public TaskService(ITaskRepository<Task> repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
            var _configTask = new MapperConfiguration(ct => ct.CreateMap<Task, TaskDto>().ReverseMap());
            _Taskmapper = new Mapper(_configTask);
        }

        public void CreateTask(TaskDto newTask)
        {
            var task = _Taskmapper.Map<Task>(newTask);
            _repository.Create(task);
            //send a welcome email to a new task assignee
            if (task.Assignee != null)
                _emailService.SendEmail(task.Assignee);
        }

        public void UpdateTask(TaskDto updatedTask,int id)
        {
            var task = _Taskmapper.Map<Task>(updatedTask);
            task.Id = id;
            _repository.Update(task);
        }

        public void DeleteTask(TaskDto deletedTask,int id)
        {
            var task = _Taskmapper.Map<Task>(deletedTask);
            task.Id = id;
            _repository.Delete(task);
        }

        public TaskDto GetTask(int id)
        {
            var result = _repository.FindByCondition(t => t.Id == id).FirstOrDefault();
            var task = _Taskmapper.Map<TaskDto>(result);
            if (task == null)
            {
                throw new Exception("La valeur avec l'ID spécifié n'a pas été trouvée.");
            }

            return task;
        }

        public List<TaskDto> GetTasks()
        {
            var tasks = _repository.FindAll().ToList();
            return _Taskmapper.Map<List<TaskDto>>(tasks);
        }

    }

}
