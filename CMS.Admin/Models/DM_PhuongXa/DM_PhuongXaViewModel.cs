using PagedList;
using Business.Entities;
using Business.Entities.Domain;

namespace CMS.Admin.Models
{
    public class DM_PhuongXaViewModel
    {
        public DM_PhuongXaMapParam Search { get; set; }
        public IPagedList<DM_PhuongXaMap> Items { get; set; }
    }
}