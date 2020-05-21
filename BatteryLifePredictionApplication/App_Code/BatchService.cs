using AppFacade;
using AppFacade.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace BatteryLifePredictionApplication.App_Code
{
    // Handles all the application's functionality associated with Batches
    public static class BatchService
    {
        //  Create a new Batch
        public static bool CreateBatch(BatchDto batch)
        {
            Facade facade = new Facade();
            bool result = facade.CreateBatch(batch);
            return result;
        }

        // Get all Batches
        public static List<BatchDto> GetBatches()
        {
            Facade facade = new Facade();
            List<BatchDto> batches = facade.GetBatches();
            return batches;
        }

        // Get all Batches associated with a specific User
        public static List<BatchDto> GetBatches(int userId)
        {
            Facade facade = new Facade();
            List<BatchDto> batches = facade.GetBatches(userId);
            return batches;
        }

        // Get a Batch
        public static BatchDto GetBatch(int id)
        {
            Facade facade = new Facade();
            BatchDto batch = facade.GetBatch(id);
            return batch;
        }

        // Update a Batch
        public static bool UpdateBatch(BatchDto batch)
        {
            Facade facade = new Facade();
            bool result = facade.UpdateBatch(batch);
            return result;
        }

        // Soft-delete a Batch
        public static bool DeleteBatch(int id)
        {
            Facade facade = new Facade();
            bool result = facade.DeleteBatch(id);
            return result;
        }

        // Get the UserId of the current User
        public static int? GetUserId()
        {
            int batchId;
            try
            {
                batchId = Int32.Parse(HttpContext.Current.Request.QueryString["id"]);
            }
            catch
            {
                return null;
            }

            Facade facade = new Facade();
            BatchDto batch = facade.GetBatch(batchId);

            int userId;
            try
            {
                userId = batch.UserId;
            }
            catch
            {
                return null;
            }

            return userId;
        }

        // Get the UserId of the User associated with a specific Batch
        public static int? GetUserId(int? batchId)
        {
            Facade facade = new Facade();

            int userId;
            try
            {
                BatchDto batch = facade.GetBatch(Int32.Parse(batchId.ToString()));
                userId = batch.UserId;
            }
            catch
            {
                return null;
            }

            return userId;
        }

        // Add Batteries to a Batch, using data from a file upload
        public static bool UploadFromFile(FileUpload fileUpload)
        {
            // Initialise variables
            int batchId;
            try
            {
                batchId = Int32.Parse(Security.GetQueryString());
            }
            catch
            {
                return false;
            }

            List<BatteryDto> batteries = new List<BatteryDto>();
            string filename = fileUpload.PostedFile.FileName + "_";
            int nameIndex = 1;

            // Read from CSV and add to battery list
            try
            {
                using (StreamReader reader = new StreamReader(fileUpload.PostedFile.InputStream))
                {
                    var line = reader.ReadLine(); // Ignore Headers

                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();

                        try
                        {
                            List<string> listStrLineElements = line.Split(',').ToList();
                            batteries.Add(new BatteryDto
                            {
                                Battery_Ref = filename + nameIndex++,
                                Cycle_Index = Int32.Parse(listStrLineElements[0]),
                                Charge_Capacity = Double.Parse(listStrLineElements[1]),
                                Discharge_Capacity = Double.Parse(listStrLineElements[2]),
                                Charge_Energy = Double.Parse(listStrLineElements[3]),
                                Discharge_Energy = Double.Parse(listStrLineElements[4]),
                                dvdt = Double.Parse(listStrLineElements[5]),
                                Internal_Resistance = Double.Parse(listStrLineElements[6]),
                                BatchId = batchId,
                                Lifetime = null
                            });
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            // Add to database
            if (BatteryService.CreateBatteries(batteries))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Download Batteries associated with a Batch as a CSV
        public static bool DownloadSpreadsheet(int id)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BatchData.csv"));
            HttpContext.Current.Response.ContentType = "application/text";

            Facade facade = new Facade();
            List<BatteryDto> batteries = facade.GetBatteries(id);
            StringBuilder strbldr = new StringBuilder();

            // header columns
            strbldr.Append("BatteryId,Battery_Ref,Test_Time,Cycle_Index,Current,Voltage,Charge_Capacity,Discharge_Capacity,Charge_Energy,Discharge_Energy," +
                "dvdt,Internal_Resistance,Temperature,Lifetime\n");

            // Rows
            try
            {
                for (int j = 0; j < batteries.Count; j++)
                {
                    strbldr.Append(
                        batteries[j].BatteryId.ToString() + ',' +
                        batteries[j].Battery_Ref.ToString() + ',' +
                        batteries[j].Cycle_Index.ToString() + ',' +
                        batteries[j].Charge_Capacity.ToString() + ',' +
                        batteries[j].Discharge_Capacity.ToString() + ',' +
                        batteries[j].Charge_Energy.ToString() + ',' +
                        batteries[j].Discharge_Energy.ToString() + ',' +
                        batteries[j].dvdt.ToString() + ',' +
                        batteries[j].Internal_Resistance.ToString() + ',' +
                        batteries[j].Lifetime.ToString() + '\n'
                        );
                }
            }
            catch
            {
                return false;
            }

            HttpContext.Current.Response.Write(strbldr.ToString());
            HttpContext.Current.Response.End();

            return true;
        }
    }
}