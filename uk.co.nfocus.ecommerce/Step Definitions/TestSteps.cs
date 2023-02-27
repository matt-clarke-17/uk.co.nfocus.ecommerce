using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static uk.co.nfocus.ecommerce.Utils.SupportSpecflow.HooksClass;
using uk.co.nfocus.ecommerce.PageObjects;

namespace uk.co.nfocus.ecommerce.Step_Definitions
{
    
        [Binding]
        public class TestSteps
        {
        static string orderNumber;

            private readonly ScenarioContext _scenarioContext;

            public TestSteps(ScenarioContext scenarioContext)
            {
                _scenarioContext = scenarioContext;
            }

            [Given(@"I am on Edgewords")]
            public void GivenIAmOnEdgewords()
            {
            Console.WriteLine(driver.Url);
                Assert.That(driver.Url.Equals("https://www.edgewordstraining.co.uk/demo-site/"));
            }

            [When(@"I click on Log In")]
            public void WhenIClickOnLogIn()
            {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            }

            [Then(@"I am taken to the login page")]
            public void ThenIAmTakenToTheLoginPage()
            {
            Assert.That(driver.Title, Does.Contain("My account"));
            }
            [Given(@"I am on the login page")]
            public void GivenIAmOnTheLoginPage()
            {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            Assert.That(driver.Url.Equals("https://www.edgewordstraining.co.uk/demo-site/my-account/"));
            }

            [When(@"I click on the boxes to fill my login details")]
            public void WhenIClickOnTheBoxesToFillMyLoginDetails()
            {
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            }

            [Then(@"I can log in")]
            public void ThenICanLogIn()
            {
               driver.FindElement(By.Name("login")).Click();
            }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
        }

        [When(@"I click on shop")]
        public void WhenIClickOnShop()
        {
            TopNav topNav = new TopNav(driver);
            topNav.Shop.Click();
        }

        [Then(@"I will be taken to the shop page")]
        public void ThenIWillBeTakenToTheShopPage()
        {
            Assert.That(driver.Title, Does.Contain("Shop"));
        }

        [Given(@"I am logged in and on the shop")]
        public void GivenIAmLoggedInAndOnTheShop()
        {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
            topNav.Shop.Click();
        }

        [When(@"I search for the cap")]
        public void WhenISearchForTheCap()
        {
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys("Cap" + Keys.Enter);
            Assert.That(driver.Title, Does.Contain("Cap"));
        }

        [Then(@"I will be able to add it to the basket")]
        public void ThenIWillBeAbleToAddItToTheBasket()
        {
            driver.FindElement(By.Name("add-to-cart")).Click();
        }

        [Given(@"I am logged in and have the cap in my basket")]
        public void GivenIAmLoggedInAndHaveTheCapInMyBasket()
        {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
            topNav.Shop.Click();
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys("Cap" + Keys.Enter);
            driver.FindElement(By.Name("add-to-cart")).Click();
        }

        [When(@"I go to the checkout and add the discount code")]
        public void WhenIGoToTheCheckoutAndAddTheDiscountCode()
        {
            TopNav topNav = new TopNav(driver);
            topNav.Cart.Click();
            Assert.That(driver.Url.Equals("https://www.edgewordstraining.co.uk/demo-site/cart/"));
            driver.FindElement(By.Id("coupon_code")).SendKeys("edgewords" + Keys.Enter);
        }

        [Then(@"should reduce the cost when applied")]
        public void ThenShouldReduceTheCostWhenApplied()
        {
            Assert.That(driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount > bdi")).Text, Does.Contain("18.35"));
            //needs refinement/testing
            //delayed due to personal health concerns -  23/02/2023
            //likely need to assert on the banner pop up as opposed to the css of the form thats generated on a successful application of the discount code
            //update - feeling more alive so will revise this after feedback
            //further plans to update test cases to use parameters to test edge cases/exceptions
            //also update code to use test export 
        }

        [When(@"I insert my billing details in the check out")]
        public void WhenIInsertMyBillingDetailsInTheCheckOut()
        {
            TopNav topnav = new TopNav(driver);
            topnav.Checkout.Click();
            driver.FindElement(By.Id("billing_address_1")).Clear();
            driver.FindElement(By.Id("billing_address_1")).SendKeys("24 Palmyra Square North");
            driver.FindElement(By.Id("billing_city")).Clear();
            driver.FindElement(By.Id("billing_city")).SendKeys("Warrington");
            driver.FindElement(By.Id("billing_state")).Clear();
            driver.FindElement(By.Id("billing_state")).SendKeys("Warrington");
            driver.FindElement(By.Id("billing_postcode")).Clear();
            driver.FindElement(By.Id("billing_postcode")).SendKeys("WA1 9SJ");
            driver.FindElement(By.Id("billing_phone")).Clear();
            driver.FindElement(By.Id("billing_phone")).SendKeys("398276237692370");
        }

        [Then(@"I will be able to select cheque payments and place my order")]
        public void ThenIWillBeAbleToSelectChequePaymentsAndPlaceMyOrder()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("button#place_order")).Click();
            Thread.Sleep(10000);
        }

        [Given(@"I have placed an order")]
        public void GivenIHavePlacedAnOrder()
        {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
            topNav.Shop.Click();
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys("Cap" + Keys.Enter);
            driver.FindElement(By.Name("add-to-cart")).Click();
            TopNav topnav = new TopNav(driver);
            topnav.Checkout.Click();
            driver.FindElement(By.Id("billing_address_1")).Clear();
            driver.FindElement(By.Id("billing_address_1")).SendKeys("24 Palmyra Square North");
            driver.FindElement(By.Id("billing_city")).Clear();
            driver.FindElement(By.Id("billing_city")).SendKeys("Warrington");
            driver.FindElement(By.Id("billing_state")).Clear();
            driver.FindElement(By.Id("billing_state")).SendKeys("Warrington");
            driver.FindElement(By.Id("billing_postcode")).Clear();
            driver.FindElement(By.Id("billing_postcode")).SendKeys("WA1 9SJ");
            driver.FindElement(By.Id("billing_phone")).Clear();
            driver.FindElement(By.Id("billing_phone")).SendKeys("398276237692370");
            
        }

        [When(@"it is completed")]
        public void WhenItIsCompleted()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button#place_order")).Click();
            //crap resolution method but will refactor and refine in time 
        }

        [Then(@"I am given a order number")]
        public void ThenIAmGivenAOrderNumber()
        {
            Thread.Sleep(3000);
            var orderNumber = driver.FindElement(By.CssSelector(".order > strong")).Text;
            Console.WriteLine(orderNumber);
        }

        [When(@"it is completed and i have a order number")]
        public void WhenItIsCompletedAndIHaveAOrderNumber()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button#place_order")).Click(); 
            Thread.Sleep(3000);
            orderNumber = driver.FindElement(By.CssSelector(".order > strong")).Text;
            Console.WriteLine(orderNumber);
        }

        [Then(@"the order number in my account matches it")]
        public void ThenTheOrderNumberInMyAccountMatchesIt()
        {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.LinkText("Orders")).Click();
            if(driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text.Remove(0, 1).Equals(orderNumber))
            {
                Console.WriteLine(true);
                //Console.WriteLine(orderNumber);
                //Console.WriteLine(driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text);
            }
            else {
                Console.WriteLine(false);
                //Console.WriteLine(orderNumber);
                //Console.WriteLine(driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text);
            }

        }


    }
}


