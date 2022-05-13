using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_LoaiHinhDangKy
{
    public class DM_LoaiHinhDangKyViewModel
    {
        public DM_LoaiHinhDangKyMapParam Search { get; set; }
        public IPagedList<DM_LoaiHinhDangKyMap> Items { get; set; }
    }
}