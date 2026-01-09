using System.Collections.Generic;
using FP_CS26_2025.Services.Models;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Abstraction: Defines the contract for Promo Code operations.
    /// </summary>
    public interface IPromoService
    {
        PromoCode GetPromoCode(string code);
        IEnumerable<PromoCode> GetAllPromos();
        bool AddPromoCode(PromoCode promo);
        bool DeletePromoCode(int promoId);
        bool TogglePromoStatus(int promoId, bool isActive);
    }
}
