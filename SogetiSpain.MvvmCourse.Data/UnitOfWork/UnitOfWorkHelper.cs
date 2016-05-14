// ----------------------------------------------------------------------------
// <copyright file="UnitOfWorkHelper.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Domain;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Represents an unit of work helper.
    /// </summary>
    public static class UnitOfWorkHelper
    {
        #region Properties

        /// <summary>
        /// Gets the method parameters function.
        /// </summary>
        /// <value>
        /// The method parameters function.
        /// </value>
        private static Func<IMethodInvocation, Type[]> MethodParametersFunc
        {
            get
            {
                return new Func<IMethodInvocation, Type[]>(
                    methodInvocation =>
                    {
                        ParameterInfo[] methodParameters = methodInvocation.MethodBase.GetParameters();

                        return methodParameters.OrderBy(e => e.Position)
                                               .Select(e => e.ParameterType)
                                               .ToArray();
                    });
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Determines whether the specified method invocation has an unit of work attribute.
        /// </summary>
        /// <param name="methodInvocation">The method invocation.</param>
        /// <returns>
        ///   <c>true</c> if the specified method invocation has an unit of work attribute, otherwise, <c>false</c>.
        /// </returns>
        public static bool HasUnitOfWorkAttribute(IMethodInvocation methodInvocation)
        {
            string methodName = methodInvocation.MethodBase.Name;
            Type[] methodParameters = methodInvocation.MethodBase.GetParameters()
                                      .OrderBy(e => e.Position)
                                      .Select(e => e.ParameterType)
                                      .ToArray();
            MethodInfo methodInfo = methodInvocation
                                    .Target
                                    .GetType()
                                    .GetMethod(methodName, methodParameters);
            /*
            var methodInfo = methodInvocation.Target.GetType().GetMethod(
                methodName, UnitOfWorkHelper.MethodParameters(methodInvocation));
            */
            bool result = methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);

            return result;

            ////return methodInvocation.MethodBase.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        /// <summary>
        /// Determines whether the specified type is a repository class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is a repository class; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRepositoryClass(Type type)
        {
            return typeof(IRepository).IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified method invocation is a repository method.
        /// </summary>
        /// <param name="methodInvocation">The method invocation.</param>
        /// <returns>
        ///   <c>true</c> if the specified method invocation is a repository method.
        /// </returns>
        public static bool IsRepositoryMethod(IMethodInvocation methodInvocation)
        {
            return UnitOfWorkHelper.IsRepositoryClass(methodInvocation.MethodBase.DeclaringType);
        }

        #endregion Methods
    }
}