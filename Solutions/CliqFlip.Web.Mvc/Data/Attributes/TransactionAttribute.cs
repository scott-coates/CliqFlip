using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NHibernate;
using SharpArch.NHibernate;

namespace CliqFlip.Web.Mvc.Data.Attributes
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     Optionally holds the factory key to be used when beginning/committing a transaction
        /// </summary>
        private readonly string factoryKey;

        /// <summary>
        ///     When used, assumes the <see cref = "factoryKey" /> to be NHibernateSession.DefaultFactoryKey
        /// </summary>
        public TransactionAttribute()
        {
        }

        /// <summary>
        ///     Overrides the default <see cref = "factoryKey" /> with a specific factory key
        /// </summary>
        public TransactionAttribute(string factoryKey)
        {
            this.factoryKey = factoryKey;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            NHibernateSession.CurrentFor(GetEffectiveFactoryKey()).BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string effectiveFactoryKey = GetEffectiveFactoryKey();

            ITransaction currentTransaction = NHibernateSession.CurrentFor(effectiveFactoryKey).Transaction;

            try
            {
                if (currentTransaction.IsActive)
                {
                    if (actionExecutedContext.Exception != null)
                    {
                        currentTransaction.Rollback();
                    }
                    else
                    {
                        currentTransaction.Commit();
                    }
                }
            }
            finally
            {
                currentTransaction.Dispose();
            }
        }

        private string GetEffectiveFactoryKey()
        {
            return String.IsNullOrEmpty(factoryKey) ? SessionFactoryKeyHelper.GetKey() : factoryKey;
        }
    }
}