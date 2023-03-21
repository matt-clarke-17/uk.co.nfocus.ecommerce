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
        private HelperLib helperLib = new HelperLib();

        private IWebElement usernameBar => _driver.FindElement(By.Id("username"));
        private IWebElement passwordBar => _driver.FindElement(By.Id("password"));
        private IWebElement loginButton => _driver.FindElement(By.Name("login"));

        private IWebElement ordersButton => _driver.FindElement(By.PartialLinkText("Orders"));

        private IWebElement orderNumber => _driver.FindElement(By.CssSelector("tr > .woocommerce-orders-table__cell-order-number > a"));

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
        public string getMostRecentOrder()
        {
            ordersButton.Click();
            string mostRecentOrderNumber = orderNumber.Text.Substring(1);
            Console.WriteLine(orderNumber);
            return mostRecentOrderNumber;
        }

    }
}
