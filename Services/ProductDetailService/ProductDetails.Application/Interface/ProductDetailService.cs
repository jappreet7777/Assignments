using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using ProductDetails.Domain.DTO;
using ProductDetails.Domain.Entities;
using ProductDetails.Domain.ResponseInfoFormat;
using ProductDetails.Infrastructure.ProductDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductInfo = ProductDetails.Domain.Entities.ProductInfo;

namespace ProductDetails.Application.Interface
{
    public class ProductDetailService:IProductDetailService
    {
        private readonly ProductDBContext _productDBContext;

        public ProductDetailService(ProductDBContext productDBContext)
        {
            _productDBContext = productDBContext;
        }

        public async Task<ResponseInfo<AddProductResponseDTO>> addNewProduct(AddProductRequestDto request)
        {
            var response = new ResponseInfo<AddProductResponseDTO>();
            try
            {
                var dbcontext = _productDBContext;
                if (request.Name.IsNullOrEmpty() || request.Quantity.IsNullOrEmpty())
                {
                    return response.Fail("Please Enter All the valid Details");
                }
                ProductInfo productdetail = new ProductInfo()
                {
                    Name = request.Name,
                    Price = Convert.ToDecimal(request.Price),
                    Design = request.Description,
                    Quantity = Convert.ToDecimal(request.Quantity)
                };
                 dbcontext.Add(productdetail);
                 dbcontext.SaveChanges();


                AddProductResponseDTO productResponseDTO = new AddProductResponseDTO()
               {
                   ProductId=productdetail.Id
               };
                return response.Success(productResponseDTO, "Product Added Succesfully");
            }
            catch(Exception ex)
            {
                return response.Fail($"{ex.Message}");
            }

        }
        //public async Task<ResponseInfo<IList<ListProductDetailsResponseDTO>>> ListAllProducts()
        //{
        //    var response = new ResponseInfo<IList<ListProductDetailsResponseDTO>>();
        //    try
        //    {
        //        var dbcontext = _productDBContext;
        //        var responsedb = dbcontext.Find<ProductInfo>();
        //    }

        //    catch (Exception ex)
        //    {
        //        return response.Fail($"{ex.Message}");
        //    }

        //}
    }
}
