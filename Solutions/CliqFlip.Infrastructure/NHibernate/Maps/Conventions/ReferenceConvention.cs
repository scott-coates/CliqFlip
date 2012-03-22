using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{

	#region Using Directives

	#endregion

	public class ReferenceConvention : IReferenceConvention
	{
		#region IReferenceConvention Members

		public void Apply(IManyToOneInstance instance)
		{
			//http://stackoverflow.com/a/664076/173957
			instance.Cascade.SaveUpdate();
		}

		#endregion
	}
}