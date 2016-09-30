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
	    private PostService _postService;
	    // GET: Post
	    public PostController()
	    {
		    _postService = new PostService();
	    }

        public ActionResult Index()
        {
            
			//get from Elastic
	        var model = _postService.GetPosts();
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
			return View(new PostModel());
        }

        private void ResolveDependencies(ref PostModel model)
        {
            ResolveProjects(ref model);
            ResolveTopics(ref model);
        }
        private void ResolveProjects(ref PostModel model)
        {
            var regex = new Regex(@"(^@|(?<=\s)@\w+)");

            var matches = regex.Matches(model.Heading + " " + model.PostText)
                .OfType<Match>().Select(m => m.Groups[0].Value).Distinct();
	        var projectService = new ProjectService();
			foreach (var match in matches)
            {
                var project = projectService.GetProjectByName(match.Replace("@", ""));
                if (project != null)
                {
                    model.Heading = model.Heading.Replace(match, match + $"[{project.Id}]");
                    model.PostText = model.PostText.Replace(match, match + $"[{project.Id}]");
                    if (model.Project == null)
                        model.Project = new List<ProjectModel>();
                    model.Project.Add(project);
                }
            }
        }

        private void ResolveTopics(ref PostModel model)
        {
            var regex = new Regex(@"(^#|(?<=\s)#\w+)");

            var matches = regex.Matches(model.Heading + model.PostText)
                .OfType<Match>().Select(m => m.Groups[0].Value).Distinct();
            var topicservice = new TopicService();

            foreach (var match in matches)
            {
                var topic = topicservice.GetTopicByName(match.Replace("#", ""));
                if (topic != null)
                {
                    model.Heading = model.Heading.Replace(match, match + $"[{topic.Id}]");
                    model.PostText = model.PostText.Replace(match, match + $"[{topic.Id}]");
                    model.Topics.Add(topic);
                }
            }
        }
    }
}