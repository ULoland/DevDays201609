using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectHub.Models;

namespace ProjectHub.Service
{
	public class PostService
	{
		private ElasticService _elasticService;

		public PostService()
		{
			_elasticService = new ElasticService();
		}
		public IEnumerable<PostModel> GetPostsForTopic(string topicID)
		{
			var cl = _elasticService.GetClient();
			var res = cl.Search<PostModel>(mmd => mmd.Query(mq => mq.Term("Topics.Id", topicID)));
			var posts = res.Hits.Select(hit => {
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
			return posts;
		}

		public IEnumerable<PostModel> GetPostsForProject(string projectId)
		{
			var cl = _elasticService.GetClient();
			var res = cl.Search<PostModel>(mmd => mmd.Query(mq => mq.Term("Projects.Id", projectId)));
			var posts = res.Hits.Select(hit => {
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
			return posts;
		}



		public List<PostModel> GetPosts()
		{
			
			var cl = _elasticService.GetClient();
			var res = cl.Search<PostModel>();
			var model = res.Hits.Select(hit => {
				hit.Source.Id = hit.Id;
				return hit.Source;
			}).ToList();
			return model;
		}

		public PostModel GetPost(string id)
		{
			var elasticHit = _elasticService.GetClient().Get<PostModel>(id);
			PostModel model = elasticHit.Source;
			model.Id = elasticHit.Id;
			return model;
		}

	}
}