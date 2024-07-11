using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductDetails.Application.Interface;
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> addProduct([FromBody] AddProductRequestDto request)
        {
            if (ModelState.IsValid)
            {
               var result=_productDetailService.addNewProduct(request);
               return Ok(result.Result);

            }
            return BadRequest("Invalid Request");
        }

        [HttpPost("List-Product")]
        [SwaggerOperation(Summary = "List product details")]
        [Route("api/v1/List-Product")]
        public async Task<IActionResult> listProduct()
        {
            if (ModelState.IsValid)
            {
                var result = _productDetailService.ListAllProducts();
                return Ok(result.Result);

            }
            return BadRequest("Invalid Request");
        }

        [HttpPost("Delete-Product")]
        [SwaggerOperation(Summary = "List product details")]
        [Route("api/v1/Delete-Product")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> deleteProduct([FromBody] string Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productDetailService.deleteProducts(Id);
                return Ok(result);

            }
            return BadRequest("Invalid Request");
        }
        [HttpPost("Update-Product")]
        [SwaggerOperation(Summary = "Update  product details")]
        [Route("api/v1/Update-Product")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> updateProduct([FromBody] UpdateProductDetailsRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = _productDetailService.UpdateProduct(request);
                return Ok(result.Result);

            }
            return BadRequest("Invalid Request");
        }

        [HttpPost("Specific-Product")]
        [SwaggerOperation(Summary = "Fetch Specific  product details")]
        [Route("api/v1/Specific-Product")]

        public async Task<IActionResult> specifiProduct([FromBody] string request)
        {
            if (ModelState.IsValid)
            {
                var result = _productDetailService.specificProducts(request);
                return Ok(result.Result);

            }
            return BadRequest("Invalid Request");
        }

        [HttpPost("Checkout-Product")]
        [SwaggerOperation(Summary = "Checkout Product")]
        [Route("api/v1/Checkout-Product")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> checkoutProduct([FromBody] string request)
        {
            if (ModelState.IsValid)
            {
                var result = _productDetailService.checkout(request);
                return Ok(result.Result);

            }
            return BadRequest("Invalid Request");
        }



    }
}
