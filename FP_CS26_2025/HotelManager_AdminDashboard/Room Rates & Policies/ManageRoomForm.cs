using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025.Room_Rates___Policies
{
    public partial class ManageRoomForm : Form
    {
        private readonly IHotelDataService _dataService;
        private readonly string _roomNumber;
        private readonly bool _isEdit;

        public ManageRoomForm(IHotelDataService dataService, string roomNumber = null)
        {
            InitializeComponent();
            _dataService = dataService;
            _roomNumber = roomNumber;
            _isEdit = !string.IsNullOrEmpty(roomNumber);
            
            this.Text = _isEdit ? "Edit Physical Room" : "Add Physical Room";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            LoadRoomTypes();
            LoadStatuses();
            LoadBedConfigs();
            LoadViewTypes();
            
            if (_isEdit)
            {
                LoadRoomData();
                txtRoomNumber.ReadOnly = true;
            }

            // Input Validation
            txtRoomNumber.KeyPress += RestrictToNumbers;
        }

        private void RestrictToNumbers(object sender, KeyPressEventArgs e)
        {
            // Allow digits and backspace only
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LoadRoomTypes()
        {
            try
            {
                DataTable dt = _dataService.GetAllRoomTypes();
                cmbRoomType.DataSource = dt;
                cmbRoomType.DisplayMember = "TypeName";
                cmbRoomType.ValueMember = "RoomTypeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading room types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatuses()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Occupied");
            cmbStatus.Items.Add("Reserved");
            cmbStatus.Items.Add("Under Maintenance");
            cmbStatus.Items.Add("Cleaning");
            cmbStatus.Items.Add("Out of Service");
            cmbStatus.Items.Add("ReadyForCheckIn");
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadBedConfigs()
        {
            cmbBedConfig.Items.Clear();
            cmbBedConfig.Items.Add("King Bed");
            cmbBedConfig.Items.Add("Queen Bed");
            cmbBedConfig.Items.Add("Twin Beds");
            cmbBedConfig.Items.Add("Two Double Beds");
            cmbBedConfig.Items.Add("Multiple Beds");
            cmbBedConfig.Items.Add("Standard");
            cmbBedConfig.SelectedIndex = 5; // Default to "Standard"
        }

        private void LoadViewTypes()
        {
            cmbViewType.Items.Clear();
            cmbViewType.Items.Add("City View");
            cmbViewType.Items.Add("Garden View");
            cmbViewType.Items.Add("Sea View");
            cmbViewType.Items.Add("Panoramic View");
            cmbViewType.Items.Add("Mountain View");
            cmbViewType.SelectedIndex = 0; // Default to "City View"
        }

        private void LoadRoomData()
        {
            try
            {
                DataTable dt = _dataService.GetAllPhysicalRooms();
                DataRow[] rows = dt.Select($"RoomNumber = '{_roomNumber}'");
                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    txtRoomNumber.Text = row["RoomNumber"].ToString();
                    numFloor.Value = Convert.ToInt32(row["Floor"]);
                    cmbRoomType.SelectedValue = row["RoomTypeID"];
                    cmbStatus.SelectedItem = row["Status"].ToString();
                    
                    // Load new metadata fields
                    if (row.Table.Columns.Contains("BedConfig") && row["BedConfig"] != DBNull.Value)
                        cmbBedConfig.SelectedItem = row["BedConfig"].ToString();
                    if (row.Table.Columns.Contains("ViewType") && row["ViewType"] != DBNull.Value)
                        cmbViewType.SelectedItem = row["ViewType"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading room info: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Please enter a room number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string rNum = txtRoomNumber.Text.Trim();
                int typeId = (int)cmbRoomType.SelectedValue;
                int floor = (int)numFloor.Value;
                string status = cmbStatus.SelectedItem.ToString();
                string bedConfig = cmbBedConfig.SelectedItem?.ToString() ?? "Standard";
                string viewType = cmbViewType.SelectedItem?.ToString() ?? "City View";
                
                // Sanitize for DB Constraint (No spaces allowed in Enum-mapped constraints)
                if (status == "Under Maintenance") status = "UnderMaintenance";
                if (status == "Out of Service") status = "OutOfService";

                _dataService.SavePhysicalRoom(rNum, typeId, floor, status, bedConfig, viewType);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving room: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
