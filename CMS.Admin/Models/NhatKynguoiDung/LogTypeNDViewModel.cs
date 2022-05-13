using PagedList;
using Business.Entities;
using Business.Entities.Domain;

namespace CMS.Admin.Models
{
    public class LogTypeNDViewModel
    {
        public LogTypeNDParam Search { get; set; }
        public IPagedList<DSLogTypeNDmap> Items { get; set; }
    }
}