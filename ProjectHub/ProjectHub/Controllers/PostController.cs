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
            ResolveTopics(ref model);
        }
        private void ResolveProjects(ref PostModel model)
        {
            var regex = new Regex(@"(^@|(?<=\s)@\w+)");

            var matches = regex.Matches(model.Heading + model.PostText)
                .OfType<Match>().Select(m => m.Groups[0].Value).Distinct();
            var el = new ElasticService();

            foreach (var match in matches)
            {
                var project = el.GetProject(match.Replace("@", ""));
                if (project != null)
                {
                    model.Heading = model.Heading.Replace(match, match + $"[{project.Id}]");
                    model.PostText = model.PostText.Replace(match, match + $"[{project.Id}]");
                    model.Project.Add(project);
                }
            }
        }

        private void ResolveTopics(ref PostModel model)
        {
            var regex = new Regex(@"(^#|(?<=\s)#\w+)");

            var matches = regex.Matches(model.Heading + model.PostText)
                .OfType<Match>().Select(m => m.Groups[0].Value).Distinct();
            var el = new ElasticService();

            foreach (var match in matches)
            {
                var topic = el.GetProject(match.Replace("#", ""));
                if (topic != null)
                {
                    model.Heading = model.Heading.Replace(match, match + $"[{topic.Id}]");
                    model.PostText = model.PostText.Replace(match, match + $"[{topic.Id}]");
                    //model.Topics.Add(topic);
                }
            }
        }
    }
}