using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using uk.co.nfocus.ecommerce.Utils.SupportSpecflow;

namespace uk.co.nfocus.ecommerce.PageObjects
{
    class CheckoutNav
    {
        private IWebDriver _driver;
        private HelperLib helperLib = new HelperLib();
        private IWebElement billingStreet => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement billingArea => _driver.FindElement(By.Id("billing_city"));
        private IWebElement billingRegion => _driver.FindElement(By.Id("billing_state"));
        private IWebElement billingPostcode => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement billingPhoneNumber => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement orderNumberLoc => _driver.FindElement(By.CssSelector(".order > strong"));
        private IWebElement placeOrderButton => _driver.FindElement(By.Id("place_order"));

        public CheckoutNav(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void addBillingDetails(string street, string area, string region, string postcode, string phoneNumber)
        {
            clearBillingDetails();
            billingStreet.SendKeys(street);
            billingArea.SendKeys(area);
            billingRegion.SendKeys(region);
            billingPostcode.SendKeys(postcode);
            billingPhoneNumber.SendKeys(phoneNumber);
            helperLib.TakeScreenshotOfElement(_driver, "OrderDetailsConfirmation");
        }

        private void clearBillingDetails()
        {
            billingStreet.Clear();
            billingArea.Clear();
            billingRegion.Clear();
            billingPostcode.Clear();
            billingPhoneNumber.Clear();
        }

        public void submitOrder()
        {
            Thread.Sleep(1000);
            placeOrderButton.Click();
        }

        public string collectOrderNumber()
        {
            string orderNumber = orderNumberLoc.Text;
            Console.WriteLine(orderNumber);
            return orderNumber;
        }

    }
}
