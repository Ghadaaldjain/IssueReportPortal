using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IssueReportPortal.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.ServiceBus;
using System.Text;
using Newtonsoft.Json;

namespace IssueReportPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;



        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

            public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string IssesContent, string UrgencyLevel)
        {

            IQueueClient queueClient = new QueueClient(_configuration.GetConnectionString("QueueConnectionString"), _configuration["QueueName"]);
            var JSONData = JsonConvert.SerializeObject(new { IssesContent, UrgencyLevel });
            var MsgID = Guid.NewGuid().ToString();
            var issueMessage = new Message(Encoding.UTF8.GetBytes(JSONData))
            {
                MessageId = MsgID,
                ContentType = "application/json"
            };
            await queueClient.SendAsync(issueMessage).ConfigureAwait(false);

            ViewBag.Message = String.Format("Thank you for reporting, this is the Id of the issue: {0}.", MsgID);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
