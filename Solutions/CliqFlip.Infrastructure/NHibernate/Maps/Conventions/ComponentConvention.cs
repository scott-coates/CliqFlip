using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{
	#region Using Directives

	

	#endregion

	public class ComponentConvention : IComponentConvention
	{
		public void Apply(IComponentInstance instance)
		{
			instance.Access.CamelCaseField(CamelCasePrefix.Underscore);
		}
	}
}