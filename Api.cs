using System.Text.Json;

namespace Api
{
	public class Api<TAuthentication> where TAuthentication : IAuthenticator
	{
		/** Base URL */
		internal readonly Uri BaseUrl;
		/** Endpoints */
		public readonly List<Endpoint> Endpoints;
		/** Authentication */
		public readonly Type Authentication;

		public Api(Uri baseUrl, List<Endpoint> endpoints)
		{
			BaseUrl = baseUrl;
			Endpoints = endpoints;
			this.Authentication = typeof(TAuthentication);
		}
	}
}
