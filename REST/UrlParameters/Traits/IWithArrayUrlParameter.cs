using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceComposition.REST.UrlParameters.Traits
{
    public interface IWithArrayUrlParameter : IUrlParameter
    {
        protected List<object> Values { get; }

        protected IEnumerable<string?> GetUrlEncodedArrayValues()
        {
            var stringValues = Values.Select(value => value.ToString());

            if (!stringValues.Any() || stringValues.Any(string.IsNullOrEmpty))
                throw new System.ArgumentNullException(nameof(Values));

            return stringValues.Select(HttpUtility.UrlEncode);
        }
    }
}
