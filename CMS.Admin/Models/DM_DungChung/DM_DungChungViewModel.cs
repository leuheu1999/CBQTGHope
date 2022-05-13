using Business.Entities.Domain;
using PagedList;

namespace CMS.Admin.Models
{
    public class DM_DungChungViewModel
    {
        public DM_DungChungMapParam Search { get; set; }
        public IPagedList<DM_DungChungMap> List{ get; set; }
    }
}