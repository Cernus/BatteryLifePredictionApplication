using AppFacade.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AppFacade
{
    public class Facade : IFacade
    {
        #region Battery
        // Create list of Batteries using BatteryApi
        public bool CreateBatteries(List<BatteryDto> batteries)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batteries/creates";

            HttpResponseMessage response = client.PostAsJsonAsync<List<BatteryDto>>(uri, batteries).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Create Battery using BatteryApi
        public bool CreateBattery(BatteryDto battery)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batteries/create";

            HttpResponseMessage response = client.PostAsJsonAsync<BatteryDto>(uri, battery).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Get list of Batteries using BatteryApi
        public List<BatteryDto> GetBatteries()
        {
            List<BatteryDto> batteries;

            HttpClient client = BatteryClient();
            string uri = "api/batteries/details";

            HttpResponseMessage response = client.GetAsync(uri).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                batteries = response.Content.ReadAsAsync<List<BatteryDto>>().Result;
            }
            else
            {
                return null;
            }

            return batteries;
        }

        // Get Battery using Battery Api
        public List<BatteryDto> GetBatteries(int batchId)
        {
            List<BatteryDto> batteries;

            HttpClient client = BatteryClient();
            string uri = "api/batteries/details/";

            HttpResponseMessage response = client.GetAsync(uri + batchId).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                batteries = response.Content.ReadAsAsync<List<BatteryDto>>().Result;
            }
            else
            {
                return null;
            }

            return batteries;
        }

        // Get Battery using Battery Api
        public BatteryDto GetBattery(int id)
        {
            BatteryDto battery;

            HttpClient client = BatteryClient();
            string uri = "api/batteries/detail/";

            HttpResponseMessage response = client.GetAsync(uri + id).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                battery = response.Content.ReadAsAsync<BatteryDto>().Result;
            }
            else
            {
                return null;
            }

            return battery;
        }

        // Update Battery using Battery Api
        public bool UpdateBattery(BatteryDto battery)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batteries/update";

            HttpResponseMessage response = client.PutAsJsonAsync<BatteryDto>(uri, battery).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Update list of Batteries using Battery Api
        public bool UpdateBatteries(List<BatteryDto> batteries)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batteries/updates/";

            HttpResponseMessage response = client.PutAsJsonAsync<List<BatteryDto>>(uri, batteries).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete Battery using Battery Api
        public bool DeleteBattery(int id)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batteries/delete/";

            HttpResponseMessage response = client.PutAsJsonAsync<BatteryDto>(uri + id, null).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Battery

        #region Batch
        // Create Batch using Battery Api
        public bool CreateBatch(BatchDto batch)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batches/create";

            HttpResponseMessage response = client.PostAsJsonAsync<BatchDto>(uri, batch).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Get list of Batches using Batch Api
        public List<BatchDto> GetBatches()
        {
            List<BatchDto> batches;

            HttpClient client = BatteryClient();
            string uri = "api/batches/details";

            HttpResponseMessage response = client.GetAsync(uri).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                batches = response.Content.ReadAsAsync<List<BatchDto>>().Result;
            }
            else
            {
                return null;
            }

            return batches;
        }

        // Get list of Batches associated with a User, using Battery Api
        public List<BatchDto> GetBatches(int userId)
        {
            List<BatchDto> batches;

            HttpClient client = BatteryClient();
            string uri = "api/batches/details/";

            HttpResponseMessage response = client.GetAsync(uri + userId).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                batches = response.Content.ReadAsAsync<List<BatchDto>>().Result;
            }
            else
            {
                return null;
            }

            return batches;
        }

        // Get Batch using Batch Api
        public BatchDto GetBatch(int id)
        {
            BatchDto batch;

            HttpClient client = BatteryClient();
            string uri = "api/batches/detail/";

            HttpResponseMessage response = client.GetAsync(uri + id).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                batch = response.Content.ReadAsAsync<BatchDto>().Result;
            }
            else
            {
                return null;
            }

            return batch;
        }

        // Update Batch using Batch Api
        public bool UpdateBatch(BatchDto batch)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batches/update/";

            HttpResponseMessage response = client.PutAsJsonAsync<BatchDto>(uri, batch).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete Batch using Batch Api
        public bool DeleteBatch(int id)
        {
            HttpClient client = BatteryClient();
            string uri = "api/batches/delete/";

            HttpResponseMessage response = client.PutAsJsonAsync<BatchDto>(uri + id, null).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Get Prediction Status of Batch using Batch Api
        public PredictionStatus GetPredictionStatus(int id)
        {
            PredictionStatus predictionStatus;

            HttpClient client = BatteryClient();
            string uri = "api/batches/getpredictionstatus/";

            HttpResponseMessage response = client.GetAsync(uri + id).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                predictionStatus = response.Content.ReadAsAsync<PredictionStatus>().Result;
            }
            else
            {
                return PredictionStatus.NotStarted;
            }

            return predictionStatus;
        }

        #endregion Batch

        #region Prediction
        // Predict remaining lifetime of Battery using Prediction Api
        public double PredictLifetime(BatteryDto battery)
        {
            double prediction;

            HttpClient client = PredictClient();
            string uri = "api/predictions/Lifetime/";

            HttpResponseMessage response = client.PutAsJsonAsync<BatteryDto>(uri, battery).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                prediction = response.Content.ReadAsAsync<double>().Result;
            }
            else
            {
                throw new Exception("Unable to make prediction");
            }

            return prediction;
        }

        // Start prediction jobs for Batch of Batteries using Prediction Api
        public bool PredictLifetimes(List<BatteryDto> batteries)
        {
            // Get batchId from battery lists
            int batchId;
            try
            {
                batchId = Int32.Parse(batteries[0].BatchId.ToString());
            }
            catch
            {
                return false;
            }

            // Separate batteries into two lists; Linear, Decision Forest
            List<BatteryDto> batteries_decisionForest = new List<BatteryDto>();
            List<BatteryDto> batteries_linear = new List<BatteryDto>();

            foreach(BatteryDto battery in batteries)
            {
                if(LinearThreshold(battery.Discharge_Energy))
                {
                    batteries_linear.Add(battery);
                }
                else
                {
                    batteries_decisionForest.Add(battery);
                }
            }

            // Get jobIds. If list is empty then set string to "NoJob";
            string jobId_linear;
            string jobId_decisionForest;

            if(batteries_linear.Count > 0)
            {
                jobId_linear = BatchRequest(batteries_linear, batchId);
            }
            else
            {
                jobId_linear = "NoJob";
            }

            if(batteries_decisionForest.Count > 0)
            {
                jobId_decisionForest = BatchRequest(batteries_decisionForest, batchId);
            }
            else
            {
                jobId_decisionForest = "NoJob";
            }

            // Get Batch from Batch Api
            BatchDto batch = GetBatch(batchId);

            // Add jobIds to batch
            batch.LinearRegressionJobId = jobId_linear;
            batch.DecisionForestRegressionJobId = jobId_decisionForest;

            // Update battery info in database in BatteryApi after prediction has been found
            if (!UpdateBatch(batch))
            {
                return false;
            }

            return true;
        }

        // Get results from Prediction Api and update database.
        public bool DownloadResults(int id)
        {
            // Get Batteries in batch
            List<BatteryDto> batteries = GetBatteries(id);

            // Get Batch
            BatchDto batch = GetBatch(id);

            // Get jobids
            string jobId_linear = batch.LinearRegressionJobId;
            string jobId_decisionForest = batch.DecisionForestRegressionJobId;

            // predictionUris
            string uri_linear = "api/predictions/DownloadLinearResults/";
            string uri_decisionForest = "api/predictions/DownloadDecisionForestResults/";

            // Get predictions for Linear Regression
            List<double> predictions_linear = new List<double>();

            if(batch.LinearRegressionJobId !="NoJob")
            {
                predictions_linear = GetPredictions(id, uri_linear, jobId_linear);
            }

            // Get predicions for Decision Forest Regression
            List<double> predictions_decisionForest = new List<double>();

            if (batch.DecisionForestRegressionJobId != "NoJob")
            {
                predictions_decisionForest = GetPredictions(id, uri_decisionForest, jobId_decisionForest);
            }

            // If either are null then return false
            try
            {
                if ((predictions_linear.Count <= 0 && jobId_linear != "NoJob" ) 
                 || (predictions_decisionForest.Count <= 0 && jobId_decisionForest != "NoJob"))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            // Separate batteries into two lists
            List<BatteryDto> batteries_linear = new List<BatteryDto>();
            List<BatteryDto> batteries_decisionForest = new List<BatteryDto>();
            foreach(BatteryDto battery in batteries)
            {
                if(LinearThreshold(battery.Discharge_Energy))
                {
                    batteries_linear.Add(battery);
                }
                else
                {
                    batteries_decisionForest.Add(battery);
                }
            }

            // Add predictions to battery lists
            for(int i = 0; i < predictions_linear.Count; i++)
            {
                 batteries_linear[i].Lifetime = predictions_linear[i];
            }

            for(int i = 0; i < predictions_decisionForest.Count; i++)
            {
                batteries_decisionForest[i].Lifetime = predictions_decisionForest[i];
            }

            // Add lists together
            List<BatteryDto> batteries_complete = new List<BatteryDto>();
            foreach(BatteryDto battery in batteries_linear)
            {
                batteries_complete.Add(battery);
            }

            foreach(BatteryDto battery in batteries_decisionForest)
            {
                batteries_complete.Add(battery);
            }

            // Update and return true
            UpdateBatteries(batteries_complete);
            return true;
        }

        // Return the minimum that a Battery's discharge energy can be for it's lifetime to be predicted using the Linear Regression web service
        private bool LinearThreshold(double discharge_energy)
        {
            if(discharge_energy > 3.1070666)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Start a batch request job for a Batch of Batteries
        private string BatchRequest(List<BatteryDto> batteries, int batchId)
        {
            // Start Batch Job in Azure Portal
            HttpClient client = PredictClient();
            string predictionUri = "api/predictions/Lifetimes/";

            HttpResponseMessage predictionResponse = client.PutAsJsonAsync<List<BatteryDto>>(predictionUri + batchId, batteries).Result;
            client.Dispose();

            if (predictionResponse.IsSuccessStatusCode)
            {
                return predictionResponse.Content.ReadAsAsync<string>().Result;
            }
            else
            {
                return null;
            }
        }

        // Get predictions for list of batteries using Predict Api
        private List<double> GetPredictions(int id, string predictionUri, string jobId)
        {
            HttpClient client = PredictClient();

            HttpResponseMessage predictionResponse = client.PutAsJsonAsync<string>(predictionUri + id, jobId).Result;
            client.Dispose();

            if (predictionResponse.IsSuccessStatusCode)
            {
                return predictionResponse.Content.ReadAsAsync<List<double>>().Result;
            }
            else
            {
                return null;
            }
        }
        #endregion Prediction

        #region User
        // Create User using User Api
        public bool CreateUser(UserDto user)
        {
            HttpClient client = UserClient();
            string uri = "api/users/create";

            HttpResponseMessage response = client.PostAsJsonAsync<UserDto>(uri, user).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Get list of Users using User Api
        public List<UserDto> GetUsers()
        {
            List<UserDto> users;

            HttpClient client = UserClient();
            string uri = "api/users/details";

            HttpResponseMessage response = client.GetAsync(uri).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                users = response.Content.ReadAsAsync<List<UserDto>>().Result;
            }
            else
            {
                return null;
            }

            return users;
        }

        // Get User using Using Api
        public UserDto GetUser(int id)
        {
            UserDto user;

            HttpClient client = UserClient();
            string uri = "api/users/detail/";

            HttpResponseMessage response = client.GetAsync(uri + id).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                user = response.Content.ReadAsAsync<UserDto>().Result;
            }
            else
            {
                return null;
            }

            return user;
        }

        // Check if there is a user with this username and password in the database in User Api
        public UserDto GetUser(string username, string password)
        {
            var client = UserClient();

            List<string> authDetails = new List<string>
            {
                username,
                password
            };
            var uri = "api/users/getuser/";

            HttpResponseMessage response = client.PostAsJsonAsync(uri, authDetails).Result;

            UserDto user;
            if (response.IsSuccessStatusCode)
            {
                user = response.Content.ReadAsAsync<UserDto>().Result;
            }
            else
            {
                throw new Exception("Received a bad response from the web service.");
            }

            client.Dispose();
            return user;
        }

        // Update User using User Api
        public bool UpdateUser(UserDto user)
        {
            HttpClient client = UserClient();
            string uri = "api/users/update/";

            HttpResponseMessage response = client.PutAsJsonAsync<UserDto>(uri, user).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete User using User Api
        public bool DeleteUser(int id)
        {
            HttpClient client = UserClient();
            string uri = "api/users/delete/";

            HttpResponseMessage response = client.PutAsJsonAsync<UserDto>(uri + id, null).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Check if this user has not been soft-deleted in the database in User Api
        public bool UserIsActive(int id)
        {
            var client = UserClient();

            var uri = "api/users/IsActive/";

            HttpResponseMessage response = client.GetAsync(uri + id).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Check if this username exists in the database in User Api
        public bool? UsernameExists(string username)
        {
            var client = UserClient();

            var uri = "api/users/UsernameExists/";

            HttpResponseMessage response = client.PutAsJsonAsync<string>(uri, username).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                bool? result = response.Content.ReadAsAsync<bool>().Result;
                return result;
            }
            else
            {
                return null;
            }
        }
        #endregion User

        #region HttpClient
        // Create a client that is used to communicate with BatteryApi
        private static HttpClient BatteryClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }

        // Create a client that is used to communicate with PredictApi
        private static HttpClient PredictClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44322/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }

        // Create a client that is used to communicate with UserApi
        private static HttpClient UserClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44315/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }
        #endregion HttpClient
    }
}
