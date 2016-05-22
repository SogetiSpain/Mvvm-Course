// ----------------------------------------------------------------------------
// <copyright file="NavigationItem.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.ViewModels
{
    using System.Windows.Input;
    using Events;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    /// <summary>
    /// Represents a navigation item.
    /// </summary>
    public class NavigationItem : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the event aggregator.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Defines the display member.
        /// </summary>
        private string displayMember;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationItem" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="displayMember">The display member.</param>
        public NavigationItem(IEventAggregator eventAggregator, int id, string displayMember)
        {
            this.eventAggregator = eventAggregator;

            this.DisplayMember = displayMember;
            this.Id = id;

            this.OpenTeamMemberEditViewCommand = new DelegateCommand(this.OnOpenTeamMemberEditView);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the display member.
        /// </summary>
        /// <value>
        /// The display member.
        /// </value>
        public string DisplayMember
        {
            get
            {
                return this.displayMember;
            }

            set
            {
                this.SetProperty(ref this.displayMember, value);
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
            get;
            private set;
        }

        /// <summary>
        /// Gets the open team member edit view command.
        /// </summary>
        /// <value>
        /// The open team member edit view command.
        /// </value>
        public ICommand OpenTeamMemberEditViewCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Called when the user request to open the team member edit view.
        /// </summary>
        private void OnOpenTeamMemberEditView()
        {
            this.eventAggregator
                .GetEvent<OpenTeamMemberEditViewEvent>()
                .Publish(this.Id);
        }

        #endregion Methods
    }
}