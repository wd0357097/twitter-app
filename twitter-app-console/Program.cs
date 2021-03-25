
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using System.Configuration;
using Tweetinvi.Models;

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
            //    Console.WriteLine(eventArgs.Tweet);
            //};

            //await sampleStream.StartAsync();

            //Console.WriteLine(user);
            //Console.WriteLine("Hello World!");

            IAppStream data = new TwitterStream();
            await data.StartStreamAsync("https://api.twitter.com/2/tweets/sample/stream");
        }
    }
}
