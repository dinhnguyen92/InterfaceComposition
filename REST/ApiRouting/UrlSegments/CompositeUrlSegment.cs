using System.Collections.Generic;
using System.Linq;

namespace InterfaceComposition.REST.ApiRouting.UrlSegments
{
    public class CompositeUrlSegment : BaseUrlSegment
    {
        private List<BaseUrlSegment> SubSegments { get; }

        public CompositeUrlSegment()
        {
            SubSegments = new List<BaseUrlSegment>();
        }

        public CompositeUrlSegment(List<BaseUrlSegment> subSegments)
        {
            SubSegments = subSegments;
        }

        public CompositeUrlSegment(params BaseUrlSegment[] subSegments) : this(subSegments.ToList())
        {
        }

        public override string Template =>
            string.Join("/", SubSegments.Select(segment => segment.NormalizedTemplate));

        public override string Text =>
            string.Join("/", SubSegments.Select(segment => segment.UrlEncodedText));

        public CompositeUrlSegment AddSegment(BaseUrlSegment segment) =>
            new (new List<BaseUrlSegment>(SubSegments) { segment });

        public bool IsEmpty() => !SubSegments.Any();

        public List<ParameterizedUrlSegment> GetParameterizedSegments() =>
            SubSegments.OfType<ParameterizedUrlSegment>().ToList();
    }
}
