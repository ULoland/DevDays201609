using System;
using System.Collections.Generic;
using System.Linq;
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
	        var model = res.Hits.Select(hit => hit.Source).ToList();
			return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new PostModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PostModel model)
        {
			var el = new ElasticService();
			el.IndexDocument(model);
			return View(model);
        }
    }
}