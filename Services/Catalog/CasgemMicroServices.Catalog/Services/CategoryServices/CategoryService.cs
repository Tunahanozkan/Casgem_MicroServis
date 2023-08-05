using AutoMapper;
using CasgemMicroServices.Catalog.Dtos.CategoryDtos;
using CasgemMicroServices.Catalog.Models;
using CasgemMicroServices.Catalog.Settings.Abstract;
using MicroServices.Shared.Dtos;
using MongoDB.Driver;

namespace CasgemMicroServices.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            _mapper = mapper;
            var client= new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }

        public async Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
            return Response<CreateCategoryDto>.Success(_mapper.Map<CreateCategoryDto>(value),200);
        }

        public async Task<Response<NoContent>> DeleteCategoryAsync(string id)
        {
            var value = await _categoryCollection.DeleteOneAsync(x=>x.CategoryID==id);
            if (value.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek Kategori Bulunamadı!", 404);
            }
        }

        public async Task<Response<ResultCategoryDtos>> GetCategoryByIdAsync(string id)
        {
            var value = await _categoryCollection.Find<Category>(x=>x.CategoryID==id).FirstOrDefaultAsync();
            if(value == null)
            {
                return Response<ResultCategoryDtos>.Fail("Böyle Bir ID Bulunamadı !", 404);
            }
            else
            {
                return Response<ResultCategoryDtos>.Success(_mapper.Map<ResultCategoryDtos>(value),200);
            }
        }

        public async Task<Response<List<ResultCategoryDtos>>> GetCategoryListAsync()
        {
            var values =await _categoryCollection.Find(x=>true).ToListAsync();
            return Response<List<ResultCategoryDtos>>.Success(_mapper.Map<List<ResultCategoryDtos>>(values),200);
        }

        public async Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryID == updateCategoryDto.CategoryID, value);
            if(result == null)
            {
                return Response<UpdateCategoryDto>.Fail("Güncellenicek veri bulunamadı!", 404);
            }
            else
            {
                return Response<UpdateCategoryDto>.Success(204);
            }
        }
    }
}
