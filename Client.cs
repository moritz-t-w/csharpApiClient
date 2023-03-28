namespace Api
{
    public class Client
    {
        private readonly Api _api;
        private readonly HttpClient _client;
        private readonly IAuthenticator _authenticator;
        public Client(Api api, HttpClient client, IAuthenticator authenticator)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
        }
    }
}