using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
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
       /// Get Repositories Based on Prefix
       /// </summary>
       /// <param name="hackathonPrefix"></param>
        public async void GetRepositories(string hackathonPrefix)
        {
            //Search Repostiory Based on Prefix
            var request = new SearchRepositoriesRequest(hackathonPrefix);
            request.In = new[] {
                InQualifier.Name
            };

            var result = await _client.Search.SearchRepo(request);
        }

        /// <summary>
        /// Get list of Repositories based on Keywords
        /// </summary>
        /// <param name="keywords"></param>
        public async void GetRepositoriesBasedOnKeywords(string keywords)
        {
            //Search Repostiory Based on Prefix
            var request = new SearchRepositoriesRequest(keywords);
            request.In = new[] {
                InQualifier.Name,
                InQualifier.Description,
                InQualifier.Readme
            };

            var result = await _client.Search.SearchRepo(request);
        }
    }
}