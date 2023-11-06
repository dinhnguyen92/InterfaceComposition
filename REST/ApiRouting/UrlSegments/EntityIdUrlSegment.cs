namespace InterfaceComposition.REST.ApiRouting.UrlSegments
{
    public class EntityIdUrlSegment : ParameterizedUrlSegment
    {
        public const string ENTITY_ID_SEGMENT_PARAMETER_NAME = "id";

        public EntityIdUrlSegment(object entityId) :
            base(ENTITY_ID_SEGMENT_PARAMETER_NAME, entityId)
        {
        }
    }
}
