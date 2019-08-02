using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Twilio;

namespace starter_dotnet_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                TwilioClient.Init(
                    Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
                    Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
                );
            }
            catch (Twilio.Exceptions.AuthenticationException)
            {
                Console.WriteLine("You must configure your TWILIO_* variables first.");
                Environment.Exit(-1);
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
