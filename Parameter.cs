namespace Api
{
    internal class Parameter<T> where T : Type
    {
        public bool Required;
        public T DefaultValue;
        public Uri Documentation;
    }
}
