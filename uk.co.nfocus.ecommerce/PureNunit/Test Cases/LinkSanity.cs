using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.ecommerce.PageObjects;
using uk.co.nfocus.ecommerce.Utils.SupportNunit;

namespace uk.co.nfocus.ecommerce.Test_Cases
{
    internal class LinkSanity:TestBaseClass
    {
        [Test]
        public void LinksSanityTest()
        {
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).Clear();
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).Click();
            driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys("Cap" + Keys.Enter);
            Console.WriteLine("Cap has been entered!");
            driver.FindElement(By.Name("add-to-cart")).Click();

            TopNav topNav = new TopNav(driver);
            topNav.Home.Click();
            Assert.That(driver.Title, Does.Contain("Edgewords Shop – e-commerce demo site for Training"));
            topNav.Shop.Click();
            Assert.That(driver.Title, Does.Contain("Shop"));
            topNav.Cart.Click();
            Assert.That(driver.Title, Does.Contain("Cart"));
            topNav.Checkout.Click();
            Assert.That(driver.Title, Does.Contain("Checkout"));
            topNav.MyAccount.Click();
            Assert.That(driver.Title, Does.Contain("My account"));
            topNav.Blog.Click();
            Assert.That(driver.Title, Does.Contain("Blog"));
        }
    }
}
