using PagedList;
using Business.Entities;
using Business.Entities.Domain;

namespace CMS.Admin.Models
{
    public class NguoiDungHeThongViewModel
    {
        public NguoiDungHeThongMapParam Search { get; set; }
        public IPagedList<NguoiDungHeThongMap> Items { get; set; }
    }
}