using CasgemMicroServices.Catalog.Dtos.ProductDtos;
using MicroServices.Shared.Dtos;

namespace CasgemMicroServices.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<Response<List<ResultProductDto>>> GetProductListAsync();
        Task<Response<ResultProductDto>> GetProductByIdAsync(string id);
        Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<Response<NoContent>> DeleteProductAsync(string id);
        Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync();
    }
}
