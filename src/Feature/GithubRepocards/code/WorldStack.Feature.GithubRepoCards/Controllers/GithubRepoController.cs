using Sitecore.Mvc.Presentation;
using Sitecore.XA.Foundation.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldStack.Foundation.Github.Repository;

namespace WorldStack.Feature.GithubRepoCards.Controllers
{
    /// <summary>
    /// Github Repo Controller
    /// </summary>
    public class GithubRepoController : StandardController
    {
        private readonly GithubRepository _repository;

        public GithubRepoController()
        {
            _repository = new GithubRepository();
        }

        // GET: GithubRepo
        public ActionResult GetGithubRepoList()
        {
            var keyword = RenderingContext.Current.Rendering.Parameters[Templates.RenderingKeywordPrefix_Param];
            var model = new Models.GithubRepoRenderingModel()
            {
                Repositories = _repository.GetGithubDetailsUsingName(keyword)
            };
            return View("~/Views/GithubRepo/GithubRepoList.cshtml", model);
        }
    }
}