using System.Threading.Tasks;

namespace twitter_app_console
{
    interface IAppStream
    {
        Task StartStreamAsync(string url);
    }
}
