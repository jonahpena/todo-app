using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PracticeAPI.Models;
using System.Threading.Tasks;
using Task = PracticeAPI.Models.Task;
using System.Linq;

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



    }

}