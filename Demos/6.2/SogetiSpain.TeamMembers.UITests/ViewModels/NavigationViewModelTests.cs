// ----------------------------------------------------------------------------
// <copyright file="NavigationViewModelTests.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UITests.ViewModels
{
    using SogetiSpain.TeamMembers.UITests.Extensions;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Moq;
    using Prism.Events;
    using UI.Events;
    using UI.Models;
    using UI.ViewModels;
    using System.Linq;
    using UI.DataProviders;

    /// <summary>
    /// Represents the tests of the view model for navigation view.
    /// </summary>
    [TestClass]
    public class NavigationViewModelTests
    {
        private NavigationViewModel _viewModel;
        private TeamMemberSavedEvent _friendSavedEvent;
        private TeamMemberDeletedEvent _friendDeletedEvent;

        public NavigationViewModelTests()
        {
            _friendSavedEvent = new TeamMemberSavedEvent();
            _friendDeletedEvent = new TeamMemberDeletedEvent();

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberSavedEvent>())
              .Returns(_friendSavedEvent);
            eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberDeletedEvent>())
              .Returns(_friendDeletedEvent);

            var navigationDataProviderMock = new Mock<INavigationDataProvider>();
            navigationDataProviderMock.Setup(dp => dp.GetAllTeamMembers())
              .Returns(new List<SearchItem>
              {
          new SearchItem { Id = 1, DisplayMember = "Pedro" },
          new SearchItem { Id = 2, DisplayMember = "Antonio" }
             });
            _viewModel = new NavigationViewModel(
              navigationDataProviderMock.Object,
              eventAggregatorMock.Object);
        }

        [TestMethod]
        public void ShouldLoadFriends()
        {
            _viewModel.OnLoad();

            Assert.AreEqual(2, _viewModel.TeamMembers.Count);

            var friend = _viewModel.TeamMembers.SingleOrDefault(f => f.Id == 1);
            Assert.IsNotNull(friend);
            Assert.AreEqual("Pedro", friend.DisplayMember);

            friend = _viewModel.TeamMembers.SingleOrDefault(f => f.Id == 2);
            Assert.IsNotNull(friend);
            Assert.AreEqual("Antonio", friend.DisplayMember);
        }

        [TestMethod]
        public void ShouldLoadFriendsOnlyOnce()
        {
            _viewModel.OnLoad();
            _viewModel.OnLoad();

            Assert.AreEqual(2, _viewModel.TeamMembers.Count);
        }

        [TestMethod]
        public void ShouldUpdateNavigationItemWhenFriendIsSaved()
        {
            _viewModel.OnLoad();
            var navigationItem = _viewModel.TeamMembers.First();

            var friendId = navigationItem.Id;

            _friendSavedEvent.Publish(
              new TeamMember
              {
                  Id = friendId,
                  FirstName = "Juan",
                  LastName = "Gómez"
              });

            Assert.AreEqual("Juan Gómez", navigationItem.DisplayMember);
        }

        [TestMethod]
        public void ShouldAddNavigationItemWhenAddedFriendIsSaved()
        {
            _viewModel.OnLoad();

            const int newFriendId = 97;

            _friendSavedEvent.Publish(new TeamMember
            {
                Id = newFriendId,
                FirstName = "Juan",
                LastName = "Gómez"
            });

            Assert.AreEqual(3, _viewModel.TeamMembers.Count);

            var addedItem = _viewModel.TeamMembers.SingleOrDefault(f => f.Id == newFriendId);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual("Juan Gómez", addedItem.DisplayMember);
        }

        [TestMethod]
        public void ShouldRemoveNavigationItemWhenFriendIsDeleted()
        {
            _viewModel.OnLoad();

            var deletedFriendId = _viewModel.TeamMembers.First().Id;

            _friendDeletedEvent.Publish(deletedFriendId);

            Assert.AreEqual(1, _viewModel.TeamMembers.Count);
            Assert.AreNotEqual(deletedFriendId, _viewModel.TeamMembers.Single().Id);
        }
    }
}