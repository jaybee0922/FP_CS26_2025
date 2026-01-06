using System;
using System.IO;
using System.Xml.Serialization;

namespace FP_CS26_2025
{
    public class AppConfig
    {
        public string HotelName { get; set; } = "Grand Nexus Hotel";
        public string HotelAddress { get; set; } = "123 Main St, City, Country";
        public string HotelPhone { get; set; } = "+1 234 567 890";
        public string HotelEmail { get; set; } = "info@grandnexus.com";
        public decimal TaxRate { get; set; } = 12.0m;
        public string BackupPath { get; set; } = @"C:\Backups";

        // Policies
        public string PolicyText { get; set; } = "• Check-in: 2:00 PM\n• Check-out: 12:00 PM\n• No smoking inside rooms\n• Pets allowed only in designated rooms\n• Free cancellation up to 24 hours";
    }

    public static class ConfigHelper
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "system_config.xml");
        private static AppConfig _currentConfig;

        public static AppConfig LoadConfig()
        {
            if (_currentConfig != null) return _currentConfig;

            if (!File.Exists(ConfigPath))
            {
                _currentConfig = new AppConfig();
                SaveConfig(_currentConfig);
            }
            else
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));
                    using (StreamReader reader = new StreamReader(ConfigPath))
                    {
                        _currentConfig = (AppConfig)serializer.Deserialize(reader);
                    }
                }
                catch
                {
                    _currentConfig = new AppConfig(); // Fallback on error
                }
            }
            return _currentConfig;
        }

        public static void SaveConfig(AppConfig config)
        {
            _currentConfig = config;
            XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));
            using (StreamWriter writer = new StreamWriter(ConfigPath))
            {
                serializer.Serialize(writer, config);
            }
        }
    }
}
