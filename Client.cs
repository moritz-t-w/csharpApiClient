namespace Api
{
	public class Client<Api, TAuthentication> where Api : Api<TAuthentication> where TAuthentication : IAuthenticator
	{
		private readonly HttpClient _client = new();
	}
}