using System;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025.Rooms
{
    public partial class RoomCard : UserControl
    {
        private IRoom _room;
        public event EventHandler<string> BookNowClicked;

        public RoomCard()
        {
            InitializeComponent();
            SetupModernDesign();
            AttachHoverEvents();
            
            // Wire up the button
            _btnLearnMore.Click += (s, e) => BookNowClicked?.Invoke(this, _room?.Name);
        }

        private void AttachHoverEvents()
        {
            foreach (Control control in new Control[] { this, _mainPanel, _roomImage, _lblName, _lblCategory, _lblDescription })
            {
                control.MouseEnter += (s, e) => OnMouseEnter();
                control.MouseLeave += (s, e) => OnMouseLeave();
            }
        }

        private void OnMouseEnter()
        {
            _mainPanel.ShadowColor = Color.FromArgb(180, 180, 180);
            _mainPanel.ShadowDepth = 8;
            _mainPanel.Invalidate();
        }

        private void OnMouseLeave()
        {
            _mainPanel.ShadowColor = Color.FromArgb(230, 230, 230);
            _mainPanel.ShadowDepth = 5;
            _mainPanel.Invalidate();
        }

        public void SetRoom(IRoom room)
        {
            _room = room;
            _lblName.Text = _room.Name;
            _lblCategory.Text = $"{_room.Category.ToUpper()} | FROM PHP {_room.Price:N0}/NIGHT";
            _lblDescription.Text = _room.Description;
            _btnLearnMore.Text = "BOOK NOW";

            if (!string.IsNullOrEmpty(_room.ImagePath) && System.IO.File.Exists(_room.ImagePath))
            {
                try {
                    _roomImage.Image = Image.FromFile(_room.ImagePath);
                } catch {
                    _roomImage.BackColor = Color.LightGray;
                }
            }
            else
            {
                _roomImage.BackColor = Color.LightGray;
            }
        }

        private void SetupModernDesign()
        {
            this.Size = new Size(300, 350);
            this.BackColor = Color.Transparent;
            _mainPanel.Cursor = Cursors.Hand;
        }
    }
}
