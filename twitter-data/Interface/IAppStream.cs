using System;
using System.Threading.Tasks;
using twitter_data.Managers;

namespace twitter_data.Interface
{
    public interface IAppStream 
    {
        event EventHandler<ReportingData> ReportingData;
        Task StartStreamAsync(string url);
    }
}
