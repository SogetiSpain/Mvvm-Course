// ----------------------------------------------------------------------------
// <copyright file="NavigationViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using DataProviders;
    using Events;
    using Model;
    using Prism.Events;

    /// <summary>
    /// Represents the view model for navigation view.
    /// </summary>
    public class NavigationViewModel : INavigationViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the data provider.
        /// </summary>
        private readonly INavigationDataProvider dataProvider;

        /// <summary>
        /// Defines the event aggregator.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewModel"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public NavigationViewModel(
            INavigationDataProvider dataProvider,
            IEventAggregator eventAggregator)
        {
            this.TeamMembers = new ObservableCollection<NavigationItem>();

            this.dataProvider = dataProvider;
            this.eventAggregator = eventAggregator;

            this.eventAggregator
                .GetEvent<TeamMemberDeletedEvent>()
                .Subscribe(this.OnTeamMemberDeleted);

            this.eventAggregator
                .GetEvent<TeamMemberSavedEvent>()
                .Subscribe(this.OnTeamMemberSaved);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the team members.
        /// </summary>
        /// <value>
        /// The team members.
        /// </value>
        public ObservableCollection<NavigationItem> TeamMembers
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Called when load.
        /// </summary>
        public void OnLoad()
        {
            this.TeamMembers.Clear();

            foreach (SearchItem searchItem in this.dataProvider.GetAllTeamMembers())
            {
                this.TeamMembers.Add(
                    new NavigationItem(
                        this.eventAggregator,
                        searchItem.Id,
                        searchItem.DisplayMember));
            }
        }

        /// <summary>
        /// Called when a team member is deleted.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        private void OnTeamMemberDeleted(int teamMemberId)
        {
            NavigationItem navigationItem = this.TeamMembers.Single(e => (e.Id == teamMemberId));
            this.TeamMembers.Remove(navigationItem);
        }

        /// <summary>
        /// Called when a team member is saved.
        /// </summary>
        /// <param name="teamMember">The team member.</param>
        private void OnTeamMemberSaved(TeamMember teamMember)
        {
            string displayMember = $"{teamMember.FirstName} {teamMember.LastName}";

            NavigationItem navigationItem = this.TeamMembers.SingleOrDefault(e => (e.Id == teamMember.Id));
            if (navigationItem != null)
            {
                navigationItem.DisplayMember = displayMember;
            }
            else
            {
                this.TeamMembers.Add(
                    new NavigationItem(
                        this.eventAggregator,
                        teamMember.Id,
                        displayMember));
            }
        }

        #endregion Methods
    }
}