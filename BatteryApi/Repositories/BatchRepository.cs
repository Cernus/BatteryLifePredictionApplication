using BatteryApi.DAL;
using BatteryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BatteryApi.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly BatteryLifetimeContext _context;
        public BatchRepository(BatteryLifetimeContext context)
        {
            _context = context;
        }

        // Create a new Batch in the database
        public async Task<Batch> CreateBatch(BatchDto batch)
        {
            try
            {
                Batch entity = new Batch
                {
                    Batch_Ref = batch.Batch_Ref,
                    LinearRegressionJobId = batch.LinearRegressionJobId,
                    DecisionForestRegressionJobId = batch.DecisionForestRegressionJobId,
                    UserId = batch.UserId,
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

        // Get all Batches in the database
        public async Task<List<BatchDto>> GetBatches()
        {
            BatteryRepository batteryRepository = new BatteryRepository(_context);

            try
            {
                List<Batch> batches = await _context.Batches
                    .Where(b => b.Active == true)
                    .ToListAsync();

                List<BatchDto> batchesDto = batches.Select(b => new BatchDto
                {
                    BatchId = b.BatchId,
                    Batch_Ref = b.Batch_Ref,
                    LinearRegressionJobId = b.LinearRegressionJobId,
                    DecisionForestRegressionJobId = b.DecisionForestRegressionJobId,
                    UserId = b.UserId,
                    BatteryCount = batteryRepository.GetBatteries(b.BatchId).Result.Count,
                    PredictionStatus = GetPredictionStatus(b.BatchId).Result
                }).ToList();

                return batchesDto;
            }
            catch
            {
                return null;
            }
        }

        // Get all Batches associated with a specific User in the database
        public async Task<List<BatchDto>> GetBatches(int userId)
        {
            BatteryRepository batteryRepository = new BatteryRepository(_context);

            try
            {
                List<Batch> batches = await _context.Batches
                    .Where(b => b.Active == true && b.UserId == userId)
                    .ToListAsync();

                List<BatchDto> batchesDto = batches.Select(b => new BatchDto
                {
                    BatchId = b.BatchId,
                    Batch_Ref = b.Batch_Ref,
                    LinearRegressionJobId = b.LinearRegressionJobId,
                    DecisionForestRegressionJobId = b.DecisionForestRegressionJobId,
                    UserId = b.UserId,
                    BatteryCount = batteryRepository.GetBatteries(b.BatchId).Result.Count,
                    PredictionStatus = GetPredictionStatus(b.BatchId).Result
                }).ToList();

                return batchesDto;
            }
            catch
            {
                return null;
            }
        }

        // Get a Batch in the database
        public async Task<BatchDto> GetBatch(int id)
        {
            BatteryRepository batteryRepository = new BatteryRepository(_context);

            try
            {
                Batch batch = await _context.Batches
                    .Where(b => b.BatchId == id && b.Active == true)
                    .FirstOrDefaultAsync();

                BatchDto batchDto = new BatchDto
                {
                    BatchId = batch.BatchId,
                    Batch_Ref = batch.Batch_Ref,
                    LinearRegressionJobId = batch.LinearRegressionJobId,
                    DecisionForestRegressionJobId = batch.DecisionForestRegressionJobId,
                    UserId = batch.UserId,
                    BatteryCount = batteryRepository.GetBatteries(batch.BatchId).Result.Count,
                    PredictionStatus = PredictionStatus.NotStarted // Set to NotStarted rather than call method to avoid infinite loop
                };

                return batchDto;
            }
            catch
            {
                return null;
            }
        }
        
        // Update a Batch in the database.
        public async Task<Batch> UpdateBatch(BatchDto batch)
        {
            try
            {
                Batch entity = await _context.Batches.FirstOrDefaultAsync(b => b.BatchId == batch.BatchId);

                entity.Batch_Ref = batch.Batch_Ref;
                entity.LinearRegressionJobId = batch.LinearRegressionJobId;
                entity.DecisionForestRegressionJobId = batch.DecisionForestRegressionJobId;
                entity.UserId = batch.UserId;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        // Delete a Batch in the database
        public async Task<Batch> DeleteBatch(int id)
        {
            try
            {
                Batch entity = await _context.Batches.FirstOrDefaultAsync(b => b.BatchId == id);
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

        // Get the Prediction Status for a Batch
        public async Task<PredictionStatus> GetPredictionStatus(int batchId)
        {
            try
            {
                // If all batteries' have a lifetime then Complete
                BatteryRepository batteryRepository = new BatteryRepository(_context);
                List<BatteryDto> batteries = batteryRepository.GetBatteries(batchId).Result;

                bool nullLifetime = false;
                foreach (BatteryDto battery in batteries)
                {
                    if (battery.Lifetime == null)
                    {
                        nullLifetime = true;
                        break;
                    }
                }

                if (nullLifetime == false)
                {
                    return PredictionStatus.Complete;
                }

                // If batch has job ID's then: InProgress
                BatchDto batch = GetBatch(batchId).Result;

                if (batch.DecisionForestRegressionJobId != null || batch.LinearRegressionJobId != null)
                {
                    return PredictionStatus.InProgress;
                }

                // Otherwise Not Started
                return PredictionStatus.NotStarted;
            }
            catch
            {
                return PredictionStatus.NotStarted;
            }
        }
    }
}
