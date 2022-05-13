using System.Collections.Generic;

namespace Core.Common.Utilities
{
    public class PagedData<T>
    {
        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public PagedData()
        {

        }
        public PagedData(List<T> data, int totalItems, int currentPageIndex, int pageSize)
        {
            Data = data;
            TotalItems = totalItems;
            PageIndex = currentPageIndex;
            PageSize = pageSize;
        }
    }
}
