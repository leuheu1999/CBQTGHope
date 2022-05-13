using Business.Entities.Domain;
using PagedList;
namespace CMS.Admin.Models
{
    public class DM_QuocGiaViewModel
    {
        public DM_QuocGiaMapParam Search { get; set; }
        public IPagedList<DM_QuocGiaMap> List{ get; set; }
    }
}