using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.BC_ThongKeHoSoQuyenLienQuan
{
    public class BC_ThongKeHoSoQuyenLienQuanViewModel
    {
        public BC_ThongKeHoSoQuyenLienQuanParam Search { get; set; }
        public List<BC_ThongKeHoSoQuyenLienQuanMap> Items { get; set; }
    }

}