using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThunderWings.Core.Services;
using ThunderWings.Core.Services.Interfaces;

namespace ThunderWings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ICheckoutService _checkoutService;

        public BasketController(IBasketService basketService, ICheckoutService checkoutService)
        {
            _basketService = basketService;
            _checkoutService = checkoutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basket = await _basketService.GetCurrentBasket();

            return Ok(basket);
        }

        [HttpPost("items")]
        public IActionResult AddToBasket(int aircraftId, int quantity)
        {
            try
            {
                _basketService.AddItemToPersistedBasket(aircraftId, quantity);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "An error occurred while adding the item to the basket.");
            }
        }

        [HttpDelete("items/{aircraftId}")]
        public async Task<IActionResult> RemoveFromBasket(int aircraftId)
        {
            try
            {
                await _basketService.RemoveItemFromPersistedBasket(aircraftId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while removing the item from the basket.");
            }
        }

        [HttpPost("checkout")]
        // basketId defaulted to 1 for task demo 
        public IActionResult Checkout(int basketId = 1)
        {
            try
            {
                var basket = _checkoutService.ValidateBasket(basketId);

                var total = _checkoutService.CalculateBasketTotal(basket);

                var receipt = _checkoutService.GenerateReceipt(basket);

                return Ok(receipt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while attempting to checkout.");
            }
        }
    }
}
