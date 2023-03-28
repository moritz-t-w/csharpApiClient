namespace Api
{
	/** <summary> Interface for authenticating http requests </summary> */
	public interface IAuthenticator
	{
		/** <summary> Authenticate an http request </summary>
		 * <param name="request"> Request to authenticate </param>
		 */
		public void Authenticate(HttpRequestMessage request);
	}
}
