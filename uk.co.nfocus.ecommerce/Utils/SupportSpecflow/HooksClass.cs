﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.nfocus.ecommerce.PageObjects;

namespace uk.co.nfocus.ecommerce.Utils.SupportSpecflow
{
    [Binding]
    public class HooksClass
    {
        //Create an empty object of type ScenarioContext
        private readonly ScenarioContext _scenarioContext;
        //Create an empty object of type IWebDriver
        private IWebDriver driver;
        public static string browser;
        public static string baseUrl;
        public static string username;
        public static string password;
        public static string street;
        public static string area;
        public static string region;
        public static string postcode;
        public static string phoneNumber;

        public HooksClass(ScenarioContext scenarioContext)
        {
            //Instantiate the scenario context dictionary properly
            _scenarioContext = scenarioContext;
            //allows us to store useful variables and context information between tests/scenario segments
        }

        [Before, Scope(Tag = "GUI")] //Equivalent of nUnit [SetUp] - could also use [BeforeScenario]
        public void SetUp()
        {
            loadVariables();            

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

            //Store the driver we just created in the scenario context dictionary
            //so it may be used in the actual tests 
            _scenarioContext["myDriver"] = driver;           
            //launches driver at given url 
            driver.Url = baseUrl;
        }


        [After("GUI")] //Equivalent of nUnit [TearDown] - could also use [AfterScenario]
        public void TearDown()
        {
            driver.Quit();
        }

        private void loadVariables()
        {
            //loads in all parameters from external library function with Assertions to allow for debugging 
            username = NonDriverAssists.acquireEnvironmentParameter("USERNAME");
            password = NonDriverAssists.acquireEnvironmentParameter("PASSWORD");
            browser = NonDriverAssists.acquireTestParameter("browser");
            baseUrl = NonDriverAssists.acquireTestParameter("baseURL");
            street = NonDriverAssists.acquireTestParameter("street");
            area = NonDriverAssists.acquireTestParameter("area");
            region = NonDriverAssists.acquireTestParameter("region");
            postcode = NonDriverAssists.acquireTestParameter("postcode");
            phoneNumber = NonDriverAssists.acquireTestParameter("phoneNumber");
        }
    }
}
