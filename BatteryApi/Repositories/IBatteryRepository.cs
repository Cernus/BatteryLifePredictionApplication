using BatteryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApi.Repositories
{
    public interface IBatteryRepository
    {
        Task<List<Battery>> CreateBatteries(List<BatteryDto> batteries);
        Task<Battery> CreateBattery(BatteryDto battery);
        Task<List<BatteryDto>> GetBatteries();
        Task<List<BatteryDto>> GetBatteries(int batchId);
        Task<BatteryDto> GetBattery(int id);
        Task<Battery> UpdateBattery(BatteryDto battery);
        Task<List<Battery>> UpdateBatteries(List<BatteryDto> batteries);
        Task<Battery> DeleteBattery(int id);
        Task<bool> HasPredictions(int id);
    }
}
