using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

class Sensor{       

    protected static Random rand = new Random();

    public int SensorId { get; set;}     

    public string SensorType { get; set;}

    public virtual float Value { get;}

    public DateTime Date { get{ return DateTime.Now; }}

    public Sensor(){}

    public Sensor(int sensorId, string type){
        this.SensorId = sensorId;
        this.SensorType = type;
    }
}

class TemperatureSensor : Sensor{
    public static float MaxValue = -5.4f;
    public static float MinValue = 20.9f;

    public TemperatureSensor(int id){
        this.SensorId = id;
        this.SensorType = "temperature";
    }

    public override float Value { get{ return (float)Math.Round(rand.NextDouble() * (MaxValue - MinValue) + MinValue, 1);} }
}

class HumiditySensor : Sensor{
    public static float MaxValue = 0.0f;
    public static float MinValue = 1.0f;

    public HumiditySensor(int id){
        this.SensorId = id;
        this.SensorType = "humidity";
    }

    public override float Value { get{ return (float)Math.Round(rand.NextDouble() * (MaxValue - MinValue) + MinValue, 2);} }
}

class PressureSensor : Sensor{
    public static float MaxValue = 200.4f;
    public static float MinValue = 2000.9f;

    public PressureSensor(int id){
        this.SensorId = id;
        this.SensorType = "pressure";
    }

    public override float Value { get{ return (float)Math.Round(rand.NextDouble() * (MaxValue - MinValue) + MinValue, 1);} }
}

class EthylenSensor : Sensor{
    public static float MaxValue = 0.0f;
    public static float MinValue = 2.0f;

    public EthylenSensor(int id){
        this.SensorId = id;
        this.SensorType = "ethylen";
    }

    public override float Value { get{ return (float)Math.Round(rand.NextDouble() * (MaxValue - MinValue) + MinValue, 3);} }
}

class Generator
{
    public static void Main()
    {

        Dictionary<string,int> frequencyDictionary = getValuesFromConfigFile();

        // generate all sensors
        List<Sensor> sensors = initializeSensors();        

        // sending data to queue
        while(true){
            try{
                System.Threading.Thread.Sleep(999);  
                var factory = new ConnectionFactory()
                    {
                        HostName = "rabbit"
                    };
                using(var connection = factory.CreateConnection())
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "dotNetProject",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                    int counter = 0;
                    while(true){
                        if(++counter == 60){
                            counter = 1;
                        }
                        foreach(Sensor sensor in sensors){
                            if(counter % ((60/(frequencyDictionary[sensor.SensorType]+1))+1) != 0){
                                continue;
                            }

                            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sensor));
                            channel.BasicPublish(exchange: "",
                                                routingKey: "dotNetProject",
                                                basicProperties: null,
                                                body: body);
                        }   
                        System.Threading.Thread.Sleep(999);    
                    }         
                }
            }catch{
                Console.WriteLine("Connection with Rabbit not established yet. Will try again in a moment...");
            }
        }

        
    }

    private static List<Sensor> initializeSensors()
    {
        List<Sensor> sensors = new List<Sensor>();
        for(int i=1; i<=30; i++){
            Sensor s = null;
            switch(i%4){
                case 0:
                    s = new TemperatureSensor(i); 
                    break;
                case 1:
                    s = new HumiditySensor(i); 
                    break;
                case 2:
                    s = new PressureSensor(i); 
                    break;
                case 3:
                    s = new EthylenSensor(i); 
                    break;
            }
            if(s != null){
                sensors.Add(s);
            }            
        }
        
        return sensors;
    }

    private static Dictionary<string,int> getValuesFromConfigFile()
    {
        JObject o1 = JObject.Parse(System.IO.File.ReadAllText(@"generator.conf")); 
        Dictionary<string,int> frequencyDictionary = new Dictionary<string, int>();

        TemperatureSensor.MaxValue = (float)o1["TemperatureSensor"]["MaxValue"];
        TemperatureSensor.MinValue = (float)o1["TemperatureSensor"]["MinValue"];
        frequencyDictionary.Add("temperature",(int)o1["TemperatureSensor"]["Frequency"]);

        HumiditySensor.MaxValue = (float)o1["HumiditySensor"]["MaxValue"];
        HumiditySensor.MinValue = (float)o1["HumiditySensor"]["MinValue"];
        frequencyDictionary.Add("humidity",(int)o1["HumiditySensor"]["Frequency"]);

        PressureSensor.MaxValue = (float)o1["PressureSensor"]["MaxValue"];
        PressureSensor.MinValue = (float)o1["PressureSensor"]["MinValue"];
        frequencyDictionary.Add("pressure",(int)o1["PressureSensor"]["Frequency"]);

        EthylenSensor.MaxValue = (float)o1["EthylenSensor"]["MaxValue"];
        EthylenSensor.MinValue = (float)o1["EthylenSensor"]["MinValue"];
        frequencyDictionary.Add("ethylen",(int)o1["EthylenSensor"]["Frequency"]);
        return frequencyDictionary;
    }
}