namespace Api
{
	public class Parameter<T> where T : Type
	{
		public readonly bool Required = false;
		public readonly T? DefaultValue;
		public readonly Type Type;

		public Parameter(bool required, T? defaultValue)
		{
			Required = required;
			DefaultValue = defaultValue;
			Type = typeof(T);
		}
	}
}
