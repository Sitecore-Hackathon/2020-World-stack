using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WorldStack.Foundation.Github.Repository
{
    /// <summary>
    /// Api To retrieve Github Repository Info
    /// </summary>
    public class GithubRepository
    {
        /// <summary>
        /// Github Octakit Client , Header Value and Accestoken
        /// </summary>
        private readonly GitHubClient _client;
        private readonly string _headerValue = Sitecore.Configuration.Settings.GetSetting("GithubRepo.HeaderValue", "HackathonApp");
        private readonly string _accessToken = Sitecore.Configuration.Settings.GetSetting("GithubRepo.Token", "");
        private readonly string _userName = Sitecore.Configuration.Settings.GetSetting("GithubRepo.UserName", "nkdram");

        /// <summary>
        /// Constructor for Github Access
        /// </summary>
        public GithubRepository()
        {
            ///Setting up GithubClient
            _client = new GitHubClient(new ProductHeaderValue(_headerValue));
            _client.Credentials = new Credentials(_accessToken);
        }

        /// <summary>
        /// USed to retrieve Github repositories using Names
        /// </summary>
        /// <param name="namePrefix"></param>
        /// <returns></returns>
        public IEnumerable<Model.GithubRepoModel> GetGithubDetailsUsingName(string namePrefix, string winningTeams = null)
        {
            var results = Task.Run(() => this.GetRepositories(namePrefix));
            if (results == null || results.IsCanceled)
                return null;

            if (winningTeams == null)
                ///Maps the model
                return results.Result.Select(x => new Model.GithubRepoModel()
                {
                    UserName = x.FullName,
                    Description = x.Description,
                    ForkCount = x.ForksCount,
                    StarsCount = x.StargazersCount,
                    Url = x.GitUrl,
                    HtmlUrl = x.HtmlUrl,
                    LastPush = GetTimeDifference(x.UpdatedAt.DateTime)
                });

            var winningTeamArr = winningTeams.Split(';').Select(x => x.ToLower()).ToList();
            return results.Result.Select(x => new Model.GithubRepoModel()
            {
                UserName = x.FullName,
                Description = x.Description,
                ForkCount = x.ForksCount,
                StarsCount = x.StargazersCount,
                Url = x.GitUrl,
                HtmlUrl = x.HtmlUrl,
                LastPush = GetTimeDifference(x.UpdatedAt.DateTime),
                isWinningTeam = winningTeams.Contains(x.FullName)
            });
        }

        //shows time difference
        private string GetTimeDifference(DateTime time)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - time.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }

        }

        /// <summary>
        /// Search Github Name, Description and Readme files to match a string
        /// </summary>
        /// <param name="namePrefix"></param>
        /// <returns></returns>
        public IEnumerable<Model.GithubRepoModel> SearchGithubFields(string keywords)
        {
            var results = Task.Run(() => this.GetRepositoriesBasedOnKeywords(keywords));
            if (results == null || results.IsCanceled)
                return null;

            ///Maps the model
            return results.Result.Select(x => new Model.GithubRepoModel()
            {
                UserName = x.FullName,
                Description = x.Description,
                ForkCount = x.ForksCount,
                StarsCount = x.StargazersCount,
                Url = x.Url
            });
        }

        /// <summary>
        /// Get Repositories Based on Prefix
        /// </summary>
        /// <param name="hackathonPrefix"></param>
        private async Task<IEnumerable<Octokit.Repository>> GetRepositories(string namePrefix)
        {
            //Search Repostiory Based on Prefix
            var request = new SearchRepositoriesRequest(namePrefix);
            request.User = _userName;

            request.In = new[] {
                InQualifier.Name
            };
            request.SortField = RepoSearchSort.Updated;
            //_client.User = 
            var results = await _client.Search.SearchRepo(request);
            return results.Items;
        }

        /// <summary>
        /// Get list of Repositories based on Keywords
        /// </summary>
        /// <param name="keywords"></param>
        private async Task<IEnumerable<Octokit.Repository>> GetRepositoriesBasedOnKeywords(string keywords)
        {
            //Search Repostiory Based on Prefix
            var request = new SearchRepositoriesRequest(keywords);

            request.User = _userName;
            request.In = new[] {
                InQualifier.Name,
                InQualifier.Description,
                InQualifier.Readme
            };
            request.SortField = RepoSearchSort.Updated;
            var result = await _client.Search.SearchRepo(request);
            return result.Items;
        }
    }
}