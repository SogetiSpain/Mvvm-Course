// ----------------------------------------------------------------------------
// <copyright file="TeamMemberEditViewModelTests.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UITests.ViewModels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Model;

    using Moq;

    using Prism.Events;

    using SogetiSpain.TeamMembers.UITests.Extensions;

    using UI.DataProviders;
    using UI.Dialogs;
    using UI.Events;
    using UI.ViewModels;

    /// <summary>
    /// Represents the tests of the view model for team member edit view.
    /// </summary>
    [TestClass]
    public class TeamMemberEditViewModelTests
    {
        #region Fields

        /// <summary>
        /// Defines the team member identifier.
        /// </summary>
        private const int TeamMemberId = 5;

        /// <summary>
        /// Defines the data provider mock.
        /// </summary>
        private Mock<ITeamMemberDataProvider> dataProviderMock;

        /// <summary>
        /// Defines the event aggregator mock.
        /// </summary>
        private Mock<IEventAggregator> eventAggregatorMock;

        /// <summary>
        /// Defines the message dialog service mock.
        /// </summary>
        private Mock<IMessageDialogService> messageDialogServiceMock;

        /// <summary>
        /// Defines the team member deleted event mock.
        /// </summary>
        private Mock<TeamMemberDeletedEvent> teamMemberDeletedEventMock;

        /// <summary>
        /// Defines the team member saved event mock.
        /// </summary>
        private Mock<TeamMemberSavedEvent> teamMemberSavedEventMock;

        /// <summary>
        /// Defines the view model.
        /// </summary>
        private TeamMemberEditViewModel viewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberEditViewModelTests"/> class.
        /// </summary>
        public TeamMemberEditViewModelTests()
        {
            this.teamMemberDeletedEventMock = new Mock<TeamMemberDeletedEvent>();
            this.teamMemberSavedEventMock = new Mock<TeamMemberSavedEvent>();

            this.eventAggregatorMock = new Mock<IEventAggregator>();
            this.eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberSavedEvent>())
            .Returns(this.teamMemberSavedEventMock.Object);
            this.eventAggregatorMock.Setup(ea => ea.GetEvent<TeamMemberDeletedEvent>())
            .Returns(this.teamMemberDeletedEventMock.Object);

            this.dataProviderMock = new Mock<ITeamMemberDataProvider>();
            this.dataProviderMock.Setup(dp => dp.GetTeamMemberById(TeamMemberId))
            .Returns(new TeamMember { Id = TeamMemberId, FirstName = "Antonio" });

            this.messageDialogServiceMock = new Mock<IMessageDialogService>();

            this.viewModel = new TeamMemberEditViewModel(
                this.dataProviderMock.Object,
                this.eventAggregatorMock.Object,
                this.messageDialogServiceMock.Object);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Should accept the changes when save command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldAcceptChangesWhenSaveCommandIsExecuted()
        {
            this.viewModel.Load(TeamMemberId);
            this.viewModel.TeamMember.FirstName = "Changed";

            this.viewModel.SaveCommand.Execute(null);
            Assert.IsFalse(this.viewModel.TeamMember.IsChanged);
        }

        /// <summary>
        /// Should call the delete team member when delete command is executed and dialog result is yes.
        /// </summary>
        [TestMethod]
        public void ShouldCallDeleteTeamMemberWhenDeleteCommandIsExecutedAndDialogResultIsYes()
        {
            this.viewModel.Load(TeamMemberId);

            this.messageDialogServiceMock.Setup(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>())).Returns(MessageDialogResult.Yes);

            this.viewModel.DeleteCommand.Execute(null);

            this.dataProviderMock.Verify(
                dp => dp.DeleteTeamMeamber(TeamMemberId),
                Times.Exactly(1));

            this.messageDialogServiceMock.Verify(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Should call the save method of data provider when save command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldCallSaveMethodOfDataProviderWhenSaveCommandIsExecuted()
        {
            this.viewModel.Load(TeamMemberId);
            this.viewModel.TeamMember.FirstName = "Changed";

            this.viewModel.SaveCommand.Execute(null);
            this.dataProviderMock.Verify(dp => dp.SaveTeamMember(this.viewModel.TeamMember.Model), Times.Once);
        }

        /// <summary>
        /// Should create the new team member when null is passed to load method.
        /// </summary>
        [TestMethod]
        public void ShouldCreateNewTeamMemberWhenNullIsPassedToLoadMethod()
        {
            this.viewModel.Load(null);

            Assert.IsNotNull(this.viewModel.TeamMember);
            Assert.AreEqual(0, this.viewModel.TeamMember.Id);
            Assert.IsNull(this.viewModel.TeamMember.FirstName);
            Assert.IsNull(this.viewModel.TeamMember.LastName);
            Assert.IsNull(this.viewModel.TeamMember.Birthday);
            Assert.IsFalse(this.viewModel.TeamMember.IsDeveloper);

            this.dataProviderMock.Verify(dp => dp.GetTeamMemberById(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Should disable the delete command for new team member.
        /// </summary>
        [TestMethod]
        public void ShouldDisableDeleteCommandForNewTeamMember()
        {
            this.viewModel.Load(null);
            Assert.IsFalse(this.viewModel.DeleteCommand.CanExecute(null));
        }

        /// <summary>
        /// Should disable the delete command without load.
        /// </summary>
        [TestMethod]
        public void ShouldDisableDeleteCommandWithoutLoad()
        {
            Assert.IsFalse(this.viewModel.DeleteCommand.CanExecute(null));
        }

        /// <summary>
        /// Should disable the save command when team member is loaded.
        /// </summary>
        [TestMethod]
        public void ShouldDisableSaveCommandWhenTeamMemberIsLoaded()
        {
            this.viewModel.Load(TeamMemberId);

            Assert.IsFalse(this.viewModel.SaveCommand.CanExecute(null));
        }

        /// <summary>
        /// Should disable the save command without load.
        /// </summary>
        [TestMethod]
        public void ShouldDisableSaveCommandWithoutLoad()
        {
            Assert.IsFalse(this.viewModel.SaveCommand.CanExecute(null));
        }

        /// <summary>
        /// Should display the correct message in delete dialog.
        /// </summary>
        [TestMethod]
        public void ShouldDisplayCorrectMessageInDeleteDialog()
        {
            this.viewModel.Load(TeamMemberId);

            var f = this.viewModel.TeamMember;
            f.FirstName = "Antonio";
            f.LastName = "Fernández";

            this.viewModel.DeleteCommand.Execute(null);

            this.messageDialogServiceMock.Verify(
                d => d.ShowYesNoDialog(
                    "Delete Team Member",
                    $"Do you really want to delete the team member '{f.FirstName} {f.LastName}'"),
                Times.Once);
        }

        /// <summary>
        /// Should enable the delete command for existing team member.
        /// </summary>
        [TestMethod]
        public void ShouldEnableDeleteCommandForExistingTeamMember()
        {
            this.viewModel.Load(TeamMemberId);
            Assert.IsTrue(this.viewModel.DeleteCommand.CanExecute(null));
        }

        /// <summary>
        /// Should enable the save command when team member is changed.
        /// </summary>
        [TestMethod]
        public void ShouldEnableSaveCommandWhenTeamMemberIsChanged()
        {
            this.viewModel.Load(TeamMemberId);

            this.viewModel.TeamMember.FirstName = "Changed";

            Assert.IsTrue(this.viewModel.SaveCommand.CanExecute(null));
        }

        /// <summary>
        /// Should load the team member.
        /// </summary>
        [TestMethod]
        public void ShouldLoadTeamMember()
        {
            this.viewModel.Load(TeamMemberId);

            Assert.IsNotNull(this.viewModel.TeamMember);
            Assert.AreEqual(TeamMemberId, this.viewModel.TeamMember.Id);

            this.dataProviderMock.Verify(dp => dp.GetTeamMemberById(TeamMemberId), Times.Once);
        }

        /// <summary>
        /// Should not call the delete team member when delete command is executed and dialog result is no.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallDeleteTeamMemberWhenDeleteCommandIsExecutedAndDialogResultIsNo()
        {
            this.viewModel.Load(TeamMemberId);

            this.messageDialogServiceMock.Setup(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>())).Returns(MessageDialogResult.No);

            this.viewModel.DeleteCommand.Execute(null);

            this.dataProviderMock.Verify(
                dp => dp.DeleteTeamMeamber(TeamMemberId),
                Times.Exactly(0));

            this.messageDialogServiceMock.Verify(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Should not publish the team member deleted event when delete command is executed and dialog result is yes.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishTeamMemberDeletedEventWhenDeleteCommandIsExecutedAndDialogResultIsYes()
        {
            this.viewModel.Load(TeamMemberId);

            this.messageDialogServiceMock.Setup(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>())).Returns(MessageDialogResult.Yes);

            this.viewModel.DeleteCommand.Execute(null);

            this.teamMemberDeletedEventMock.Verify(
                e => e.Publish(TeamMemberId),
                Times.Exactly(1));

            this.messageDialogServiceMock.Verify(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Should publish the team member deleted event when delete command is executed and dialog result is yes.
        /// </summary>
        [TestMethod]
        public void ShouldPublishTeamMemberDeletedEventWhenDeleteCommandIsExecutedAndDialogResultIsYes()
        {
            this.viewModel.Load(TeamMemberId);

            this.messageDialogServiceMock.Setup(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>())).Returns(MessageDialogResult.Yes);

            this.viewModel.DeleteCommand.Execute(null);

            this.teamMemberDeletedEventMock.Verify(
                e => e.Publish(TeamMemberId),
                Times.Exactly(1));

            this.messageDialogServiceMock.Verify(
                ds => ds.ShowYesNoDialog(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Should publish the team member saved event when save command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldPublishTeamMemberSavedEventWhenSaveCommandIsExecuted()
        {
            this.viewModel.Load(TeamMemberId);
            this.viewModel.TeamMember.FirstName = "Changed";

            this.viewModel.SaveCommand.Execute(null);
            this.teamMemberSavedEventMock.Verify(e => e.Publish(this.viewModel.TeamMember.Model), Times.Once);
        }

        /// <summary>
        /// Should raise the can execute changed for delete command after load.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandAfterLoad()
        {
            var fired = false;
            this.viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            this.viewModel.Load(TeamMemberId);
            Assert.IsTrue(fired);
        }

        /// <summary>
        /// Should raise the can execute changed for delete command when accepting changes.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandWhenAcceptingChanges()
        {
            this.viewModel.Load(TeamMemberId);
            var fired = false;
            this.viewModel.TeamMember.FirstName = "Changed";
            this.viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            this.viewModel.TeamMember.AcceptChanges();
            Assert.IsTrue(fired);
        }

        /// <summary>
        /// Should raise the can execute changed for save command after load.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForSaveCommandAfterLoad()
        {
            var fired = false;
            this.viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            this.viewModel.Load(TeamMemberId);
            Assert.IsTrue(fired);
        }

        /// <summary>
        /// Should raise the can execute changed for save command when team member is changed.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseCanExecuteChangedForSaveCommandWhenTeamMemberIsChanged()
        {
            this.viewModel.Load(TeamMemberId);
            var fired = false;
            this.viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            this.viewModel.TeamMember.FirstName = "Changed";
            Assert.IsTrue(fired);
        }

        /// <summary>
        /// Should raise the property changed event for team member.
        /// </summary>
        [TestMethod]
        public void ShouldRaisePropertyChangedEventForTeamMember()
        {
            var fired = this.viewModel.IsPropertyChangedFired(
                            () => this.viewModel.Load(TeamMemberId),
                            nameof(this.viewModel.TeamMember));

            Assert.IsTrue(fired);
        }

        #endregion Methods
    }
}