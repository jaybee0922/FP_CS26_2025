using System;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FP_CS26_2025.HotelManager_AdminDashboard;

namespace FP_CS26_2025.Room_Rates___Policies
{
    public partial class RoomRatesControl : UserControl
    {
        private DataManager _dataManager;
        private readonly string _baseImagePath;

        public RoomRatesControl()
        {
            InitializeComponent();
            _baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Assets", "IMAGES");
        }

        public void SetDataManager(DataManager manager)
        {
            _dataManager = manager;
            LoadRoomData();
        }

        private void RoomRatesControl_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                LoadRoomData();
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
                            (SELECT TOP 1 Status FROM Rooms WHERE RoomTypeID = rt.RoomTypeID) as Status
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
                                string roomType = reader["RoomType"].ToString() ;
                                Image roomImg = GetRoomImage(roomType);
                                string status = reader["Status"]?.ToString() ?? "N/A";
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
            string priceOld = selectedRow.Cells[2].Value.ToString();

            using (PriceInputDialog dialog = new PriceInputDialog(roomType, priceOld))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(dialog.NewPrice))
                    {
                        if (UpdateRoomPrice(roomType, dialog.NewPrice))
                        {
                            MessageBox.Show("Pricing updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRoomData();
                        }
                    }
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
            LoadRoomData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRoomData();
        }
    }
}
