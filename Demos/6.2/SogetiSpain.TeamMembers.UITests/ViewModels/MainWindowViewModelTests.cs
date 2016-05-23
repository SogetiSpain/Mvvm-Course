// ----------------------------------------------------------------------------
// <copyright file="MainWindowViewModelTests.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UITests.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Model;

    using Moq;

    using Prism.Events;

    using SogetiSpain.TeamMembers.UITests.Extensions;

    using UI.Events;
    using UI.Models;
    using UI.ViewModels;

    /// <summary>
    /// Represents the tests of the view model for main window.
    /// </summary>
    [TestClass]
    public class MainWindowViewModelTests
    {
        #region Fields

        /// <summary>
        /// Defines the event aggregator mock.
        /// </summary>
        private Mock<IEventAggregator> eventAggregatorMock;

        /// <summary>
        /// Defines the navigation view model mock.
        /// </summary>
        private Mock<INavigationViewModel> navigationViewModelMock;

        /// <summary>
        /// Defines the open team member edit view event.
        /// </summary>
        private OpenTeamMemberEditViewEvent openTeamMemberEditViewEvent;

        /// <summary>
        /// Defines the team member deleted event.
        /// </summary>
        private TeamMemberDeletedEvent teamMemberDeletedEvent;

        /// <summary>
        /// Defines the team member edit view model mocks.
        /// </summary>
        private List<Mock<ITeamMemberEditViewModel>> teamMemberEditViewModelMocks;

        /// <summary>
        /// Defines the view model.
        /// </summary>
        private MainWindowViewModel viewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModelTests"/> class.
        /// </summary>
        public MainWindowViewModelTests()
        {
            this.teamMemberEditViewModelMocks = new List<Mock<ITeamMemberEditViewModel>>();
            this.navigationViewModelMock = new Mock<INavigationViewModel>();

            this.openTeamMemberEditViewEvent = new OpenTeamMemberEditViewEvent();
            this.teamMemberDeletedEvent = new TeamMemberDeletedEvent();
            this.eventAggregatorMock = new Mock<IEventAggregator>();
            this.eventAggregatorMock.Setup(ea => ea.GetEvent<OpenTeamMemberEditViewEvent>())
            .Returns(this.openTeamMemberEditViewEvent);
            this.eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberDeletedEvent>())
            .Returns(this.teamMemberDeletedEvent);

            this.viewModel = new MainWindowViewModel(
                this.eventAggregatorMock.Object,
                this.navigationViewModelMock.Object,
                this.CreateTeamMemberEditViewModel);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Should add the team member edit view model and load and select it.
        /// </summary>
        [TestMethod]
        public void ShouldAddTeamMemberEditViewModelAndLoadAndSelectIt()
        {
            const int TeamMemberId = 7;
            this.openTeamMemberEditViewEvent.Publish(TeamMemberId);

            Assert.AreEqual(1, this.viewModel.TeamMemberEditViewModels.Count);
            var teamMemberEditViewModel = this.viewModel.TeamMemberEditViewModels.First();
            Assert.AreEqual(teamMemberEditViewModel, this.viewModel.SelectedTeamMemberEditViewModel);
            this.teamMemberEditViewModelMocks.First().Verify(vm => vm.Load(TeamMemberId), Times.Once);
        }

        /// <summary>
        /// Should add the team member edit view model and load it with identifier null and select it.
        /// </summary>
        [TestMethod]
        public void ShouldAddTeamMemberEditViewModelAndLoadItWithIdNullAndSelectIt()
        {
            this.viewModel.AddTeamMemberCommand.Execute(null);

            Assert.AreEqual(1, this.viewModel.TeamMemberEditViewModels.Count);
            var teamMemberEditViewModel = this.viewModel.TeamMemberEditViewModels.First();
            Assert.AreEqual(teamMemberEditViewModel, this.viewModel.SelectedTeamMemberEditViewModel);
            this.teamMemberEditViewModelMocks.First().Verify(vm => vm.Load(null), Times.Once);
        }

        /// <summary>
        /// Should add the team member edit view models only once.
        /// </summary>
        [TestMethod]
        public void ShouldAddTeamMemberEditViewModelsOnlyOnce()
        {
            this.openTeamMemberEditViewEvent.Publish(5);
            this.openTeamMemberEditViewEvent.Publish(5);
            this.openTeamMemberEditViewEvent.Publish(6);
            this.openTeamMemberEditViewEvent.Publish(7);
            this.openTeamMemberEditViewEvent.Publish(7);

            Assert.AreEqual(3, this.viewModel.TeamMemberEditViewModels.Count);
        }

        /// <summary>
        /// Should call the load method of the navigation view model.
        /// </summary>
        [TestMethod]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            this.viewModel.OnLoad();

            this.navigationViewModelMock.Verify(vm => vm.OnLoad(), Times.Once);
        }

        /// <summary>
        /// Should raise the property changed event for selected team member edit view model.
        /// </summary>
        [TestMethod]
        public void ShouldRaisePropertyChangedEventForSelectedTeamMemberEditViewModel()
        {
            var teamMemberEditViewModelMock = new Mock<ITeamMemberEditViewModel>();
            var fired = this.viewModel.IsPropertyChangedFired(
                            () =>
            {
                this.viewModel.SelectedTeamMemberEditViewModel = teamMemberEditViewModelMock.Object;
            },
            nameof(this.viewModel.SelectedTeamMemberEditViewModel));

            Assert.IsTrue(fired);
        }

        /// <summary>
        /// Should remove the team member edit view model on close team member tab command.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveTeamMemberEditViewModelOnCloseTeamMemberTabCommand()
        {
            this.openTeamMemberEditViewEvent.Publish(7);

            var teamMemberEditViewModel = this.viewModel.SelectedTeamMemberEditViewModel;

            this.viewModel.CloseTeamMemberTabCommand.Execute(teamMemberEditViewModel);

            Assert.AreEqual(0, this.viewModel.TeamMemberEditViewModels.Count);
        }

        /// <summary>
        /// Should remove the team member edit view model on team member deleted event.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveTeamMemberEditViewModelOnTeamMemberDeletedEvent()
        {
            const int DeletedTeamMemberId = 7;

            this.openTeamMemberEditViewEvent.Publish(DeletedTeamMemberId);
            this.openTeamMemberEditViewEvent.Publish(8);
            this.openTeamMemberEditViewEvent.Publish(9);

            this.teamMemberDeletedEvent.Publish(DeletedTeamMemberId);

            Assert.AreEqual(2, this.viewModel.TeamMemberEditViewModels.Count);
            Assert.IsTrue(this.viewModel.TeamMemberEditViewModels.All(vm => vm.TeamMember.Id != DeletedTeamMemberId));
        }

        /// <summary>
        /// Creates the team member edit view model.
        /// </summary>
        /// <returns>
        /// The team member edit view model.
        /// </returns>
        private ITeamMemberEditViewModel CreateTeamMemberEditViewModel()
        {
            var teamMemberEditViewModelMock = new Mock<ITeamMemberEditViewModel>();
            teamMemberEditViewModelMock.Setup(vm => vm.Load(It.IsAny<int>()))
            .Callback<int?>(teamMemberId =>
            {
                teamMemberEditViewModelMock.Setup(vm => vm.TeamMember)
                .Returns(new WrappedTeamMember(new TeamMember()
                {
                    Id = teamMemberId.Value
                }));
            });
            this.teamMemberEditViewModelMocks.Add(teamMemberEditViewModelMock);
            return teamMemberEditViewModelMock.Object;
        }

        #endregion Methods
    }
}