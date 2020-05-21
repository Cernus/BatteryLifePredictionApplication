using PredictionApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictionApi.Facades
{
    public interface IPredictionFacade
    {
        Task<double> GetLifetime(BatteryDto battery);
        Task<string> GetLifetimes(List<BatteryDto> batteries, int fileNumber);
        Task<List<double>> DownloadResults(int fileNumber, string jobId, bool decisonForestRegression);
    }
}
