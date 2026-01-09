using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.Rooms;
using FP_CS26_2025.Services;
using FP_CS26_2025.Services.Models;
using FP_CS26_2025.Services.Validation;

namespace FP_CS26_2025.ModernDesign
{
    /// <summary>
    /// Modern booking modal following OOP and SOLID principles.
    /// Implements robust exception handling and precise date/financial calculations.
    /// </summary>
    public partial class BookingModalForm : Form
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private List<IRoom> _allRooms;
        private int _stayNights;
        private DateTime _checkIn;
        private DateTime _checkOut;
        private string _promoCode;
        
        // Property Encapsulation
        public BookingResultData BookingResult { get; private set; }

        // Dependency Inversion: Injecting services and stay context
        private readonly IValidator<BookingRequestData> _validator;
        private readonly string _preSelectedRoom;

        public BookingModalForm(IRoomService roomService, IBookingService bookingService, DateTime checkIn, DateTime checkOut, string promoCode = null, string preSelectedRoom = null)
        {
            InitializeComponent();
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            _validator = new BookingValidator(); 
            _promoCode = promoCode;
            _preSelectedRoom = preSelectedRoom;

            // Precision Date Handling
            _checkIn = checkIn.Date;
            _checkOut = checkOut.Date;
            _stayNights = Math.Max(1, (_checkOut - _checkIn).Days);
            
            // UI Event Hooks
            this.btnBookNow.Click += BtnBookNow_Click;
            this.cmbRoomType.SelectedIndexChanged += (s, e) => UpdateLiveTotalSafely();
            this.numRooms.ValueChanged += (s, e) => UpdateLiveTotalSafely();
            
            this.Load += (s, e) => LoadRoomsDataSafely();

            // Input Restrictions (Real-time Validation)
            SetupInputRestrictions();
        }

        private void SetupInputRestrictions()
        {
            // First/Last Name: Letters, Control keys, and Space only.
            this.txtFirstName.KeyPress += (s, e) => RestrictToLetters(e);
            this.txtLastName.KeyPress += (s, e) => RestrictToLetters(e);

            // Phone: Numbers and Control keys only.
            this.txtPhone.MaxLength = 11;
            this.txtPhone.KeyPress += (s, e) => RestrictToNumbers(e);
        }

