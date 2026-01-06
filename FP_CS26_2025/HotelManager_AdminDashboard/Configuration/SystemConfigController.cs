using FP_CS26_2025;
using System;

namespace FP_CS26_2025.HotelManager_AdminDashboard.Configuration
{
    // Controller class handling business logic and mediation
    public class SystemConfigController
    {
        private readonly IConfigService _configService;
        private readonly IDatabaseService _databaseService;

        // Constructor Injection
        public SystemConfigController(IConfigService configService, IDatabaseService databaseService)
        {
            _configService = configService;
            _databaseService = databaseService;
        }

        public AppConfig GetCurrentConfig()
        {
            return _configService.LoadConfig();
        }

        public void SaveGeneralSettings(string name, string address, string phone, string email, decimal taxRate)
        {
            // Business logic validation could go here
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Hotel Name cannot be empty.");
            
            _configService.UpdateGeneralSettings(name, address, phone, email, taxRate);
        }

        public void SavePolicies(string policyText)
        {
            _configService.UpdatePolicySettings(policyText);
        }

        public void SaveBackupPath(string path)
        {
            _configService.UpdateBackupPath(path);
        }

        public bool PerformBackup(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Backup path is invalid.");
            return _databaseService.BackupDatabase(path);
        }
    }
}
