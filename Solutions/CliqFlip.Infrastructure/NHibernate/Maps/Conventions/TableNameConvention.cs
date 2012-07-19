using FluentNHibernate.Conventions;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{
    #region Using Directives

	

	#endregion

    public class TableNameConvention : IClassConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
            instance.Table(instance.EntityType.Name.InflectTo().Pluralized);			
        }
    }
}