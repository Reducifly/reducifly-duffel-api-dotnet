using System.Linq;
using Duffel.ApiClient.Exceptions;
using Duffel.ApiClient.Models.Requests;
using Duffel.ApiClient.Models.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Duffel.ApiClient.Converters
{
    public static class OrderConverter
    {
        public static string Serialize(OrderRequest request)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter {NamingStrategy = new SnakeCaseNamingStrategy()});
            var wrapped = new DuffelDataWrapper<OrderRequest>(request);
            return JsonConvert.SerializeObject(wrapped, Formatting.None, settings);
        }

        public static string SerializeMetadata(OrderMetadata metadata)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter {NamingStrategy = new SnakeCaseNamingStrategy()});
            var wrapped = new DuffelDataWrapper<OrderMetadata>(metadata);
            return JsonConvert.SerializeObject(wrapped, Formatting.None, settings);
        }
        
        public static Order Deserialize(string payload)
        {
            var wrappedResponse =
                JsonConvert.DeserializeObject<DuffelResponseWrapper<Order>>(payload);
            if (wrappedResponse != null && wrappedResponse.Errors != null && wrappedResponse.Errors.Any())
            {
                throw new ApiException(wrappedResponse.Metadata, wrappedResponse.Errors);
            }

            return (wrappedResponse?.Data ?? null) ?? throw new ApiDeserializationException(null, payload);
        }
    }
}