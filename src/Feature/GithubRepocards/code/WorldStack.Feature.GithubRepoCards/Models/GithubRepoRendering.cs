using Sitecore.XA.Foundation.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldStack.Feature.GithubRepoCards.Models
{
    /// <summary>
    /// Simple Rendering Model to list Repositories
    /// </summary>
    public class GithubRepoRenderingModel : RenderingModelBase
    {
        public IEnumerable<Foundation.Github.Model.GithubRepoModel> Repositories { get; set; }
    }

}