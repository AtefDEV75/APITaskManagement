
using BusinessLayer.Dtos;
using Task = DataAccessLayer.Entities.Task;

namespace BusinessLayer.Services.TaskService
{
    public interface ITaskService 
    {
        List<TaskDto> GetTasks();
        TaskDto GetTask(int id);
        void CreateTask(TaskDto task);
        void UpdateTask(TaskDto task,int id);
        void DeleteTask(TaskDto task,int id);
    }
}
