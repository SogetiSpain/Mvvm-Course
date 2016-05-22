// ----------------------------------------------------------------------------
// <copyright file="TeamMemberEditViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using DataProviders;
    using Dialogs;
    using Events;
    using Model;
    using Models;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    /// <summary>
    /// Represents the view model for team member edit view.
    /// </summary>
    public class TeamMemberEditViewModel : BindableBase, ITeamMemberEditViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the data provider.
        /// </summary>
        private readonly ITeamMemberDataProvider dataProvider;

        /// <summary>
        /// Defines the event aggregator.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Defines the message dialog service.
        /// </summary>
        private readonly IMessageDialogService messageDialogService;

        /// <summary>
        /// Defines the team member.
        /// </summary>
        private WrappedTeamMember teamMember;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberEditViewModel"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="messageDialogService">The message dialog service.</param>
        public TeamMemberEditViewModel(
            ITeamMemberDataProvider dataProvider,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this.dataProvider = dataProvider;
            this.eventAggregator = eventAggregator;
            this.messageDialogService = messageDialogService;

            this.DeleteCommand = new DelegateCommand(this.OnDelete, this.CanDelete);
            this.SaveCommand = new DelegateCommand(this.OnSave, this.CanSave);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public ICommand DeleteCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the team member.
        /// </summary>
        /// <value>
        /// The team member.
        /// </value>
        public WrappedTeamMember TeamMember
        {
            get
            {
                return this.teamMember;
            }

            set
            {
                this.SetProperty(ref this.teamMember, value);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the specified team member.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        public void Load(int? teamMemberId)
        {
            var teamMember = teamMemberId.HasValue
              ? this.dataProvider.GetTeamMemberById(teamMemberId.Value)
              : new TeamMember();

            this.TeamMember = new WrappedTeamMember(teamMember);
            this.TeamMember.PropertyChanged += this.OnTeamMemberPropertyChanged;

            this.InvalidateCommands();
        }

        /// <summary>
        /// Determines whether this instance can delete.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can delete; otherwise, <c>false</c>.
        /// </returns>
        private bool CanDelete()
        {
            return (this.TeamMember != null) && (this.TeamMember.Id > 0);
        }

        /// <summary>
        /// Determines whether this instance can save.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can save; otherwise, <c>false</c>.
        /// </returns>
        private bool CanSave()
        {
            return (this.TeamMember != null) && this.TeamMember.IsChanged;
        }

        /// <summary>
        /// Invalidates the commands.
        /// </summary>
        private void InvalidateCommands()
        {
            ((DelegateCommand)this.DeleteCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SaveCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Called when delete.
        /// </summary>
        private void OnDelete()
        {
            MessageDialogResult result = this.messageDialogService.ShowYesNoDialog(
                "Delete Team Member",
                $"Do you really want to delete the team member '{this.TeamMember.FirstName} {this.TeamMember.LastName}'");

            if (result == MessageDialogResult.Yes)
            {
                this.dataProvider.DeleteTeamMeamber(this.TeamMember.Id);
                this.eventAggregator.GetEvent<TeamMemberDeletedEvent>().Publish(this.TeamMember.Id);
            }
        }

        /// <summary>
        /// Called when save.
        /// </summary>
        private void OnSave()
        {
            this.dataProvider.SaveTeamMember(this.TeamMember.Model);
            this.TeamMember.AcceptChanges();

            this.eventAggregator
                .GetEvent<TeamMemberSavedEvent>()
                .Publish(this.TeamMember.Model);
        }

        /// <summary>
        /// Called when the team member property is changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnTeamMemberPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.InvalidateCommands();
        }

        #endregion Methods
    }
}