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
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
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
            Assert.IsType<NotFoundResult>(result.Result);
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
        
        [Fact]
        public async Task PutTaskItem_UpdatesTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var taskToUpdate = new TaskItem { Id = 1, Title = "Task 1", Completed = false };
            dbContext.Tasks.Add(taskToUpdate);
            dbContext.SaveChanges();

            var updatedTask = new TaskItem { Id = 1, Title = "Updated Task 1", Completed = true };

            // Act
            var result = await controller.PutTaskItem(1, updatedTask);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var task = await dbContext.Tasks.FindAsync(1);
            Assert.Equal(updatedTask.Id, task.Id);
            Assert.Equal(updatedTask.Title, task.Title);
            Assert.Equal(updatedTask.Completed, task.Completed);
        }
        [Fact]
        public async Task DeleteTaskItem_ReturnsNotFoundOnNonExistentTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            // Act
            var result = await controller.DeleteTaskItem(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public async Task PutTaskItem_ReturnsNotFoundOnNonExistentTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var nonExistentTask = new TaskItem { Id = 1, Title = "Non-Existent Task", Completed = false };

            // Act
            var result = await controller.PutTaskItem(1, nonExistentTask);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        
        [Fact]
        public void TaskItem_DescriptionProperty()
        {
            // Arrange
            var taskItem = new TaskItem();
            var description = "Sample description";

            // Act
            taskItem.Description = description;

            // Assert
            Assert.Equal(description, taskItem.Description);
        }
        
        [Fact]
        public async Task PutTaskItem_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var existingTask = new TaskItem { Id = 1, Title = "Existing Task", Completed = false };
            dbContext.Tasks.Add(existingTask);
            await dbContext.SaveChangesAsync();

            var updatedTask = new TaskItem { Id = 2, Title = "Updated Task", Completed = true };

            // Act
            var result = await controller.PutTaskItem(1, updatedTask);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public async Task PutTaskItem_ReturnsBadRequest_WhenTitleMissing()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var existingTask = new TaskItem { Id = 1, Title = "Existing Task", Completed = false };
            dbContext.Tasks.Add(existingTask);
            await dbContext.SaveChangesAsync();

            var updatedTask = new TaskItem { Id = 1, Title = null, Completed = true };

            // Act
            var result = await controller.PutTaskItem(1, updatedTask);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        
    }

}