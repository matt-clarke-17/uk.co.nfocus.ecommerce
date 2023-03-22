using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using NuGet.Frameworks;
using OpenQA.Selenium;
using uk.co.nfocus.ecommerce.Utils.SupportSpecflow;

namespace uk.co.nfocus.ecommerce.PageObjects
{
    class CartNav
    {
        private IWebDriver _driver;
        private HelperLib helperLib = new HelperLib();

        private IWebElement applyCodeButton => _driver.FindElement(By.Name("apply_coupon"));
        private By subtotalLocator = By.CssSelector("tr.cart-subtotal > * > *");
        private IWebElement basketSubtotal => _driver.FindElement(subtotalLocator);
        private By discountSubtotalLocator = By.CssSelector("td[data-title*='Coupon:'] " +
            "> span.woocommerce-Price-amount");
        private IWebElement discountSubtotal => _driver.FindElement(discountSubtotalLocator);
        private IWebElement couponCodeBox => _driver.FindElement(By.Id("coupon_code"));            
        private By alertBoxLocator = By.CssSelector("div[role='alert']");
        private By removeDiscountByLinkText = By.LinkText("[Remove]");
        private By removeItemByLinkText = By.LinkText("×");

        public CartNav(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string addCouponCode(string discountCode)
        {
            couponCodeBox.SendKeys(discountCode);
            applyCodeButton.Click();
            HelperLib helperLib = new HelperLib();
            helperLib.WaitForElementPresent(_driver, alertBoxLocator, 1);
            return _driver.FindElement(alertBoxLocator).Text;
        }

        public string acquireReductionPercent()
        {
            HelperLib helperLib = new HelperLib();
            Console.WriteLine(basketSubtotal.Text);
            decimal basePrice = getValue(basketSubtotal);
            helperLib.WaitForElementPresent(_driver, discountSubtotalLocator, 1);            
            decimal discountPrice = getValue(discountSubtotal);            
            string percentReductionStr = convertToPercent(basePrice, discountPrice);
            return percentReductionStr;
        }

        private decimal getValue(IWebElement foundValue)
        {
            decimal foundValueDecimal = Convert.ToDecimal(foundValue.Text.Substring(1));
            Console.WriteLine(foundValueDecimal);
            return foundValueDecimal;
        }

        private string convertToPercent(decimal basePrice, decimal discountPrice)
        {
            decimal percentReduction = (discountPrice / basePrice) * 100;
            string percentReductionStr = Convert.ToString(percentReduction).Remove(2);
            return percentReductionStr;        
        }
  
        public void cleanUpAddingDiscount()
        {
            _driver.FindElement(removeDiscountByLinkText).Click();
            _driver.FindElement(removeItemByLinkText).Click();
        }
    }
}
