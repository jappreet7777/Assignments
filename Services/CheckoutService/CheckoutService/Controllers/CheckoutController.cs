using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutService.Controllers
{
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService _checkoutservie;
        public CheckoutController(CheckoutService checkoutService) {
        _checkoutservie = checkoutService;
        }
        [HttpPost("check-Checkout")]
        [Route("api/v3/avaialable-Checkout")]
        public async Task<IActionResult> isCheckoutavailable()
        {
            if (ModelState.IsValid)
            {
                var result = _checkoutservie.isCheckoutavailable();
                return Ok(result.Result);

            }
            return BadRequest("Invalid Request");
        }
    }
}
