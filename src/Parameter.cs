namespace Api
{
	/** <summary> Parameter for an Endpoint </summary>
	 * <typeparam name="T"> Type of the value </typeparam>
	 */
	public class Parameter<T> where T : Type
	{
		public readonly bool Required = false;
		public readonly T? DefaultValue;
		/** <summary> A public copy of <see cref="T"/> </summary> */
		public readonly Type Type;
		/** <summary> Validation logic </summary>
		 * <exception cref = "ArgumentException" />
		 */
		public readonly Action<T> Validate = (T subject) => { };

		public Parameter(bool? required, T? defaultValue, Action<T>? validate)
		{
			Required = required ?? this.Required;
			DefaultValue = defaultValue;
			Type = typeof(T);
			Validate = validate ?? this.Validate;
		}
	}
}
