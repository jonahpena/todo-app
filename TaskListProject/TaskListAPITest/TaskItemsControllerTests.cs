using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListAPIProject.Controllers;
using TaskListAPIProject.Data;
using TaskListAPIProject.Models;

namespace TaskListAPIProject.Tests
{
    public class TaskItemsControllerTests
    {
        private TaskDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new TaskDbContext(options);
        }

        [Fact]
        public async Task GetTasks_ReturnsAllTasks()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            dbContext.Tasks.Add(new TaskItem { Id = 1, Title = "Task 1", Completed = false });
            dbContext.Tasks.Add(new TaskItem { Id = 2, Title = "Task 2", Completed = false });
            dbContext.SaveChanges();

            // Act
            var result = await controller.GetTasks();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TaskItem>>>(result);
            var tasks = Assert.IsType<List<TaskItem>>(actionResult.Value);
            Assert.Equal(2, tasks.Count);
        }

        [Fact]
        public async Task GetTaskItem_ReturnsNotFoundOnNonExistentTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            // Act
            var result = await controller.GetTaskItem(1);

            // Assert
            Assert.Null(result.Value);
        }
        [Fact]
        public async Task GetTasks_ReturnsEmptyListWhenNoTasks()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            // Act
            var result = await controller.GetTasks();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TaskItem>>>(result);
            var tasks = Assert.IsType<List<TaskItem>>(actionResult.Value);
            Assert.Empty(tasks);
        }






        [Fact]
        public async Task GetTaskItem_ReturnsTaskById()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var taskItem = new TaskItem { Id = 1, Title = "Task 1", Completed = false };
            dbContext.Tasks.Add(taskItem);
            dbContext.SaveChanges();

            // Act
            var result = await controller.GetTaskItem(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TaskItem>>(result);
            var returnedTask = Assert.IsType<TaskItem>(actionResult.Value);
            Assert.Equal(taskItem.Id, returnedTask.Id);
            Assert.Equal(taskItem.Title, returnedTask.Title);
            Assert.Equal(taskItem.Completed, returnedTask.Completed);
        }
        // PostTaskItem tests
        [Fact]
        public async Task PostTaskItem_CreatesNewTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var newTask = new TaskItem { Id = 1, Title = "Task 1", Completed = false };

            // Act
            var result = await controller.PostTaskItem(newTask);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TaskItem>>(result);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdTask = Assert.IsType<TaskItem>(createdAtActionResult.Value);
            Assert.Equal(newTask.Id, createdTask.Id);
            Assert.Equal(newTask.Title, createdTask.Title);
            Assert.Equal(newTask.Completed, createdTask.Completed);
        }
        // DeleteTaskItem tests
        [Fact]
        public async Task DeleteTaskItem_DeletesTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var taskToDelete = new TaskItem { Id = 1, Title = "Task 1", Completed = false };
            dbContext.Tasks.Add(taskToDelete);
            dbContext.SaveChanges();

            // Act
            var result = await controller.DeleteTaskItem(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var task = await dbContext.Tasks.FindAsync(1);
            Assert.Null(task);
        }

    }

}