// ----------------------------------------------------------------------------
// <copyright file="NavigationItemTests.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UITests.ViewModels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Prism.Events;
    using SogetiSpain.TeamMembers.UITests.Extensions;
    using UI.Events;
    using UI.ViewModels;

    /// <summary>
    /// Represents the tests of the navigation item.
    /// </summary>
    [TestClass]
    public class NavigationItemTests
    {
        const int _friendId = 7;
        private Mock<IEventAggregator> _eventAggregatorMock;
        private NavigationItem _viewModel;

        public NavigationItemTests()
        {
            _eventAggregatorMock = new Mock<IEventAggregator>();

            _viewModel = new NavigationItem(
                _eventAggregatorMock.Object,
                _friendId,
                "Antonio");
        }

        [TestMethod]
        public void ShouldPublishOpenFriendEditViewEvent()
        {
            var eventMock = new Mock<OpenTeamMemberEditViewEvent>();
            _eventAggregatorMock
              .Setup(ea => ea.GetEvent<OpenTeamMemberEditViewEvent>())
              .Returns(eventMock.Object);

            _viewModel.OpenTeamMemberEditViewCommand.Execute(null);

            eventMock.Verify(e => e.Publish(_friendId), Times.Once);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForDisplayMember()
        {
            var fired = _viewModel.IsPropertyChangedFired(() =>
            {
                _viewModel.DisplayMember = "Changed";
            }, nameof(_viewModel.DisplayMember));

            Assert.IsTrue(fired);
        }
    }
}