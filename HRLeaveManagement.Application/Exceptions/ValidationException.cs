using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRLeaveManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();

        public ValidationException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                ErrorMessages.Add(error.ErrorMessage);
            }
        }
    }
}
