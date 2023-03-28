using System.Text.Json;

namespace Api
{
    public class Api
    {
        /** Base URL with a parameter for the version */
        private readonly Uri _baseUrl;
        /** Endpoints */
        public readonly List<Endpoint> Endpoints;

        public Api(Uri baseUrl, List<Endpoint> endpoints)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            Endpoints = endpoints ?? throw new ArgumentNullException(nameof(endpoints));
        }
    }
}
