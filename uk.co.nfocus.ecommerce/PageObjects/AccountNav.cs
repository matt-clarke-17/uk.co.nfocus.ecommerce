using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using uk.co.nfocus.ecommerce.Utils.SupportSpecflow;

namespace uk.co.nfocus.ecommerce.PageObjects
{
    class AccountNav
    {
        private IWebDriver _driver;
        HelperLib helperLib = new HelperLib();

        public AccountNav(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void LogIn(string username, string password)
        {
            usernameBar.SendKeys(username);
            passwordBar.SendKeys(password);
            loginButton.Click();
        }

        public IWebElement usernameBar => _driver.FindElement(By.Id("username"));
        public IWebElement passwordBar => _driver.FindElement(By.Id("password"));
        public IWebElement loginButton => _driver.FindElement(By.Name("login"));

        public IWebElement ordersButton => _driver.FindElement(By.PartialLinkText("Orders"));

        public IWebElement orderNumber => _driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));

    }
}
