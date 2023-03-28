namespace Api
{
	public class Parameter<T> where T : Type
	{
		public bool Required = false;
		public T? DefaultValue;
	}
}
