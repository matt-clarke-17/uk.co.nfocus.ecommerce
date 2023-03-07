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
using System.Xml.Linq;

namespace uk.co.nfocus.ecommerce.StepDefinitions
{
    
        [Binding]
        public class TestSteps
        {

            private readonly ScenarioContext _scenarioContext;

            public TestSteps(ScenarioContext scenarioContext)
            {
                _scenarioContext = scenarioContext;
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
            driver.FindElement(By.Id("coupon_code")).SendKeys("edgewords");
            driver.FindElement(By.CssSelector("button[name = 'apply_coupon']")).Click();


            Thread.Sleep(2000);
            Console.WriteLine(driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).Text);
        }

        [Then(@"should reduce the cost when applied")]
        public void ThenShouldReduceTheCostWhenApplied()
        {
            Assert.That(driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).Text, Does.Contain("£2.40"));
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

        /*[When(@"it is completed and i have a order number")]
        public void WhenItIsCompletedAndIHaveAOrderNumber()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button#place_order")).Click(); 
            

        }*/
        [When(@"it is completed")]
        public void WhenItIsCompleted()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button#place_order")).Click();
            Thread.Sleep(2000);
  
        }

        [Then(@"I am given a order number")]
        public void ThenIAmGivenAOrderNumber()
        {
            Thread.Sleep(3000);
            var orderNumber = driver.FindElement(By.CssSelector(".order > strong")).Text;
            Console.WriteLine(orderNumber);
            _scenarioContext["orderNumber"] = orderNumber;
        }

        [Then(@"it matches the order in the top of my account")]
        public void ThenItMatchesTheOrderInTheTopOfMyAccount()
        {
            var orderNumber = _scenarioContext["orderNumber"];
            Console.WriteLine(orderNumber);
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.PartialLinkText("Orders")).Click();            
            Thread.Sleep(2000);
            var orderNumberAccount = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text.Remove(0, 1);
            Console.WriteLine(orderNumber);
            Assert.That(orderNumber.Equals(orderNumberAccount));
        }

        /*
        [Then(@"the order number in my account matches it")]
        public void ThenTheOrderNumberInMyAccountMatchesIt()
        {
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.LinkText("Orders")).Click();
            if (driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text.Remove(0, 1).Equals(orderNumber))
            {
                Console.WriteLine(true);
                //Console.WriteLine(orderNumber);
                //Console.WriteLine(driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text);
            }
            else
            {
                Console.WriteLine(false);
                //Console.WriteLine(orderNumber);
                //Console.WriteLine(driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text);
            }
        */
        }
    }


