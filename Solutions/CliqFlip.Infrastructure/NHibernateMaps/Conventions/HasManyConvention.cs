using FluentNHibernate.Conventions.Inspections;

namespace CliqFlip.Infrastructure.NHibernateMaps.Conventions
{
	#region Using Directives

	using FluentNHibernate.Conventions;

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