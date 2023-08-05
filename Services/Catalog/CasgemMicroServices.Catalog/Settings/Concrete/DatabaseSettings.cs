using CasgemMicroServices.Catalog.Settings.Abstract;

namespace CasgemMicroServices.Catalog.Settings.Concrete
{
    public class DatabaseSettings:IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
    }
}
