using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace TaskListAPITest
{
    public class SeleniumTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
    {
        private readonly FirefoxDriver _driver;
        private readonly CustomWebApplicationFactory _factory;

        public SeleniumTests(CustomWebApplicationFactory factory)
        {
            
            _factory = factory;

            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            _driver = new FirefoxDriver(options);
        }
        [Fact]
        public void AppLoadsSuccessfully()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000");

            Thread.Sleep(2000);
            
            // Verify that the page title is correct
            Assert.Equal("DoMore", _driver.Title);

            // Verify that there are no broken links or missing resources
            var elements = _driver.FindElements(By.TagName("img"));
            foreach (var element in elements)
            {
                Assert.True(element.Displayed);
            }
        }
        [Fact]
        public void CreateTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");   

            // Find the input field for the new task and enter a task name
            IWebElement inputField = _driver.FindElement(By.Id("todo-input"));
   
            inputField.SendKeys("New task");
            Thread.Sleep(2000);
            inputField.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            // Get all task items
            var taskItems = _driver.FindElements(By.CssSelector("[data-testid='task-item']"));

            // Verify that the new task was added to the list
            var newTask = taskItems.FirstOrDefault(task => task.Text.Contains("New task"));
            Assert.NotNull(newTask);
        }



        
        [Fact]
        public void CompleteTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");

            IWebElement inputField = _driver.FindElement(By.Id("todo-input"));
            inputField.SendKeys("New task");
            Thread.Sleep(2000);
            inputField.SendKeys(Keys.Enter);

            // Find the checkbox for the first task
            // Find the checkbox for the first task
            IWebElement firstTaskCheckbox = _driver.FindElement(By.CssSelector("li[data-testid='task-item']:first-child input[type='checkbox']"));

            // Click the checkbox using JavaScript
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", firstTaskCheckbox);
            Thread.Sleep(2000);

            // Verify that the first task is marked as completed
            bool isCompleted = Convert.ToBoolean(firstTaskCheckbox.GetAttribute("completed"));
            Assert.True(isCompleted);
        }



        [Fact]
        public void DeleteTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");

            // Find the button to delete the first task and click it
            IWebElement firstTaskDeleteButton = _driver.FindElement(By.CssSelector("li:first-child [data-testid='delete-button']"));
            firstTaskDeleteButton.Click();

            // Verify that the first task was deleted
            Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.CssSelector("li:first-child")));
        }



        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
