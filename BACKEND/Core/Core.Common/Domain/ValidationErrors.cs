﻿using System.Collections.Generic;

namespace Core.Common.Domain
{
    public class ValidationErrors
    {        
        private readonly List<ValidationError> _errors;
       
        public IList<ValidationError> Items
        {
            get
            {
                return _errors;
            }
        }
        
        public ValidationErrors()
        {
            _errors = new List<ValidationError>();
        }
       
        public void Add(string propertyName)
        {
            _errors.Add(new ValidationError(propertyName, propertyName + " is required."));
        }
       
        public void Add(string propertyName, string errorMessage)
        {
            _errors.Add(new ValidationError(propertyName, errorMessage));
        }
       
        public void Add(ValidationError error)
        {
            _errors.Add(error);
        }
                
        public void AddRange(IList<ValidationError> errors)
        {
            _errors.AddRange(errors);
        }
       
        internal void Clear()
        {
            _errors.Clear();
        }
    }
}
