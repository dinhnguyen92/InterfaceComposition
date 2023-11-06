using System.Web;

namespace InterfaceComposition.REST.UrlParameters.Traits
{
    public interface IWithSingleValueUrlParameter : IUrlParameter
    {
        protected object Value { get; }

        protected string GetUrlEncodedParamValue()
        {
            string? stringValue = Value.ToString();
            if (string.IsNullOrEmpty(stringValue)) throw new ArgumentNullException(nameof(Value));
            return HttpUtility.UrlEncode(stringValue); ;
        }

        string IUrlParameter.ToUrlEncodedString()
        {
            return $"{EncodedKey}={GetUrlEncodedParamValue()}";
        }
    }
}
