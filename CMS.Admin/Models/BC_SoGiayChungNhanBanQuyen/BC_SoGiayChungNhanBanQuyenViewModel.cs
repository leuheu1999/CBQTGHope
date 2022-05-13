using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.BC_SoGiayChungNhanBanQuyen
{
    public class BC_SoGiayChungNhanBanQuyenViewModel
    {
        public BC_SoGiayChungNhanBanQuyenParam Search { get; set; }
        public List<BC_SoGiayChungNhanBanQuyenMap> Items { get; set; }
    }

}