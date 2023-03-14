using Microsoft.AspNetCore.Mvc;
using Task = PracticeAPI.Models.Task;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static readonly List<Task> tasks = new()
    {
        new Task(){TaskId = 0, Title = "Task One", Description = "The First Task"},

    };

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return tasks;
        }

        [HttpPost]
        public void Post([FromBody] Task task)
        {
            tasks.Add(task);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Task task)
        {
            tasks[id] = task;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            tasks.RemoveAt(id);
        }
    }

}