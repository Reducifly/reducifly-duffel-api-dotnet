using System;
using Duffel.ApiClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Duffel.ApiClient.Converters.Json
{
    public class PlaceJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            var placeType = (string)jo["type"]!;
            //if placeType is null, figure out if it's a city or airport by checking if the airports property is present
            if (placeType == null)
            {
                if (jo["airports"] != null)
                {
                    placeType = "city";
                }
                else
                {
                    placeType = "airport";
                }
            }
            Place result;
            
            switch(placeType?.ToLower())
            {
                case "city":
                    result = new City();
                    break;
                
                case "airport":
                    result = new Airport();
                    break;
                
                default:
                    throw new NotImplementedException($"{placeType} is not a recognised place type.");
            };
            
            serializer.Populate(jo.CreateReader(), result);
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Place).IsAssignableFrom(objectType);
        }
    }
}