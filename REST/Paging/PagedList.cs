using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace InterfaceComposition.REST.Paging
{
    public class PagedList<T>
    {
        public int CurrentPage { get; private set; }
        public int? TotalPageCount { get; private set; }
        public int PageSize { get; private set; }
        public int? TotalItemCount { get; private set; }
        public IEnumerable<T> Items { get; private set; }
        public bool HasNext
        {
            get
            {
                if (TotalPageCount.HasValue) return CurrentPage < TotalPageCount;
                if (TotalItemCount.HasValue) return CurrentPage * PageSize < TotalItemCount;
                return Items.Any();
            }
        }

        [JsonConstructor]
        public PagedList(IEnumerable<T> items, int? totalItemCount, int currentPage, int pageSize, int? totalPageCount)
        {
            Items = items;
            TotalItemCount = totalItemCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPageCount = totalPageCount;
        }

        public PagedList(IEnumerable<T> items, int? totalItemCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalItemCount = totalItemCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPageCount = totalItemCount.HasValue ? (int)Math.Ceiling(totalItemCount.Value / (double)pageSize) : null;
        }

        public PagedList<DerivedType> SelectPagedList<DerivedType>(Func<T, DerivedType> selector)
        {
            var selectedItems = Items.Select(selector);
            return new PagedList<DerivedType>(selectedItems, selectedItems.Count(), CurrentPage, PageSize);
        }

        public static Task<PagedList<T>> CreateAsync(
            IEnumerable<T> source,
            PagingParameters pagingParams)
        {
            return CreateAsync(source, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public static Task<PagedList<T>> CreateAsync(
            IEnumerable<T> source,
            int currentPage,
            int pageSize)
        {
            return Task.Run(() =>
            {
                var totalItemCount = source.Count();
                var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new PagedList<T>(items, totalItemCount, currentPage, pageSize);
            });
        }

        public static async Task<PagedList<T>> CreateAsync(
            IAsyncEnumerable<T> source,
            PagingParameters pagingParams,
            Func<T, bool> filterPredicate,
            CancellationToken cancellationToken)
        {
            return await CreateAsync(source, pagingParams.PageNumber, pagingParams.PageSize, filterPredicate, cancellationToken);
        }

        public static async Task<PagedList<T>> CreateAsync(
            IAsyncEnumerable<T> source,
            int currentPage,
            int pageSize,
            Func<T, bool> filterPredicate,
            CancellationToken cancellationToken)
        {
            var numItemsToSkip = (currentPage - 1) * pageSize;
            var numItemsSkipped = 0;
            var items = new List<T>();

            await foreach (var item in source.WithCancellation(cancellationToken))
            {
                if (items.Count >= pageSize)
                {
                    break;
                }
                else if (numItemsSkipped == numItemsToSkip && filterPredicate(item))
                {
                    items.Add(item);
                }
                else
                {
                    numItemsSkipped++;
                }
            }

            return new PagedList<T>(items, null, currentPage, pageSize);
        }
    }
}
