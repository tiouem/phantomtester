using System;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Worker.Commands
{
    public class ClickElement : WorkerCommand
    {
        /// <summary>
        /// Clicks the first element that it finds using the provided identifier (id/class/tag/text/name) and value
        /// </summary>
        /// <param name="parameters">String array in this format: ["identifierType", "value"] - Example: ["id", "myButton"] or ["text", "Click Here!"] - NOTE: identifier can be "id", "class", "tag", "text" or "name".</param>
        public ClickElement(string[] parameters) : base(parameters)
        {

        }

        public override bool Execute(PhantomJSDriver driver)
        {
            IWebElement element = null;
            switch (Parameters[0])
            {
                case "id":
                    element = driver.FindElementById(Parameters[1]);
                    break;
                case "class":
                    element = driver.FindElementByClassName(Parameters[1]);
                    break;
                case "tag":
                    element = driver.FindElementByTagName(Parameters[1]);
                    break;
                case "text":
                    element = driver.FindElementByLinkText(Parameters[1]);
                    break;
                case "name":
                    element = driver.FindElementByName(Parameters[1]);
                    break;
                default:
                    return false;
            }

            //Element not found
            if (element == null) return false;
            try
            {
                element.Click();
                return true;
            }
            catch (Exception e)
            {
                return false;
                //Element not clickable
            }
        }
    }
}
