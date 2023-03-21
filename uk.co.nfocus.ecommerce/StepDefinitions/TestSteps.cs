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
            AccountNav accountNav = new AccountNav(_driver);
            accountNav.LogIn(username, password);
        }

        [When(@"I apply a discount code (.*)")]
        public void WhenIAddTheDiscountCode(string discountCode)
        {
            //coupon application code 
            CartNav cartNav = new CartNav(_driver);
            cartNav.addCouponCode(discountCode);
            //insert code here to check code appllication
        }

        [Then(@"it should reduce the cost when applied by (.*)")]
        public void ThenShouldReduceTheCostWhenApplied(string discountPercentage)
        { 

            CartNav cartNav = new CartNav(_driver);

            helperLib.TakeScreenshotOfElement(_driver,"DiscountApplied");
            //obtain raw item price
            string percentReductionStr = cartNav.acquireReductionPercent();
            Assert.That(percentReductionStr, Is.EqualTo(discountPercentage));

        }

        [Given(@"I have placed an order")]
        public void GivenIHavePlacedAnOrder()
        {

            //flushes out any info prior to data insertion for order placement 
            TopNav topNav = new TopNav(_driver);
            topNav.Shop.Click();
            ShopNav shopNav = new ShopNav(_driver);
            shopNav.addItemToBasket("cap");
            topNav.Checkout.Click();
            //parameterise this as test parameters
            CheckoutNav checkoutNav = new CheckoutNav(_driver);
            checkoutNav.addBillingDetails(street, area, region, postcode, phoneNumber);
        }

        [When(@"it is completed")]
        public void WhenItIsCompleted()
        {
            //wait for website js to refresh and update
            CheckoutNav checkoutNav = new CheckoutNav(_driver);
            checkoutNav.submitOrder();
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
            AccountNav accountNav = new AccountNav(_driver);
            var orderNumberAccount = accountNav.getMostRecentOrder();
            Assert.That(orderNumber.Equals(orderNumberAccount));
            helperLib.TakeScreenshotOfElement(_driver, "AccountOrderObservation");
        }

        [Given(@"I am on the Cart Page")]
        public void IAmOnTheCartPage()
        {
            //navigates to the basket after the cap has been added to the basket 
            TopNav topNav = new TopNav(_driver);
            topNav.Cart.Click();
            Assert.That(_driver.Title, Does.Contain("Cart"));
        }

        [Given(@"I have (.*) in my basket")]
        public void AddItemToBasket(string itemName)
        {
            TopNav topNav = new TopNav(_driver);
            topNav.Shop.Click();
            ShopNav shopNav = new ShopNav(_driver);
            shopNav.addItemToBasket(itemName);
        }
    }
}


