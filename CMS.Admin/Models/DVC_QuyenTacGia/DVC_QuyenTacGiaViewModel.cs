using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models
{
    public class DVC_QuyenTacGiaViewModel
    {
        public DVC_QTG_QuyenTacGiaParam Search { get; set; }
        public IPagedList<DVC_QTG_QuyenTacGiaMap> List { get; set; }
    }
}