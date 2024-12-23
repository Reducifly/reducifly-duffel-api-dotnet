using System;
using Duffel.ApiClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Duffel.ApiClient.Converters.Json
{
    public class CabinRowsSectionsElementJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //Default serialization is fine
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            var elementType = (string)jo["type"]!;
            RowSectionElement result;
            
            switch (elementType?.ToLower())
            {
                case "seat":
                    result = new RowSectionElement { ElementType = RowSectionElementType.Seat };
                    break;

                case "bassinet":
                    result = new RowSectionElement { ElementType = RowSectionElementType.Bassinet };
                    break;
                
                case "empty":
                    result = new RowSectionElement { ElementType = RowSectionElementType.Empty };
                    break;
                
                case "exit_row":
                    result = new RowSectionElement { ElementType = RowSectionElementType.ExitRow };
                    break;

                case "lavatory":
                    result = new RowSectionElement { ElementType = RowSectionElementType.Lavatory };
                    break;

                case "galley":
                    result = new RowSectionElement { ElementType = RowSectionElementType.Galley };
                    break;
                
                case "closet":
                    result = new RowSectionElement { ElementType = RowSectionElementType.Closet };
                    break;

                case "stairs":
                    result = new RowSectionElement { ElementType = RowSectionElementType.Stairs };
                    break;
                
                default:
                    throw new NotImplementedException($"Seat map element type: {elementType} is not supported");
            }
            
            serializer.Populate(jo.CreateReader(), result);
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(RowSectionElement).IsAssignableFrom(objectType);
        }
    }
}