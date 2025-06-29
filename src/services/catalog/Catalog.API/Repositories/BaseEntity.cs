using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Repositories
{
    public class BaseEntity
    {
        // Snow Flake 
        // MongoDb deki karşılığı
        [BsonElement("_id")]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
