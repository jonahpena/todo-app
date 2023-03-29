using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

using Assert = Xunit.Assert;

namespace TaskListAPITest
{
    [TestCaseOrderer("TaskListAPITest.TestMethodOrderer", "TaskListAPITest")]
    public class SeleniumTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
    {
        private readonly FirefoxDriver _driver;

        public SeleniumTests(CustomWebApplicationFactory factory)
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            _driver = new FirefoxDriver(options);
        }
        
        
        [Fact]
        [TestOrder(1)]
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
        
        [TestOrder(2)]
        [Fact]
        public void CreateTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");   

            // Find the input field for the new task and enter a task name
            IWebElement inputField = _driver.FindElement(By.Id("todo-input"));
   
            inputField.SendKeys("New task");
            Thread.Sleep(1000);
            inputField.SendKeys(Keys.Enter);
            Thread.Sleep(1000);
            inputField.SendKeys("New task");
            
            Thread.Sleep(1000);
            inputField.SendKeys(Keys.Enter);
            
            Thread.Sleep(1000);

            // Get all task items
            var taskItems = _driver.FindElements(By.CssSelector("[data-testid='task-item']"));

            // Verify that the new task was added to the list
            var newTask = taskItems.FirstOrDefault(task => task.Text.Contains("New task"));
            Assert.NotNull(newTask);
        }

        [TestOrder(3)]
        [Fact]
        public void CompleteTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");

            Thread.Sleep(1000);
            // Find the checkbox for the first task
            IWebElement firstTaskCheckbox = _driver.FindElements(By.CssSelector("input[data-testid='task-checkbox']"))[0];
            Thread.Sleep(1000);
            
            // Click the checkbox using JavaScript
            IJavaScriptExecutor js = _driver;
            js.ExecuteScript("arguments[0].click();", firstTaskCheckbox);
    
            
            Thread.Sleep(1000);
            
            // find the button element by its ID or other locators
            IWebElement buttonElement = _driver.FindElement(By.Id("completed-button"));

// click the button
            buttonElement.Click();
            
            Thread.Sleep(1000);
            
            // Refetch the checkbox element
            IWebElement firstCompletedTaskCheckbox = _driver.FindElement(By.CssSelector("ul[data-testid='completed-task-list'] input[data-testid='task-checkbox']:nth-child(1)"));
            bool isCompleted = Convert.ToBoolean(firstCompletedTaskCheckbox.GetAttribute("checked"));
            Assert.True(isCompleted);
        }
        
        [Fact]
        [TestOrder(4)]        
        public void DeleteTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");
            Thread.Sleep(1000);

            // Find the button to delete the first task and click it
            IWebElement firstTaskDeleteButton = _driver.FindElement(By.CssSelector("li:first-child [data-testid='delete-button']"));
            firstTaskDeleteButton.Click();

            Thread.Sleep(1000);
            // Verify that the first task was deleted
            Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.CssSelector("li:first-child")));
        }
        
        [Fact]
        [TestOrder(5)]        
        public void DeleteCompletedTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");
            
            IWebElement buttonElement = _driver.FindElement(By.Id("completed-button"));
            buttonElement.Click();
            
            Thread.Sleep(1500);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement firstCompletedTaskDeleteButton = _driver.FindElement(By.CssSelector(".completed-list-item + .completed-delete-icon [data-testid='completed-delete-button']"));
            firstCompletedTaskDeleteButton.Click();
            
            Thread.Sleep(1000);
     
            // Verify that the first completed task was deleted
            Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.CssSelector(".completed-items-dropdown li.completed-list-bubble:first-child")));        }


        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
