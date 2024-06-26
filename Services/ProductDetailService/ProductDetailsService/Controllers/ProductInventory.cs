using Microsoft.AspNetCore.Mvc;
using ProductDetails.Application;
using ProductDetails.Domain.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductDetailsService.Controllers
{
    public class ProductInventory : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;
        public ProductInventory(IProductDetailService productDetailService) {
            _productDetailService = productDetailService;
        }
        [HttpPost("add-product")]
        [SwaggerOperation(Summary = "add new product details")]
        [Route("api/v1/add-product")]
        public async Task<IActionResult> addProduct([FromBody] AddProductRequestDto request)
        {
            if (ModelState.IsValid)
            {
               var result=_productDetailService.addNewProduct(request);
               return Ok(result.Result);

            }
            return BadRequest("Invalid Request");
        }


    }
}
