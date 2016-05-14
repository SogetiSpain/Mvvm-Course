// ----------------------------------------------------------------------------
// <copyright file="NHibernateUnitOfWork.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using System;
    using Domain;
    using NHibernate;

    /// <summary>
    /// Represents an unit of work for NHibernate.
    /// </summary>
    public sealed class NHibernateUnitOfWork : IUnitOfWork
    {
        #region Fields

        /// <summary>
        /// Defines the current unit of work.
        /// </summary>
        [ThreadStatic]
        private static NHibernateUnitOfWork current;

        /// <summary>
        /// Defines the session factory.
        /// </summary>
        private readonly ISessionFactory sessionFactory;

        /// <summary>
        /// Defines the transaction.
        /// </summary>
        private ITransaction transaction;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateUnitOfWork"/> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the current unit of work.
        /// </summary>
        /// <value>
        /// The current unit of work.
        /// </value>
        public static NHibernateUnitOfWork Current
        {
            get
            {
                return NHibernateUnitOfWork.current;
            }

            set
            {
                NHibernateUnitOfWork.current = value;
            }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public ISession Session
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Opens database connection and begins transaction.
        /// </summary>
        public void BeginTransaction()
        {
            this.Session = this.sessionFactory.OpenSession();
            this.transaction = this.Session.BeginTransaction();
        }

        /// <summary>
        /// Commits transaction and closes database connection.
        /// </summary>
        public void Commit()
        {
            try
            {
                this.transaction.Commit();
            }
            finally
            {
                this.Session.Close();
            }
        }

        /// <summary>
        /// Rollbacks transaction and closes database connection.
        /// </summary>
        public void Rollback()
        {
            try
            {
                this.transaction.Rollback();
            }
            finally
            {
                this.Session.Close();
            }
        }

        #endregion Methods
    }
}