using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Duffel.ApiClient.Converters;
using Duffel.ApiClient.Models;
using Duffel.ApiClient.Models.Requests;
using Duffel.ApiClient.Models.Responses;
using OffersResponseConverter = Duffel.ApiClient.Converters.OffersResponseConverter;

namespace Duffel.ApiClient.Resources
{
    public interface IPlacesRequest
    {
        /// <summary>
        /// TBD
        /// </summary>
        /// <param name="query">Name of city or airport code</param>
        /// <param name="rad">Radius in meters to pull from</param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        Task<List<Place>> Create(string query, decimal? rad = null, string latitude = null, string longitude = null);
    }

    public class PlacesRequest : BaseResource<PlacesRequest>, IPlacesRequest
    {
        public PlacesRequest(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<List<Place>> Create(string query, decimal? rad = null, string latitude = null, string longitude = null)
        {
            string queryString = $"places/suggestions?query={query}";
            if (rad != null)
            {
                queryString += $"&rad={rad}";
            }
            if (latitude != null)
            {
                queryString += $"&latitude={latitude}";
            }
            if (longitude != null)
            {
                queryString += $"&longitude={longitude}";
            }
            var result = await HttpClient.GetAsync(queryString).ConfigureAwait(false);
            return await SingleItemResponseConverter.GetAndDeserialize<List<Place>>(result);
        }

    }
}
