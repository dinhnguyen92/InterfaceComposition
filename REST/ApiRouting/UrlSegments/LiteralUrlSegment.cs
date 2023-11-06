namespace InterfaceComposition.REST.ApiRouting.UrlSegments
{
    public class LiteralUrlSegment : BaseUrlSegment
    {
        public string SegmentName { get; }
        public override string Template => SegmentName;
        public override string Text => SegmentName;

        public LiteralUrlSegment(string segmentName)
        {
            SegmentName = segmentName;
        }

        public const string ENTITY_EXISTENCE_CHECK_SEGMENT_NAME = "exists";
        public static LiteralUrlSegment EntityExistenceCheckUrlSegment =>
            new (ENTITY_EXISTENCE_CHECK_SEGMENT_NAME);

        public const string ENTITY_SEARCH_SEGMENT_NAME = "search";
        public static LiteralUrlSegment EntitySearchUrlSegment =>
            new (ENTITY_SEARCH_SEGMENT_NAME);

        public const string SERVICE_HEALTH_CHECK_SEGMENT_NAME = "check";
        public static LiteralUrlSegment ServiceHealthCheckUrlSegment =>
            new (SERVICE_HEALTH_CHECK_SEGMENT_NAME);
    }
}
