using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace uk.co.nfocus.ecommerce.PageObjects
{
    class TopNav
    {
        private IWebDriver driver;
        public IWebElement Home => driver.FindElement(By.LinkText("Home"));

        public IWebElement Cart => driver.FindElement(By.LinkText("Cart"));

        public IWebElement Checkout => driver.FindElement(By.LinkText("Checkout"));

        public IWebElement Shop => driver.FindElement(By.LinkText("Shop"));

        public IWebElement MyAccount => driver.FindElement(By.LinkText("My account"));

        public IWebElement Blog => driver.FindElement(By.LinkText("Blog"));

        public TopNav(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
        
}
