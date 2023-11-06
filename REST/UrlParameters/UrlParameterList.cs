using InterfaceComposition.REST.UrlParameters.Traits;

namespace InterfaceComposition.REST.UrlParameters
{
    public class UrlParameterList
    {
        private List<IUrlParameter> Parameters { get; } = new();

        public UrlParameterList(params IUrlParameter[] parameters) : this(parameters.ToList())
        {
        }

        public UrlParameterList(List<IUrlParameter> parameters)
        {
            Parameters = parameters;
        }

        public UrlParameterList(string paramKey, object paramValue)
        {
            Parameters.Add(new UrlParameter(paramKey, paramValue));
        }

        public static UrlParameterList Empty => new();

        public bool Any() => Parameters.Any();

        public void Add(IUrlParameter parameter)
        {
            Parameters.Add(parameter);
        }

        public void AddRange(UrlParameterList parameters)
        {
            Parameters.AddRange(parameters.Parameters);
        }

        public UrlParameterList Concat(UrlParameterList other)
        {
            return new(Parameters.Concat(other.Parameters).ToList());
        }

        public string ToUrlEncodedString()
        {
            return string.Join("&", Parameters.Select(parameter => parameter.ToUrlEncodedString()));
        }
    }
}
