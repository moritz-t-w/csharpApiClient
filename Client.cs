namespace Api
{
	public class Client<AuthenticationType> where AuthenticationType : IAuthenticator, new()
	{
		private readonly Api<AuthenticationType> _api;
		private readonly HttpClient _client = new();
		private readonly AuthenticationType _authenticator = new();

		public Client(Api<AuthenticationType> Subject, HttpClient? client = null)
		{
			_api = Subject;
			_client = client ?? _client;
		}

		/** <summary> Build an endpoint URL </summary>
		 * <param name="endpoint"> Endpoint to build the URL for </param>
		 * <returns> The URL for the Endpoint </returns>
		 * <exception cref = "ArgumentException" />
		 */
		private Uri BuildUrl<Result>(Endpoint<Result> endpoint, Dictionary<string, object>? arguments) where Result : Type
		{
			UriBuilder builder = new(_api.BaseUrl);
			builder.Path += endpoint.Path;
			if (arguments != null)
			{
				builder.Query = string.Join("&", arguments.Select(x => $"{x.Key}={x.Value}"));
			}
			return builder.Uri;
		}
	}
}