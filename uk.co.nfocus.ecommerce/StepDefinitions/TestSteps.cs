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

        private readonly ScenarioContext _scenarioContext;
        HelperLib helperLib = new HelperLib();

        public TestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            
        }

        //implement a background
        //similar code start point for both scenarios
        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            //top nav cannot be implemented as a feature of scenario context so must be instanciated for each method that wishes to use pom
            TopNav topNav = new TopNav(driver);
            //navigates through site and adds the cap to the basket 
            topNav.MyAccount.Click();
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
        }

        [When(@"I apply a discount code (.*)")]
        public void WhenIAddTheDiscountCode(string discountCode)
        {
            //coupon application code 
            driver.FindElement(By.Id("coupon_code")).SendKeys(discountCode);
            driver.FindElement(By.CssSelector("button[name = 'apply_coupon']")).Click();
            Thread.Sleep(1500);
        }

        [Then(@"it should reduce the cost when applied by (.*)")]
        public void ThenShouldReduceTheCostWhenApplied(string discountPercentage)
        {
            //test could show failure if the basket is not completely empty prior to start
            //could revamp to use modulo (Lack of remainder) on a divisiion
            //would need double coversion to implement 
            helperLib.TakeScreenshotOfElement(driver,"DiscountApplied");
            //obtain raw item price
            Console.WriteLine(driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount > bdi")).Text);
            decimal basePrice = decimal.Parse(driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount > bdi")).Text.Remove(0, 1));
            Console.WriteLine(basePrice);
            Console.WriteLine(driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).Text);
            decimal discountPrice = decimal.Parse(driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount")).Text.Remove(0, 1));
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
            TopNav topNav = new TopNav(driver);
            topNav.Shop.Click();
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys("Cap" + Keys.Enter);
            driver.FindElement(By.Name("add-to-cart")).Click();
            TakeScreenshotOfElement(driver, "CapInBasket");
            topNav.Checkout.Click();
            //parameterise this as test parameters
            driver.FindElement(By.Id("billing_address_1")).Clear();
            driver.FindElement(By.Id("billing_address_1")).SendKeys(street);
            driver.FindElement(By.Id("billing_city")).Clear();
            driver.FindElement(By.Id("billing_city")).SendKeys(area);
            driver.FindElement(By.Id("billing_state")).Clear();
            driver.FindElement(By.Id("billing_state")).SendKeys(region);
            driver.FindElement(By.Id("billing_postcode")).Clear();
            driver.FindElement(By.Id("billing_postcode")).SendKeys(postcode);
            driver.FindElement(By.Id("billing_phone")).Clear();
            driver.FindElement(By.Id("billing_phone")).SendKeys(phoneNumber);
            helpeTakeScreenshotOfElement(driver, "OrderDetailsConfirmation");

        }

        [When(@"it is completed")]
        public void WhenItIsCompleted()
        {
            //wait for website js to refresh and update
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button#place_order")).Click();
            Thread.Sleep(1000);

        }

        [Then(@"I am given a order number")]
        public void ThenIAmGivenAOrderNumber()
        {
            //wait for site to accept order and then inherit the order number off the page for reference later
            //in scenario context
            Thread.Sleep(2000);
            var orderNumber = driver.FindElement(By.CssSelector(".order > strong")).Text;
            Console.WriteLine(orderNumber);
            _scenarioContext["orderNumber"] = orderNumber;
            TakeScreenshotOfElement(driver, "PostOrderNumber");
        }

        [Then(@"it matches the order in the top of my account")]
        public void ThenItMatchesTheOrderInTheTopOfMyAccount()
        {
            //use value stored in scenario context to verify the order exists in the users history 
            var orderNumber = _scenarioContext["orderNumber"];
            Console.WriteLine(orderNumber);
            TopNav topNav = new TopNav(driver);
            topNav.MyAccount.Click();
            driver.FindElement(By.PartialLinkText("Orders")).Click();
            Thread.Sleep(2000);
            var orderNumberAccount = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text.Remove(0, 1);
            Console.WriteLine(orderNumber);
            Assert.That(orderNumber.Equals(orderNumberAccount));
            TakeScreenshotOfElement(driver, "AccountOrderObservation");
        }

        [Given(@"I am on the Cart Page")]
        public void IAmOnTheCartPage()
        {
            //navigates to the basket after the cap has been added to the basket 
            TopNav topNav = new TopNav(driver);
            topNav.Cart.Click();
            Assert.That(driver.Url.Equals("https://www.edgewordstraining.co.uk/demo-site/cart/"));
        }
        [Given(@"I have (.*) in my basket")]
        public void AddItemToBasket(string itemName)
        {
            TopNav topNav = new TopNav(driver);
            topNav.Shop.Click();
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys(itemName + Keys.Enter);
            driver.FindElement(By.Name("add-to-cart")).Click();
            //re-use of variable to avoid pointless variable generation
            itemName = itemName + "InBasket";
            TakeScreenshotOfElement(driver, itemName);
        }
    }
    //POMs
}


