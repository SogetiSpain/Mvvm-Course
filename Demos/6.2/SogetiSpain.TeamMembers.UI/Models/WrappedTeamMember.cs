// ----------------------------------------------------------------------------
// <copyright file="WrappedTeamMember.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.Models
{
    using System;
    using System.Runtime.CompilerServices;
    using Model;
    using Prism.Mvvm;

    /// <summary>
    /// Represents a wrapped team member.
    /// </summary>
    public class WrappedTeamMember : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the model.
        /// </summary>
        private readonly TeamMember model;

        /// <summary>
        /// Defines the value indicating whether this instance is changed.
        /// </summary>
        private bool isChanged;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WrappedTeamMember"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public WrappedTeamMember(TeamMember model)
        {
            this.model = model;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>
        /// The birthday.
        /// </value>
        public DateTime? Birthday
        {
            get
            {
                return this.model.Birthday;
            }

            set
            {
                if (this.model.Birthday != value)
                {
                    this.model.Birthday = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get
            {
                return this.model.FirstName;
            }

            set
            {
                if (this.model.FirstName != value)
                {
                    this.model.FirstName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get
            {
                return this.model.Id;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is developer.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is developer; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeveloper
        {
            get
            {
                return this.model.IsDeveloper;
            }

            set
            {
                if (this.model.IsDeveloper != value)
                {
                    this.model.IsDeveloper = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is changed; otherwise, <c>false</c>.
        /// </value>
        public bool IsChanged
        {
            get
            {
                return this.isChanged;
            }

            private set
            {
                this.SetProperty(ref this.isChanged, value);
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get
            {
                return this.model.LastName;
            }

            set
            {
                if (this.model.LastName != value)
                {
                    this.model.LastName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public TeamMember Model
        {
            get
            {
                return this.model;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Accepts the changes.
        /// </summary>
        public void AcceptChanges()
        {
            this.IsChanged = false;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName != nameof(this.IsChanged))
            {
                this.IsChanged = true;
            }
        }

        #endregion Methods
    }
}