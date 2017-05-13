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
            var commandNr = 1;
            var commandResults = new List<string>();
            _driver.Navigate().GoToUrl(request.RootUrl);
            foreach (var command in request.Commands)
            {
                var success = command.Execute(_driver);
                commandResults.Add("Command nr " + commandNr + " '" + command.Cmd + "' - " + (success ? "success" : "failed"));
                commandNr++;
            }
            var response = new WorkerResponse {Assertions = commandResults};
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