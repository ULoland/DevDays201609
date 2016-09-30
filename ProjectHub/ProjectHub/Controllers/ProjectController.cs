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
            return View();
        }

        [HttpGet]
        public ActionResult CreateTopic()
        {
            var model = new ProjectModel();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTopic(ProjectModel model)
        {
            var el = new ElasticService();
            el.IndexDocument(model);
            return View(model);
        }

        //[HttpPost]
        //public ActionResult AddPost(int Id, PostModel Post)
        //{
        //    //Find by Projct ID
        //    //Add post to project

        //    return View();
        //}

        //public ActionResult GetProjectList()
        //{
        //    ProjectModel pm1 = new ProjectModel()
        //    {
        //        Name = "A project"
        //    };

        //    return View(pm1);
        //}
    }
}