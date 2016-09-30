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
		public List<TopicModel> GetTopics(int numberOfTopics=20)
		{
			var cl = _elasticService.GetClient();
			var res = cl.Search<TopicModel>(sss => sss.Size (numberOfTopics));
			var model = res.Hits.Select(hit =>
			{
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
			return model;
		}

		public TopicModel GetTopic(string id)
		{
			var elasticModel = _elasticService.GetClient().Get<TopicModel>(id);
			TopicModel topic = elasticModel.Source;
			topic.Id = elasticModel.Id;
			return topic;
		}

		public TopicModel GetTopicByName (string topicName)
		{
			var res = _elasticService.GetClient().Search<TopicModel>(q => q.Query(qm => qm.Match (qmm => qmm.Field("name.keyword").Query(topicName))));
			var topic = res.Hits.Select(m =>
			{
				m.Source.Id = m.Id;
				return m.Source;
			}).FirstOrDefault();
			return topic;
		}
	}
}