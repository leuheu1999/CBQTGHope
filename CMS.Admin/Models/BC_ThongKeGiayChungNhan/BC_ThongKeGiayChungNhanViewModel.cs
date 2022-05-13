using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.BC_ThongKeGiayChungNhan
{
    public class BC_ThongKeGiayChungNhanViewModel
    {
        public BC_ThongKeGiayChungNhanParam Search { get; set; }
        public List<BC_ThongKeGiayChungNhanMap> Items { get; set; }
    }

}