//using Microsoft.EntityFrameworkCore;
//using TestTask.Data;
//using TestTask.Enums;
//using TestTask.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TestTask.Services.Interfaces
//{
//    public class TaskService : ITaskService
//    {
//        private readonly ApplicationDbContext _dbContext;

//        public TaskService(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<ToDoTask> Get()
//        {
//            try
//            {
//                var lastAddedTask = await _dbContext.Tasks
//                    .OrderByDescending(u => u.Id)
//                    .FirstOrDefaultAsync();

//                return lastAddedTask;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred in GetUser: {ex.Message}");
//                throw;
//            }
//        }

//        public async Task<List<ToDoTask>> Gets()
//        {
//            try
//            {
//                var inactiveTasks = await _dbContext.Tasks
//                    .ToListAsync();

//                return inactiveTasks;

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred in GetUsers: {ex.Message}");
//                throw;
//            }
//        }
//    }
//}


using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Services.Interfaces
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ToDoTask> Get()
        {
            try
            {
                var lastAddedTask = await _dbContext.Tasks
                    .OrderByDescending(u => u.Id)
                    .FirstOrDefaultAsync();

                return lastAddedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Get: {ex.Message}");
                throw;
            }
        }

        public async Task<ToDoTask> Get(int Id)
        {
            try
            {
                var lastAddedTask = await _dbContext.Tasks
                    .FirstOrDefaultAsync(u => u.Id == Id);


                return lastAddedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Get: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ToDoTask>> Gets()
        {
            try
            {
                var tasks = await _dbContext.Tasks
                    .ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Gets: {ex.Message}");
                throw;
            }
        }

        public async Task<ToDoTask> Add(ToDoTask task)
        {
            try
            {
                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();

                var lastAddedTask = await _dbContext.Tasks
                   .OrderByDescending(u => u.Id)
                   .FirstOrDefaultAsync();

                return lastAddedTask;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Add: {ex.Message}");
                throw;
            }
        }

        public async Task Destroy(int taskId)
        {
            try
            {
                var taskToDelete = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
                if (taskToDelete != null)
                {
                    _dbContext.Tasks.Remove(taskToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Destroy: {ex.Message}");
                throw;
            }
        }

        public async Task Toggle(int taskId)
        {
            try
            {
                var taskToToggle = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
                if (taskToToggle != null)
                {
                    taskToToggle.Completed = !taskToToggle.Completed;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Toggle: {ex.Message}");
                throw;
            }
        }

        public async Task<ToDoTask> Copy(int taskId)
        {
            try
            {
                var taskToCopy = await Get(taskId);
                var newTask = new ToDoTask
                {
                    Id = 0,
                    Text = taskToCopy.Text,
                    Completed=false
                                           
                };
                _dbContext.Tasks.Add(newTask);
                await _dbContext.SaveChangesAsync();

                var lastAddedTask = await _dbContext.Tasks
                   .OrderByDescending(u => u.Id)
                   .FirstOrDefaultAsync();

                return lastAddedTask;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Copy: {ex.Message}");
                throw;
            }
        }

        public async Task Update(int taskId,string newText)
        {
            try
            {
                var taskToUpdate = await Get(taskId);

                //if (taskToUpdate != null)
                //{
                 taskToUpdate.Text = newText;
                //taskToUpdate.Completed = !taskToUpdate.Completed;
                await _dbContext.SaveChangesAsync();
               // }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Update: {ex.Message}");
                throw;
            }
        }
    }
}
