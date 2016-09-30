using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectHub.Models;
using ProjectHub.Models.Dto;
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
            _postService = new PostService();
        }

        // GET: Topic
        public ActionResult Index()
        {
	        var model = _topicService.GetTopics();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new TopicModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TopicModel topic)
        {
            _elasticService.IndexDocument(topic);
            
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(string id)
        {
			var topic = _topicService.GetTopic(id);

            var model = new TopicDetailDto
            {
                Name = topic.Name,
                Description = topic.Description,
                Posts = _postService.GetPostsForTopic(id)
            };

            return View(model);
        }

        //public IEnumerable<PostModel>  GetListOfPostsForTopic(string topicID)
        //{
        //    var cl = _elasticService.GetClient();
        //    var res = cl.Search<PostModel>(mmd => mmd.Query(mq => mq.Term(qmt => qmt.Id,topicID) ));
        //    var posts = res.Hits.Select(hit => {
        //                                           hit.Source.Id = hit.Id;
        //                                           return hit.Source;
        //    }).ToList();
        //    return posts;
        //}

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