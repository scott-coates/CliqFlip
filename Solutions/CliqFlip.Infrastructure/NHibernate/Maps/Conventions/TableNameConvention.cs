using FluentNHibernate.Conventions;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{
    #region Using Directives

	

	#endregion

    public class TableNameConvention : IClassConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
			//instance.Table(Inflector.Net.Inflector.Singularize(instance.EntityType.Name));
			instance.Table(Inflector.Net.Inflector.Pluralize(instance.EntityType.Name));
        }
    }
}