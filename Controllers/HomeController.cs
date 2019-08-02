using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using starter_dotnet_core.Models;
using Twilio.TwiML;
using Twilio.TwiML.Voice;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace starter_dotnet_core.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhoneNumber _fromPhoneNumber;

        public HomeController() {
            _fromPhoneNumber = new PhoneNumber(
                Environment.GetEnvironmentVariable("TWILIO_PHONE_NUMBER")
            );
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("message")]
        [HttpPost]
        public IActionResult Message(string to)
        {
            var toPhoneNumber = new PhoneNumber(to);
            var message = MessageResource.Create(
                body: "Good luck on your Twilio quest!",
                from: _fromPhoneNumber,
                to: toPhoneNumber
            );

            return Content("Message inbound!");
        }

        [Route("call")]
        [HttpPost]
        public IActionResult Call(string to)
        {
            var toPhoneNumber = new PhoneNumber(to);
            var message = CallResource.Create(
                url: new Uri("http://demo.twilio.com/docs/voice.xml"),
                from: _fromPhoneNumber,
                to: toPhoneNumber
            );

            return Content("Call inbound!");
        }

        [Route("hello")]
        public IActionResult Hello()
        {
            var response = new VoiceResponse();
            response.Say("Hello there! You have successfully configured a web hook.");
            response.Say("Good luck on your Twilio quest!", Say.VoiceEnum.Woman);
            return new ContentResult
            {
                ContentType = "text/xml",
                Content = response.ToString(),
                StatusCode = 200
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
