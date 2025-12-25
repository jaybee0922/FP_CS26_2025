using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class RoomsCalendarPanel : BaseFrontDeskPanel
    {
        public RoomsCalendarPanel() : base() { InitializeComponents(); }

        public RoomsCalendarPanel(FrontDeskController controller) : base(controller, "Room Status")
        {
            InitializeComponents();
            if (controller != null) LoadRooms();
        }

        private void InitializeComponents()
        {
            // Initial component setup if needed
        }

        private void LoadRooms()
        {
            var dgvRooms = new DataGridView
            {
                Location = new Point(30, 80),
                Size = new Size(600, 400),
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = _controller.GetAllRooms().Select(r => new {
                    Room = r.RoomNumber,
                    Type = r.RoomType,
                    Price = r.BasePrice,
                    Status = r.Status.ToString()
                }).ToList()
            };
            this.Controls.Add(dgvRooms);
        }
    }
}
