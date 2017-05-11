using System;
using System.Collections.Generic;
using OpenQA.Selenium.PhantomJS;
using Worker.Commands;

namespace Worker
{

    public class PhantomWorker : IDisposable
    {
        private readonly PhantomJSDriver _driver;

        public PhantomWorker()
        {
            _driver = new PhantomJSDriver();
            _driver.Manage().Window.Maximize();
        }

        public WorkerResponse ExecuteRequest(WorkerRequest request)
        {
            var stepNr = 1;
            var assertions = new List<string>();
            _driver.Navigate().GoToUrl(request.RootUrl);
            foreach (var command in request.Commands)
            {
                if (command is ElementExists)
                {
                    var success = command.Execute(_driver);
                    assertions.Add("Step " + stepNr + " - " + command.Cmd + " - " + (success ? "success" : "failed"));
                }
                else
                {
                    command.Execute(_driver);
                }
                stepNr++;
            }
            var response = new WorkerResponse { Assertions = assertions };
            if (request.ReturnHtml)
            {
                response.HtmlBody = _driver.PageSource;
            }
            return response;
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
