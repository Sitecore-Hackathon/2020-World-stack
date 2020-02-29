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
        public IEnumerable<Model.GithubRepoModel> GetGithubDetailsUsingName(string namePrefix)
        {
            var results = Task.Run(() => this.GetRepositories(namePrefix));
            if (results == null || results.IsCanceled)
                return null;

            ///Maps the model
            return results.Result.Select(x => new Model.GithubRepoModel()
            {
                UserName = x.FullName,
                Description = x.Description,
                ForkCount = x.ForksCount,
                StarsCount = x.StargazersCount,
                Url = x.GitUrl
            });
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

            var result = await _client.Search.SearchRepo(request);
            return result.Items;
        }
    }
}