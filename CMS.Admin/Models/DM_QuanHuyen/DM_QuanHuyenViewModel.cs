using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_QuanHuyen
{
    public class DM_QuanHuyenViewModel
    {
        public DM_QuanHuyenMapParam Search { get; set; }
        public IPagedList<DM_QuanHuyenMap> Items { get; set; }
    }
}