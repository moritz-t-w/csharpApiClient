namespace Api
{
	public interface IAuthenticator
	{
		public void Authenticate(HttpRequestMessage request);
	}
}
