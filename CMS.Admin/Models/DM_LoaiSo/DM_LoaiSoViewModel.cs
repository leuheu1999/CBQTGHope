using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_LoaiSo
{
    public class DM_LoaiSoViewModel
    {
        public DM_LoaiSoMapParam Search { get; set; }
        public IPagedList<DM_LoaiSoMap> Items { get; set; }
    }
}