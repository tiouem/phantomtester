using System;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Worker.Commands
{
    public class GiveInput : WorkerCommand
    {
        /// <summary>
        /// Finds the input element and enters the provided text into it.
        /// </summary>
        /// <param name="parameters">String array in this format: ["identifier","identifierValue","input"] - Example: ["id","googleSearchBox","Hello world!"] - NOTE: identifier can be "id", "name" or "class".</param>
        public GiveInput(string[] parameters) : base(parameters)
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
                    case "class":
                        element = driver.FindElementByClassName(Parameters[1]);
                        break;
                    case "name":
                        element = driver.FindElementByName(Parameters[1]);
                        break;
                }
            }
            catch (Exception e)
            {
                //Element was not found
                throw new NotImplementedException();
            }

            //Wrong parameters used
            if (element == null) return false;
            try
            {
                element.SendKeys(Parameters[2]);
                return true;
            }
            catch (Exception e)
            {
                //ERROR element not editable/interactable
                return false;
            }
        }
    }
}
