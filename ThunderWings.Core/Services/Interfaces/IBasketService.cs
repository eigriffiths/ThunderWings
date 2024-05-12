namespace ThunderWings.Core.Services.Interfaces
{
    public interface IBasketService
    {
        void AddItemToBasket(int aircraftId, int quantity);
        void RemoveItemFromBasket(int aircraftId);
    }
}