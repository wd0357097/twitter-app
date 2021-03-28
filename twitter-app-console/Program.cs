
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace twitter_app_console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Event handlers to report data 
            // https://stackoverflow.com/questions/44366270/implementing-a-custom-event-handler-from-interface
            Console.OutputEncoding = Encoding.UTF8;
            IAppStream data = new TwitterStream();   
            Console.WriteLine("Welcome to the Twitter Sample Stream Data Collection");
            Console.WriteLine("Press any key to start the twitter stream...");
            Console.ReadKey();// wait for the 'any key' to be pressed, 'where's the any key??'
            Console.WriteLine();// for spacing 

            // get the reporting data
            data.ReportingData += (s, e) =>
            {
                Console.SetCursorPosition(0, 2);
                Console.Write($"\r{e.ToString()}");
            };

            // start the stream
            await data.StartStreamAsync(ConfigurationManager.AppSettings["twitter-stream"]);
        }
    }
}
