using BatteryApi.DAL;
using BatteryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace BatteryApi.Repositories
{
    public class BatteryRepository : IBatteryRepository
    {
        private readonly BatteryLifetimeContext _context;
        public BatteryRepository(BatteryLifetimeContext context)
        {
            _context = context;
        }

        // Create multiple Batteries in the database
        public async Task<List<Battery>> CreateBatteries(List<BatteryDto> batteries)
        {
            try
            {
                List<Battery> entities = new List<Battery>();

                foreach (BatteryDto battery in batteries)
                {
                    Battery entity = new Battery
                    {
                        BatteryId = battery.BatteryId,
                        Battery_Ref = battery.Battery_Ref,
                        Cycle_Index = battery.Cycle_Index,
                        Charge_Capacity = battery.Charge_Capacity,
                        Discharge_Capacity = battery.Discharge_Capacity,
                        Charge_Energy = battery.Charge_Energy,
                        Discharge_Energy = battery.Discharge_Energy,
                        dvdt = battery.dvdt,
                        Internal_Resistance = battery.Internal_Resistance,
                        Lifetime = battery.Lifetime,
                        BatchId = battery.BatchId,
                        Active = true
                    };

                    entities.Add(entity);
                }

                foreach (Battery entity in entities)
                {
                    _context.Add(entity);
                }
                await _context.SaveChangesAsync();

                return entities;
            }
            catch
            {
                return null;
            }
        }

        // Create a Battery in the database
        public async Task<Battery> CreateBattery(BatteryDto battery)
        {
            try
            {
                Battery entity = new Battery
                {
                    BatteryId = battery.BatteryId,
                    Battery_Ref = battery.Battery_Ref,
                    Cycle_Index = battery.Cycle_Index,
                    Charge_Capacity = battery.Charge_Capacity,
                    Discharge_Capacity = battery.Discharge_Capacity,
                    Charge_Energy = battery.Charge_Energy,
                    Discharge_Energy = battery.Discharge_Energy,
                    dvdt = battery.dvdt,
                    Internal_Resistance = battery.Internal_Resistance,
                    Lifetime = battery.Lifetime,
                    BatchId = battery.BatchId,
                    Active = true
                };

                _context.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        // Get all Batteries in the database
        public async Task<List<BatteryDto>> GetBatteries()
        {
            try
            {
                List<Battery> batteries = await _context.Batteries
                    .Where(b => b.Active == true)
                    .ToListAsync();

                List<BatteryDto> batteriesDto = batteries.Select(b => new BatteryDto
                {
                    BatteryId = b.BatteryId,
                    Battery_Ref = b.Battery_Ref,
                    Cycle_Index = b.Cycle_Index,
                    Charge_Capacity = b.Charge_Capacity,
                    Discharge_Capacity = b.Discharge_Capacity,
                    Charge_Energy = b.Charge_Energy,
                    Discharge_Energy = b.Discharge_Energy,
                    dvdt = b.dvdt,
                    Internal_Resistance = b.Internal_Resistance,
                    Lifetime = b.Lifetime,
                    BatchId = b.BatchId
                }).ToList();

                return batteriesDto;
            }
            catch
            {
                return null;
            }
        }

        // Get all Batteries in a specific Batch in the database
        public async Task<List<BatteryDto>> GetBatteries(int batchId)
        {
            try
            {
                List<Battery> batteries = await _context.Batteries
                    .Where(b => b.Active == true && b.BatchId == batchId)
                    .ToListAsync();

                List<BatteryDto> batteriesDto = batteries.Select(b => new BatteryDto
                {
                    BatteryId = b.BatteryId,
                    Battery_Ref = b.Battery_Ref,
                    Cycle_Index = b.Cycle_Index,
                    Charge_Capacity = b.Charge_Capacity,
                    Discharge_Capacity = b.Discharge_Capacity,
                    Charge_Energy = b.Charge_Energy,
                    Discharge_Energy = b.Discharge_Energy,
                    dvdt = b.dvdt,
                    Internal_Resistance = b.Internal_Resistance,
                    Lifetime = b.Lifetime,
                    BatchId = b.BatchId
                }).ToList();

                return batteriesDto;
            }
            catch
            {
                return null;
            }
        }

        // Get a Battery in the database
        public async Task<BatteryDto> GetBattery(int id)
        {
            try
            {
                Battery battery = await _context.Batteries
                    .Where(b => b.BatteryId == id && b.Active == true)
                    .FirstOrDefaultAsync();

                BatteryDto batteryDto = new BatteryDto
                {
                    BatteryId = battery.BatteryId,
                    Battery_Ref = battery.Battery_Ref,
                    Cycle_Index = battery.Cycle_Index,
                    Charge_Capacity = battery.Charge_Capacity,
                    Discharge_Capacity = battery.Discharge_Capacity,
                    Charge_Energy = battery.Charge_Energy,
                    Discharge_Energy = battery.Discharge_Energy,
                    dvdt = battery.dvdt,
                    Internal_Resistance = battery.Internal_Resistance,
                    Lifetime = battery.Lifetime,
                    BatchId = battery.BatchId
                };

                return batteryDto;
            }
            catch
            {
                return null;
            }
        }

        // Update a Battery in the database
        public async Task<Battery> UpdateBattery(BatteryDto battery)
        {
            try
            {
                Battery entity = await _context.Batteries.FirstOrDefaultAsync(b => b.BatteryId == battery.BatteryId);

                entity.Battery_Ref = battery.Battery_Ref;
                entity.Battery_Ref = battery.Battery_Ref;
                entity.Cycle_Index = battery.Cycle_Index;
                entity.Charge_Capacity = battery.Charge_Capacity;
                entity.Discharge_Capacity = battery.Discharge_Capacity;
                entity.Charge_Energy = battery.Charge_Energy;
                entity.Discharge_Energy = battery.Discharge_Energy;
                entity.dvdt = battery.dvdt;
                entity.Internal_Resistance = battery.Internal_Resistance;
                entity.Lifetime = battery.Lifetime;
                entity.BatchId = battery.BatchId;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        // Update multiple Batteries in the database
        public async Task<List<Battery>> UpdateBatteries(List<BatteryDto> batteries)
        {
            try
            {
                List<Battery> entities = new List<Battery>();

                foreach (BatteryDto battery in batteries)
                {
                    try
                    {
                        Battery entity = await _context.Batteries.FirstOrDefaultAsync(b => b.BatteryId == battery.BatteryId);

                        entity.Battery_Ref = battery.Battery_Ref;
                        entity.Battery_Ref = battery.Battery_Ref;
                        entity.Cycle_Index = battery.Cycle_Index;
                        entity.Charge_Capacity = battery.Charge_Capacity;
                        entity.Discharge_Capacity = battery.Discharge_Capacity;
                        entity.Charge_Energy = battery.Charge_Energy;
                        entity.Discharge_Energy = battery.Discharge_Energy;
                        entity.dvdt = battery.dvdt;
                        entity.Internal_Resistance = battery.Internal_Resistance;
                        entity.Lifetime = battery.Lifetime;
                        entity.BatchId = battery.BatchId;

                        entities.Add(entity);
                    }
                    catch
                    {
                        return null;
                    }
                }

                foreach (Battery entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Delete a Battery in the database
        public async Task<Battery> DeleteBattery(int id)
        {
            try
            {
                Battery entity = await _context.Batteries.FirstOrDefaultAsync(b => b.BatteryId == id);
                entity.Active = false;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        // Check if a Battery has a Prediction
        public async Task<bool> HasPredictions(int id)
        {
            try
            {
                List<Battery> batteries = await _context.Batteries
                    .Where(b => b.Active == true && b.BatchId == id)
                    .ToListAsync();

                foreach (Battery battery in batteries)
                {
                    if (battery.Lifetime != null)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
