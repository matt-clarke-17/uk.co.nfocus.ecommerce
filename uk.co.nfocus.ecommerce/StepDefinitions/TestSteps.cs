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
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.DevTools.V108.Page;
using System.Globalization;
using uk.co.nfocus.ecommerce.Utils.SupportSpecflow;

namespace uk.co.nfocus.ecommerce.StepDefinitions
{

    [Binding]
    public class TestSteps
    {
        // Create a blank scenario context dictionary.
        private readonly ScenarioContext _scenarioContext;
        HelperLib helperLib = new HelperLib();

        // Createa blank IWebDriver.
        private readonly IWebDriver _driver;

        public TestSteps(ScenarioContext scenarioContext)
        {
            // Fill in the dictionary with what the hooks class gave us.
            _scenarioContext = scenarioContext;
            // Grab the driver from the scenario context dictionary.
            _driver = (IWebDriver)_scenarioContext["myDriver"];
            
        }

        //implement a background
        //similar code start point for both scenarios
        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            //top nav cannot be implemented as a feature of scenario context so must be instanciated for each method that wishes to use pom
            TopNav topNav = new TopNav(_driver);
            //navigates through site and adds the cap to the basket 
            topNav.MyAccount.Click();
            _driver.FindElement(By.Id("username")).SendKeys(username);
            _driver.FindElement(By.Id("password")).SendKeys(password);
            _driver.FindElement(By.Name("login")).Click();
        }

        [When(@"I apply a discount code (.*)")]
        public void WhenIAddTheDiscountCode(string discountCode)
        {
            //coupon application code 
            _driver.FindElement(By.Id("coupon_code")).SendKeys(discountCode);
            _driver.FindElement(By.CssSelector("button[name = 'apply_coupon']")).Click();
            Thread.Sleep(1500);
        }

        [Then(@"it should reduce the cost when applied by (.*)")]
        public void ThenShouldReduceTheCostWhenApplied(string discountPercentage)
        {
            //test could show failure if the basket is not completely empty prior to start
            //could revamp to use modulo (Lack of remainder) on a divisiion
            //would need double coversion to implement 



            helperLib.TakeScreenshotOfElement(_driver,"DiscountApplied");
            //obtain raw item price
            Console.WriteLine(_driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount > bdi")).Text);
            decimal basePrice = decimal.Parse(_driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount > bdi")).Text.Remove(0, 1));
            Console.WriteLine(basePrice);
            Console.WriteLine(_driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).Text);
            decimal discountPrice = decimal.Parse(_driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).Text.Remove(0, 1));
            Console.WriteLine(discountPrice);
            decimal percentReduction = (discountPrice / basePrice) * 100;
            Console.WriteLine(percentReduction);
            string percentReductionStr = Convert.ToString(percentReduction).Remove(2);
            Console.WriteLine(percentReductionStr);
            Assert.That(percentReductionStr, Is.EqualTo(discountPercentage));

        }

        [Given(@"I have placed an order")]
        public void GivenIHavePlacedAnOrder()
        {

            //flushes out any info prior to data insertion for order placement 
            TopNav topNav = new TopNav(_driver);
            topNav.Shop.Click();
            _driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys("Cap" + Keys.Enter);
            _driver.FindElement(By.Name("add-to-cart")).Click();
            helperLib.TakeScreenshotOfElement(_driver, "CapInBasket");
            topNav.Checkout.Click();
            //parameterise this as test parameters
            _driver.FindElement(By.Id("billing_address_1")).Clear();
            _driver.FindElement(By.Id("billing_address_1")).SendKeys(street);
            _driver.FindElement(By.Id("billing_city")).Clear();
            _driver.FindElement(By.Id("billing_city")).SendKeys(area);
            _driver.FindElement(By.Id("billing_state")).Clear();
            _driver.FindElement(By.Id("billing_state")).SendKeys(region);
            _driver.FindElement(By.Id("billing_postcode")).Clear();
            _driver.FindElement(By.Id("billing_postcode")).SendKeys(postcode);
            _driver.FindElement(By.Id("billing_phone")).Clear();
            _driver.FindElement(By.Id("billing_phone")).SendKeys(phoneNumber);
            helperLib.TakeScreenshotOfElement(_driver, "OrderDetailsConfirmation");

        }

        [When(@"it is completed")]
        public void WhenItIsCompleted()
        {
            //wait for website js to refresh and update
            Thread.Sleep(1000);
            _driver.FindElement(By.CssSelector("button#place_order")).Click();
            Thread.Sleep(1000);

        }

        [Then(@"I am given a order number")]
        public void ThenIAmGivenAOrderNumber()
        {
            //wait for site to accept order and then inherit the order number off the page for reference later
            //in scenario context
            Thread.Sleep(2000);
            var orderNumber = _driver.FindElement(By.CssSelector(".order > strong")).Text;
            Console.WriteLine(orderNumber);
            _scenarioContext["orderNumber"] = orderNumber;
            helperLib.TakeScreenshotOfElement(_driver, "PostOrderNumber");
        }

        [Then(@"it matches the order in the top of my account")]
        public void ThenItMatchesTheOrderInTheTopOfMyAccount()
        {
            //use value stored in scenario context to verify the order exists in the users history 
            var orderNumber = _scenarioContext["orderNumber"];
            Console.WriteLine(orderNumber);
            TopNav topNav = new TopNav(_driver);
            topNav.MyAccount.Click();
            _driver.FindElement(By.PartialLinkText("Orders")).Click();
            Thread.Sleep(2000);
            var orderNumberAccount = _driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text.Remove(0, 1);
            Console.WriteLine(orderNumber);
            Assert.That(orderNumber.Equals(orderNumberAccount));
            helperLib.TakeScreenshotOfElement(_driver, "AccountOrderObservation");
        }

        [Given(@"I am on the Cart Page")]
        public void IAmOnTheCartPage()
        {
            //navigates to the basket after the cap has been added to the basket 
            TopNav topNav = new TopNav(_driver);
            topNav.Cart.Click();
            Assert.That(_driver.Url.Equals("https://www.edgewordstraining.co.uk/demo-site/cart/"));
        }
        [Given(@"I have (.*) in my basket")]
        public void AddItemToBasket(string itemName)
        {
            TopNav topNav = new TopNav(_driver);
            topNav.Shop.Click();
            _driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys(itemName + Keys.Enter);
            _driver.FindElement(By.Name("add-to-cart")).Click();
            //re-use of variable to avoid pointless variable generation
            itemName = itemName + "InBasket";
            helperLib.TakeScreenshotOfElement(_driver, itemName);
        }
    }
    //POMs
}


