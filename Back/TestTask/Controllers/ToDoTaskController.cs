using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    /// <summary>
    /// Orders controller.
    /// DO NOT change anything here. Use created contract and provide only needed implementation.
    /// </summary>
    [Route("api/v1/orders")]///tasks
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public ToDoTaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }


        [HttpGet]
        [Route("Task-Get")]
        public async Task<IActionResult> Get()
        {
            var result = await this.taskService.Get();
            return this.Ok(result);
        }

        [HttpGet]
        [Route("Task-Gets")]
        public async Task<IActionResult> Gets()
        {
            var result = await this.taskService.Gets();
            return this.Ok(result);
        }

        [HttpPost]
        [Route("Task-Add")]
        public async Task<IActionResult> Add([FromBody] ToDoTask task)
        {
            try
            {
                var result=await this.taskService.Add(task);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to add task: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Task-Destroy/{taskId}")]
        public async Task<IActionResult> Destroy(int taskId)
        {
            try
            {
                await this.taskService.Destroy(taskId);
                return this.Ok($"Task with ID {taskId} deleted successfully");
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to delete task: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Task-Toggle/{taskId}")]
        public async Task<IActionResult> Toggle(int taskId)
        {
            try
            {
                await this.taskService.Toggle(taskId);
                return this.Ok($"Task with ID {taskId} Toggle successfully");
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to Toggle task: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Task-Copy/{taskId}")]
        public async Task<IActionResult> Copy(int taskId)
        {
            try
            {
                var result = await this.taskService.Copy(taskId);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to copy task: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Task-Update/{taskId}")]
        public async Task<IActionResult> Update(int taskId, [FromBody] string newText)
        {
            try
            {
                await this.taskService.Update(taskId, newText);
                return this.Ok($"Task with ID {taskId} Update successfully");
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to Update task: {ex.Message}");
            }
        }

    }
}
