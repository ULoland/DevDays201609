using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectHub.Models;

namespace ProjectHub.Service
{
	public class TopicService
	{
		private ElasticService _elasticService;

		public TopicService()
		{
			_elasticService = new ElasticService();
		}
		public List<TopicModel> GetTopics()
		{
			var cl = _elasticService.GetClient();
			var res = cl.Search<TopicModel>();
			var model = res.Hits.Select(hit =>
			{
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
			return model;
		}

		public TopicModel GetTopic(string id)
		{
			TopicModel topic = _elasticService.GetClient().Get<TopicModel>(id).Source;
			return topic;
		}
	}
}