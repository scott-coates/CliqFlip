using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace CliqFlip.Infrastructure.NHibernate.Maps.Conventions
{
    #region Using Directives

	

	#endregion

    public class CustomForeignKeyConvention : ForeignKeyConvention 
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
            {
                return type.Name + "Id";
            }

            return property.Name + "Id";  
        }
    }
}