namespace Api
{
    public class Endpoint<Result> where Result : Type
    {
        /** Path to the endpoint */
        public readonly Uri Path = new("/");
        /** HTTP method */
        public readonly HttpMethod Method;
        /** Request Parameters */
        public readonly Dictionary<string, Parameter<Type>>? Parameters;

        public Endpoint(Uri? path, HttpMethod method, Dictionary<string, Parameter<Type>>? parameters)
        {
            Path = path ?? Path;
            Method = method;
            Parameters = parameters;
        }
        public Task<Result> Request(HttpClient httpClient, Dictionary<string, object> arguments)
        {
            
        }
    }
}
