namespace Api
{
	public class Parameter<T> where T : Type
	{
		public readonly bool Required = false;
		public readonly T? DefaultValue;
	}
}
