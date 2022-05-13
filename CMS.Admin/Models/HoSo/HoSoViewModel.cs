using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.HoSo
{
    public class HoSoViewModel
    {
        public HoSoParam Search { get; set; }
        public DSHoSo_1CuaMap Items { get; set; }
    }

}