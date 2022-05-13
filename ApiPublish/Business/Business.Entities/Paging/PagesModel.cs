using Core.Common.Domain;

namespace Business.Entities
{
    public class PagesParamModel
    {
        private int pageSize;
        private int pageIndex;

        public int PageSize
        {
            get
            {
                return pageSize;
            }

            set
            {
                pageSize = value;
            }
        }

        public int PageIndex
        {
            get
            {
                return pageIndex;
            }

            set
            {
                pageIndex = value;
            }
        }
    }

    public class PagesModel : EntityBase
    {
        private int totalCount;
        private int pageSize;
        private int pageIndex;
        public int TotalCount
        {
            get
            {
                return totalCount;
            }

            set
            {
                totalCount = value;
            }
        }

        public int PageSize
        {
            get
            {
                return pageSize;
            }

            set
            {
                pageSize = value;
            }
        }

        public int PageIndex
        {
            get
            {
                return pageIndex;
            }

            set
            {
                pageIndex = value;
            }
        }
    }
}
