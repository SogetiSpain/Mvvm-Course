// ----------------------------------------------------------------------------
// <copyright file="NavigationViewModelTests.cs" company="SOGETI Spain">
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

    using UI.DataProviders;
    using UI.Events;
    using UI.ViewModels;

    /// <summary>
    /// Represents the tests of the view model for navigation view.
    /// </summary>
    [TestClass]
    public class NavigationViewModelTests
    {
        #region Fields

        /// <summary>
        /// Defines the team member deleted event.
        /// </summary>
        private TeamMemberDeletedEvent teamMemberDeletedEvent;

        /// <summary>
        /// Defines the team member saved event.
        /// </summary>
        private TeamMemberSavedEvent teamMemberSavedEvent;

        /// <summary>
        /// Defines the view model.
        /// </summary>
        private NavigationViewModel viewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewModelTests"/> class.
        /// </summary>
        public NavigationViewModelTests()
        {
            this.teamMemberSavedEvent = new TeamMemberSavedEvent();
            this.teamMemberDeletedEvent = new TeamMemberDeletedEvent();

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberSavedEvent>())
            .Returns(this.teamMemberSavedEvent);
            eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberDeletedEvent>())
            .Returns(this.teamMemberDeletedEvent);

            var navigationDataProviderMock = new Mock<INavigationDataProvider>();
            navigationDataProviderMock.Setup(dp => dp.GetAllTeamMembers())
            .Returns(new List<SearchItem>
            {
                new SearchItem { Id = 1, DisplayMember = "Pedro" },
                new SearchItem { Id = 2, DisplayMember = "Antonio" }
            });
            this.viewModel = new NavigationViewModel(
                navigationDataProviderMock.Object,
                eventAggregatorMock.Object);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Should add the navigation item when added team member is saved.
        /// </summary>
        [TestMethod]
        public void ShouldAddNavigationItemWhenAddedTeamMemberIsSaved()
        {
            this.viewModel.OnLoad();

            const int NewTeamMemberId = 97;

            this.teamMemberSavedEvent.Publish(new TeamMember
            {
                Id = NewTeamMemberId,
                FirstName = "Juan",
                LastName = "Gómez"
            });

            Assert.AreEqual(3, this.viewModel.TeamMembers.Count);

            var addedItem = this.viewModel.TeamMembers.SingleOrDefault(f => f.Id == NewTeamMemberId);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual("Juan Gómez", addedItem.DisplayMember);
        }

        /// <summary>
        /// Should load the team member only once.
        /// </summary>
        [TestMethod]
        public void ShouldLoadTeamMemberOnlyOnce()
        {
            this.viewModel.OnLoad();
            this.viewModel.OnLoad();

            Assert.AreEqual(2, this.viewModel.TeamMembers.Count);
        }

        /// <summary>
        /// Should load the team members.
        /// </summary>
        [TestMethod]
        public void ShouldLoadTeamMembers()
        {
            this.viewModel.OnLoad();

            Assert.AreEqual(2, this.viewModel.TeamMembers.Count);

            var teamMember = this.viewModel.TeamMembers.SingleOrDefault(f => f.Id == 1);
            Assert.IsNotNull(teamMember);
            Assert.AreEqual("Pedro", teamMember.DisplayMember);

            teamMember = this.viewModel.TeamMembers.SingleOrDefault(f => f.Id == 2);
            Assert.IsNotNull(teamMember);
            Assert.AreEqual("Antonio", teamMember.DisplayMember);
        }

        /// <summary>
        /// Should remove the navigation item when team member is deleted.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveNavigationItemWhenTeamMemberIsDeleted()
        {
            this.viewModel.OnLoad();

            var deletedTeamMemberId = this.viewModel.TeamMembers.First().Id;

            this.teamMemberDeletedEvent.Publish(deletedTeamMemberId);

            Assert.AreEqual(1, this.viewModel.TeamMembers.Count);
            Assert.AreNotEqual(deletedTeamMemberId, this.viewModel.TeamMembers.Single().Id);
        }

        /// <summary>
        /// Should update the navigation item when team member is saved.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateNavigationItemWhenTeamMemberIsSaved()
        {
            this.viewModel.OnLoad();
            var navigationItem = this.viewModel.TeamMembers.First();

            var teamMemberId = navigationItem.Id;

            this.teamMemberSavedEvent.Publish(
                new TeamMember
                {
                    Id = teamMemberId,
                    FirstName = "Juan",
                    LastName = "Gómez"
                });

            Assert.AreEqual("Juan Gómez", navigationItem.DisplayMember);
        }

        #endregion Methods
    }
}