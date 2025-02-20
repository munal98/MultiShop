using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;
using MongoDB.Bson;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _ProductImageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _ProductImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var value = _mapper.Map<ProductImage>(createProductImageDto);
            await _ProductImageCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            // ObjectId'yi doğru şekilde dönüştürüp sorgulama yapıyoruz.
            var objectId = new ObjectId(id);
            await _ProductImageCollection.DeleteOneAsync(x => x.ProductImageID == objectId.ToString());
        }

        public async Task<List<ResultProductImageDto>> GetAllCategoriesAsync()
        {
            var values = await _ProductImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductImageDto>>(values);
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            // ObjectId'yi doğru şekilde dönüştürüp sorgulama yapıyoruz.
            var objectId = new ObjectId(id);
            var values = await _ProductImageCollection.Find<ProductImage>(x => x.ProductImageID == objectId.ToString()).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var values = _mapper.Map<ProductImage>(updateProductImageDto);
            // ObjectId'yi doğru şekilde dönüştürüp güncelleme yapıyoruz.
            var objectId = new ObjectId(updateProductImageDto.ProductImageID);
            await _ProductImageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == objectId.ToString(), values);
        }
    }
}
