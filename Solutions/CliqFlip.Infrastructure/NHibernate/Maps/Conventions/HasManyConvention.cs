using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{
	#region Using Directives

	

	#endregion

	public class HasManyConvention : IHasManyConvention
	{
		public void Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
		{
			instance.Key.Column(instance.EntityType.Name + "Id");
			instance.Cascade.AllDeleteOrphan();
			instance.Inverse();
			instance.Access.CamelCaseField(CamelCasePrefix.Underscore);//http://stackoverflow.com/questions/781443/private-collection-mapping-in-fluent-nhibernate
		}
	}
}