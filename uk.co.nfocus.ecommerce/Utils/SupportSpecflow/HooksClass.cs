using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace uk.co.nfocus.ecommerce.Utils.SupportSpecflow
{
    [Binding]
    public class HooksClass
    {

        public static IWebDriver driver;
        protected string browser;
        protected string baseUrl;
        public static string username;
        public static string password;

        [Before, Scope(Tag = "GUI")] //Equivalent of nUnit [SetUp] - could also use [BeforeScenario]
        public void SetUp()
        {
            username = Environment.GetEnvironmentVariable("USERNAME");
            //Console.WriteLine(username);
            password = Environment.GetEnvironmentVariable("PASSWORD");
            //Console.WriteLine(password);
            browser = TestContext.Parameters["browser"];
            //Console.WriteLine(browser);
            baseUrl = TestContext.Parameters["baseURL"];
            //Console.WriteLine(baseUrl);

            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                default:
                    Console.WriteLine("Unknown browser - starting chrome");
                    driver = new ChromeDriver();
                    break;
            }

            driver.Url = baseUrl;
        }
    

        [After("GUI")] //Equivalent of nUnit [TearDown] - could also use [AfterScenario]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
