using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.DataFilters
{
    public interface IEntityUrlParamFilter
    {
        public bool IsEmpty();

        public UrlParameterList ToUrlParameters();
    }
}
