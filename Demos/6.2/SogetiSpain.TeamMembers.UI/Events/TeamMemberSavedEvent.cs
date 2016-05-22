// ----------------------------------------------------------------------------
// <copyright file="TeamMemberSavedEvent.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.Events
{
    using Model;
    using Prism.Events;

    /// <summary>
    /// Represents the event for saved team member.
    /// </summary>
    public class TeamMemberSavedEvent : PubSubEvent<TeamMember>
    {
    }
}