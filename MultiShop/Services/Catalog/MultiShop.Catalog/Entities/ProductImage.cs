using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities
{
    public class ProductImage
    {
        [BsonId] // MongoDB'ye bu özelliğin anahtar olduğunu belirtir.
        [BsonRepresentation(BsonType.ObjectId)] // ObjectId kullanacağımızı belirtiyoruz.
        public string ProductImageID { get; set; }  // ObjectId'nin string temsilini kullanıyoruz.

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }

        public string ProductId { get; set; }

        [BsonIgnore]
        public Product Product { get; set; }
    }
}
