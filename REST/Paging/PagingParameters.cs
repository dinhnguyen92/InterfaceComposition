using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.REST.Paging
{
    public class PagingParameters : UrlParameterList
    {
        public const int DEFAULT_MAX_PAGE_SIZE = 50;

        public int PageNumber { get; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(value, DEFAULT_MAX_PAGE_SIZE);
        }

        public PagingParameters(int pageNumber, int pageSize) : base(
            new UrlParameter(nameof(PageNumber), pageNumber),
            new UrlParameter(nameof(PageSize), pageSize))
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static PagingParameters DefaultFirstPageFilter => new(1, DEFAULT_MAX_PAGE_SIZE);

        public static PagingParameters GetNextPageFilter<T>(PagedList<T> currentPagedList)
        {
            return new PagingParameters(currentPagedList.CurrentPage + 1, currentPagedList.PageSize);
        }
    }
}
