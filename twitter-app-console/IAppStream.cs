using System;
using System.Threading.Tasks;

namespace twitter_app_console
{
    interface IAppStream
    {
        event EventHandler<IReportingData> ReportingData;
        Task StartStreamAsync(string url);
    }
}
