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

        public IWebElement applyCodeButton => _driver.FindElement(By.Name("apply_coupon"));

        public IWebElement basketSubtotal => _driver.FindElement(By.CssSelector("tr.cart-subtotal > * > *"));
        public IWebElement discountSubtotal => _driver.FindElement(By.CssSelector("td[data-title*='Coupon:'] " +
            "> span.woocommerce-Price-amount"));
        public IWebElement couponCodeBox => _driver.FindElement(By.Id("coupon_code"));

        public CartNav(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void addCouponCode(string discountCode)
        {
            couponCodeBox.SendKeys(discountCode);
            applyCodeButton.Click();
            Thread.Sleep(1000);
        }

        public string acquireReductionPercent()
        {
            Console.WriteLine(basketSubtotal.Text);
            decimal basePrice = getValue(basketSubtotal);
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
            return percentReductionStr;        }
  
    }
}
