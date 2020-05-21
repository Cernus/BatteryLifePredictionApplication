using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryApi.Models
{
    public class Batch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchId { get; set; }
        public string Batch_Ref { get; set; }
        public string LinearRegressionJobId { get; set; }
        public string DecisionForestRegressionJobId { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}
