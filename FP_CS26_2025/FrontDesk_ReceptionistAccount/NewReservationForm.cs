using System;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025.FrontDesk_ReceptionistAccount
{
    public partial class NewReservationForm : Form
    {
        private readonly FrontDeskController _controller;
        
        public NewReservationForm(FrontDeskController controller)
        {
            InitializeComponent();
            _controller = controller;
            
            LoadRoomTypes();
            InitializeValidation();
            
            // Event Wiring
            cmbRoomType.SelectedIndexChanged += (s, e) => UpdateAvailableRooms();
            dtpCheckIn.ValueChanged += (s, e) => {
                if (dtpCheckOut.Value <= dtpCheckIn.Value)
                    dtpCheckOut.Value = dtpCheckIn.Value.AddDays(1);
                UpdateAvailableRooms();
            };
            dtpCheckOut.ValueChanged += (s, e) => {
                 if (dtpCheckOut.Value <= dtpCheckIn.Value)
                    dtpCheckIn.Value = dtpCheckOut.Value.AddDays(-1);
                 UpdateAvailableRooms();
            };
            
            // Recalculate price when any factor changes
            cmbRoomType.SelectedIndexChanged += (s, e) => UpdateTotalPrice();
            cmbRoomNumber.SelectedIndexChanged += (s, e) => UpdateTotalPrice();
            dtpCheckIn.ValueChanged += (s, e) => UpdateTotalPrice();
            dtpCheckOut.ValueChanged += (s, e) => UpdateTotalPrice();
            numRooms.ValueChanged += (s, e) => UpdateTotalPrice();
        }

        private void InitializeValidation()
        {
            dtpCheckIn.MinDate = DateTime.Today;
            dtpCheckOut.MinDate = DateTime.Today.AddDays(1);
        }

        private void LoadRoomTypes()
        {
            try
            {
                var types = _controller.GetRoomTypes();
                cmbRoomType.Items.Clear();
                foreach (var t in types)
                {
                    cmbRoomType.Items.Add(t);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading room types: " + ex.Message);
            }
        }

        private void UpdateAvailableRooms()
        {
            try
            {
                cmbRoomNumber.Items.Clear();
                lblTotal.Text = "Total: P 0.00";

                if (cmbRoomType.SelectedItem == null) return;

                string selectedType = cmbRoomType.SelectedItem.ToString();
                
                // Get rooms available for this specific date range and type
                var availableRooms = _controller.GetAvailableRoomsByDate(selectedType, dtpCheckIn.Value, dtpCheckOut.Value);

                foreach (var room in availableRooms)
                {
                    cmbRoomNumber.Items.Add(room.RoomNumber); // Assuming RoomNumber is int or readable string
                }

                if (cmbRoomNumber.Items.Count > 0)
                    cmbRoomNumber.SelectedIndex = 0;
                else
                    cmbRoomNumber.Text = ""; // Clear if none
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking availability: " + ex.Message);
            }
        }

        private void UpdateTotalPrice()
        {
            if (cmbRoomNumber.SelectedItem == null || cmbRoomType.SelectedItem == null)
            {
                lblTotal.Text = "Total: P 0.00";
                return;
            }

            try 
            {
                 // Get price from the first available room of this type (Standardized pricing per type assumed)
                 // or ideally fetch specific room price
                 int roomNum = Convert.ToInt32(cmbRoomNumber.SelectedItem);
                 var room = _controller.GetAvailableRooms().FirstOrDefault(r => r.RoomNumber == roomNum) 
                            ?? _controller.GetAllRooms().FirstOrDefault(r => r.RoomNumber == roomNum);
                 
                 if (room != null)
                 {
                     int nights = (dtpCheckOut.Value.Date - dtpCheckIn.Value.Date).Days;
                     if (nights < 1) nights = 1;

                     decimal total = room.BasePrice * nights * (int)numRooms.Value;
                     lblTotal.Text = $"Total: P {total:N2}";
                 }
            }
            catch {}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             if (!ValidateForm()) return;

             try
             {
                 var guest = new Guest
                 {
                     FullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}",
                     Email = txtEmail.Text.Trim(),
                     PhoneNumber = txtPhone.Text.Trim()
                 };

                 int roomNum = Convert.ToInt32(cmbRoomNumber.SelectedItem);
                 DateTime checkIn = dtpCheckIn.Value;
                 DateTime checkOut = dtpCheckOut.Value;

                 // Create Reservation via Controller
                 // Note: Controller will handle availability check again for safety
                 _controller.CreateReservation(guest, roomNum, checkIn, checkOut);
                 
                 MessageBox.Show("Reservation Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 this.DialogResult = DialogResult.OK;
                 this.Close();
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Failed to create reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text)) { ShowError("First Name is required."); return false; }
            if (string.IsNullOrWhiteSpace(txtLastName.Text)) { ShowError("Last Name is required."); return false; }
            if (cmbRoomType.SelectedItem == null) { ShowError("Please select a Room Type."); return false; }
            if (cmbRoomNumber.SelectedItem == null) { ShowError("Please select a Room Number."); return false; }
            
            return true;
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
