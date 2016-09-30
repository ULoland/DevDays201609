using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectHub.Models;

namespace ProjectHub.Service
{
	public class ProjectService

	{
		private ElasticService _elasticService;

		public ProjectService()
		{
			_elasticService = new ElasticService();
		}

		public List<ProjectModel> GetProjects()
		{

			var cl = _elasticService.GetClient();
			var res = cl.Search<ProjectModel>();
			var model = res.Hits.Select(hit =>
			{
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
			return model;
		}

		public ProjectModel GetProject(string id)
		{
			
			ProjectModel model = _elasticService.GetClient().Get<ProjectModel>(id).Source;
			return model;
		}
	}
}