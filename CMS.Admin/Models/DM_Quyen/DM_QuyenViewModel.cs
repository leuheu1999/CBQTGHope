using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_Quyen
{
    public class DM_QuyenViewModel
    {
        public DM_QuyenMapParam Search { get; set; }
        public IPagedList<DM_QuyenMap> Items { get; set; }
    }
}