using FP_CS26_2025.HotelManager_AdminDashboard;

namespace FP_CS26_2025.HotelManager_AdminDashboard.Configuration
{
    public class SqlDatabaseService : IDatabaseService
    {
        private readonly DataManager _dataManager;

        // Dependency Injection of DataManager
        public SqlDatabaseService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public bool BackupDatabase(string destinationPath)
        {
            if (_dataManager == null) return false;
            return _dataManager.BackupDatabase(destinationPath);
        }
    }
}
