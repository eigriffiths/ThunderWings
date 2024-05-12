using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderWings.Core.DTO.Receipt;
using ThunderWings.Core.Services.Interfaces;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IBasketService _basketService;

        public CheckoutService(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public Basket ValidateBasket(int basketId)
        {
            var basket = _basketService.GetPersistedBasket(basketId).Result;

            if (basket == null || !basket.Items.Any())
            {
                throw new Exception("This basket is empty or cannot be found.");
            }

            return basket;
        }

        public Basket CalculateBasketTotal(Basket basket)
        {
            basket.Total = basket.Items.Sum(item => item.Quantity * item.Price);

            return basket;
        }

        public ReceiptDto GenerateReceipt(Basket basket)
        {
            var receipt = new ReceiptDto
            {
                Description = "Thank you for your purchase.",
                Total = basket.Total
            };

            return receipt;
        }
    }
}
