using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Gherkin.CucumberMessages.Types;

namespace uk.co.nfocus.ecommerce.Utils.SupportSpecflow
{
    public class HelperLib
    {
        public HelperLib()
        {

        }

        public void WaitForElementPresent(IWebDriver driver, By theElement, int time)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(drv => drv.FindElement(theElement).Displayed);
        }

        public void TakeScreenshotOfElement(IWebDriver driver, string filename)
        {

            ITakesScreenshot forms = driver as ITakesScreenshot;
            var screenshotForm = forms.GetScreenshot();
            screenshotForm.SaveAsFile(@"C:\Users\MatthewClarke\source\repos\uk.co.nfocus.ecommerce\uk.co.nfocus.ecommerce\TestingOutput\" + filename, ScreenshotImageFormat.Png);
            TestContext.WriteLine("Screenshot taken - see report");
            TestContext.AddTestAttachment(@"C:\Users\MatthewClarke\source\repos\uk.co.nfocus.ecommerce\uk.co.nfocus.ecommerce\TestingOutput\" + filename);
        }
    }
}
