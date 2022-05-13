using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models
{
    public class TT_TacGiaViewModel
    {
        public TT_TacGiaParam Search { get; set; }
        public IPagedList<TT_TacGiaMap> List { get; set; }
    }
}