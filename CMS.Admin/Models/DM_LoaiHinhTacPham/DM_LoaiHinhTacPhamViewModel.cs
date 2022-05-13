using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_LoaiHinhTacPham
{
    public class DM_LoaiHinhTacPhamViewModel
    {
        public DM_LoaiHinhTacPhamMapParam Search { get; set; }
        public IPagedList<DM_LoaiHinhTacPhamMap> Items { get; set; }
    }
}