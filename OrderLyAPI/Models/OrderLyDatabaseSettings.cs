namespace OrderLy_API.Models
{
    public class OrderLyDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string OrdersCollectionName { get; set; } = null!;
        public string VendorsCollectionName { get; set; } = null!;
    }
}
