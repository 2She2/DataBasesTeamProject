namespace VehicleVendor.Data
{
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class VehicleVendorMongoDb : IVehicleVendorMongoDb
    {
        private MongoDatabase database;

        public VehicleVendorMongoDb() : this("mongodb://localhost", "NissanMongo")
        {
        }

        public VehicleVendorMongoDb(string connectionString, string databaseName)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoServer = mongoClient.GetServer();
            this.database = mongoServer.GetDatabase(databaseName);
        }

        public MongoDatabase Database 
        { 
            get
            {
                return this.database;
            }
        }

        public IEnumerable<BsonDocument> GetDocument(string documentName)
        {
            var collection = this.database.GetCollection(documentName);

            return collection.FindAll();
        }
    }
}
