using AppFacade.Models;
using System.Collections.Generic;

namespace AppFacade
{
    public interface IFacade
    {
        #region Battery
        bool CreateBatteries(List<BatteryDto> batteries);
        bool CreateBattery(BatteryDto battery);
        List<BatteryDto> GetBatteries();
        List<BatteryDto> GetBatteries(int batchId);
        BatteryDto GetBattery(int id);
        bool UpdateBattery(BatteryDto battery);
        bool UpdateBatteries(List<BatteryDto> batteries);
        bool DeleteBattery(int id);
        #endregion battery

        #region Batch
        bool CreateBatch(BatchDto batch);
        List<BatchDto> GetBatches();
        List<BatchDto> GetBatches(int userId);
        BatchDto GetBatch(int id);
        bool UpdateBatch(BatchDto batch);
        bool DeleteBatch(int id);
        PredictionStatus GetPredictionStatus(int id);
        #endregion Batch

        #region Prediction
        double PredictLifetime(BatteryDto battery);
        bool PredictLifetimes(List<BatteryDto> batteries);
        bool DownloadResults(int id);
        #endregion Prediction

        #region User
        bool CreateUser(UserDto user);
        List<UserDto> GetUsers();
        UserDto GetUser(int id);
        bool UpdateUser(UserDto user);
        bool DeleteUser(int id);
        UserDto GetUser(string username, string password);
        bool UserIsActive(int id);
        bool? UsernameExists(string username);
        #endregion User
    }
}
