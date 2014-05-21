using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class test
    {
        private RemoteWebDriver driver;


        [SetUp]
        public void SetupTest()
        {
            // Look for an environment variable
            string server = null;
            server = System.Environment.GetEnvironmentVariable("SELENIUM_SERVER");
            if (server == null)
            {
                server = "http://hornet:4444/wd/hub";
            }

            // Remote testing
            driver = new RemoteWebDriver(new Uri(server), DesiredCapabilities.Firefox());
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void FirstSeleniumTest()
        {
            driver.Navigate().GoToUrl("http://www.google.com/");
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("a test");
            Assert.AreEqual(driver.Title, "Google");
        }
        [Test]
        public void MySecondSeleniumTest()
        {
            // Navigate to google
            driver.Navigate().GoToUrl("http://www.google.com/");
            IWebElement query = driver.FindElement(By.Name("q"));

            // Write a query into the window
            query.SendKeys("a test");

            // wait at maximum ten seconds for results to display
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("ires"));
            });

            // take a screenshot of the result for visual verification
            var fileName = TestContext.CurrentContext.Test.Name + "-" + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".png";
            driver.GetScreenshot().SaveAsFile(fileName, System.Drawing.Imaging.ImageFormat.Png);

            // perform an code assertion
            Assert.AreEqual(driver.Title, "Google");
        }
    }
}