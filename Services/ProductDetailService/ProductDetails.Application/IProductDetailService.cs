using ProductDetails.Domain.DTO;
using ProductDetails.Domain.ResponseInfoFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDetails.Application
{
    public interface IProductDetailService
    {
        public Task<ResponseInfo<AddProductResponseDTO>> addNewProduct(AddProductRequestDto request);
        public  Task<ResponseInfo<IList<ListProductDetailsResponseDTO>>> ListAllProducts();
        public Task<ResponseInfo<string>> deleteProducts(string id);
        public Task<ResponseInfo<string>> UpdateProduct(UpdateProductDetailsRequestDTO request);


    }
}
