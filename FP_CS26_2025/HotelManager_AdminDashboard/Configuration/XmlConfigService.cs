using FP_CS26_2025;
using System;

namespace FP_CS26_2025.HotelManager_AdminDashboard.Configuration
{
    public class XmlConfigService : IConfigService
    {
        // Encapsulates the static ConfigHelper
        public AppConfig LoadConfig()
        {
            return ConfigHelper.LoadConfig();
        }

        public void SaveConfig(AppConfig config)
        {
            ConfigHelper.SaveConfig(config);
        }

        public void UpdateGeneralSettings(string name, string address, string phone, string email, decimal taxRate)
        {
            var config = ConfigHelper.LoadConfig();
            config.HotelName = name;
            config.HotelAddress = address;
            config.HotelPhone = phone;
            config.HotelEmail = email;
            config.TaxRate = taxRate;
            ConfigHelper.SaveConfig(config);
        }

        public void UpdatePolicySettings(string policyText)
        {
            var config = ConfigHelper.LoadConfig();
            config.PolicyText = policyText;
            ConfigHelper.SaveConfig(config);
        }

        public void UpdateBackupPath(string path)
        {
            var config = ConfigHelper.LoadConfig();
            config.BackupPath = path;
            ConfigHelper.SaveConfig(config);
        }
    }
}
