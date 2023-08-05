using CasgemMicroServices.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace CasgemMicroServices.Catalog.Dtos.ProductDtos
{
    public class CreateProductDto
    {

        public string ProductName { get; set; }
        public string Description { get; set; }
        public int stock { get; set; }
        public decimal Price { get; set; }
        public string CategoryID { get; set; }

    }
}
