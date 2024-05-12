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

        [HttpPost]
        public IActionResult AddToBasket(int aircraftId, int quantity)
        {
            try
            {
                _basketService.AddItemToBasket(aircraftId, quantity);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "An error occurred while adding the item to the basket.");
            }
        }

        [HttpDelete]
        public IActionResult RemoveFromBasket(int aircraftId)
        {
            try
            {
                _basketService.RemoveItemFromBasket(aircraftId);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while removing the item from the basket.");
            }
        }
    }
}