        private void RestrictToLetters(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void RestrictToNumbers(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LoadRoomsDataSafely()
        {
            try
            {
                _allRooms = _roomService.GetAllRooms()?.ToList() ?? new List<IRoom>();
                cmbRoomType.Items.Clear();
                
                foreach (var room in _allRooms)
                {
                    cmbRoomType.Items.Add(room.Name);
                }

                if (cmbRoomType.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(_preSelectedRoom) && cmbRoomType.Items.Contains(_preSelectedRoom))
                    {
                        cmbRoomType.SelectedItem = _preSelectedRoom;
                        cmbRoomType.Enabled = false; // Lock selection
                    }
                    else
                    {
                        cmbRoomType.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Robustness: No crashes allowed
                MessageBox.Show("Security Alert: System failed to load room data safely.\n" + ex.Message, 
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLiveTotalSafely()
        {
            try
            {
                if (cmbRoomType.SelectedIndex < 0) return;

                var selectedRoomName = cmbRoomType.SelectedItem.ToString();
                var selectedRoom = _allRooms.FirstOrDefault(r => r.Name == selectedRoomName);

                if (selectedRoom != null)
                {
                    UpdateRoomDetailsUI(selectedRoom);

                    // Financial Accuracy: Using precise types
                    // Financial Accuracy: Using precise service-layer calculation
                    int roomCount = (int)numRooms.Value;
                    decimal total = _bookingService.CalculateTotal(selectedRoomName, roomCount, _stayNights, _promoCode);

                    lblPricePerNight.Text = $"Price: P{selectedRoom.Price:N2}/nt";
                    lblStayDuration.Text = $"Stay Duration: {_stayNights} night(s)";
                    
                    if (!string.IsNullOrEmpty(_promoCode))
                    {
                        decimal originalTotal = selectedRoom.Price * roomCount * _stayNights;
                        decimal discount = originalTotal - total;
                        
                        lblLiveTotal.Text = $"Est. Total: P{total:N2}";

                        if (discount > 0)
                        {
                            lblSavings.Text = $"(Saved P{discount:N2}!)";
                            lblSavings.Visible = true;
                            // Dynamic positioning relative to the total label
                            lblSavings.Left = lblLiveTotal.Right + 10;
                            lblLiveTotal.ForeColor = Color.ForestGreen;
                        }
                        else
                        {
                            lblSavings.Visible = false;
                            lblLiveTotal.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        lblLiveTotal.Text = $"Est. Total: P{total:N2}";
                        lblLiveTotal.ForeColor = Color.Black;
                        lblSavings.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                // Silently fail or log for live updates to prevent annoying popups during typing/selection
                lblLiveTotal.Text = "Calculation Error";
            }
        }

        private void UpdateRoomDetailsUI(IRoom room)
        {
            lblRoomDesc.Text = room.Description;
            
            // Dynamic Amenities Update
            if (!string.IsNullOrEmpty(room.Amenities))
            {
                lblRoomAmenities.Text = $"Amenities:\n{room.Amenities}";
                lblRoomAmenities.Visible = true;
            }
            else
            {
                lblRoomAmenities.Visible = false;
            }
            
            if (!string.IsNullOrEmpty(room.ImagePath) && File.Exists(room.ImagePath))
            {
                try
                {
                    picRoom.Image?.Dispose();
                    picRoom.Image = Image.FromFile(room.ImagePath);
                }
                catch
                {
                    picRoom.Image = null; // Robust fallback
                }
            }
            else
            {
                picRoom.Image = null;
            }
        }

        private void BtnBookNow_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    ConfirmBookingRequested();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Security Failure: An error occurred while processing your booking.\n" + ex.Message,
                    "Process Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            // Construct a temporary DTO for validation purposes
            var tempBookingData = new BookingRequestData
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };

            if (!_validator.Validate(tempBookingData, out List<string> errors))
            {
                string errorMessage = string.Join("\n", errors);
                MessageBox.Show("Validation Error:\n" + errorMessage, "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Additional Contextual Validation (Capacity)
            var selectedRoomName = cmbRoomType.SelectedItem.ToString();
            var selectedRoom = _allRooms.FirstOrDefault(r => r.Name == selectedRoomName);
            if (selectedRoom != null)
            {
                int totalGuests = (int)numAdults.Value + (int)numChildren.Value;
                if (totalGuests > selectedRoom.Capacity)
                {
                    MessageBox.Show($"Capacity Error: The '{selectedRoomName}' only allows up to {selectedRoom.Capacity} guests.\n" +
                                    $"You have entered {totalGuests} guests (Adults: {numAdults.Value}, Children: {numChildren.Value}).",
                                    "Capacity Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void ConfirmBookingRequested()
        {
            var selectedRoomName = cmbRoomType.SelectedItem.ToString();
            
            // Precision & Security: Double check availability one last time before allowing OK
            if (!_bookingService.CheckAvailability(_checkIn, _checkOut, selectedRoomName, (int)numRooms.Value))
            {
                MessageBox.Show($"Availability Error: Unfortunately, the '{selectedRoomName}' is no longer available for your selected dates.", 
                    "No Availability", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRoom = _allRooms.First(r => r.Name == selectedRoomName);
            decimal finalPrice = _bookingService.CalculateTotal(selectedRoomName, (int)numRooms.Value, _stayNights, _promoCode);
            decimal discountApplied = (selectedRoom.Price * (int)numRooms.Value * _stayNights) - finalPrice;

            // Encapsulation: Creating the DTO securely
            BookingResult = new BookingResultData
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim().ToLower(),
                Phone = txtPhone.Text.Trim(),
                NumAdults = (int)numAdults.Value,
                NumChildren = (int)numChildren.Value,
                NumRooms = (int)numRooms.Value,
                RoomType = selectedRoomName,
                CheckInDate = _checkIn,
                CheckOutDate = _checkOut,
                TotalPrice = finalPrice,
                PromoCode = string.IsNullOrEmpty(_promoCode) ? null : _promoCode.ToUpper(),
                DiscountAmount = discountApplied
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    public class BookingResultData : BookingRequestData
    {
    }
}
