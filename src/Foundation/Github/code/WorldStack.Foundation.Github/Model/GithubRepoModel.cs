using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldStack.Foundation.Github.Model
{
    /// <summary>
    /// Simple Model for Github Repo
    /// </summary>
    public class GithubRepoModel
    {
        public string UserName { get; set; }

        public int ForkCount { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int StarsCount { get; set; }

        /// <summary>
        /// formated DateTime
        /// </summary>
        public string LastPush { get; set; }

        public string HtmlUrl { get; set; }

        public bool isWinningTeam { get; set; } = false;
    }
}