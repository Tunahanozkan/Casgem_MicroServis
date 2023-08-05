using AutoMapper;
using CasgemMicroServices.Catalog.Dtos.CategoryDtos;
using CasgemMicroServices.Catalog.Dtos.ProductDtos;
using CasgemMicroServices.Catalog.Models;
using CasgemMicroServices.Catalog.Settings.Abstract;
using MicroServices.Shared.Dtos;
using MongoDB.Driver;

namespace CasgemMicroServices.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productsCollection;
        private readonly IMongoCollection<Category> _categoryColection;
         
        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var cilent = new MongoClient(databaseSettings.ConnectionString);
            var database = cilent.GetDatabase(databaseSettings.DatabaseName);
            _productsCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryColection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            await _productsCollection.InsertOneAsync(values);
            return Response<CreateProductDto>.Success(_mapper.Map<CreateProductDto>(values),200);
        }

        public async Task<Response<NoContent>> DeleteProductAsync(string id)
        {
            var value = await _productsCollection.DeleteOneAsync(x => x.ProductID == id);
            if (value.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("silinicek data yok ", 400);
            }
        }

        public async Task<Response<ResultProductDto>> GetProductByIdAsync(string id)
        {
            var value = await _productsCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync();
            if (value == null)
            {
                return Response<ResultProductDto>.Fail("Böyle Bir ID Bulunamadı !", 404);
            }
            else
            {
                return Response<ResultProductDto>.Success(_mapper.Map<ResultProductDto>(value), 200);
            }
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListAsync()
        {
            var values = await _productsCollection.Find(x => true).ToListAsync();
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync()
        {
            var values = await _productsCollection.Find(x=> true).ToListAsync();
            if (values.Any())
            {
                foreach (var item in values)
                {
                    item.Category= await _categoryColection.Find(x=>x.CategoryID==item.CategoryID).FirstOrDefaultAsync();
                }
            }
            else
            {
                values = new List<Product>();    
            }
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            var result = await _productsCollection.FindOneAndReplaceAsync(x=>x.ProductID==updateProductDto.ProductID,value);
            if(result ==null)
            {
                return Response<UpdateProductDto>.Fail("Güncellenecek veri bulunamadı", 404);
            }
            else
            {
                return Response<UpdateProductDto>.Success(204);
            }
        }
    }
}
