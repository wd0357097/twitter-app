
using System;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using twitter_data.Interface;
using twitter_data.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace twitter_app_console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IAppStream>(x =>
                    new TwitterStream(ConfigurationManager.AppSettings["token"]))
                .BuildServiceProvider();
            // get service from DI
            var data = serviceProvider.GetService<IAppStream>();

            // Event handlers to report data 
            // https://stackoverflow.com/questions/44366270/implementing-a-custom-event-handler-from-interface
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Welcome to the Twitter Sample Stream Data Collection");
            Console.WriteLine("Press any key to start the twitter stream...");
            Console.ReadKey();// wait for the 'any key' to be pressed, 'where's the any key??'
            Console.WriteLine();// for spacing 

            // get the reporting data
            data.ReportingData += (s, e) =>
            {
                Console.SetCursorPosition(0, 2);
                Console.Write($"\r{e}");
            };

            // start the stream
            await data.StartStreamAsync(ConfigurationManager.AppSettings["twitter-stream"], new CancellationToken());
        }
    }
}
