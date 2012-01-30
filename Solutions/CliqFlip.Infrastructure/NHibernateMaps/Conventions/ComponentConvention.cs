using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CliqFlip.Infrastructure.NHibernateMaps.Conventions
{
	#region Using Directives

	using System;

	using FluentNHibernate;
	using FluentNHibernate.Conventions;

	#endregion

	public class ComponentConvention : IComponentConvention
	{
		public void Apply(IComponentInstance instance)
		{
			instance.Access.CamelCaseField(CamelCasePrefix.Underscore);
		}
	}
}