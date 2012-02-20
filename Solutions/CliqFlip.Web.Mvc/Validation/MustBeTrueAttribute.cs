using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Validation
{
	/// <summary>
	/// Validation attribute that demands that a boolean value must be true.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class MustBeTrueAttribute : ValidationAttribute
	{
		//http://stackoverflow.com/questions/2245185/how-to-handle-booleans-checkboxes-in-asp-net-mvc-2-with-dataannotations
		public override bool IsValid(object value)
		{
			return value != null && value is bool && (bool)value;
		}
	}
}