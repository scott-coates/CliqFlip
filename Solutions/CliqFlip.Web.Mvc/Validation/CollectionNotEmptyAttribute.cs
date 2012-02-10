using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Validation
{
	//http://stackoverflow.com/questions/4747184/perform-client-side-validation-for-custom-attribute/4782235#4782235
	public class CollectionNotEmptyAttribute : ValidationAttribute, IClientValidatable
	{
		public override bool IsValid(object value)
		{
			bool retVal = true;
			
			var col = value as IEnumerable;

			if(col != null)
			{
				retVal = col.Cast<object>().Any();
			}
			
			return retVal;
		} 
		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientValidationRule
			{
				ErrorMessage = ErrorMessage,
				ValidationType = "collectionNotEmpty"
			};
		}
	}
}