using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models
{
    public class DVC_QuyenLienQuanViewModel
    {
        public DVC_QLQ_QuyenLienQuanParam Search { get; set; }
        public IPagedList<DVC_QLQ_QuyenLienQuanMap> List { get; set; }
    }
}