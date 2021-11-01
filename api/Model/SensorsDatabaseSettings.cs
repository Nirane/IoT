using System;

namespace api
{
    public class SensorsDatabaseSettings : ISensorsDatabaseSettings
    {
        public string SensorsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISensorsDatabaseSettings
    {
        string SensorsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
