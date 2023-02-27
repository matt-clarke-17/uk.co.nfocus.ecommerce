using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework.Internal;

namespace uk.co.nfocus.ecommerce.Utils.SupportNunit
{
    public class TestBaseClass
    {
        public IWebDriver driver;
        protected string browser;
        protected string baseUrl;

        [SetUp]
        public void Setup()
        {
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

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
