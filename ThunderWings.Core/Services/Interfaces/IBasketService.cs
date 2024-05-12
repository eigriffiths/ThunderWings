using ThunderWings.Core.DTO.Basket;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDto> GetCurrentBasket();
        void AddItemToBasket(int aircraftId, int quantity);
        void RemoveItemFromBasket(int aircraftId);
        Task AddItemToPersistedBasket(int aircraftId, int quantity);
        Task RemoveItemFromPersistedBasket(int aircraftId);
        Task<Basket> GetPersistedBasket(int basketId = 1);
    }
}