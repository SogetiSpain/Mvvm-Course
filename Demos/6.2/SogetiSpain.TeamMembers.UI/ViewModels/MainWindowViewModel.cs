// ----------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Events;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    /// <summary>
    /// Represents the view model for main window.
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the event aggregator.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Defines the team member edit view model creator.
        /// </summary>
        private readonly Func<ITeamMemberEditViewModel> teamMemberEditViewModelCreator;

        /// <summary>
        /// Defines the selected team member edit view model.
        /// </summary>
        private ITeamMemberEditViewModel selectedTeamMemberEditViewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="navigationViewModel">The navigation view model.</param>
        /// <param name="teamMemberEditViewModelCreator">The team member edit view model creator.</param>
        public MainWindowViewModel(
            IEventAggregator eventAggregator,
            INavigationViewModel navigationViewModel,
            Func<ITeamMemberEditViewModel> teamMemberEditViewModelCreator)
        {
            this.TeamMemberEditViewModels = new ObservableCollection<ITeamMemberEditViewModel>();

            this.NavigationViewModel = navigationViewModel;
            this.teamMemberEditViewModelCreator = teamMemberEditViewModelCreator;

            this.eventAggregator = eventAggregator;

            this.eventAggregator
                .GetEvent<OpenTeamMemberEditViewEvent>()
                .Subscribe(this.OnOpenTeamMemberEditView, ThreadOption.UIThread, true);

            this.eventAggregator
                .GetEvent<TeamMemberDeletedEvent>()
                .Subscribe(this.OnTeamMemberDeleted, ThreadOption.UIThread, true);

            this.AddTeamMemberCommand = new DelegateCommand(this.OnAddTeamMember);
            this.CloseTeamMemberTabCommand = new DelegateCommand<ITeamMemberEditViewModel>(this.OnCloseTeamMemberTab);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the add team member command.
        /// </summary>
        /// <value>
        /// The add team member command.
        /// </value>
        public ICommand AddTeamMemberCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the close team member tab command.
        /// </summary>
        /// <value>
        /// The close team member tab command.
        /// </value>
        public ICommand CloseTeamMemberTabCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the navigation view model.
        /// </summary>
        /// <value>
        /// The navigation view model.
        /// </value>
        public INavigationViewModel NavigationViewModel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected team member edit view model.
        /// </summary>
        /// <value>
        /// The selected team member edit view model.
        /// </value>
        public ITeamMemberEditViewModel SelectedTeamMemberEditViewModel
        {
            get
            {
                return this.selectedTeamMemberEditViewModel;
            }

            set
            {
                this.SetProperty(ref this.selectedTeamMemberEditViewModel, value);
            }
        }

        /// <summary>
        /// Gets the team member edit view models.
        /// </summary>
        /// <value>
        /// The team member edit view models.
        /// </value>
        public ObservableCollection<ITeamMemberEditViewModel> TeamMemberEditViewModels
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
            this.NavigationViewModel.OnLoad();
        }

        /// <summary>
        /// Creates and loads a team member edit view model.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        /// <returns>
        /// The created and loaded team member edit view model.
        /// </returns>
        private ITeamMemberEditViewModel CreateAndLoadTeamMemberEditViewModel(int? teamMemberId)
        {
            ITeamMemberEditViewModel teamMemberEditViewModel = this.teamMemberEditViewModelCreator();
            this.TeamMemberEditViewModels.Add(teamMemberEditViewModel);
            teamMemberEditViewModel.Load(teamMemberId);

            return teamMemberEditViewModel;
        }

        /// <summary>
        /// Called when add a team member.
        /// </summary>
        private void OnAddTeamMember()
        {
            this.SelectedTeamMemberEditViewModel = this.CreateAndLoadTeamMemberEditViewModel(null);
        }

        /// <summary>
        /// Called when close a team member tab.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OnCloseTeamMemberTab(object parameter)
        {
            ITeamMemberEditViewModel teamMemberEditViewModel = (ITeamMemberEditViewModel)parameter;
            this.TeamMemberEditViewModels.Remove(teamMemberEditViewModel);
        }

        /// <summary>
        /// Called when open a team member edit view.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        private void OnOpenTeamMemberEditView(int teamMemberId)
        {
            ITeamMemberEditViewModel teamMemberEditViewModel = this.TeamMemberEditViewModels.SingleOrDefault(e => (e.TeamMember.Id == teamMemberId));
            if (teamMemberEditViewModel == null)
            {
                teamMemberEditViewModel = this.CreateAndLoadTeamMemberEditViewModel(teamMemberId);
            }

            this.SelectedTeamMemberEditViewModel = teamMemberEditViewModel;
        }

        /// <summary>
        /// Called when a team member is deleted.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        private void OnTeamMemberDeleted(int teamMemberId)
        {
            ITeamMemberEditViewModel teamMemberViewModel = this.TeamMemberEditViewModels.Single(e => (e.TeamMember.Id == teamMemberId));
            this.TeamMemberEditViewModels.Remove(teamMemberViewModel);
        }

        #endregion Methods
    }
}