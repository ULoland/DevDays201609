using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectHub.Models;
using ProjectHub.Service;

namespace ProjectHub.Controllers
{
    public class TopicController : Controller
    {
        private readonly ElasticService _elasticService;
	    private TopicService _topicService;
	    private PostService _postService;

	    public TopicController()
        {
	        _topicService = new TopicService();
        }

        // GET: Topic
        public ActionResult Index()
        {
	        var model = _topicService.GetTopics();
            return View(model);
        }

		public IEnumerable<PostModel>  GetListOfPostsForTopic(string topicID)
		{
			var posts = _postService.GetPostsForTopic(topicID);
			return posts;
		}

		public ActionResult Create()
        {
            var model = new TopicModel();
            return View(model);
        }


		public ActionResult Details(string id)
		{
			var topic = _topicService.GetTopic(id);
			return View(topic);
		}


		[HttpPost]
        public ActionResult Create(TopicModel topic)
        {
            _elasticService.IndexDocument(topic);
            
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit()
        {
            return View(new TopicModel());
        }

        [HttpPost]
        public ActionResult Edit(TopicModel topic)
        {

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete()
        {
            return View(new TopicModel());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}