using System.Text.Json;

namespace Api
{
	public class Api
	{
		/** Base URL */
		internal readonly Uri BaseUrl;
		/** Endpoints */
		public readonly List<Endpoint> Endpoints;
		/** Authentication Methods */
		public readonly List<Type> AuthenticationMethods;

		public Api(Uri baseUrl, List<Endpoint> endpoints, List<Type> authenticationMethods)
		{
			BaseUrl = baseUrl;
			Endpoints = endpoints;
			AuthenticationMethods = authenticationMethods;
		}
	}
}
