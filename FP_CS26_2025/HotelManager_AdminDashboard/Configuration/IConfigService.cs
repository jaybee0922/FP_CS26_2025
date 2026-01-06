using FP_CS26_2025;

namespace FP_CS26_2025.HotelManager_AdminDashboard.Configuration
{
    public interface IConfigService
    {
        AppConfig LoadConfig();
        void SaveConfig(AppConfig config);
        void UpdateGeneralSettings(string name, string address, string phone, string email, decimal taxRate);
        void UpdatePolicySettings(string policyText);
        void UpdateBackupPath(string path);
    }
}
