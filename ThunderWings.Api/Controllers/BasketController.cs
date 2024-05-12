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

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basket = await _basketService.GetCurrentBasket();

            return Ok(basket);
        }

        [HttpPost]
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

        [HttpDelete]
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
    }
}
