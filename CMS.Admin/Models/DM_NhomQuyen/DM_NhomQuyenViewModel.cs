using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_NhomQuyen
{
    public class DM_NhomQuyenViewModel
    {
        public DM_NhomQuyenParam Search { get; set; }
        public IPagedList<DM_NhomQuyenMap> Items { get; set; }
    }
}