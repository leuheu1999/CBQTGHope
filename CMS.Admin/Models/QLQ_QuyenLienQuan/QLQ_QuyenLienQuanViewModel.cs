using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models
{
    public class QLQ_QuyenLienQuanViewModel
    {
        public QLQ_QuyenLienQuanParam Search { get; set; }
        public IPagedList<QLQ_QuyenLienQuanMap> List { get; set; }
    }
}