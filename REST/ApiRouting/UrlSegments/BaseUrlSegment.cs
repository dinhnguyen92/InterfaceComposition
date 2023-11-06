using InterfaceComposition.Utils;
using System.Web;

namespace InterfaceComposition.REST.ApiRouting.UrlSegments
{
    public abstract class BaseUrlSegment
    {
        public abstract string Text { get; }
        public abstract string Template { get; }

        public string NormalizedTemplate => Template
            .TrimLeadingPrefixIgnoreCase("/")
            .TrimTrailingSuffixIgnoreCase("/");

        public string UrlEncodedText => HttpUtility.UrlEncode(Text);
    }
}
