using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.BC_ThongKeHoSoQuyenTacGia
{
    public class BC_ThongKeHoSoQuyenTacGiaViewModel
    {
        public BC_ThongKeHoSoQuyenTacGiaParam Search { get; set; }
        public List<BC_ThongKeHoSoQuyenTacGiaMap> Items { get; set; }
    }

}