using CasgemMicroServices.Catalog.Dtos.CategoryDtos;
using MicroServices.Shared.Dtos;

namespace CasgemMicroServices.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Response<List<ResultCategoryDtos>>> GetCategoryListAsync();
        Task<Response<ResultCategoryDtos>> GetCategoryByIdAsync(string id);
        Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<Response<NoContent>> DeleteCategoryAsync(string id);
    }
}
