using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public partial class UserManagementControl : UserControl
    {
        private DataManager _dataManager;
        private int _currentPage = 1;
        private const int ItemsPerPage = 10;
        private List<SystemUser> _allUsers;
        private List<SystemUser> _filteredUsers;

        public UserManagementControl()
        {
            InitializeComponent();
            SetupSearchBox();
        }

        private void SetupSearchBox()
        {
            searchTextBox.Text = "Search users...";
            searchTextBox.ForeColor = System.Drawing.Color.Gray;

            searchTextBox.GotFocus += (s, e) => 
            {
                if (searchTextBox.Text == "Search users...")
                {
                    searchTextBox.Text = "";
                    searchTextBox.ForeColor = System.Drawing.Color.Black;
                }
            };

            searchTextBox.LostFocus += (s, e) => 
            {
                if (string.IsNullOrWhiteSpace(searchTextBox.Text))
                {
                    searchTextBox.Text = "Search users...";
                    searchTextBox.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        public void SetDataManager(DataManager manager)
        {
            _dataManager = manager;
            _dataManager.DataChanged += (s, e) => RefreshList();
            RefreshList();
        }

        private void RefreshList()
        {
            if (_dataManager == null) return;

            string filterType = "All";
            if (filterComboBox.SelectedIndex == 1) filterType = "Admin";
            if (filterComboBox.SelectedIndex == 2) filterType = "User";

            string sortOrder = "A-Z"; // Default for now, can add toggle

            string searchText = searchTextBox.Text == "Search users..." ? "" : searchTextBox.Text;
            _filteredUsers = _dataManager.FilterUsers(filterType, searchText, sortOrder);
            
            RenderPage();
        }

        private void RenderPage()
        {
            usersFlowPanel.Controls.Clear();
            
            if (_filteredUsers == null) return;

            int totalPages = (int)Math.Ceiling((double)_filteredUsers.Count / ItemsPerPage);
            if (totalPages == 0) totalPages = 1;
            
            if (_currentPage > totalPages) _currentPage = totalPages;
            if (_currentPage < 1) _currentPage = 1;

            pageLabel.Text = $"Page {_currentPage} of {totalPages}";
            prevButton.Enabled = _currentPage > 1;
            nextButton.Enabled = _currentPage < totalPages;

            var pageUsers = _filteredUsers
                .Skip((_currentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToList();

            foreach (var user in pageUsers)
            {
                var item = new UserListItemControl(user);
                item.EditClicked += (s, e) => EditUser(user);
                item.DeleteClicked += (s, e) => DeleteUser(user);
                usersFlowPanel.Controls.Add(item);
            }
        }

        private void ChangePage(int delta)
        {
            _currentPage += delta;
            RenderPage();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            RefreshList();
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            RefreshList();
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            // Simple input dialog or simulated replacement for now
            string newName = "New User " + new Random().Next(100);
            _dataManager.AddUser(new GeneralUser(newName, $"{newName.Replace(" ","")}@test.com", "password"));
        }

        private void EditUser(SystemUser user)
        {
             // For simulation, just rename slightly
             user.Username += " (Edited)";
             _dataManager.UpdateUser(user);
        }

        private void DeleteUser(SystemUser user)
        {
            if (MessageBox.Show($"Delete {user.Username}?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _dataManager.RemoveUser(user.Id);
            }
        }
    }
}
