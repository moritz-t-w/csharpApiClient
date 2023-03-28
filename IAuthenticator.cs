namespace Api
{
    public interface IAuthenticator
    {
        public void Authenticate(HttpClient client);
    }
}
