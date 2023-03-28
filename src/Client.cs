namespace Api
{
	/** <summary>
    * Client for sending requests to an API
    * </summary>
    * <typeparam name="AuthenticationType">The type of authentication to use</typeparam>
	 */
	public class Client<AuthenticationType> where AuthenticationType : IAuthenticator, new()
	{
		private readonly Api _api;
		private readonly HttpClient _client = new();
		private readonly AuthenticationType _authenticator = new();

		public Client(Api Subject, HttpClient? client = null)
		{
			_api = Subject;
			_client = client ?? _client;
		}

		/** <summary> Send an http request </summary>
		 * <param name="target"> Endpoint to send the request to </param>
		 * <param name="arguments"> Arguments to send with the request </param>
		 * <returns> The response from the Endpoint </returns>
		 * <exception cref = "ArgumentException" />
		 * <exception cref = "HttpRequestException" />
		 */
		public async Task<HttpResponseMessage> Request<Result>(Endpoint<Result> target, Dictionary<string, object>? arguments) where Result : Type
		{
			target.Prepare(arguments);
			HttpRequestMessage request = new(target.Method, BuildUrl(target, arguments));
			_authenticator.Authenticate(request);
			return await _client.SendAsync(request);
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