using api.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace api.Services
{
    public class SensorService
    {
        private readonly IMongoCollection<SensorData> _sensorData;

        public SensorService(ISensorsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _sensorData = database.GetCollection<SensorData>(settings.SensorsCollectionName);
        }

        public List<SensorData> Get()
        {
            return _sensorData.Find(sensorData => true).ToList();
        }

        public List<SensorData> GetByType(string type, int limit)
        {
            return _sensorData
                .Find(sensorData => sensorData.Type == type)
                .SortByDescending(sensorData => sensorData.Date)
                .Limit(limit)
                .ToList();
        }

        public SensorData Get(string id)
        {
            return _sensorData.Find(sensorData => sensorData.Id == id).FirstOrDefault();
        }     

        public SensorData Create(SensorData sensorData)
        {
            _sensorData.InsertOne(sensorData);
            return sensorData;
        }

        public void Update(string id, SensorData sensorDataIn)
        {
            _sensorData.ReplaceOne(sensorData => sensorData.Id == id, sensorDataIn);
        }
            

        public void Remove(SensorData sensorDataIn)
        {
            _sensorData.DeleteOne(sensorData => sensorData.Id == sensorDataIn.Id);
        }
            
        public void Remove(string id)
        {
            _sensorData.DeleteOne(sensorData => sensorData.Id == id);
        }
            
    }
}
