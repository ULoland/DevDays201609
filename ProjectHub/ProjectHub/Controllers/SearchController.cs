using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectHub.Service;

namespace ProjectHub.Controllers
{
    public class SearchController : Controller
    {
        private readonly SearchService _searchService;

        public SearchController()
        {
            _searchService = new SearchService();
        }

        // GET: Search
        public ActionResult Index(string query)
        {
            var result = _searchService.Search(query);

            return View(result);
        }
    }
}