namespace InterfaceComposition.REST.UrlParameters
{
    public abstract class BaseArrayUrlParameter
    {
        protected readonly string _key;
        protected readonly List<object> _values;

        public BaseArrayUrlParameter(string key, List<object> values)
        {
            _key = key;
            _values = values;
        }

        public BaseArrayUrlParameter(string key, params object[] values)
            : this(key, values.ToList())
        {
        }
    }
}
