using System;
using System.Threading;
using System.Threading.Tasks;

namespace twitter_app_console
{
    interface IAppStream 
    {
        event EventHandler<ReportingData> ReportingData;
        Task StartStreamAsync(string url);
    }
}
