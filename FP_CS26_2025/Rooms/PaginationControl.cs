using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_CS26_2025.Rooms
{
    public partial class PaginationControl : UserControl
    {
        private int _totalPages = 1;
        private int _currentPage = 1;

        public event EventHandler<int> PageChanged;

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                CreatePageButtons();
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                UpdateSelection();
            }
        }

        public PaginationControl()
        {
            InitializeComponent();
            this.Height = 40;
        }

        private void CreatePageButtons()
        {
            this.flowLayoutPanel1.Controls.Clear();
            for (int i = 1; i <= _totalPages; i++)
            {
                var btn = new Label
                {
                    Text = i.ToString(),
                    AutoSize = false,
                    Size = new Size(30, 30),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand,
                    Font = new Font("Segoe UI Semibold", 10F),
                    ForeColor = Color.Goldenrod,
                    Tag = i,
                    Margin = new Padding(5, 0, 5, 0)
                };
                btn.Click += PageButton_Click;
                this.flowLayoutPanel1.Controls.Add(btn);
            }
            UpdateSelection();
            CenterFlowPanel();
        }

        private void CenterFlowPanel()
        {
            if (this.flowLayoutPanel1 != null)
            {
                this.flowLayoutPanel1.Location = new Point(
                    (this.Width - this.flowLayoutPanel1.Width) / 2,
                    (this.Height - this.flowLayoutPanel1.Height) / 2
                );
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterFlowPanel();
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            if (sender is Label lbl && lbl.Tag is int page)
            {
                CurrentPage = page;
                PageChanged?.Invoke(this, page);
            }
        }

        private void UpdateSelection()
        {
            foreach (Control ctrl in this.flowLayoutPanel1.Controls)
            {
                if (ctrl is Label lbl)
                {
                    int page = (int)lbl.Tag;
                    if (page == _currentPage)
                    {
                        lbl.Font = new Font("Segoe UI Bold", 11F, FontStyle.Underline);
                        lbl.ForeColor = Color.Peru;
                    }
                    else
                    {
                        lbl.Font = new Font("Segoe UI Semibold", 10F);
                        lbl.ForeColor = Color.Goldenrod;
                    }
                }
            }
        }
    }
}
