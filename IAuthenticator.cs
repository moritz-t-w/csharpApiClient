namespace Api
{
    internal interface IAuthenticator
    {
        public void Authenticate(HttpClient client);
    }
}
