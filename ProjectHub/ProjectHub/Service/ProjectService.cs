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
			var elasticHit = _elasticService.GetClient().Get<ProjectModel>(id);
			ProjectModel model = elasticHit.Source;
			model.Id = elasticHit.Id;
			return model;
		}

		public ProjectModel GetProjectByName(string projectName)
		{
			var res = _elasticService.GetClient().Search<ProjectModel>(q => q.Query(qm => qm.Term("name.keyword", projectName)));
			var projectids = res.Hits.Select(m =>
			{
				m.Source.Id = m.Id;
				return m.Source;
			}).FirstOrDefault();
			return projectids;
		}
	}
}