using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models.DM_QuyenChucNang
{
    public class DM_QuyenChucNangViewModel
    {
        public DM_QuyenChucNangMapParam Search { get; set; }
        public IPagedList<DM_QuyenChucNangMap> Items { get; set; }
    }
}