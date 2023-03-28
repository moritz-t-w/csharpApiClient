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
			ValidateArguments(arguments);
		}
		private void ValidateArguments(Dictionary<string, object>? arguments)
		{
			if (arguments == null || Parameters == null) return;
			/** Unique parameter names */
			HashSet<string> parameterNames = new(Parameters.Keys);
			/** Unique argument names */
			HashSet<string> argumentNames = new(arguments.Keys);

			// if there are duplicate args, throw an error with the duplicate names
			if (argumentNames.Count > arguments.Count)
			{
				argumentNames.ExceptWith(parameterNames);
				int n = argumentNames.Count;
				throw new ArgumentException($"{n} Duplicate argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", argumentNames)}");
			}
			}
			// if required parameters are missing, throw an error with the missing names
			if (parameterNames.Count > arguments.Count)
			{
				parameterNames.ExceptWith(argumentNames);
				int n = parameterNames.Count;
				throw new ArgumentException($"{n} Missing required argument{(n > 1 ? "s" : "")}:\n{string.Join(",\n", parameterNames)}");
			}
		}
	}
}
