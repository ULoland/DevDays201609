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
            var model = new List<PostModel>();
            //get from Elastic
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