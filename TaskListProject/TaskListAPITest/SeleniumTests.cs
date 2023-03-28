using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace TaskListAPITest
{
    public class SeleniumTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public SeleniumTests()
        {
            _driver = new FirefoxDriver();
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
            Thread.Sleep(1000);
            inputField.SendKeys(Keys.Enter);
            
            Thread.Sleep(1000);
            inputField.SendKeys(Keys.Enter);

            // Verify that the new task was added to the list
            IWebElement newTask = _driver.FindElement(By.XPath("//li[contains(text(),'New task')]"));
            Assert.NotNull(newTask);
        }
        
        [Fact]
        public void CompleteTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");

            // Find the checkbox for the first task and click it
            IWebElement firstTaskCheckbox = _driver.FindElement(By.CssSelector("li:first-child input[type=checkbox]"));
            firstTaskCheckbox.Click();

            // Verify that the first task is marked as completed
            IWebElement firstTask = _driver.FindElement(By.CssSelector("li:first-child"));
            Assert.Contains("completed", firstTask.GetAttribute("class"));
        }

        [Fact]
        public void DeleteTask()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/tasklist");

            // Find the button to delete the first task and click it
            IWebElement firstTaskDeleteButton = _driver.FindElement(By.CssSelector("li:first-child button.delete-button"));
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
