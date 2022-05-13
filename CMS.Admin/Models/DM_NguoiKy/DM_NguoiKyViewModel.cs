using Business.Entities.Domain;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Admin.Models.DM_NguoiKy
{
    public class DM_NguoiKyViewModel
    {
        public DM_NguoiKyMapParam Search { get; set; }
        public IPagedList<DM_NguoiKyMap> Items { get; set; }
    }
}