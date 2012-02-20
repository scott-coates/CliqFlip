using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class EmailAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                try
                {
					//TODO: MVC3 Futures already has this - we should probably just use that
                    new MailAddress(value.ToString());
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule { ValidationType = "email", ErrorMessage = FormatErrorMessage(metadata.DisplayName) };
        }
    }
}