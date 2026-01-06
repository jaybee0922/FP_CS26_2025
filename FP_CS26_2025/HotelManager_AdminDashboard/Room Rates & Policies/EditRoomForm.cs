using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.Room_Rates___Policies
{
    public partial class EditRoomForm : Form
    {
        private string _originalRoomTypeName;
        private int _roomTypeId;

        public EditRoomForm(string roomTypeName)
        {
            InitializeComponent();
            _originalRoomTypeName = roomTypeName;
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT RoomTypeID, TypeName, BasePrice, Capacity, Description 
                        FROM RoomTypes 
                        WHERE TypeName = @TypeName";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TypeName", _originalRoomTypeName);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _roomTypeId = reader.GetInt32(0);
                                txtRoomType.Text = reader["TypeName"].ToString();
                                txtPrice.Text = reader["BasePrice"].ToString();
                                numCapacity.Value = Convert.ToDecimal(reader["Capacity"]);
                                txtDescription.Text = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "";
                            }
                        }
                    }

                    // Get Current Quantity
                    string countQuery = "SELECT COUNT(*) FROM Rooms WHERE RoomTypeID = @TypeId";
                    using (var cmdCount = new SqlCommand(countQuery, conn))
                    {
                        cmdCount.Parameters.AddWithValue("@TypeId", _roomTypeId);
                        int count = (int)cmdCount.ExecuteScalar();
                        numQuantity.Value = count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading room details: " + ex.Message);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomType.Text))
            {
                MessageBox.Show("Room Type Name cannot be empty.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid Price.");
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();


                    // 1. Update RoomTypes
                    // Check for duplicate name if changed
                    if (txtRoomType.Text != _originalRoomTypeName)
                    {
                         string check = "SELECT Count(*) FROM RoomTypes WHERE TypeName = @NewName";
                         using (var cmdCheck = new SqlCommand(check, conn))
                         {
                             cmdCheck.Parameters.AddWithValue("@NewName", txtRoomType.Text);
                             if ((int)cmdCheck.ExecuteScalar() > 0)
                             {
                                 MessageBox.Show("Room Name already exists!");
                                 return;
                             }
                         }
                    }

                    string updateQuery = @"
                        UPDATE RoomTypes 
                        SET TypeName = @Name, BasePrice = @Price, Capacity = @Capacity, Description = @Desc 
                        WHERE RoomTypeID = @Id";

                    using (var cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtRoomType.Text);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Capacity", (int)numCapacity.Value);
                        cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@Id", _roomTypeId);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Adjust Quantity
                    int targetCount = (int)numQuantity.Value;
                    string countQuery = "SELECT COUNT(*) FROM Rooms WHERE RoomTypeID = @TypeId";
                    int currentCount;
                    using (var cmdCount = new SqlCommand(countQuery, conn)) {
                         cmdCount.Parameters.AddWithValue("@TypeId", _roomTypeId);
                         currentCount = (int)cmdCount.ExecuteScalar();
                    }

                    if (targetCount > currentCount)
                    {
                        AddRooms(conn, targetCount - currentCount);
                    }
                    else if (targetCount < currentCount)
                    {
                        RemoveRooms(conn, currentCount - targetCount);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message);
            }
        }

        private void AddRooms(SqlConnection conn, int countToAdd)
        {
            // Determine default floor from existing rooms
            int floor = 1;
            using (var cmd = new SqlCommand("SELECT TOP 1 Floor FROM Rooms WHERE RoomTypeID = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", _roomTypeId);
                var res = cmd.ExecuteScalar();
                if (res != null && res != DBNull.Value) floor = (int)res;
            }

            int added = 0;
            // Try to find numbers
            // Simple strategy: Check numbers from 101 to 999.
            for (int i = 1; i <= 99; i++) // Try to fit on same floor first
            {
                if (added >= countToAdd) break;
                string roomNum = $"{floor}{i:D2}";
                if (IsRoomNumberAvailable(conn, roomNum))
                {
                    InsertRoom(conn, roomNum, floor);
                    added++;
                }
            }
            
            // If still need rooms, go to next floors
            int searchFloor = 1;
            while (added < countToAdd && searchFloor <= 10)
            {
                for (int i = 1; i <= 20; i++)
                {
                     if (added >= countToAdd) break;
                     string roomNum = $"{searchFloor}{i:D2}";
                     if (IsRoomNumberAvailable(conn, roomNum))
                     {
                         InsertRoom(conn, roomNum, searchFloor);
                         added++;
                     }
                }
                searchFloor++;
            }
        }

        private bool IsRoomNumberAvailable(SqlConnection conn, string roomNum)
        {
            using (var cmd = new SqlCommand("SELECT Count(*) FROM Rooms WHERE RoomNumber = @Num", conn))
            {
                cmd.Parameters.AddWithValue("@Num", roomNum);
                return (int)cmd.ExecuteScalar() == 0;
            }
        }

        private void InsertRoom(SqlConnection conn, string roomNum, int floor)
        {
            string query = "INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES (@Num, @Id, @Floor, 'Available')";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Num", roomNum);
                cmd.Parameters.AddWithValue("@Id", _roomTypeId);
                cmd.Parameters.AddWithValue("@Floor", floor);
                cmd.ExecuteNonQuery();
            }
        }

        private void RemoveRooms(SqlConnection conn, int countToRemove)
        {
             // Remove 'Available' rooms only, preferably highest numbers first
             string query = @"
                DELETE TOP (@Count) FROM Rooms 
                WHERE RoomTypeID = @Id AND Status = 'Available'";
             
             using (var cmd = new SqlCommand(query, conn))
             {
                 cmd.Parameters.AddWithValue("@Count", countToRemove);
                 cmd.Parameters.AddWithValue("@Id", _roomTypeId);
                 int deleted = cmd.ExecuteNonQuery();
                 
                 if (deleted < countToRemove)
                 {
                     MessageBox.Show($"Could only remove {deleted} rooms. Others may be occupied or reserved.");
                 }
             }
        }
    }
}
