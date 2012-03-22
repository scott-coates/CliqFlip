using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{
    #region Using Directives

	

	#endregion

    public class PropertyConvention : IPropertyConvention 
    {
    	public void Apply(IPropertyInstance instance)
    	{
			//Components have stupid default names
    		instance.Column(instance.Property.Name);
    	}
    }
}