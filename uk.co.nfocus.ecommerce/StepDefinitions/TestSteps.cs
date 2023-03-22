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
        //Create a blank scenario context dictionary
        private readonly ScenarioContext _scenarioContext;
        HelperLib helperLib = new HelperLib();
        //Create a blank IWebDriver
        private readonly IWebDriver _driver;

        public TestSteps(ScenarioContext scenarioContext)
        {
            //Fill in the dictionary with what the hooks class gave us
            //to assist OOP Formatting 
            _scenarioContext = scenarioContext;
            //Grab the driver from the scenario context dictionary
            _driver = (IWebDriver)_scenarioContext["myDriver"];

        }

        //similar code start point for both scenarios
        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            TopNav topNav = new TopNav(_driver);
            //navigates through site and adds the cap to the basket 
            topNav.MyAccount.Click();
            AccountNav accountNav = new AccountNav(_driver);
            accountNav.LogIn(username, password);
            Console.WriteLine("Background Complete");
        }

        [When(@"I apply a discount code (.*)")]
        public void WhenIAddTheDiscountCode(string discountCode)
        {
            //coupon application code 
            CartNav cartNav = new CartNav(_driver);
            string alertBoxText = cartNav.addCouponCode(discountCode);
            Assert.That(alertBoxText, Does.Contain("Coupon code applied successfully"), "Please clear basket prior to running of existing items and discount code before running");
            //insert code here to check code application
            Console.WriteLine("Discount Application Complete");
        }

        [Then(@"it should reduce the cost when applied by (.*)")]
        public void ThenShouldReduceTheCostWhenApplied(string discountPercentage)
        {
            CartNav cartNav = new CartNav(_driver);
            helperLib.TakeScreenshotOfElement(_driver, "DiscountApplied");
            //obtain raw item price
            string percentReductionStr = cartNav.acquireReductionPercent();
            //assert that both prices are the same 
            Assert.That(percentReductionStr, Is.EqualTo(discountPercentage), "Discount Percentage Expected does not match received");
            Console.WriteLine("Discount Verification Complete");
            //cleanup method
            cartNav.cleanUpAddingDiscount();

        }

        [Given(@"I have placed an order containing atleast a (.*)")]
        public void GivenIHavePlacedAnOrder(string itemName)
        {
            //initialise drivers as and when required and in order to maintain consistency  
            TopNav topNav = new TopNav(_driver);
            topNav.Shop.Click();
            ShopNav shopNav = new ShopNav(_driver);
            shopNav.addItemToBasket(itemName);
            topNav.Checkout.Click();
            //generate checkout driver to drive functionality on the checkout page 
            CheckoutNav checkoutNav = new CheckoutNav(_driver);
            Assert.That(_driver.Title, Does.Contain("Checkout"), "Not on the Checkout page => Navigation Failed");
            checkoutNav.addBillingDetails(street, area, region, postcode, phoneNumber);
            Console.WriteLine("Order details added successfully");
        }

        [When(@"it is completed")]
        public void WhenItIsCompleted()
        {
            //wait for website js to refresh and update
            CheckoutNav checkoutNav = new CheckoutNav(_driver);
            checkoutNav.submitOrder();
            Console.WriteLine("Order Submitted");
        }

        [Then(@"I am given a order number")]
        public void ThenIAmGivenAOrderNumber()
        {
            //wait for site to accept order and then inherit the order number off the page for reference later
            //in scenario context
            CheckoutNav checkoutNav = new CheckoutNav(_driver);
            //enables test to inherit data for later comparison
            _scenarioContext["orderNumber"] = checkoutNav.collectOrderNumber();
            Assert.That(_scenarioContext["orderNumber"] != null, "Null Value acquired from page => Acquisition Failed");
            helperLib.TakeScreenshotOfElement(_driver, "PostOrderNumber");
            Console.WriteLine("Order Number Acquisition Sucessful");
        }

        [Then(@"it matches the order in the top of my account")]
        public void ThenItMatchesTheOrderInTheTopOfMyAccount()
        {
            TopNav topNav = new TopNav(_driver);
            //use value stored in scenario context to verify the order exists in the users history 
            var orderNumber = _scenarioContext["orderNumber"];
            topNav.MyAccount.Click();
            Assert.That(_driver.Title, Does.Contain("account"), "Not on the Account page => Navigation Failed");
            AccountNav accountNav = new AccountNav(_driver);
            //acquire order number from page details
            var orderNumberAccount = accountNav.getMostRecentOrder();
            //assert that the two are the same
            Assert.That(orderNumber.Equals(orderNumberAccount), "Order Numbers do not match");
            helperLib.TakeScreenshotOfElement(_driver, "AccountOrderObservation");
            Console.WriteLine("Order Number Verification Successful");
        }

        [Given(@"I am on the Cart Page")]
        public void IAmOnTheCartPage()
        {
            //navigates to the basket after the cap has been added to the basket 
            TopNav topNav = new TopNav(_driver);
            topNav.Cart.Click();
            Assert.That(_driver.Title, Does.Contain("Cart"), "Not on the cart page => Navigation Failed");
            Console.WriteLine("Navigation to Cart Page Successful");
        }

        [Given(@"I have (.*) in my basket")]
        public void AddItemToBasket(string itemName)
        {
            TopNav topNav = new TopNav(_driver);
            topNav.Shop.Click();
            ShopNav shopNav = new ShopNav(_driver);
            //access utility method to add item to basket
            shopNav.addItemToBasket(itemName);
            Console.WriteLine("Item successfully added to basket");
        }
    }
}


