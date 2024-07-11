using Azure.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Fluent;
using ProductDetails.Application.Interface;
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

namespace ProductDetails.Application.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly ProductDBContext _productDBContext;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ProductDetailService(ProductDBContext productDBContext) { 
            _productDBContext = productDBContext;
            //_logger = logger;
        }

        public async Task<ResponseInfo<AddProductResponseDTO>> addNewProduct(AddProductRequestDto request)
        {
            var response = new ResponseInfo<AddProductResponseDTO>();
            try
            {
                if (request.Quantity == "30")
                {
                    throw new Exception("An error Occured");
                }
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
                    ProductId = productdetail.Id
                };
                return response.Success(productResponseDTO, "Product Added Succesfully");
            }
            catch (Exception ex)
            {
              logger.Error(ex, "Error Occured");
                return response.Fail($"{ex.Message}");
            }

        }
        public async Task<ResponseInfo<IList<ListProductDetailsResponseDTO>>> ListAllProducts()
        {
            var response = new ResponseInfo<IList<ListProductDetailsResponseDTO>>();
            try
            {
                var dbcontext = _productDBContext;
                var responsedb = dbcontext.ProductInfo.Where(x => x.isDelete == false).Select(x => new ListProductDetailsResponseDTO { Name = x.Name, Price = Convert.ToString(x.Price), Description = x.Design }).ToList();
                if (responsedb.Count > 0)
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
                    responsedb.Price = Convert.ToDecimal(request.Price);
                    responsedb.Design = request.Description;
                    responsedb.Quantity = Convert.ToDecimal(request.Quantity);
                    dbcontext.Update(responsedb);
                    dbcontext.SaveChanges();
                    return response.Success(request.Id, "Product Updated Succesfully");

                }

                return response.Fail("Invalid Id");
            }

            catch (Exception ex)
            {
                return response.Fail($"{ex.Message}");
            }

        }

        public async Task<ResponseInfo<ListProductDetailsResponseDTO>> specificProducts(string id)
        {
            var response = new ResponseInfo<ListProductDetailsResponseDTO>();
            if (id.IsNullOrEmpty())
            {
                return response.Fail("Please provide a valid Id");
            }
            try
            {
                var dbcontext = _productDBContext;
                var responsedb = dbcontext.ProductInfo.Where(x => x.Id == id).Select(x => new ListProductDetailsResponseDTO { Name = x.Name, Price = Convert.ToString(x.Price), Description = x.Design }).FirstOrDefault();
                if (responsedb != null)
                {
                    return response.Success(responsedb);
                   
                }

                return response.Fail("Invalid Id");
            }

            catch (Exception ex)
            {
                return response.Fail($"{ex.Message}");
            }

        }

        public async Task<ResponseInfo<bool>> checkout(string id)
        {
            var response = new ResponseInfo<bool>();
            try
            {
                var dbcontext = _productDBContext;
                var responsedb = dbcontext.ProductInfo.Where(x => x.Id == id).FirstOrDefault();
                if (responsedb?.Quantity < 0)
                {
                    response.Fail("Checkout Not available");
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage responseInfo = await client.PostAsync("http://localhost:5039/api/v3/avaialable-Checkout", null);
                        if (responseInfo.IsSuccessStatusCode)
                        {
                            responsedb.Quantity = responsedb.Quantity - 1;
                            dbcontext.Update(responsedb);
                            dbcontext.SaveChanges();
                            return response.Success(true, "Checkout Succesful");
                        }
                    }
                   

                }
                return response.Fail("Checkout failed");

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return response.Fail($"{ex.Message}");

            }
        }






    }
}
