using AppFacade;
using AppFacade.Models;
using System.Collections.Generic;

namespace BatteryLifePredictionApplication.App_Code
{
    // Handles all the application's functionality associated with Predicting the remaining lifetime of Batteries
    public static class PredictService
    {
        // Predict the remaining lifetime of a single Battery
        public static double? Single(int batteryId)
        {
            // Get battery
            Facade facade = new Facade();
            BatteryDto battery = facade.GetBattery(batteryId);

            double prediction;
            try
            {
                prediction = facade.PredictLifetime(battery);
            }
            catch
            {
                return null;
            }

            return prediction;
        }

        // Predict the remaining lifetime of a batch of Batteries
        public static bool Batch(int batchId)
        {
            // Get batteries
            Facade facade = new Facade();
            List<BatteryDto> batteries = facade.GetBatteries(batchId);


            if(!facade.PredictLifetimes(batteries))
            {
                return false;
            }

            return true;
        }

        // Download batch prediction results from Azure
        public static bool DownloadResults(int id)
        {
            Facade facade = new Facade();
            bool result = facade.DownloadResults(id);
            return result;
        }
    }
}