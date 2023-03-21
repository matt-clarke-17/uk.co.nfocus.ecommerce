using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using uk.co.nfocus.ecommerce.Utils.SupportSpecflow;

namespace uk.co.nfocus.ecommerce.PageObjects
{
    class ShopNav
    {
        private IWebDriver _driver;
        HelperLib helperLib = new HelperLib();

        public ShopNav(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void addItemToBasket(string itemName)
        {
            searchBar.SendKeys(itemName + Keys.Enter);
            addToCart.Click();
            //re-use of variable to avoid pointless variable generation
            itemName = itemName + "InBasket";
            helperLib.TakeScreenshotOfElement(_driver, itemName);
        }

        private IWebElement searchBar => _driver.FindElement(By.Name("s"));

        private IWebElement addToCart => _driver.FindElement(By.Name("add-to-cart"));


    }
}
