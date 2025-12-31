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
        private List<SystemUser> _filteredUsers;

        public UserManagementControl()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            SetupSearchBox();
            RefreshList();
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
            if (filterComboBox.SelectedIndex == 1) filterType = "Manager";
            if (filterComboBox.SelectedIndex == 2) filterType = "Front Desk";

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
            using (var dialog = new UserAddDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Create new User based on Role
                    string email = $"{dialog.Username.ToLower()}@hotel.com";
                    SystemUser newUser = null;

                    // Generate new Employee ID
                    string newId = _dataManager.GetNextEmployeeId();

                    if (dialog.Role == "Manager")
                    {
                        newUser = new ManagerUser(
                            dialog.FirstName,
                            dialog.MiddleName,
                            dialog.LastName,
                            dialog.Username,
                            email,
                            dialog.Password,
                            dialog.Birthday,
                            newId
                        );
                    }
                    else
                    {
                        newUser = new FrontDeskUser(
                           dialog.FirstName,
                           dialog.MiddleName,
                           dialog.LastName,
                           dialog.Username,
                           email,
                           dialog.Password,
                           dialog.Birthday,
                           newId
                       );
                    }

                    if (newUser != null)
                    {
                        newUser.LastUpdated = DateTime.Now; // Set initial update time
                        _dataManager.AddUser(newUser);
                        RefreshList();
                    }
                }
            }
        }

        private void EditUser(SystemUser user)
        {
            using (var dialog = new UserEditDialog(user))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Update properties
                    user.FirstName = dialog.FirstName;
                    user.MiddleName = dialog.MiddleName;
                    user.LastName = dialog.LastName;
                    user.Username = dialog.Username;
                    user.Birthday = dialog.Birthday;
                    user.LastUpdated = DateTime.Now; // Update timestamp

                    if (!string.IsNullOrEmpty(dialog.Password))
                    {
                        user.Password = dialog.Password;
                    }

                    // Handle Role Change
                    // If role changed, we might need to recreate the user object with new type
                    // But for simplicity in this implementation, if we want to support type changing, 
                    // we should remove old user and add new user of correct type.

                    bool roleChanged = false;
                    if (dialog.Role == "Manager" && !(user is ManagerUser)) roleChanged = true;
                    if (dialog.Role == "Front Desk" && !(user is FrontDeskUser)) roleChanged = true;

                    if (roleChanged)
                    {
                        _dataManager.RemoveUser(user.Id);

                        SystemUser newUser = null;
                        if (dialog.Role == "Manager")
                        {
                            newUser = new ManagerUser(user.FirstName, user.MiddleName, user.LastName, user.Username, user.Email, user.Password, user.Birthday, user.EmployeeId);
                        }
                        else
                        {
                            newUser = new FrontDeskUser(user.FirstName, user.MiddleName, user.LastName, user.Username, user.Email, user.Password, user.Birthday, user.EmployeeId);
                        }
                        newUser.LastUpdated = DateTime.Now; // Update timestamp on role change too
                                                            // Preserve ID or other metadata if needed, but for now new ID is fine or we can add constructor to accept ID
                        _dataManager.AddUser(newUser);
                    }
                    else
                    {
                        _dataManager.UpdateUser(user);
                    }

                    RefreshList();
                }
            }
        }

        private void DeleteUser(SystemUser user)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _dataManager.RemoveUser(user.Id);
            }
        }
    }
}
