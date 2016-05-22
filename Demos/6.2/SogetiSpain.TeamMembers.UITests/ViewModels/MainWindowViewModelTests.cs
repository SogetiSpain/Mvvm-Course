// ----------------------------------------------------------------------------
// <copyright file="MainWindowViewModelTests.cs" company="SOGETI Spain">
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

    /// <summary>
    /// Represents the tests of the view model for main window.
    /// </summary>
    [TestClass]
    public class MainWindowViewModelTests
    {
        private Mock<INavigationViewModel> _navigationViewModelMock;
        private MainWindowViewModel _viewModel;
        private Mock<IEventAggregator> _eventAggregatorMock;
        private OpenTeamMemberEditViewEvent _openFriendEditViewEvent;
        private List<Mock<ITeamMemberEditViewModel>> _friendEditViewModelMocks;
        private TeamMemberDeletedEvent _friendDeletedEvent;

        public MainWindowViewModelTests()
        {
            _friendEditViewModelMocks = new List<Mock<ITeamMemberEditViewModel>>();
            _navigationViewModelMock = new Mock<INavigationViewModel>();

            _openFriendEditViewEvent = new OpenTeamMemberEditViewEvent();
            _friendDeletedEvent = new TeamMemberDeletedEvent();
            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<OpenTeamMemberEditViewEvent>())
              .Returns(_openFriendEditViewEvent);
            _eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberDeletedEvent>())
              .Returns(_friendDeletedEvent);

            _viewModel = new MainWindowViewModel(
                _eventAggregatorMock.Object,
                _navigationViewModelMock.Object,
                CreateTeamMemberEditViewModel);

        }

        [TestMethod]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            _viewModel.OnLoad();

            _navigationViewModelMock.Verify(vm => vm.OnLoad(), Times.Once);
        }

        [TestMethod]
        public void ShouldAddFriendEditViewModelAndLoadAndSelectIt()
        {
            const int friendId = 7;
            _openFriendEditViewEvent.Publish(friendId);

            Assert.AreEqual(1, _viewModel.TeamMemberEditViewModels.Count);
            var friendEditVm = _viewModel.TeamMemberEditViewModels.First();
            Assert.AreEqual(friendEditVm, _viewModel.SelectedTeamMemberEditViewModel);
            _friendEditViewModelMocks.First().Verify(vm => vm.Load(friendId), Times.Once);
        }

        [TestMethod]
        public void ShouldAddFriendEditViewModelAndLoadItWithIdNullAndSelectIt()
        {
            _viewModel.AddTeamMemberCommand.Execute(null);

            Assert.AreEqual(1, _viewModel.TeamMemberEditViewModels.Count);
            var friendEditVm = _viewModel.TeamMemberEditViewModels.First();
            Assert.AreEqual(friendEditVm, _viewModel.SelectedTeamMemberEditViewModel);
            _friendEditViewModelMocks.First().Verify(vm => vm.Load(null), Times.Once);
        }

        [TestMethod]
        public void ShouldAddFriendEditViewModelsOnlyOnce()
        {
            _openFriendEditViewEvent.Publish(5);
            _openFriendEditViewEvent.Publish(5);
            _openFriendEditViewEvent.Publish(6);
            _openFriendEditViewEvent.Publish(7);
            _openFriendEditViewEvent.Publish(7);

            Assert.AreEqual(3, _viewModel.TeamMemberEditViewModels.Count);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForSelectedFriendEditViewModel()
        {
            var friendEditVmMock = new Mock<ITeamMemberEditViewModel>();
            var fired = _viewModel.IsPropertyChangedFired(() =>
            {
                _viewModel.SelectedTeamMemberEditViewModel = friendEditVmMock.Object;
            }, nameof(_viewModel.SelectedTeamMemberEditViewModel));

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRemoveFriendEditViewModelOnCloseFriendTabCommand()
        {
            _openFriendEditViewEvent.Publish(7);

            var friendEditVm = _viewModel.SelectedTeamMemberEditViewModel;

            _viewModel.CloseTeamMemberTabCommand.Execute(friendEditVm);

            Assert.AreEqual(0, _viewModel.TeamMemberEditViewModels.Count);
        }

        [TestMethod]
        public void ShouldRemoveFriendEditViewModelOnFriendDeletedEvent()
        {
            const int deletedFriendId = 7;

            _openFriendEditViewEvent.Publish(deletedFriendId);
            _openFriendEditViewEvent.Publish(8);
            _openFriendEditViewEvent.Publish(9);

            _friendDeletedEvent.Publish(deletedFriendId);

            Assert.AreEqual(2, _viewModel.TeamMemberEditViewModels.Count);
            Assert.IsTrue(_viewModel.TeamMemberEditViewModels.All(vm => vm.TeamMember.Id != deletedFriendId));
        }

        private ITeamMemberEditViewModel CreateTeamMemberEditViewModel()
        {
            var friendEditViewModelMock = new Mock<ITeamMemberEditViewModel>();
            friendEditViewModelMock.Setup(vm => vm.Load(It.IsAny<int>()))
              .Callback<int?>(friendId =>
              {
                  friendEditViewModelMock.Setup(vm => vm.TeamMember)
            .Returns(new WrappedTeamMember(new TeamMember() { Id = friendId.Value }));
              });
            _friendEditViewModelMocks.Add(friendEditViewModelMock);
            return friendEditViewModelMock.Object;
        }
    }
}