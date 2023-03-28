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
			Validate(arguments);
		}
		private void Validate(Dictionary<string, object>? arguments)
		{
			if (arguments == null || Parameters == null) return;
			/** Unique parameter names */
			HashSet<string> uniqueParams = new(Parameters.Keys);
			/** Unique argument names */
			HashSet<string> uniqueArgs = new(arguments.Keys);

			// Arguments must be unique
			if (uniqueArgs.Count > arguments.Count)
			{
				uniqueArgs.ExceptWith(uniqueParams);
				int n = uniqueArgs.Count;
				throw new ArgumentException($"{n} Duplicate argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", uniqueArgs)}");
			}
			// Argument names must match parameter names
			if (arguments.Count > uniqueParams.Count)
			{
				uniqueArgs.ExceptWith(uniqueParams);
				int n = uniqueArgs.Count;
				throw new ArgumentException($"{n} Extra argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", uniqueArgs)}");
			}
			// Argument types must match parameter types
			foreach (KeyValuePair<string, object> argument in arguments)
			{
				//at this point, we know that the argument name is a valid parameter name
				Parameter<Type> parameter = Parameters[argument.Key];
				if (!parameter.Type.IsAssignableFrom(argument.Value.GetType()))
				{
					throw new ArgumentException($"Argument '{argument.Key}' must be assignable to '{parameter.Type.Name}', '{argument.Value.GetType().Name}' given");
				}
			}
			// Required parameters must be satisfied
			if (uniqueParams.Count > arguments.Count)
			{
				HashSet<string> missing = new(uniqueParams); missing.ExceptWith(uniqueArgs);
				int n = missing.Count;
				if (n > 0)
				{
					throw new ArgumentException($"{n} Missing required argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", missing)}");
				}
			}
		}
	}
}
