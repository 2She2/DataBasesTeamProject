namespace VehicleVendor.Data
{
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public interface IVehicleVendorMongoDb
    {
        IEnumerable<BsonDocument> GetDocument(string documentName);

        MongoDatabase Database { get; } 
    }
}
