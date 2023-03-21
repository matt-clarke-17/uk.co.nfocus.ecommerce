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

        public IWebElement basketSubtotal => _driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount > bdi"));
        public IWebElement discountSubtotal => _driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));


    }
}
