using Business.Entities.Domain;
using PagedList;
namespace CMS.Admin.Models
{
    public class DM_TinhThanhViewModel
    {
        public DM_TinhThanhMapParam Search { get; set; }
        public IPagedList<DM_TinhThanhMap> Items { get; set; }
    }
}