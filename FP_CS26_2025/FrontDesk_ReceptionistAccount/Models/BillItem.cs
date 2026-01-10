namespace FP_CS26_2025.FrontDesk_MVC
{
    /// <summary>
    /// Represents an itemized charge in a guest's bill.
    /// Supports additional services beyond room charges (e.g., Mini-bar, Late Checkout).
    /// </summary>
    public class BillItem
    {
        public int ItemId { get; set; }
        public string ReservationId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public BillItem(string reservationId, string description, int quantity, decimal unitPrice)
        {
            ReservationId = reservationId;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
        }

        public BillItem()
        {
            Quantity = 1;
        }
    }
}
