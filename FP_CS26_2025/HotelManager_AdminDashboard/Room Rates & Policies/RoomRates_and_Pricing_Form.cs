using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_CS26_2025.Room_Rates___Policies
{
    public partial class RoomRates_and_Pricing_Form : Form
    {
        public RoomRates_and_Pricing_Form()
        {
            InitializeComponent();
        }

        private void RoomRates_and_Pricing_Form_Load(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            dgvRoomRates.Rows.Clear();

            try
            {
                using (var conn = FP_CS26_2025.Data.DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT RoomType, PricePerNight, Status, CapacityInfo FROM RoomRates";

                    // Add Filter if search text is not empty
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        query += " WHERE RoomType LIKE @Search";
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
                                dgvRoomRates.Rows.Add(
                                    reader["RoomType"].ToString(),
                                    reader["PricePerNight"].ToString(),
                                    reader["Status"].ToString(),
                                    reader["CapacityInfo"].ToString()
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

        private void btnChangePrice_Click(object sender, EventArgs e)
        {
            if (dgvRoomRates.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvRoomRates.SelectedRows[0];
            string roomType = selectedRow.Cells[0].Value.ToString();
            string priceOld = selectedRow.Cells[1].Value.ToString();

            PriceInputDialog dialog = new PriceInputDialog(roomType, priceOld);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.NewPrice))
                {
                    // Update Database
                    if (UpdateRoomPrice(roomType, dialog.NewPrice))
                    {
                        MessageBox.Show("Pricing updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRoomData(); // Refresh grid
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
                    string query = "UPDATE RoomRates SET PricePerNight = @Price WHERE RoomType = @RoomType";
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

        // Search functionality
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
            LoadRoomData();
        }
    }
}
