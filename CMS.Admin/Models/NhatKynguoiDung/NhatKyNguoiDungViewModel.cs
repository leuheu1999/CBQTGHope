using PagedList;
using Business.Entities;
using Business.Entities.Domain;

namespace CMS.Admin.Models
{
    public class NhatKyNguoiDungViewModel
    {
        public ND_NhatKyNguoiDungParam Search { get; set; }
        public IPagedList<ND_NhatKyNguoiDungMap> Items { get; set; }
    }
}