using Business.Entities.Domain;
using PagedList;
using System.Collections.Generic;

namespace CMS.Admin.Models.TT_CapQuyen
{
    public class TT_CapQuyenViewModel
    {
        public TT_CapQuyenParam Search { get; set; }
        public IPagedList<TT_CapQuyenMap> Items { get; set; }
    }

}