using System;
using Newtonsoft.Json;

namespace Duffel.ApiClient.Converters.Json
{
    internal class StringDurationToTimeStampJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //Default serialization is fine
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null) return TimeSpan.Zero;
            var duration = reader.Value.ToString();
            TimeSpan result;
            if (TimeSpan.TryParse(duration, out result))
            {
                return result;
            }
            return TimeSpan.Zero;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
