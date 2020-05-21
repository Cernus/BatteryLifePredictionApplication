using AppFacade;
using AppFacade.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace BatteryLifePredictionApplication.App_Code
{
    // Handles all the application's functionality associated with Batteries
    public static class BatteryService
    {
        // Create multiple Batteries
        public static bool CreateBatteries(List<BatteryDto> batteries)
        {
            Facade facade = new Facade();
            if (facade.CreateBatteries(batteries))
            {
                // Remove JobIds from batch if changes made to batteries in batch
                int batchId = batteries[0].BatchId;
                BatchDto batch = facade.GetBatch(batchId);

                batch.DecisionForestRegressionJobId = null;
                batch.LinearRegressionJobId = null;

                if (!facade.UpdateBatch(batch))
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        // Create a Battery
        public static bool CreateBattery(BatteryDto battery)
        {
            Facade facade = new Facade();
            if (facade.CreateBattery(battery))
            {
                // Remove JobIds from batch if changes made to batteries in batch
                int batchId = battery.BatchId;
                BatchDto batch = facade.GetBatch(batchId);

                batch.DecisionForestRegressionJobId = null;
                batch.LinearRegressionJobId = null;

                if (!facade.UpdateBatch(batch))
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        // Get a Battery associated with a specific Batch
        public static BatteryDto GetBattery(int id)
        {
            Facade facade = new Facade();
            BatteryDto battery = facade.GetBattery(id);
            return battery;
        }

        // Get all Batteries associated with a specific Batch. Also, attempts to download prediction results from Azure
        public static List<BatteryDto> GetBatteries(int id)
        {
            Facade facade = new Facade();

            // If Batch's PredictionStatus is InProgress then download results and update in database
            if (facade.GetPredictionStatus(id) == PredictionStatus.InProgress)
            {
                if (!PredictService.DownloadResults(id))
                {
                    // Redirect to home page if results are not ready
                    HttpContext.Current.Response.Redirect("Default?Message=ResultsNotDownloaded");
                }
            }

            // Get batteries using batchid
            List<BatteryDto> batteries = facade.GetBatteries(id);

            return batteries;
        }

        // Get the Batch Id of a specific Battery
        public static int? GetBatchId()
        {
            int batteryId = Int32.Parse(HttpContext.Current.Request.QueryString["id"]);

            Facade facade = new Facade();
            BatteryDto battery = facade.GetBattery(batteryId);

            try
            {
                int? batchId = battery.BatchId;
                return batchId;
            }
            catch
            {
                return null;
            }
        }

        // Update a Battery
        public static bool UpdateBattery(BatteryDto battery)
        {
            Facade facade = new Facade();
            if(facade.UpdateBattery(battery))
            {
                // Remove JobIds from batch if changes made to batteries in batch
                int batchId = battery.BatchId;
                BatchDto batch = facade.GetBatch(batchId);

                batch.DecisionForestRegressionJobId = null;
                batch.LinearRegressionJobId = null;

                if (!facade.UpdateBatch(batch))
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        // Update multiple Batteries
        public static bool UpdateBatteries(List<BatteryDto> batteries)
        {
            Facade facade = new Facade();
            if (facade.UpdateBatteries(batteries))
            {
                // Remove JobIds from batch if changes made to batteries in batch
                int batchId = batteries[0].BatchId;
                BatchDto batch = facade.GetBatch(batchId);

                batch.DecisionForestRegressionJobId = null;
                batch.LinearRegressionJobId = null;

                if (!facade.UpdateBatch(batch))
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        // Soft-delete a Battery
        public static bool DeleteBattery(int id)
        {
            Facade facade = new Facade();
            bool result = facade.DeleteBattery(id);
            return result;
        }
    }
}