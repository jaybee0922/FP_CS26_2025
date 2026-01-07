using System;
using System.Windows.Forms;
using FP_CS26_2025;
using FP_CS26_2025.HotelManager_AdminDashboard.Configuration; // Import Configuration namespace

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public partial class SystemConfigurationControl : UserControl
    {
        private SystemConfigController _controller;

        public SystemConfigurationControl()
        {
            InitializeComponent();
            // Initialize with default implementations (Composition Root for this control)
            // In a larger app, this would be done via DI Container
            IConfigService configService = new XmlConfigService();
            // Database service will be fully initialized when DataManager is set
            // For now, we defer controller creation slightly or handle nulls
        }

        public void SetDataManager(DataManager dataManager)
        {
            // Initialize Controller with dependencies
            IConfigService configService = new XmlConfigService();
            IDatabaseService databaseService = new SqlDatabaseService(dataManager);
            _controller = new SystemConfigController(configService, databaseService);

            // Load initial settings
            LoadSettings();
            
            // Wire up events if not already wired
            btnSaveSettings.Click -= BtnSaveSettings_Click; // Prevent double subscription
            btnSaveSettings.Click += BtnSaveSettings_Click;
            
            btnSavePolicies.Click -= BtnSavePolicies_Click;
            btnSavePolicies.Click += BtnSavePolicies_Click;
            
            btnBrowseBackup.Click -= BtnBrowseBackup_Click;
            btnBrowseBackup.Click += BtnBrowseBackup_Click;
            
            btnBackupNow.Click -= BtnBackupNow_Click;
            btnBackupNow.Click += BtnBackupNow_Click;
        }

        private void LoadSettings()
        {
            if (_controller == null) return;

            var config = _controller.GetCurrentConfig();
            // General
            txtHotelName.Text = config.HotelName;
            txtAddress.Text = config.HotelAddress;
            txtPhone.Text = config.HotelPhone;
            txtEmail.Text = config.HotelEmail;
            txtCopyright.Text = config.CopyrightText;
            
            // Policies
            txtPolicyConfig.Text = config.PolicyText;
            
            // Database
            txtBackupPath.Text = config.BackupPath;
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            if (_controller == null) return;

            try
            {
                _controller.SaveGeneralSettings(
                    txtHotelName.Text,
                    txtAddress.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtCopyright.Text
                );
                MessageBox.Show("General settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSavePolicies_Click(object sender, EventArgs e)
        {
            if (_controller == null) return;

            try
            {
                _controller.SavePolicies(txtPolicyConfig.Text);
                MessageBox.Show("Policy settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving policies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBrowseBackup_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Backup Directory";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath.Text = dialog.SelectedPath;
                    _controller?.SaveBackupPath(dialog.SelectedPath);
                }
            }
        }

        private void BtnBackupNow_Click(object sender, EventArgs e)
        {
            if (_controller == null) return;

            string path = txtBackupPath.Text;
            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool success = _controller.PerformBackup(path);
                this.Cursor = Cursors.Default;

                if (success)
                {
                     MessageBox.Show("Database backup created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Database backup failed. Check permissions or logs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error backing up database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
