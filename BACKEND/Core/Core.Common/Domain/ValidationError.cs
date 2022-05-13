using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Domain
{
    public class ValidationError
    {        
        public string PropertyName { get; set; }
       
        public string Message { get; set; }
        
        public ValidationError(string propertyName, string message)
        {
            this.PropertyName = propertyName;
            this.Message = message;
        }
    }
}
