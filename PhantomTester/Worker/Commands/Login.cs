using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Worker.Commands
{
    public class Login : WorkerCommand
    {
        /// <summary>
        /// Attempts a login at the current URL, using a username/email and password.
        /// </summary>
        /// <param name="parameters">String array in this format: ["username/e-mail", "password"] - Example: ["chris88","123Password123"]</param>
        public Login(string[] parameters) : base(parameters)
        {
        }

        public override bool Execute(PhantomJSDriver driver)
        {
            try
            {
                var passwordBox =
               driver.FindElements(By.TagName("input"))
                   .FirstOrDefault(e => e.GetAttribute("type").Contains("password"));
                var userEmailBox =
                    driver.FindElements(By.TagName("input"))
                        .FirstOrDefault(
                            e =>
                                e.GetAttribute("type").Contains("email") || e.GetAttribute("name").Contains("email") ||
                                e.GetAttribute("type").Contains("user") || e.GetAttribute("name").Contains("user"));
                userEmailBox.SendKeys(Parameters[0]);
                passwordBox.SendKeys(Parameters[1]);
                driver.FindElementByTagName("form").Submit();
                return true;
            }
            catch (Exception)
            {
                //Something went wrong
                return false;
            }
        }
    }
}
