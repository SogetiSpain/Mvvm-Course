// ----------------------------------------------------------------------------
// <copyright file="TeamMemberEditViewModelTests.cs" company="SOGETI Spain">
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
    using UI.Dialogs;

    /// <summary>
    /// Represents the tests of the view model for team member edit view.
    /// </summary>
    [TestClass]
    public class TeamMemberEditViewModelTests
    {
        private const int _friendId = 5;
        private Mock<ITeamMemberDataProvider> _dataProviderMock;
        private TeamMemberEditViewModel _viewModel;
        private Mock<TeamMemberSavedEvent> _friendSavedEventMock;
        private Mock<IEventAggregator> _eventAggregatorMock;
        private Mock<TeamMemberDeletedEvent> _friendDeletedEventMock;
        private Mock<IMessageDialogService> _messageDialogServiceMock;

        public TeamMemberEditViewModelTests()
        {
            _friendDeletedEventMock = new Mock<TeamMemberDeletedEvent>();
            _friendSavedEventMock = new Mock<TeamMemberSavedEvent>();

            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberSavedEvent>())
              .Returns(_friendSavedEventMock.Object);
            _eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberDeletedEvent>())
              .Returns(_friendDeletedEventMock.Object);

            _dataProviderMock = new Mock<ITeamMemberDataProvider>();
            _dataProviderMock.Setup(dp => dp.GetTeamMemberById(_friendId))
              .Returns(new TeamMember { Id = _friendId, FirstName = "Antonio" });

            _messageDialogServiceMock = new Mock<IMessageDialogService>();

            _viewModel = new TeamMemberEditViewModel(_dataProviderMock.Object,
              _eventAggregatorMock.Object,
              _messageDialogServiceMock.Object);
        }

        [TestMethod]
        public void ShouldLoadFriend()
        {
            _viewModel.Load(_friendId);

            Assert.IsNotNull(_viewModel.TeamMember);
            Assert.AreEqual(_friendId, _viewModel.TeamMember.Id);

            _dataProviderMock.Verify(dp => dp.GetTeamMemberById(_friendId), Times.Once);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForFriend()
        {
            var fired = _viewModel.IsPropertyChangedFired(
              () => _viewModel.Load(_friendId),
              nameof(_viewModel.TeamMember));

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldDisableSaveCommandWhenFriendIsLoaded()
        {
            _viewModel.Load(_friendId);

            Assert.IsFalse(_viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void ShouldEnableSaveCommandWhenFriendIsChanged()
        {
            _viewModel.Load(_friendId);

            _viewModel.TeamMember.FirstName = "Changed";

            Assert.IsTrue(_viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void ShouldDisableSaveCommandWithoutLoad()
        {
            Assert.IsFalse(_viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForSaveCommandWhenFriendIsChanged()
        {
            _viewModel.Load(_friendId);
            var fired = false;
            _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.TeamMember.FirstName = "Changed";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForSaveCommandAfterLoad()
        {
            var fired = false;
            _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Load(_friendId);
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandAfterLoad()
        {
            var fired = false;
            _viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Load(_friendId);
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandWhenAcceptingChanges()
        {
            _viewModel.Load(_friendId);
            var fired = false;
            _viewModel.TeamMember.FirstName = "Changed";
            _viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.TeamMember.AcceptChanges();
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldCallSaveMethodOfDataProviderWhenSaveCommandIsExecuted()
        {
            _viewModel.Load(_friendId);
            _viewModel.TeamMember.FirstName = "Changed";

            _viewModel.SaveCommand.Execute(null);
            _dataProviderMock.Verify(dp => dp.SaveTeamMember(_viewModel.TeamMember.Model), Times.Once);
        }

        [TestMethod]
        public void ShouldAcceptChangesWhenSaveCommandIsExecuted()
        {
            _viewModel.Load(_friendId);
            _viewModel.TeamMember.FirstName = "Changed";

            _viewModel.SaveCommand.Execute(null);
            Assert.IsFalse(_viewModel.TeamMember.IsChanged);
        }

        [TestMethod]
        public void ShouldPublishFriendSavedEventWhenSaveCommandIsExecuted()
        {
            _viewModel.Load(_friendId);
            _viewModel.TeamMember.FirstName = "Changed";

            _viewModel.SaveCommand.Execute(null);
            _friendSavedEventMock.Verify(e => e.Publish(_viewModel.TeamMember.Model), Times.Once);
        }

        [TestMethod]
        public void ShouldCreateNewFriendWhenNullIsPassedToLoadMethod()
        {
            _viewModel.Load(null);

            Assert.IsNotNull(_viewModel.TeamMember);
            Assert.AreEqual(0, _viewModel.TeamMember.Id);
            Assert.IsNull(_viewModel.TeamMember.FirstName);
            Assert.IsNull(_viewModel.TeamMember.LastName);
            Assert.IsNull(_viewModel.TeamMember.Birthday);
            Assert.IsFalse(_viewModel.TeamMember.IsDeveloper);

            _dataProviderMock.Verify(dp => dp.GetTeamMemberById(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void ShouldEnableDeleteCommandForExistingFriend()
        {
            _viewModel.Load(_friendId);
            Assert.IsTrue(_viewModel.DeleteCommand.CanExecute(null));
        }

        [TestMethod]
        public void ShouldDisableDeleteCommandForNewFriend()
        {
            _viewModel.Load(null);
            Assert.IsFalse(_viewModel.DeleteCommand.CanExecute(null));
        }

        [TestMethod]
        public void ShouldDisableDeleteCommandWithoutLoad()
        {
            Assert.IsFalse(_viewModel.DeleteCommand.CanExecute(null));
        }

        /*
        [Theory]
        [InlineData(MessageDialogResult.Yes, 1)]
        [InlineData(MessageDialogResult.No, 0)]
        public void ShouldCallDeleteFriendWhenDeleteCommandIsExecuted(
          MessageDialogResult result, int expectedDeleteFriendCalls)
        {
            _viewModel.Load(_friendId);

            _messageDialogServiceMock.Setup(ds => ds.ShowYesNoDialog(It.IsAny<string>(),
              It.IsAny<string>())).Returns(result);

            _viewModel.DeleteCommand.Execute(null);

            _dataProviderMock.Verify(dp => dp.DeleteFriend(_friendId),
              Times.Exactly(expectedDeleteFriendCalls));
            _messageDialogServiceMock.Verify(ds => ds.ShowYesNoDialog(It.IsAny<string>(),
              It.IsAny<string>()), Times.Once);
        }
        */

        /*
        [Theory]
        [InlineData(MessageDialogResult.Yes, 1)]
        [InlineData(MessageDialogResult.No, 0)]
        public void ShouldPublishFriendDeletedEventWhenDeleteCommandIsExecuted(
          MessageDialogResult result, int expectedPublishCalls)
        {
            _viewModel.Load(_friendId);

            _messageDialogServiceMock.Setup(ds => ds.ShowYesNoDialog(It.IsAny<string>(),
             It.IsAny<string>())).Returns(result);

            _viewModel.DeleteCommand.Execute(null);

            _friendDeletedEventMock.Verify(e => e.Publish(_friendId),
              Times.Exactly(expectedPublishCalls));

            _messageDialogServiceMock.Verify(ds => ds.ShowYesNoDialog(It.IsAny<string>(),
             It.IsAny<string>()), Times.Once);
        }
        */

        [TestMethod]
        public void ShouldDisplayCorrectMessageInDeleteDialog()
        {
            _viewModel.Load(_friendId);

            var f = _viewModel.TeamMember;
            f.FirstName = "Antonio";
            f.LastName = "Fernández";

            _viewModel.DeleteCommand.Execute(null);

            _messageDialogServiceMock.Verify(d => d.ShowYesNoDialog("Delete Team Member",
              $"Do you really want to delete the team member '{f.FirstName} {f.LastName}'"),
              Times.Once);
        }
    }
}