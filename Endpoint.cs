namespace Api
{
    public class Endpoint
    {
        /** Path to the endpoint */
        public readonly Uri Path = new("/");
        /** HTTP method */
        public readonly HttpMethod Method;
        /** Request Parameters */
        public readonly Dictionary<string, Parameter<Type>>? Parameters;
    }
}
