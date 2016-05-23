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
        #region Fields

        /// <summary>
        /// Defines the team member identifier.
        /// </summary>
        private const int TeamMemberId = 7;

        /// <summary>
        /// Defines the event aggregator mock.
        /// </summary>
        private Mock<IEventAggregator> eventAggregatorMock;

        /// <summary>
        /// Defines the view model.
        /// </summary>
        private NavigationItem viewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationItemTests"/> class.
        /// </summary>
        public NavigationItemTests()
        {
            this.eventAggregatorMock = new Mock<IEventAggregator>();

            this.viewModel = new NavigationItem(
                this.eventAggregatorMock.Object,
                TeamMemberId,
                "Antonio");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Should publish the open team member edit view event.
        /// </summary>
        [TestMethod]
        public void ShouldPublishOpenTeamMemberEditViewEvent()
        {
            var eventMock = new Mock<OpenTeamMemberEditViewEvent>();
            this.eventAggregatorMock
            .Setup(ea => ea.GetEvent<OpenTeamMemberEditViewEvent>())
            .Returns(eventMock.Object);

            this.viewModel.OpenTeamMemberEditViewCommand.Execute(null);

            eventMock.Verify(e => e.Publish(TeamMemberId), Times.Once);
        }

        /// <summary>
        /// Should raise the property changed event for display member.
        /// </summary>
        [TestMethod]
        public void ShouldRaisePropertyChangedEventForDisplayMember()
        {
            var fired = this.viewModel.IsPropertyChangedFired(
                            () =>
            {
                this.viewModel.DisplayMember = "Changed";
            },
            nameof(this.viewModel.DisplayMember));

            Assert.IsTrue(fired);
        }

        #endregion Methods
    }
}