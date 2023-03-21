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

        public CheckoutNav(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void addBillingDetails(string street, string area, string region, string postcode, string phoneNumber)
        {
            clearBillingDetails();
            billingLine1.SendKeys(street);
            billingLine2.SendKeys(area);
            billingLine3.SendKeys(region);
            billingLine4.SendKeys(postcode);
            billingLine5.SendKeys(phoneNumber);
            helperLib.TakeScreenshotOfElement(_driver, "OrderDetailsConfirmation");
        }

        private IWebElement placeOrderButton => _driver.FindElement(By.Id("place_order"));
        

        private IWebElement billingLine1 => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement billingLine2 => _driver.FindElement(By.Id("billing_city"));
        private IWebElement billingLine3 => _driver.FindElement(By.Id("billing_state"));
        private IWebElement billingLine4 => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement billingLine5 => _driver.FindElement(By.Id("billing_phone"));

        private void clearBillingDetails()
        {
            billingLine1.Clear();
            billingLine2.Clear();
            billingLine3.Clear();
            billingLine4.Clear();
            billingLine5.Clear();
        }

        public void submitOrder()
        {
            Thread.Sleep(1000);
            placeOrderButton.Click();
        }

    }
}
