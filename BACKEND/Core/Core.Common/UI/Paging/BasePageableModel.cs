//Contributor : MVCContrib
using System;
namespace Core.Common.UI.Paging
{
    public abstract class BasePageableModel : IPageableModel
    {
        #region Methods

        public virtual void LoadPagedList(PagedList pagedList)
        {
            FirstItem = (pagedList.PageIndex * pagedList.PageSize) + 1;
            HasNextPage = pagedList.HasNextPage;
            HasPreviousPage = pagedList.HasPreviousPage;
            LastItem = Math.Min(pagedList.TotalCount, ((pagedList.PageIndex * pagedList.PageSize) + pagedList.PageSize));
            PageNumber = pagedList.PageIndex + 1;
            PageSize = pagedList.PageSize;
            TotalItems = pagedList.TotalCount;
            if(pagedList.TotalCount > 0 && pagedList.PageSize > 0)
            {
                TotalPages = pagedList.TotalCount/pagedList.PageSize;
                if (pagedList.TotalCount % pagedList.PageSize > 0)
                    TotalPages++;
            }
            else
            {
                TotalPages = 0;
            }
            
        }

        #endregion

        #region Properties

        public int FirstItem { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public int LastItem { get; set; }

        public int PageIndex
        {
            get
            {
                if (PageNumber > 0)
                    return PageNumber - 1;
                
                return 0;
            }
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        #endregion
    }
}