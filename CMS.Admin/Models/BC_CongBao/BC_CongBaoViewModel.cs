using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.BC_CongBao
{
    public class BC_CongBaoViewModel
    {
        public BC_CongBaoParam Search { get; set; }
        public List<BC_CongBaoMap> Items { get; set; }
    }

}