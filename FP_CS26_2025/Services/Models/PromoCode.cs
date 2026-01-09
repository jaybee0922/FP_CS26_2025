using System;

namespace FP_CS26_2025.Services.Models
{
    /// <summary>
    /// Encapsulation: Represents a Promo Code with its rules.
    /// </summary>
    public class PromoCode
    {
        public int PromoID { get; set; }
        public string Code { get; set; }
        public string DiscountType { get; set; } // 'Percentage' or 'Fixed'
        public decimal DiscountValue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public bool IsValid()
        {
            return IsActive && ExpiryDate.Date >= DateTime.Today;
        }

        public decimal CalculateDiscount(decimal originalPrice)
        {
            if (!IsValid()) return 0;

            decimal discount = 0;
            if (DiscountType.Equals("Percentage", StringComparison.OrdinalIgnoreCase))
            {
                discount = originalPrice * (DiscountValue / 100m);
            }
            else if (DiscountType.Equals("Fixed", StringComparison.OrdinalIgnoreCase))
            {
                discount = DiscountValue;
            }

            // Robustness: Discount should not exceed the original price (preventing negative totals)
            return Math.Min(discount, originalPrice);
        }
    }
}
