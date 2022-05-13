using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_CoQuanCap
{
    public class DM_CoQuanCapViewModel
    {
        public DM_CoQuanCapMapParam Search { get; set; }
        public IPagedList<DM_CoQuanCapMap> Items { get; set; }

    }
}