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

        public TopicController()
        {
            _elasticService = new ElasticService();
        }

        // GET: Topic
        public ActionResult Index()
        {
            var vm = new List<TopicModel>
            {
                new TopicModel
                {
                    Name = "C#",
                    Description = "C# Language"
                },
                new TopicModel
                {
                    Name = "ASP.NET",
                    Description = "ASP.NET Framework"
                },
            };

            return View(vm);
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