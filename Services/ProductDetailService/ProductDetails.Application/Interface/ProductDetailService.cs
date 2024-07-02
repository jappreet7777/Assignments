using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using ProductDetails.Domain.DTO;
using ProductDetails.Domain.Entities;
using ProductDetails.Domain.ResponseInfoFormat;
using ProductDetails.Infrastructure.ProductDetails;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<ResponseInfo<IList<ListProductDetailsResponseDTO>>> ListAllProducts()
        {
            var response = new ResponseInfo<IList<ListProductDetailsResponseDTO>>();
            try
            {
                var dbcontext = _productDBContext;
                var responsedb = dbcontext.ProductInfo.Where(x => x.isDelete == false).Select(x => new ListProductDetailsResponseDTO {Name=x.Name,Price=Convert.ToString(x.Price),Description=x.Design}).ToList();
                if (responsedb.Count>0)
                {
                    return response.Success(responsedb, "List of Products");
                }
                return response.Fail();
            }

            catch (Exception ex)
            {
                return response.Fail($"{ex.Message}");
            }

        }
        public async Task<ResponseInfo<string>> deleteProducts(string id)
        {
            var response = new ResponseInfo<string>();
            if (id.IsNullOrEmpty())
            {
                return response.Fail("Please provide a valid Id");
            }
            try
            {
                var dbcontext = _productDBContext;
                var responsedb = dbcontext.ProductInfo.Where(x => x.Id == id).FirstOrDefault();
                if (responsedb != null)
                {
                    responsedb.isDelete = true;
                    dbcontext.Update(responsedb);
                    dbcontext.SaveChanges();
                    return response.Success(id, "Product Deleted Succesfully");
                }

                return response.Fail("Invalid Id");
            }

            catch (Exception ex)
            {
                return response.Fail($"{ex.Message}");
            }

        }

        public async Task<ResponseInfo<string>> UpdateProduct(UpdateProductDetailsRequestDTO request)
        {
            var response = new ResponseInfo<string>();

            try
            {
                if (request.Id.IsNullOrEmpty())
                {
                    return response.Fail("Enter a Valid Id");
                }
                var dbcontext = _productDBContext;
                var responsedb = dbcontext.ProductInfo.Where(x => x.Id == request.Id).FirstOrDefault();
                if (responsedb != null)
                {
                    responsedb.Price=Convert.ToDecimal(request.Price);
                    responsedb.Design = request.Description;
                    responsedb.Quantity = Convert.ToDecimal(request.Quantity);
                    dbcontext.Update(responsedb);
                    dbcontext.SaveChanges();
                    return response.Success(request.Id, "Product Added Succesfully");

                }

                return response.Fail("Invalid Id");
            }

            catch (Exception ex)
            {
                return response.Fail($"{ex.Message}");
            }

        }



    }
}
