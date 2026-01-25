using MongoDB.Bson;

namespace DungeonCrawler.MongoDB
{
    public class Character
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}

