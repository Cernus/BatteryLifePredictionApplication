using System.Collections.Generic;

namespace PredictionApi.Batch
{
    public class BatchExecutionRequest
    {
        public IDictionary<string, AzureBlobDataReference> Inputs { get; set; }

        public IDictionary<string, string> GlobalParameters { get; set; }

        // Locations for the potential multiple batch scoring outputs
        public IDictionary<string, AzureBlobDataReference> Outputs { get; set; }
    }
}
