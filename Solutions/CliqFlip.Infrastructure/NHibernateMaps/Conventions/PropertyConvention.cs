using FluentNHibernate.Conventions.Instances;

namespace CliqFlip.Infrastructure.NHibernateMaps.Conventions
{
    #region Using Directives

    using System;

    using FluentNHibernate;
    using FluentNHibernate.Conventions;

    #endregion

    public class PropertyConvention : IPropertyConvention 
    {
    	public void Apply(IPropertyInstance instance)
    	{
    		instance.Column(instance.Property.Name);
    	}
    }
}