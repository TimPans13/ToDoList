using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ToDoTask> Get();

        Task<List<ToDoTask>> Gets();

        Task<ToDoTask> Add(ToDoTask task);

        Task Destroy(int taskId);

        Task Toggle(int taskId);

        Task<ToDoTask> Copy(int taskId); 
        Task Update(int taskId, string newText);

    }
}
