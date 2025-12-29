using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.Rooms;
using FP_CS26_2025.Services;

namespace FP_CS26_2025.ModernDesign
{
    public partial class BookingModalForm : Form
    {
        private readonly IRoomService _roomService;
        private List<Rooms.IRoom> _allRooms;
        public BookingRequestData BookingResult { get; private set; }

        public BookingModalForm()
        {
            InitializeComponent();
            _roomService = new RoomService();
            
            this.btnBookNow.Click += BtnBookNow_Click;
            this.cmbRoomType.SelectedIndexChanged += CmbRoomType_SelectedIndexChanged;
            
            LoadRooms();
        }

        private void LoadRooms()
        {
            try
            {
                _allRooms = _roomService.GetAllRooms().ToList();
                cmbRoomType.Items.Clear();
                
                foreach (var room in _allRooms)
                {
                    cmbRoomType.Items.Add(room.Name);
                }

                if (cmbRoomType.Items.Count > 0)
                {
                    cmbRoomType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rooms: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoomType.SelectedIndex >= 0)
            {
                var selectedRoomName = cmbRoomType.SelectedItem.ToString();
                var selectedRoom = _allRooms.FirstOrDefault(r => r.Name == selectedRoomName);

                if (selectedRoom != null)
                {
                    lblRoomDesc.Text = selectedRoom.Description;
                    
                    if (!string.IsNullOrEmpty(selectedRoom.ImagePath) && File.Exists(selectedRoom.ImagePath))
                    {
                        try
                        {
                            // Dispose previous image if exists
                            if (picRoom.Image != null)
                            {
                                picRoom.Image.Dispose();
                            }
                            picRoom.Image = Image.FromFile(selectedRoom.ImagePath);
                        }
                        catch
                        {
                            picRoom.Image = null; // Fallback
                        }
                    }
                    else
                    {
                        picRoom.Image = null; // No image
                    }
                }
            }
        }

        private void BtnBookNow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter your full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter your phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BookingResult = new BookingRequestData
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                NumAdults = (int)numAdults.Value,
                NumChildren = (int)numChildren.Value,
                NumRooms = (int)numRooms.Value,
                RoomType = cmbRoomType.SelectedItem?.ToString() ?? "Standard"
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
