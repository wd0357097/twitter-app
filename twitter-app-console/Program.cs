
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using System.Configuration;
using Tweetinvi.Models;
using System.Threading;

namespace twitter_app_console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //var userClient = new TwitterClient("W9rlp07dvu6KIkblM6dqjhM8w", "ePwsTmGtOZPpQItaGFZC1QsHVYxW4AXbu0XSi3Q1I0tX0vtSyb", "1089234151172161536-4bcVJ4LBFjVMHr1iFZUlxTMiXvuPtw", "Vt6Qu6fXaLSJUojRPGgCyZZVGlImp2YURYdd1miTZSvo2");
            //var user = await userClient.Users.GetAuthenticatedUserAsync();

            //var sampleStream = userClient.Streams.CreateSampleStream();
            //sampleStream.TweetReceived += (sender, eventArgs) =>
            //{
            //    Console.WriteLine(eventArgs.Tweet.);
            //};

            // Event handlers to report data 
            // https://stackoverflow.com/questions/44366270/implementing-a-custom-event-handler-from-interface

            IAppStream data = new TwitterStream();   
            Console.WriteLine("Welcome to the Twitter Sample Stream Data Collection");
            Console.WriteLine("Press any key to start the twitter stream...");
            Console.ReadKey();// wait for the 'any key' to be pressed, 'where's the any key??'
            Console.WriteLine();// for spacing 

            // get the reporting data
            data.ReportingData += (s, e) =>
            {
                Console.SetCursorPosition(0, 2);
                Console.Write($"{e.ToString()}");
            };

            // start the stream
            await data.StartStreamAsync("https://api.twitter.com/2/tweets/sample/stream");
            Console.WriteLine("Press any key to STOP the twitter stream...");
            Console.ReadKey();

        }
    }
}
