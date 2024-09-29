using MongoDB.Driver;
using ItemApi.Models;

public class ItemService
{
    private readonly IMongoCollection<Item> _itemsCollection;

    public ItemService(IMongoClient client, MongoDBSettings settings)
    {
        // Подключаемся к базе данных и коллекции
        var database = client.GetDatabase(settings.DatabaseName);
        _itemsCollection = database.GetCollection<Item>(settings.ItemsCollectionName);
    }

    // Получить все элементы
    public async Task<List<Item>> GetAsync() =>
        await _itemsCollection.Find(item => true).ToListAsync();

    // Получить элемент по ID
    public async Task<Item> GetAsync(string id) => 
        await _itemsCollection.Find<Item>(item => item.Id == id).FirstOrDefaultAsync();


    // Создать новый элемент
    public async Task CreateAsync(Item item) =>
        await _itemsCollection.InsertOneAsync(item);

    // Обновить существующий элемент
    public async Task UpdateAsync(string id, Item itemIn) =>
        await _itemsCollection.ReplaceOneAsync(item => item.Id == id, itemIn);

    // Удалить элемент по ID
    public async Task RemoveAsync(string id) => 
        await _itemsCollection.DeleteOneAsync(item => item.Id == id);
}
