using ThunderWings.Core.DTO.Receipt;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Services.Interfaces
{
    public interface ICheckoutService
    {
        Basket CalculateBasketTotal(Basket basket);
        ReceiptDto GenerateReceipt(Basket basket);
        Basket ValidateBasket(int basketId);
    }
}