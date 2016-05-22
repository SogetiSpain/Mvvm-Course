// ----------------------------------------------------------------------------
// <copyright file="FileDataService.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Model;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the file data service.
    /// </summary>
    public class FileDataService : IDataService
    {
        #region Fields

        /// <summary>
        /// Defines the storage file.
        /// </summary>
        private const string StorageFile = "SogetiSpain.TeamMembers.json";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Deletes a team member.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        public void DeleteTeamMeamber(int teamMemberId)
        {
            List<TeamMember> teamMembers = this.ReadFromFile();
            TeamMember existing = teamMembers.Single(e => (e.Id == teamMemberId));
            teamMembers.Remove(existing);
            this.SaveToFile(teamMembers);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            // This method is added as demo-purpose.
        }

        /// <summary>
        /// Gets all team members.
        /// </summary>
        /// <returns>
        /// All retrieved team members.
        /// </returns>
        public IEnumerable<SearchItem> GetAllTeamMembers()
        {
            return this.ReadFromFile()
                   .Select(e => new SearchItem
                   {
                       DisplayMember = $"{e.FirstName} {e.LastName}",
                       Id = e.Id
                   });
        }

        /// <summary>
        /// Gets a team member by its identifier.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        /// <returns>
        /// The retrieved team member.
        /// </returns>
        public TeamMember GetTeamMemberById(int teamMemberId)
        {
            List<TeamMember> teamMembers = this.ReadFromFile();
            return teamMembers.Single(e => (e.Id == teamMemberId));
        }

        /// <summary>
        /// Saves a team member.
        /// </summary>
        /// <param name="teamMember">The team member.</param>
        public void SaveTeamMember(TeamMember teamMember)
        {
            if (teamMember.Id <= 0)
            {
                this.InsertTeamMember(teamMember);
            }
            else
            {
                this.UpdateTeamMember(teamMember);
            }
        }

        /// <summary>
        /// Inserts a team member.
        /// </summary>
        /// <param name="teamMember">The team member.</param>
        private void InsertTeamMember(TeamMember teamMember)
        {
            List<TeamMember> teamMembers = this.ReadFromFile();
            int maxTeamMemberId = (teamMembers.Count == 0) ? 0 : teamMembers.Max(e => e.Id);
            teamMember.Id = maxTeamMemberId + 1;
            teamMembers.Add(teamMember);
            this.SaveToFile(teamMembers);
        }

        /// <summary>
        /// Reads from file.
        /// </summary>
        /// <returns>
        /// The team member list.
        /// </returns>
        private List<TeamMember> ReadFromFile()
        {
            if (!File.Exists(StorageFile))
            {
                return new List<TeamMember>
                {
                    new TeamMember
                    {
                        Birthday = new DateTime(1975, 9, 1),
                        FirstName = "Óscar",
                        Id = 1,
                        IsDeveloper = true,
                        LastName = "Fernández"
                    },
                    new TeamMember
                    {
                        Birthday = new DateTime(1975, 3, 1),
                        FirstName = "Carlos",
                        Id = 2,
                        IsDeveloper = true,
                        LastName = "Mendible"
                    }
                };
            }

            string json = File.ReadAllText(StorageFile);
            return JsonConvert.DeserializeObject<List<TeamMember>>(json);
        }

        /// <summary>
        /// Saves to file.
        /// </summary>
        /// <param name="teamMemberList">The team member list.</param>
        private void SaveToFile(List<TeamMember> teamMemberList)
        {
            string json = JsonConvert.SerializeObject(teamMemberList, Formatting.Indented);
            File.WriteAllText(StorageFile, json);
        }

        /// <summary>
        /// Updates a team member.
        /// </summary>
        /// <param name="teamMember">The team member.</param>
        private void UpdateTeamMember(TeamMember teamMember)
        {
            List<TeamMember> teamMembers = this.ReadFromFile();
            TeamMember existing = teamMembers.Single(e => (e.Id == teamMember.Id));
            int indexOfExisting = teamMembers.IndexOf(existing);
            teamMembers.Insert(indexOfExisting, teamMember);
            teamMembers.Remove(existing);
            this.SaveToFile(teamMembers);
        }

        #endregion Methods
    }
}