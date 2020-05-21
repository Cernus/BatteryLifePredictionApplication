using BatteryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApi.Repositories
{
    public interface IBatchRepository
    {
        Task<Batch> CreateBatch(BatchDto batch);
        Task<List<BatchDto>> GetBatches();
        Task<List<BatchDto>> GetBatches(int userId);
        Task<BatchDto> GetBatch(int id);
        Task<Batch> UpdateBatch(BatchDto batch);
        Task<Batch> DeleteBatch(int id);
        Task<PredictionStatus> GetPredictionStatus(int id);
    }
}
