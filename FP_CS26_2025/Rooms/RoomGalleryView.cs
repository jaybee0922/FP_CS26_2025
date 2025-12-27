using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FP_CS26_2025.Rooms
{
    public partial class RoomGalleryView : UserControl
    {
        private readonly IRoomService _roomService;
        private int _currentPage = 1;
        private const int PageSize = 3; 

        public RoomGalleryView()
        {
            InitializeComponent();
            _roomService = new RoomService();
            LoadData();
        }

        private void LoadData()
        {
            _currentPage = 1;
            this.paginationControl1.TotalPages = _roomService.GetTotalPages(PageSize);
            this.paginationControl1.CurrentPage = _currentPage;
            DisplayRooms();
        }

        private void DisplayRooms()
        {
            this.roomTableLayoutPanel.Controls.Clear();
            var rooms = _roomService.GetRoomsPage(_currentPage, PageSize);

            int column = 0;
            foreach (var room in rooms)
            {
                var card = new RoomCard();
                card.SetRoom(room);
                card.Anchor = AnchorStyles.None; // Center within table cell
                this.roomTableLayoutPanel.Controls.Add(card, column++, 0);
            }
        }

        private void paginationControl1_PageChanged(object sender, int page)
        {
            _currentPage = page;
            DisplayRooms();
        }
    }
}
