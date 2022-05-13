using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Domain.Print
{
   public class E_ParametersMap
    {
        public string TypePrint { get; set; }
        public string Path { get; set; }
        public string Procdures { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string StartColumns { get; set; }
        public string EndColumns { get; set; }
        public int TotalCoumns { get; set; }
        public int RowStart { get; set; }
    }
}
