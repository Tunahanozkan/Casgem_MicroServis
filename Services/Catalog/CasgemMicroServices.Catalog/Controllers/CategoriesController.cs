using CasgemMicroServices.Catalog.Dtos.CategoryDtos;
using CasgemMicroServices.Catalog.Services.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasgemMicroServices.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _categoryService.GetCategoryListAsync();
            return Ok(products);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByID(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return Ok();
        }
    }
}
