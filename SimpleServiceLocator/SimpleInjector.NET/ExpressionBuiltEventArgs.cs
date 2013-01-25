﻿#region Copyright (c) 2010 S. van Deursen
/* The Simple Injector is an easy-to-use Inversion of Control library for .NET
 * 
 * Copyright (C) 2010 S. van Deursen
 * 
 * To contact me, please visit my blog at http://www.cuttingedge.it/blogs/steven/ or mail to steven at 
 * cuttingedge.it.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO 
 * EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE 
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

namespace SimpleInjector
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using SimpleInjector.Analysis;
    using SimpleInjector.Lifestyles;

    /// <summary>
    /// Provides data for and interaction with the 
    /// <see cref="Container.ExpressionBuilt">ExpressionBuilt</see> event of 
    /// the <see cref="Container"/>. An observer can change the 
    /// <see cref="ExpressionBuiltEventArgs.Expression"/> property to change the registered type.
    /// </summary>
    [DebuggerDisplay("ExpressionBuiltEventArgs (RegisteredServiceType: " + 
        "{SimpleInjector.Helpers.ToFriendlyName(RegisteredServiceType),nq}, Expression: {Expression})")]
    public class ExpressionBuiltEventArgs : EventArgs
    {
        private Expression expression;
        private Lifestyle lifestyle;

        /// <summary>Initializes a new instance of the <see cref="ExpressionBuiltEventArgs"/> class.</summary>
        /// <param name="registeredServiceType">Type of the registered service.</param>
        /// <param name="expression">The registered expression.</param>
        public ExpressionBuiltEventArgs(Type registeredServiceType, Expression expression)
        {
            this.RegisteredServiceType = registeredServiceType;

            this.expression = expression;
        }

        /// <summary>Gets the registered service type that is currently requested.</summary>
        /// <value>The registered service type that is currently requested.</value>
        public Type RegisteredServiceType { get; private set; }

        /// <summary>Gets or sets the currently registered <see cref="Expression"/>.</summary>
        /// <value>The current registration.</value>
        /// <exception cref="ArgumentNullException">Thrown when the supplied value is a null reference.</exception>
        public Expression Expression
        {
            get
            {
                return this.expression;
            }

            set
            {
                Requires.IsNotNull(value, "value");

                this.expression = value;
            }
        }

        // TODO: Instead of registering Lifestyle and Expression, we might simplify things by allowing
        // a Registration to be registered.
        /// <summary>Gets or sets the current lifestyle of the registration.</summary>
        /// <value>The original lifestyle of the registration.</value>
        public Lifestyle Lifestyle
        {
            get
            {
                return this.lifestyle;
            }

            set
            {
                Requires.IsNotNull(value, "value");

                this.lifestyle = value;
            }
        }

        /// <summary>
        /// Gets the list dependencies of the registration that are currently known. This collection can be
        /// altered to reflect the situation when the 
        /// <see cref="ExpressionBuiltEventArgs.Expression">Expression</see> property has been replaced.
        /// </summary>
        /// <value>The list dependencies of the registration that are currently known.</value>
        public Collection<KnownRelationship> KnownRelationships { get; internal set; }
    }
}