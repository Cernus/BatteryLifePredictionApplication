namespace AppFacade.Models
{
    public class BatteryDto
    {
        public int BatteryId { get; set; }
        public string Battery_Ref { get; set; }
        public int Cycle_Index { get; set; }
        public double Charge_Capacity { get; set; }
        public double Discharge_Capacity { get; set; }
        public double Charge_Energy { get; set; }
        public double Discharge_Energy { get; set; }
        public double dvdt { get; set; }
        public double Internal_Resistance { get; set; }
        public int BatchId { get; set; }
        public double? Lifetime { get; set; }
    }
}
