using FluentNHibernate.Conventions;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{
    #region Using Directives

	

	#endregion

    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IIdentityInstance instance)
        {
            instance.Column("Id");
			instance.UnsavedValue("0");
			instance.GeneratedBy.Identity();
        }
    }
}