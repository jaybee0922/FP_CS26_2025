using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.Services;
using FP_CS26_2025.Services.Models;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public partial class PromoManagementControl : UserControl
    {
        private readonly IPromoService _promoService;

        public PromoManagementControl()
        {
            InitializeComponent();
            _promoService = new PromoService(); // Abstraction: Using interface
            LoadPromos();
            ResetForm();
        }

        private void LoadPromos()
        {
            try
            {
                var promos = _promoService.GetAllPromos().ToList();
                dgvPromos.DataSource = null;
                dgvPromos.DataSource = promos;
                
                // Formatting
                if (dgvPromos.Columns["PromoID"] != null) dgvPromos.Columns["PromoID"].Visible = false;
                dgvPromos.Columns["Code"].HeaderText = "PROMO CODE";
                dgvPromos.Columns["DiscountType"].HeaderText = "TYPE";
                dgvPromos.Columns["DiscountValue"].HeaderText = "VALUE";
                dgvPromos.Columns["ExpiryDate"].HeaderText = "EXPIRY";
                dgvPromos.Columns["IsActive"].HeaderText = "ACTIVE";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load promos: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtCode.Text.Trim();
                string type = cmbType.SelectedItem?.ToString();
                decimal value = numValue.Value;
                DateTime expiry = dtpExpiry.Value;

                if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(type))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newPromo = new PromoCode
                {
                    Code = code,
                    DiscountType = type,
                    DiscountValue = value,
                    ExpiryDate = expiry,
                    IsActive = true
                };

                if (_promoService.AddPromoCode(newPromo))
                {
                    MessageBox.Show("Promo code generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPromos();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Failed to save promo code. Code might already exist.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving promo: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPromos.SelectedRows.Count == 0) return;

            var result = MessageBox.Show("Are you sure you want to delete the selected promo code?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = (int)dgvPromos.SelectedRows[0].Cells["PromoID"].Value;
                    if (_promoService.DeletePromoCode(id))
                    {
                        LoadPromos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting promo: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResetForm()
        {
            txtCode.Clear();
            cmbType.SelectedIndex = 0;
            numValue.Value = 0;
            dtpExpiry.Value = DateTime.Today.AddMonths(1);
        }
    }
}
