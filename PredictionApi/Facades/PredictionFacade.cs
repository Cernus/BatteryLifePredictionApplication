using PredictionApi.Models;
using PredictionApi.Batch;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Threading;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PredictionApi.Facades
{
    public class PredictionFacade : IPredictionFacade
    {
        // Get a remaining lifetime prediction from the web service
        public async Task<double> GetLifetime(BatteryDto battery)
        {
            using (var client = new HttpClient())
            {
                // Create the object to be sent to the web service
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                    {
                                        "Cycle_Index", battery.Cycle_Index.ToString()
                                    },
                                    {
                                        "Charge_Capacity", battery.Charge_Capacity.ToString()
                                    },
                                    {
                                        "Discharge_Capacity", battery.Discharge_Capacity.ToString()
                                    },
                                    {
                                        "Charge_Energy", battery.Charge_Energy.ToString()
                                    },
                                    {
                                        "Discharge_Energy", battery.Discharge_Energy.ToString()
                                    },
                                    {
                                        "dV/dt", battery.dvdt.ToString()
                                    },
                                    {
                                        "Internal_Resistance", battery.Internal_Resistance.ToString()
                                    },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                string apiKey;

                if (battery.Discharge_Energy <= GetThreshold())
                {
                    // DECISION FOREST REGRESSION
                    apiKey = "tVDT+uJyjuGsQaMho7KgoOo7ERiIcjUeYSmhbA1Vz6dekKQroe+zqogfs6HpSwGkPmxlLfj9UDl64X/+0Wju+w==";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/5a2852d9d5ca47d29a16880c797221e5/services/1f0b15f849eb4659814134fb074fd956/execute?api-version=2.0&format=swagger");
                }
                else
                {
                    // LINEAR REGRESSION
                    apiKey = "KDP0a8bu3pEGnTyK27xd79ewUxBWyrm3es9FOrRH7fI48UC9iBdjanhGGt9aHxc0jIKwf9OnZSOb09VXoDpWeg==";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/5a2852d9d5ca47d29a16880c797221e5/services/3f597ac1707843fbb40b609fee01b589/execute?api-version=2.0&format=swagger");
                }

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                // 
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    try
                    {
                        string text = "Prediction";

                        int index1 = result.IndexOf(text) + text.Length + 3;
                        int index2 = result.IndexOf("}", index1) - 1;
                        string substring = result.Substring(index1, (index2 - index1));

                        return Double.Parse(substring);
                    }
                    catch
                    {
                        throw new Exception("Lifetime returned was in an unexpected type");
                    }
                }
                else
                {
                    throw new Exception(string.Format("The request failed with status code: {0}", response.StatusCode));
                }
            }
        }

        // Send battery data to Azure and start the jobs to predict their remaining lifetime
        public async Task<string> GetLifetimes(List<BatteryDto> batteries, int fileNumber)
        {
            // Determine if Linear Regression or Decision Forest Regression should be used
            bool decisonForestRegression = (batteries[0].Discharge_Energy <= GetThreshold());
            
            string basePath = AppContext.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));

            string inputFileName = "input" + fileNumber;
            string outputFileName = "output" + fileNumber;

            if(decisonForestRegression)
            {
                inputFileName += "datablob_decisionForest.csv";
                outputFileName += "datablob_decisionForest.csv";
            }
            else
            {
                inputFileName += "datablob_linear.csv";
                outputFileName += "datablob_linear.csv";
            }

            string filePath = projectPath + "InputFiles\\" + inputFileName;

            // Create InputFile
            if (!CreateInputFile(batteries, inputFileName))
            {
                throw new Exception("Unable to create input file");
            }

            const string StorageContainerName = "generalstoragecontainer";
            const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=generalstoragej9239007;AccountKey=H6i9sWfVJBu59x0jzi2SSgCN0JMnxR0aqzIGqz4twgfFZ7zjM7tASfqpr9XtAPNAeQFmWEZDv8n1OH4kiuqd7g==;EndpointSuffix=core.windows.net";
            string apiKey;
            string BaseUrl;

            if(decisonForestRegression)
            {
                // DECISION FOREST REGRESSION
                apiKey = "tVDT+uJyjuGsQaMho7KgoOo7ERiIcjUeYSmhbA1Vz6dekKQroe+zqogfs6HpSwGkPmxlLfj9UDl64X/+0Wju+w==";
                BaseUrl = "https://ussouthcentral.services.azureml.net/workspaces/5a2852d9d5ca47d29a16880c797221e5/services/1f0b15f849eb4659814134fb074fd956/jobs";
            }
            else
            {
                //  LINEAR REGRESSION
                apiKey = "KDP0a8bu3pEGnTyK27xd79ewUxBWyrm3es9FOrRH7fI48UC9iBdjanhGGt9aHxc0jIKwf9OnZSOb09VXoDpWeg==";
                BaseUrl = "https://ussouthcentral.services.azureml.net/workspaces/5a2852d9d5ca47d29a16880c797221e5/services/3f597ac1707843fbb40b609fee01b589/jobs";
            }

            UploadFileToBlob(filePath, inputFileName, StorageContainerName, storageConnectionString);

            string jobId = await SubmitJob(BaseUrl, apiKey, storageConnectionString, StorageContainerName, inputFileName, outputFileName);

            // Return jobId
            return jobId;
        }

        // Submit and start a Job in Azure
        private async Task<string> SubmitJob(string BaseUrl, string apiKey, string storageConnectionString, string StorageContainerName, string inputFileName, string outputFileName)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = new BatchExecutionRequest()
                {

                    Inputs = new Dictionary<string, AzureBlobDataReference>()
                    {

                        {
                            "input1",
                            new AzureBlobDataReference()
                            {
                                ConnectionString = storageConnectionString,
                                RelativeLocation = string.Format("{0}/" + inputFileName, StorageContainerName)
                            }
                        },
                    },

                    Outputs = new Dictionary<string, AzureBlobDataReference>()
                    {

                        {
                            "output1",
                            new AzureBlobDataReference()
                            {
                                ConnectionString = storageConnectionString,
                                RelativeLocation = string.Format("/{0}/" + outputFileName, StorageContainerName)
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                Console.WriteLine("Submitting the job...");

                // submit the job
                var response = await client.PostAsJsonAsync(BaseUrl + "?api-version=2.0", request).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    await WriteFailedResponse(response);
                    throw new Exception("Unable to submit the job");
                }

                string jobId = await response.Content.ReadAsAsync<string>();
                Console.WriteLine(string.Format("Job ID: {0}", jobId));


                // start the job
                Console.WriteLine("Starting the job...");
                response = await client.PostAsync(BaseUrl + "/" + jobId + "/start?api-version=2.0", null);
                if (!response.IsSuccessStatusCode)
                {
                    await WriteFailedResponse(response);
                    throw new Exception("Unable to start the job");
                }

                return jobId;
            }
        }

        // Download the prediction results from Azure as a list of doubles
        public async Task<List<double>> DownloadResults(int fileNumber, string jobId, bool decisonForestRegression)
        {
            List<double> predictions = null;

            using (HttpClient client = new HttpClient())
            {
                string BaseUrl, apiKey;

                if (decisonForestRegression)
                {
                    // DECISION FOREST REGRESSION
                    apiKey = "tVDT+uJyjuGsQaMho7KgoOo7ERiIcjUeYSmhbA1Vz6dekKQroe+zqogfs6HpSwGkPmxlLfj9UDl64X/+0Wju+w==";
                    BaseUrl = "https://ussouthcentral.services.azureml.net/workspaces/5a2852d9d5ca47d29a16880c797221e5/services/1f0b15f849eb4659814134fb074fd956/jobs";
                }
                else
                {
                    //  LINEAR REGRESSION
                    apiKey = "KDP0a8bu3pEGnTyK27xd79ewUxBWyrm3es9FOrRH7fI48UC9iBdjanhGGt9aHxc0jIKwf9OnZSOb09VXoDpWeg==";
                    BaseUrl = "https://ussouthcentral.services.azureml.net/workspaces/5a2852d9d5ca47d29a16880c797221e5/services/3f597ac1707843fbb40b609fee01b589/jobs";
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                string jobLocation = BaseUrl + "/" + jobId + "?api-version=2.0";

                Console.WriteLine("Checking the job status...");
                var response = await client.GetAsync(jobLocation);
                if (!response.IsSuccessStatusCode)
                {
                    await WriteFailedResponse(response);
                    return null;
                }

                // Check the status of the Batch
                BatchScoreStatus status = await response.Content.ReadAsAsync<BatchScoreStatus>();
                switch (status.StatusCode)
                {
                    case BatchScoreStatusCode.NotStarted:
                        Console.WriteLine(string.Format("Job {0} not yet started...", jobId));
                        break;
                    case BatchScoreStatusCode.Running:
                        Console.WriteLine(string.Format("Job {0} running...", jobId));
                        break;
                    case BatchScoreStatusCode.Failed:
                        Console.WriteLine(string.Format("Job {0} failed!", jobId));
                        Console.WriteLine(string.Format("Error details: {0}", status.Details));
                        break;
                    case BatchScoreStatusCode.Cancelled:
                        Console.WriteLine(string.Format("Job {0} cancelled!", jobId));
                        break;
                    case BatchScoreStatusCode.Finished:
                        Console.WriteLine(string.Format("Job {0} finished!", jobId));

                        string outputFileName;
                        outputFileName = "output" + fileNumber + "datablob.csv";

                        predictions = ProcessResults(status, outputFileName);
                        break;
                }
            }

            return predictions;
        }

        // Write to console if the respose has failed
        private static async Task WriteFailedResponse(HttpResponseMessage response)
        {
            Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

            // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
            Console.WriteLine(response.Headers.ToString());

            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }

        // Create a CSV using the battery data
        private bool CreateInputFile(List<BatteryDto> batteries, string inputFileName)
        {
            string basePath = AppContext.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string inputFileLocation = projectPath + "InputFiles\\" + inputFileName;

            try
            {
                using (StreamWriter file = new StreamWriter(inputFileLocation, false))
                {
                    // Headers
                    file.WriteLine(
                        "Cycle_Index," +
                        "Charge_Capacity," +
                        "Discharge_Capacity," +
                        "Charge_Energy," +
                        "Discharge_Energy," +
                        "dV/dt," +
                        "Internal_Resistance,"
                        );

                    // Records
                    foreach (BatteryDto battery in batteries)
                    {
                        file.WriteLine(
                            battery.Cycle_Index + "," +
                            battery.Charge_Capacity + "," +
                            battery.Discharge_Capacity + "," +
                            battery.Charge_Energy + "," +
                            battery.Discharge_Energy + "," +
                            battery.dvdt + "," +
                            battery.Internal_Resistance + ","
                            );
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        // Create an output CSV file of the results from Azure
        private static List<double> SaveBlobToFile(AzureBlobDataReference blobLocation, string resultsLabel, string outputFileName)
        {
            string basePath = AppContext.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string outputFileLocation = projectPath + "OutputFiles\\" + outputFileName;

            var credentials = new StorageCredentials(blobLocation.SasBlobToken);
            var blobUrl = new Uri(new Uri(blobLocation.BaseLocation), blobLocation.RelativeLocation);
            var cloudBlob = new CloudBlockBlob(blobUrl, credentials);

            Console.WriteLine(string.Format("Reading the result from {0}", blobUrl.ToString()));
            try
            {
                cloudBlob.DownloadToFileAsync(outputFileLocation, FileMode.Create);
            }
            catch
            {
                throw new Exception("Unable to create output file");
            }

            Console.WriteLine(string.Format("{0} have been written to the file {1}", resultsLabel, outputFileLocation));

            List<double> predictions = new List<double>();

            // Maximum of 10 sleeps
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(outputFileLocation))
                    {
                        var line = reader.ReadLine(); // Ignore Headers

                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();

                            try
                            {
                                predictions.Add(Double.Parse(line));
                            }
                            catch
                            {
                                throw new Exception("Unable to read from stream");
                            }
                        }
                    }

                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }

            return predictions;
        }

        // Upload input CSV to Azure
        private static void UploadFileToBlob(string inputFileLocation, string inputBlobName, string storageContainerName, string storageConnectionString)
        {
            // Make sure the file exists
            if (!File.Exists(inputFileLocation))
            {
                throw new FileNotFoundException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "File {0} doesn't exist on local computer.",
                        inputFileLocation));
            }

            Console.WriteLine("Uploading the input to blob storage...");

            var blobClient = CloudStorageAccount.Parse(storageConnectionString).CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(storageContainerName);
            container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(inputBlobName);
            blob.UploadFromFileAsync(inputFileLocation);
        }

        // Process the results from Azure
        private static List<double> ProcessResults(BatchScoreStatus status, string outputFileName)
        {
            List<double> predictions = null;
            bool first = true;
            foreach (var output in status.Results)
            {
                var blobLocation = output.Value;
                Console.WriteLine(string.Format("The result '{0}' is available at the following Azure Storage location:", output.Key));
                Console.WriteLine(string.Format("BaseLocation: {0}", blobLocation.BaseLocation));
                Console.WriteLine(string.Format("RelativeLocation: {0}", blobLocation.RelativeLocation));
                Console.WriteLine(string.Format("SasBlobToken: {0}", blobLocation.SasBlobToken));
                Console.WriteLine();


                // Save the first output to disk
                if (first)
                {
                    first = false;

                    predictions = SaveBlobToFile(blobLocation, string.Format("The results for {0}", output.Key), outputFileName);
                }
            }

            return predictions;
        }

        // Get the minimum discharge energy that a Battery needs for its lifetime to be predicted using Linear Regression
        private static double GetThreshold()
        {
            return 3.1070666;
        }
    }
}
