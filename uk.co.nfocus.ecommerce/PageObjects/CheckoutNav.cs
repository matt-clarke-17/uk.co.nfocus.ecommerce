using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        HelperLib helperLib = new HelperLib();

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

        public IWebElement placeOrderButton => _driver.FindElement(By.Id("place_order"));

        public IWebElement billingLine1 => _driver.FindElement(By.Id("billing_address_1"));
        public IWebElement billingLine2 => _driver.FindElement(By.Id("billing_city"));
        public IWebElement billingLine3 => _driver.FindElement(By.Id("billing_state"));
        public IWebElement billingLine4 => _driver.FindElement(By.Id("billing_postcode"));
        public IWebElement billingLine5 => _driver.FindElement(By.Id("billing_phone"));

        private void clearBillingDetails()
        {
            billingLine1.Clear();
            billingLine2.Clear();
            billingLine3.Clear();
            billingLine4.Clear();
            billingLine5.Clear();
        }

    }
}
