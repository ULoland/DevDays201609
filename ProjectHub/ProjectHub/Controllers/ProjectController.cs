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

	    public ProjectController()
	    {
		    _projectService = new ProjectService();
	    }

	    public ActionResult Index ()
        {

	        var model = _projectService.GetProjects();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProjectCreateViewModel();

            var el = new ElasticService();
            var cl = el.GetClient();
            var res = cl.Search<TopicModel>();
            var TopicList = res.Hits.Select(hit => {
                hit.Source.Id = hit.Id;
                return hit.Source;
            }).ToList();

            
            model.projectModel = new ProjectModel();
            model.availableTopics = TopicList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProjectCreateViewModel model, string[] checkedTopics)
        {
            var el = new ElasticService();
            List<TopicModel> topicList = new List<TopicModel>();

            foreach (var items in checkedTopics)
            {
                TopicModel topic = el.GetClient().Get<TopicModel>(items).Source;
                topicList.Add(topic);
            }

            model.projectModel.Topics = topicList;
            el.IndexDocument(model.projectModel);
            return RedirectToAction("Index");
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