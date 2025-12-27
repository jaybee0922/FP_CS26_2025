using System;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025.Rooms
{
    public partial class RoomCard : UserControl
    {
        private IRoom _room;

        public RoomCard()
        {
            InitializeComponent();
            SetupModernDesign();
        }

        public void SetRoom(IRoom room)
        {
            _room = room;
            _lblName.Text = _room.Name;
            _lblCategory.Text = _room.Category;
            _lblDescription.Text = _room.Description;
            _btnLearnMore.Text = "LEARN MORE";

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
        }
    }
}
