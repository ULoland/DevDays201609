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
	    private ProjectService _projectService;

	    public ActionResult Index ()
        {

	        var model = _projectService.GetProjects();
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
	        var model = _projectService.GetProject(Id);
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