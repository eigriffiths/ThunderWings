using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThunderWings.Core.DTO.Basket;
using ThunderWings.Core.Services.Interfaces;
using ThunderWings.Repo.DAL;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Services
{
    public class BasketService : GenericRepository<Basket>, IBasketService
    {
        private readonly IAircraftService _aircraftService;
        private readonly IBasketItemService _basketItemService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BasketService(ApplicationDbContext context, 
            IAircraftService aircraftService,
            IBasketItemService basketItemService,
            IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
            : base(context)
        {
            _aircraftService = aircraftService;
            _basketItemService = basketItemService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        // Task 4 AddItemToBasket using HttpContext.Session
        public void AddItemToBasket(int aircraftId, int quantity)
        {
            var basket = GetBasket();

            var aircraft = _aircraftService.GetAircraftById(aircraftId).Result;

            BasketItem item = new BasketItem
            {
                AircraftId = aircraftId,
                Name = aircraft.Name,
                Quantity = quantity,
                Price = aircraft.Price
            };
            basket.Items.Add(item);

            SaveBasket(basket);
        }

        // Task 4 RemoveItemFromBasket using HttpContext.Session
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

        // Task 4 GetBasket using HttpContext.Session
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

        // Task 4 SaveBasket using HttpContext.Session
        private void SaveBasket(Basket basket)
        {
            var json = JsonSerializer.Serialize(basket);
            var bytes = Encoding.UTF8.GetBytes(json);

            _httpContextAccessor.HttpContext.Session.Set("basket", bytes);
        }

        public async Task<BasketDto> GetCurrentBasket()
        {
            var basket = await GetPersistedBasket();

            return _mapper.Map<BasketDto>(basket);
        }


        // Task 5 AddItemToPersistedBasket using Entity Framework
        public async Task AddItemToPersistedBasket(int aircraftId, int quantity)
        {
            var basket = await GetPersistedBasket();

            var aircraft = await _aircraftService.GetAircraftById(aircraftId);

            BasketItem item = new BasketItem
            {
                AircraftId = aircraftId,
                Name = aircraft.Name,
                Quantity = quantity,
                Price = aircraft.Price
            };
            basket.Items.Add(item);

            await SavePersistedBasket(basket);
        }

        // Task 5 RemoveItemFromPersistedBasket using Entity Framework
        public async Task RemoveItemFromPersistedBasket(int aircraftId)
        {
            var basket = await GetPersistedBasket();

            var basketItem = basket.Items.FirstOrDefault(bi => bi.AircraftId == aircraftId);

            if (basketItem != null)
            {
                _basketItemService.DeleteBasketItem(basketItem.Id).Wait();

            } else if (basketItem == null)
            {
                throw new Exception("Basket item not found");
            }
        }

        // Task 5 GetPersistedBasket using Entity Framework
        // basketId defaulted to 1 for task demo 
        public async Task<Basket> GetPersistedBasket(int basketId = 1)
        {
            var basket = await Get(basketId, b => b.Items);

            if (basket == null)
            {
                return new Basket();
            }

            return basket;
        }

        // Task 5 SavePersistedBasket using Entity Framework
        private async Task SavePersistedBasket(Basket basket)
        {
            if (basket.Id == 0)
            {
                await Add(basket);
            }
            else
            {
                await Update(basket);
            }

            await SaveChanges();
        }
    }
}
