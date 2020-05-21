using BatteryApi.Models;
using System.Linq;

namespace BatteryApi.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(BatteryLifetimeContext context)
        {
            context.Database.EnsureCreated();

            // Add initial battery data if database is empty
            if (!context.Batteries.Any())
            {
                Battery[] batteries = new Battery[]
                {
                    new Battery
                    {
                        Battery_Ref = "Ref1",
                        Cycle_Index = 1,
                        Charge_Capacity = 1.0633432,
                        Discharge_Capacity = 1.0636606,
                        Charge_Energy = 3.6937687,
                        Discharge_Energy = 3.2390177,
                        dvdt = 0.022200584,
                        Internal_Resistance = 0.015485343,
                        Lifetime = null,
                        BatchId = 1,
                        Active = true
                    },
                    new Battery
                    {
                        Battery_Ref = "Ref2",
                        Cycle_Index = 400,
                        Charge_Capacity = 1.0508872,
                        Discharge_Capacity = 1.0523232,
                        Charge_Energy = 3.625576,
                        Discharge_Energy = 3.1976273,
                        dvdt = 0.030249596,
                        Internal_Resistance = 0.015213728,
                        Lifetime = null,
                        BatchId = 1,
                        Active = true
                    },
                    new Battery
                    {
                        Battery_Ref = "Ref3",
                        Cycle_Index = 800,
                        Charge_Capacity = 1.0159504,
                        Discharge_Capacity = 1.0160737,
                        Charge_Energy = 3.5323973,
                        Discharge_Energy = 3.049123,
                        dvdt = 0.033846855,
                        Internal_Resistance = 0.015397729,
                        Lifetime = null,
                        BatchId = 1,
                        Active = true
                    }
                };

                foreach (Battery b in batteries)
                {
                    context.Batteries.Add(b);
                }
            }

            if (!context.Batches.Any())
            {
                Batch[] batches = new Batch[]
                {
                    new Batch
                    {
                        Batch_Ref = "Ref1",
                        LinearRegressionJobId = null,
                        DecisionForestRegressionJobId = null,
                        UserId = 2,
                        Active = true
                    }
                };

                foreach (Batch b in batches)
                {
                    context.Batches.Add(b);
                }
            }

            context.SaveChanges();
        }
    }
}
