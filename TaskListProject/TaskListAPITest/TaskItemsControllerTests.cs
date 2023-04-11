


namespace TaskListAPITest
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
        public void PostTaskItem_CreatesNewTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var newTaskRequest = new TaskItemRequest { Title = "Task 1", Description = "Task 1 Description" };
    
            // Act
            var result = controller.CreateTask(newTaskRequest);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdTask = Assert.IsType<TaskItem>(createdAtActionResult.Value);
            Assert.Equal(newTaskRequest.Title, createdTask.Title);
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
        public async Task PutTaskItem_UpdatesTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var taskToUpdate = new TaskItem { Id = 1, Title = "Task 1", Completed = false };
            dbContext.Tasks.Add(taskToUpdate);
            dbContext.SaveChanges();

            var taskItemUpdate = new TaskItemUpdate { Title = "Updated Task 1", Description = "Updated Description", Completed = true };

            // Act
            var result = controller.UpdateTask(1, taskItemUpdate);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var updatedTask = Assert.IsType<TaskItem>(okObjectResult.Value);
            Assert.Equal(taskItemUpdate.Title, updatedTask.Title);
            Assert.Equal(taskItemUpdate.Description, updatedTask.Description);
            Assert.Equal(taskItemUpdate.Completed, updatedTask.Completed);

            var task = await dbContext.Tasks.FindAsync(1);
            Assert.NotNull(task);
            Assert.Equal(taskItemUpdate.Title, task.Title);
            Assert.Equal(taskItemUpdate.Description, task.Description);
            Assert.Equal(taskItemUpdate.Completed, task.Completed);
        }

        [Fact]
        public async Task PutTaskItem_ReturnsNotFoundOnNonExistentTask()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var taskItemUpdate = new TaskItemUpdate { Title = "Non-Existent Task", Completed = false };
            // Act
            var result = controller.UpdateTask(1, taskItemUpdate);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void PutTaskItem_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var existingTask = new TaskItem { Id = 1, Title = "Existing Task", Completed = false };
            dbContext.Tasks.Add(existingTask);
            dbContext.SaveChanges();

            var taskItemUpdate = new TaskItemUpdate { Title = "Updated Task", Completed = true };

            // Act
            var result = controller.UpdateTask(2, taskItemUpdate);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void PutTaskItem_ReturnsBadRequest_WhenTitleMissing()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new TaskItemsController(dbContext);

            var existingTask = new TaskItem { Id = 1, Title = "Existing Task", Completed = false };
            dbContext.Tasks.Add(existingTask);
            dbContext.SaveChanges();

            var taskItemUpdate = new TaskItemUpdate { Title = null, Completed = true };

            // Act
            var result = controller.UpdateTask(1, taskItemUpdate);

            // Assert
            Assert.IsType<BadRequestResult>(result);

            // Verify that the task item was not actually updated in the database
            var updatedTask = dbContext.Tasks.Find(1);
            Assert.Equal(existingTask.Title, updatedTask.Title);
            Assert.Equal(existingTask.Completed, updatedTask.Completed);
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
        
    }

}