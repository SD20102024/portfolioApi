using MongoDB.Driver;
using Microsoft.Extensions.Options;
using PortfolioApi.Models;      
namespace PortfolioApi.Services
{

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;

    }
    

    public class EnquiryService
    {
        private readonly IMongoCollection<EnquiryFormDto> _enquiriesCollection;
        public async Task<long> CountEnquiriesAsync()
        {
            return await _enquiriesCollection.CountDocumentsAsync(FilterDefinition<EnquiryFormDto>.Empty);
        }

        public EnquiryService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _enquiriesCollection = mongoDatabase.GetCollection<EnquiryFormDto>(settings.Value.CollectionName);
        }

        public async Task CreateEnquiryAsync(EnquiryFormDto enquiry)
        {
            await _enquiriesCollection.InsertOneAsync(enquiry);
        }

        // You can add methods for Get, Update, etc. if needed
    }
}
