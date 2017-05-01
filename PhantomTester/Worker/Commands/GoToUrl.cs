using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Worker.Commands
{
    public class GoToUrl : WorkerCommand
    {
        /// <summary>
        /// Navigates to the provided URL.
        /// </summary>
        /// <param name="parameters">String array in this format: ["URL"] -(Example: ["http://www.google.com"]</param>
        public GoToUrl(string[] parameters) : base(parameters)
        {
        }

        public override bool Execute(PhantomJSDriver driver)
        {
            try
            {
                driver.Navigate().GoToUrl(Parameters[0]);
                return true;
            }
            catch (Exception)
            {
                //Url does not exist or wrong parameters
                return false;
            }
        }
    }
}
