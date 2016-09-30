using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ProjectHub.Models;
using ProjectHub.Service;

namespace ProjectHub.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            
			//get from Elastic
			var el = new ElasticService();
	        var cl = el.GetClient();
	        var res = cl.Search<PostModel>();
	        var model = res.Hits.Select(hit => {
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
			return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new PostModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostModel model)
        {
            ResolveDependencies(ref model);
			var el = new ElasticService();
			el.IndexDocument(model);
			return View(model);
        }

        private void ResolveDependencies(ref PostModel model)
        {
            ResolveProjects(ref model);
        }
        private void ResolveProjects(ref PostModel model)
        {
            var regex = new Regex(@"(^@|(?<=\s)@\w+)");

            var matches = regex.Matches(model.Heading + model.PostText);
            var el = new ElasticService();

            foreach (var match in matches)
            {
                //var id = el.
            }
        }
    }
}