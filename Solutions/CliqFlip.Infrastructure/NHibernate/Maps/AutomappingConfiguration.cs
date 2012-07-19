using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using SharpArch.Domain.DomainModel;

namespace CliqFlip.Infrastructure.NHibernate.Maps
{
	public class AutomappingConfiguration : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap(Type type)
		{
			return type.GetInterfaces().Any(x =>
			                                x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IEntityWithTypedId<>));
		}

		public override bool ShouldMap(Member member)
		{
			Member backingField;
			return base.ShouldMap(member) && (member.CanWrite || member.TryGetBackingField(out backingField));
		}

		public override bool AbstractClassIsLayerSupertype(Type type)
		{
			return type == typeof (EntityWithTypedId<>) || type == typeof (Entity);
		}

		public override bool IsId(Member member)
		{
			return member.Name == "Id";
		}

		public override bool IsComponent(Type type)
		{
			return type.BaseType == typeof (ValueObject);
		}

        public override string GetComponentColumnPrefix(Member member)
        {
            return ""; //Don't do things like Address_City table names
        }
	}
}