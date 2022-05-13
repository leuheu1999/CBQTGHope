using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.BC_BaoCaoTacPham
{
    public class BC_BaoCaoTacPhamViewModel
    {
        public BC_BaoCaoTacPhamParam Search { get; set; }
        public List<BC_BaoCaoTacPhamMap> Items { get; set; }
    }

}