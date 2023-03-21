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
    class CartNav
    {
        private IWebDriver _driver;
        HelperLib helperLib = new HelperLib();

        public CartNav(IWebDriver driver)
        {
            this._driver = driver;
        }
        public IWebElement couponCodeBox => _driver.FindElement(By.Id("coupon_code"));

        public void addCouponCode(string discountCode)
        {
            couponCodeBox.SendKeys(discountCode);
            applyCodeButton.Click();
            Thread.Sleep(1000);
        }

        public IWebElement applyCodeButton => _driver.FindElement(By.Name("apply_coupon"));

        public IWebElement basketSubtotal => _driver.FindElement(By.CssSelector("tr.cart-subtotal > * > *"));
        public IWebElement discountSubtotal => _driver.FindElement(By.CssSelector("td[data-title*='Coupon:'] " +
            "> span.woocommerce-Price-amount"));

        public string acquireReductionPercent()
        {
            Console.WriteLine(basketSubtotal.Text);
            decimal basePrice = Convert.ToDecimal(basketSubtotal.Text.Substring(1));
            Console.WriteLine(basePrice);
            Console.WriteLine(discountSubtotal.Text);
            decimal discountPrice = Convert.ToDecimal(discountSubtotal.Text.Substring(1));
            Console.WriteLine(discountPrice);
            decimal percentReduction = (discountPrice / basePrice) * 100;
            Console.WriteLine(percentReduction);
            string percentReductionStr = Convert.ToString(percentReduction).Remove(2);
            Console.WriteLine(percentReductionStr);
            return percentReductionStr;
        }
  
    }
}
