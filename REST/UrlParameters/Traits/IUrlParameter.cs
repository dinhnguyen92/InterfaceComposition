using System.Web;

namespace InterfaceComposition.REST.UrlParameters.Traits
{
    public interface IUrlParameter
    {
        protected string Key { get; }
        protected string EncodedKey => HttpUtility.UrlEncode(Key);

        public string ToUrlEncodedString();
    }
}
