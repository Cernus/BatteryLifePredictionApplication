namespace AppFacade.Models
{
    public class BatchDto
    {
        public int BatchId { get; set; }
        public string Batch_Ref { get; set; }
        public string LinearRegressionJobId { get; set; }
        public string DecisionForestRegressionJobId { get; set; }
        public int UserId { get; set; }
        public int BatteryCount { get; set; }
        public PredictionStatus PredictionStatus { get; set; }
    }
}
