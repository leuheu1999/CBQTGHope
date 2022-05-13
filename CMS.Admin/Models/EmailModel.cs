using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Admin.Models
{
    public class EmailModel
    {
        public string Subject;
        public string SendTo;
        public string TemplateMail;
        public Dictionary<string, string> Params;
    }
}