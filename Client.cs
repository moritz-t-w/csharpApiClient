namespace Api
{
	public class Client<AuthenticationType> where AuthenticationType : IAuthenticator, new()
	{
		private readonly Api<AuthenticationType> _api;
		private readonly HttpClient _client = new();
	}
}
		public Client(Api<AuthenticationType> Subject, HttpClient? client = null)
		{
			_api = Subject;
			_client = client ?? _client;
		}

