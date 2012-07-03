using CliqFlip.Domain.Entities;
using System;
using CliqFlip.Infrastructure.NHibernate.Maps.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using SharpArch.Domain.DomainModel;
using SharpArch.NHibernate.FluentNHibernate;

namespace CliqFlip.Infrastructure.NHibernate.Maps
{
	#region Using Directives

	

	#endregion

	/// <summary>
	/// Generates the automapping for the domain assembly
	/// </summary>
	public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
	{
		public AutoPersistenceModel Generate()
		{
			var mappings = AutoMap.AssemblyOf<User>(new AutomappingConfiguration());
			mappings.IgnoreBase<Entity>();
			mappings.IgnoreBase(typeof(EntityWithTypedId<>));
			mappings.Conventions.Setup(GetConventions());
			mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
		    mappings.WriteMappingsTo("C:\\temp");

			return mappings;
		}

		private static Action<IConventionFinder> GetConventions()
		{
			return c => c.AddFromAssemblyOf<PrimaryKeyConvention>();
		}
	}
}