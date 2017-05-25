using System;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Worker.Commands
{
    public class ElementExists : WorkerCommand
    {
        /// <summary>
        /// Checks if the element exists, if it does the request returns true, else it returns false.
        /// </summary>
        /// <param name="parameters">String array in this format: ["identifier","identifierValue"] - Example: ["id","myImage"] - NOTE: identifier can be "id", "name", "text" or "class".</param>
        public ElementExists(string[] parameters) : base(parameters)
        {

        }

        public override bool Execute(PhantomJSDriver driver)
        {
            IWebElement element = null;
            try
            {
                switch (Parameters[0])
                {
                    case "id":
                        element = driver.FindElementById(Parameters[1]);
                        break;
                    case "name":
                        element = driver.FindElementByName(Parameters[1]);
                        break;
                    case "class":
                        element = driver.FindElementByClassName(Parameters[1]);
                        break;
                    case "text":
                        element = driver.FindElementByLinkText(Parameters[1]);
                        break;

                }
            }
            catch (Exception e)
            {
                //Element was not found
                return false;
            }

            //Wrong parameters were used
            return element != null;
        }
    }
}
