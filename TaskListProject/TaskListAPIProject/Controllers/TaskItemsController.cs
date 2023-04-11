using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListAPIProject.Data;
using TaskListAPIProject.Models;

namespace TaskListAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskItemsController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }


            return taskItem;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItemUpdate taskItemUpdate)
        {
            var taskItem = _context.Tasks.Find(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.Title = taskItemUpdate.Title;
            taskItem.Description = taskItemUpdate.Description;
            taskItem.Completed = taskItemUpdate.Completed;
            taskItem.Version++; 

            _context.SaveChanges();

            return Ok(taskItem);
        }


        [HttpPost]
        public IActionResult CreateTask(TaskItemRequest taskItemRequest)
        {
            TaskItem taskItem = new TaskItem
            {
                Title = taskItemRequest.Title,
                Description = taskItemRequest.Description
            };

            _context.Tasks.Add(taskItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskItem);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
