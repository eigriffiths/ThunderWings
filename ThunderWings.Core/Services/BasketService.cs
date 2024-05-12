using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThunderWings.Core.Services.Interfaces;
using ThunderWings.Repo.DAL;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddItemToBasket(int aircraftId, int quantity)
        {
            var basket = GetBasket();

            BasketItem item = new BasketItem
            {
                AircraftId = aircraftId,
                Quantity = quantity
            };
            basket.Items.Add(item);

            SaveBasket(basket);
        }

        public void RemoveItemFromBasket(int aircraftId)
        {
            var basket = GetBasket();
            var basketItem = basket.Items.FirstOrDefault(bi => bi.AircraftId == aircraftId);

            if (basketItem != null)
            {
                basket.Items.Remove(basketItem);
                SaveBasket(basket);
            }
        }

        private Basket GetBasket()
        {
            var basket = new Basket();

            if (_httpContextAccessor.HttpContext.Session.TryGetValue("basket", out byte[] bytes))
            {
                var json = Encoding.UTF8.GetString(bytes);
                return JsonSerializer.Deserialize<Basket>(json);
            }

            return basket;
        }

        private void SaveBasket(Basket basket)
        {
            var json = JsonSerializer.Serialize(basket);
            var bytes = Encoding.UTF8.GetBytes(json);

            _httpContextAccessor.HttpContext.Session.Set("basket", bytes);
        }

    }
}
