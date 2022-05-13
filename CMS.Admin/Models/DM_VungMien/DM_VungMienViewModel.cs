using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_VungMien
{
    public class DM_VungMienViewModel
    {
        public DM_VungMienMapParam Search { get; set; }
        public IPagedList<DM_VungMienMap> Items { get; set; }
    }
}