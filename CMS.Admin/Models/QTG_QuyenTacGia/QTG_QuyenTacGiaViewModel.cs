using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models
{
    public class QTG_QuyenTacGiaViewModel
    {
        public QTG_QuyenTacGiaParam Search { get; set; }
        public IPagedList<QTG_QuyenTacGiaMap> List { get; set; }
    }
}