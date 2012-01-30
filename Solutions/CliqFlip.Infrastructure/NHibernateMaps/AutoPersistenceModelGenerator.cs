﻿using CliqFlip.Domain.Entities;

namespace CliqFlip.Infrastructure.NHibernateMaps
{
	#region Using Directives

	using System;

	using Conventions;

	using Domain;

	using FluentNHibernate.Automapping;
	using FluentNHibernate.Conventions;

	using SharpArch.Domain.DomainModel;
	using SharpArch.NHibernate.FluentNHibernate;

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

			return mappings;
		}

		private static Action<IConventionFinder> GetConventions()
		{
			return c => c.AddFromAssemblyOf<PrimaryKeyConvention>();
		}
	}
}