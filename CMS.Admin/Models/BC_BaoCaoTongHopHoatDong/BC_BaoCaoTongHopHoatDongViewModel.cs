using Business.Entities.Domain;
using System.Collections.Generic;

namespace CMS.Admin.Models
{
    public class BC_BaoCaoTongHopHoatDongViewModel
    {
        public BC_BaoCaoTongHopHoatDongParam Search { get; set; }
        public List<BC_BaoCaoTongHopHoatDongMap> Items { get; set; }
    }
}