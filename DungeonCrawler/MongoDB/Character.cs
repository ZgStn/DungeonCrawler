namespace DungeonCrawler.Collections
{
    internal class Character
    {
        // TODO: [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        public string Name { get; set; }

        //public string? Id { get; set; }  // lagra som string (ObjectId i Mongo) eller egen string
        //public string Description { get; set; }  = ""; 

    }
}

