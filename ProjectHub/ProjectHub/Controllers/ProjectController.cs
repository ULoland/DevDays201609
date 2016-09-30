using ProjectHub.Models;
using ProjectHub.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectHub.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult Index ()
        {
            var el = new ElasticService();
            var cl = el.GetClient();
            var res = cl.Search<ProjectModel>();
            var model = res.Hits.Select(hit => {
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProjectModel();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProjectModel model)
        {
            var el = new ElasticService();
            el.IndexDocument(model);
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string Id)
        {
            var el = new ElasticService();
            ProjectModel model = el.GetClient().Get<ProjectModel>(Id).Source;
            return View(model);
        }

        //[HttpPost]
        //public ActionResult AddPost(int Id, PostModel Post)
        //{
        //    //Find by Projct ID
        //    //Add post to project

        //    return View();
        //}
    }
}