// ----------------------------------------------------------------------------
// <copyright file="UnitOfWorkInterceptionBehavior.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using NHibernate;

    /// <summary>
    /// Represents an interception behavior for a unit of work.
    /// </summary>
    public sealed class UnitOfWorkInterceptionBehavior : IInterceptionBehavior
    {
        #region Fields

        /// <summary>
        /// Defines the session factory.
        /// </summary>
        private readonly ISessionFactory sessionFactory;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkInterceptionBehavior"/> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        public UnitOfWorkInterceptionBehavior(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        /// This is used to optimize interception. If the behaviors won't actually
        /// do anything (for example, PIAB where no policies match) then the interception
        /// mechanism can be skipped completely.
        /// </remarks>
        /// <value>
        ///   <c>true</c> if this behavior will actually do anything when invoked; otherwise, <c>false</c>.
        /// </value>
        public bool WillExecute
        {
            get
            {
                return true;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        /// The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Execute the behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn methodReturn = null;

            // If there is a running transaction, just run the method.
            if (NHibernateUnitOfWork.Current != null || !this.RequiresDbConnection(input))
            {
                // Invoke the next behavior in the chain.
                methodReturn = getNext()(input, getNext);
            }
            else
            {
                try
                {
                    NHibernateUnitOfWork.Current = new NHibernateUnitOfWork(this.sessionFactory);
                    NHibernateUnitOfWork.Current.BeginTransaction();

                    try
                    {
                        // Invoke the next behavior in the chain.
                        methodReturn = getNext()(input, getNext);
                        NHibernateUnitOfWork.Current.Commit();
                    }
                    catch
                    {
                        try
                        {
                            NHibernateUnitOfWork.Current.Rollback();
                        }
                        catch
                        {
                        }

                        throw;
                    }
                }
                finally
                {
                    NHibernateUnitOfWork.Current = null;
                }
            }

            return methodReturn;
        }

        /// <summary>
        /// Returns a value indicating if this behavior requires a database connection.
        /// </summary>
        /// <param name="methodInvocation">The method invocation.</param>
        /// <returns>
        ///   <c>true</c> if this behavior requires a database connection; otherwise, <c>false</c>.
        /// </returns>
        private bool RequiresDbConnection(IMethodInvocation methodInvocation)
        {
            if (UnitOfWorkHelper.HasUnitOfWorkAttribute(methodInvocation))
            {
                return true;
            }

            if (UnitOfWorkHelper.IsRepositoryMethod(methodInvocation))
            {
                return true;
            }

            return false;
        }

        #endregion Methods
    }
}