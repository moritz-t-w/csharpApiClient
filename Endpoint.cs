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
		public Task<Result> Request(HttpClient httpClient, Dictionary<string, object>? arguments)
		{

			if (arguments != null) { Validate(arguments); Prune(arguments); }

		}
		/** <summary>
		 * Validate arguments against parameters based on the following rules in order: <br/>
		 * <list type="number">
		 * <item>Argument names must be unique</item>
		 * <item>Argument names must match parameter names</item>
		 * <item>Argument types must match parameter types</item>
		 * <item>Required parameters must be satisfied</item>
		 * </list>
		 * </summary>
		 * <param name="subjects"> Arguments to validate </param>
		 * <exception cref = "ArgumentException" />
		 */
		private void Validate(Dictionary<string, object> subjects)
		{
			/** <summary> Unique Parameter Names </summary> */
			HashSet<string> uniqueParams = new(Parameters.Keys);
			/** Unique argument names */
			HashSet<string> uniqueArgs = new(subjects.Keys);

			// Arguments must be unique
			if (uniqueArgs.Count > subjects.Count)
			{
				uniqueArgs.ExceptWith(uniqueParams);
				int n = uniqueArgs.Count;
				throw new ArgumentException($"{n} Duplicate argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", uniqueArgs)}");
			}
			// Argument names must match parameter names
			if (subjects.Count > uniqueParams.Count)
			{
				uniqueArgs.ExceptWith(uniqueParams);
				int n = uniqueArgs.Count;
				throw new ArgumentException($"{n} Extra argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", uniqueArgs)}");
			}
			// Argument types must match parameter types
			foreach (KeyValuePair<string, object> argument in subjects)
			{
				//at this point, we know that the argument name is a valid parameter name
				Parameter<Type> parameter = Parameters[argument.Key];
				if (!parameter.Type.IsAssignableFrom(argument.Value.GetType()))
				{
					throw new ArgumentException($"Argument '{argument.Key}' must be assignable to '{parameter.Type.Name}', '{argument.Value.GetType().Name}' given");
				}
			}
			// Required parameters must be satisfied
			if (uniqueParams.Count > subjects.Count)
			{
				HashSet<string> missing = new(uniqueParams); missing.ExceptWith(uniqueArgs);
				int n = missing.Count;
				if (n > 0)
				{
					throw new ArgumentException($"{n} Missing required argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", missing)}");
				}
			}
		}

		/** <summary>
		 * Remove all arguments that are equal to the default value of their respective parameter
		 * </summary>
		 * <param name="subjects"> Arguments to prune </param>
		 */
		private void Prune(Dictionary<string, object> subjects)
		{
			foreach (KeyValuePair<string, Parameter<Type>> parameter in Parameters)
			{
				if (subjects[parameter.Key].Equals(parameter.Value.DefaultValue))
				{
					subjects.Remove(parameter.Key);
				}
			}
		}
	}
}
