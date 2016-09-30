using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectHub.Models;

namespace ProjectHub.Controllers
{
    public class TopicController : Controller
    {
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(TopicModel topic)
        {
            
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