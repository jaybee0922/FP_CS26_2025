using System;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using FP_CS26_2025.HotelManager_AdminDashboard;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025.Room_Rates___Policies
{
    public partial class RoomRatesControl : UserControl
    {
        private DataManager _dataManager;
        private readonly string _baseImagePath;
        private bool _isInventoryView = false;
        private readonly IHotelDataService _dataService = new SqlHotelDataService();

        public RoomRatesControl()
        {
            InitializeComponent();
            _baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Assets", "IMAGES");
        }

        public void SetDataManager(DataManager manager)
        {
            _dataManager = manager;
            RefreshCurrentView();
        }

        private void RoomRatesControl_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                RefreshCurrentView();
            }
        }

        private void UpdateView()
        {
            dgvRoomRates.Visible = !_isInventoryView;
            dgvRoomInventory.Visible = _isInventoryView;
            btnChangePrice.Visible = !_isInventoryView;
            btnAddRoom.Visible = _isInventoryView;
            
            // Toggle "Active" button styles
            btnRoomCategories.BackColor = _isInventoryView ? Color.FromArgb(189, 195, 199) : Color.FromArgb(41, 128, 185);
            btnRoomInventory.BackColor = _isInventoryView ? Color.FromArgb(41, 128, 185) : Color.FromArgb(189, 195, 199);
            
            RefreshCurrentView();
        }

        private void RefreshCurrentView()
        {
            if (_isInventoryView) LoadInventoryData();
            else LoadRoomData();
        }

        private void LoadInventoryData()
        {
            try
            {
                DataTable dt = _dataService.GetAllPhysicalRooms();
                
                // Add ordinal display for Floor
                if (dt.Columns.Contains("Floor") && !dt.Columns.Contains("FloorDisplay"))
                {
                    dt.Columns.Add("FloorDisplay", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Floor"] != DBNull.Value)
                        {
                            row["FloorDisplay"] = GetFloorOrdinal(Convert.ToInt32(row["Floor"]));
                        }
                    }
                }

                dgvRoomInventory.DataSource = dt;
                
                if (dgvRoomInventory.Columns.Contains("RoomTypeID"))
                    dgvRoomInventory.Columns["RoomTypeID"].Visible = false;
                
                if (dgvRoomInventory.Columns.Contains("Floor"))
                    dgvRoomInventory.Columns["Floor"].Visible = false;
                
                if (dgvRoomInventory.Columns.Contains("FloorDisplay"))
                {
                    dgvRoomInventory.Columns["FloorDisplay"].HeaderText = "Floor";
                    dgvRoomInventory.Columns["FloorDisplay"].DisplayIndex = 2;
                }
                
                // Color rows based on status
                foreach (DataGridViewRow row in dgvRoomInventory.Rows)
                {
                    string status = row.Cells["Status"].Value?.ToString();
                    if (status == "Under Maintenance") row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                    else if (status == "Out of Service") row.DefaultCellStyle.ForeColor = Color.Red;
                    else if (status == "Available") row.DefaultCellStyle.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRoomCategories_Click(object sender, EventArgs e)
        {
            _isInventoryView = false;
            UpdateView();
        }

        private void btnRoomInventory_Click(object sender, EventArgs e)
        {
            _isInventoryView = true;
            UpdateView();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            using (var form = new ManageRoomForm(_dataService))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadInventoryData();
                }
            }
        }

        private void dgvRoomInventory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            
            string roomNum = dgvRoomInventory.Rows[e.RowIndex].Cells["RoomNumber"].Value.ToString();
            using (var form = new ManageRoomForm(_dataService, roomNum))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadInventoryData();
                }
            }
        }

        private void LoadRoomData()
        {
            // Clear existing rows and dispose of any images to avoid memory leaks
            foreach (DataGridViewRow row in dgvRoomRates.Rows)
            {
                if (row.Cells[0].Value is Image img)
                {
                    img.Dispose();
                }
            }
            dgvRoomRates.Rows.Clear();

            try
            {
                using (var conn = FP_CS26_2025.Data.DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // Join RoomTypes with Rooms to get availability status
                    // Note: Since RoomTypes is the primary category, we aggregate status from Rooms
                    string query = @"
                        SELECT 
                            rt.TypeName as RoomType, 
                            rt.BasePrice as PricePerNight, 
                            rt.Capacity as CapacityInfo,
                            rt.ImageFilename,
                            (SELECT COUNT(*) FROM Rooms r WHERE r.RoomTypeID = rt.RoomTypeID AND r.Status = 'Available') as AvailableCount
                        FROM RoomTypes rt";

                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        query += " WHERE rt.TypeName LIKE @Search";
                    }

                    using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                        {
                            cmd.Parameters.AddWithValue("@Search", "%" + txtSearch.Text.Trim() + "%");
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string roomType = reader["RoomType"].ToString();
                                string imgName = reader["ImageFilename"] != DBNull.Value ? reader["ImageFilename"].ToString() : roomType;
                                Image roomImg = GetRoomImage(imgName);
                                int avail = Convert.ToInt32(reader["AvailableCount"]);
                                string status = $"Available: {avail}";
                                string capacity = reader["CapacityInfo"].ToString() + " Persons";

                                dgvRoomRates.Rows.Add(
                                    roomImg,
                                    roomType,
                                    reader["PricePerNight"].ToString(),
                                    status,
                                    capacity
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image GetRoomImage(string roomName)
        {
            try
            {
                string[] extensions = { ".png", ".jpg", ".jpeg" };
                foreach (var ext in extensions)
                {
                    string path = Path.Combine(_baseImagePath, roomName + ext);
                    if (File.Exists(path))
                    {
                        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            return Image.FromStream(stream);
                        }
                    }
                }
            }
            catch
            {
                // Return null if image not found or error loading
            }
            return null;
        }

        private void btnChangePrice_Click(object sender, EventArgs e)
        {
            if (dgvRoomRates.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvRoomRates.SelectedRows[0];
            string roomType = selectedRow.Cells[1].Value.ToString();

            using (EditRoomForm dialog = new EditRoomForm(roomType))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadRoomData();
                }
            }
        }

        private bool UpdateRoomPrice(string roomType, string newPrice)
        {
            try
            {
                using (var conn = FP_CS26_2025.Data.DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE RoomTypes SET BasePrice = @Price WHERE TypeName = @RoomType";
                    using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Price", newPrice);
                        cmd.Parameters.AddWithValue("@RoomType", roomType);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating price: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            RefreshCurrentView();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshCurrentView();
        }
        private string GetFloorOrdinal(int floor)
        {
            if (floor <= 0) return floor.ToString();
            int lastDigit = floor % 10;
            int lastTwoDigits = floor % 100;
            string suffix = "th";
            if (lastTwoDigits < 11 || lastTwoDigits > 13)
            {
                switch (lastDigit)
                {
                    case 1: suffix = "st"; break;
                    case 2: suffix = "nd"; break;
                    case 3: suffix = "rd"; break;
                }
            }
            return $"{floor}{suffix} Floor";
        }
    }
}
