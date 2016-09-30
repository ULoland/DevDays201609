using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elasticsearch.Net;
using Nest;
using ProjectHub.Models;

namespace ProjectHub.Service
{
	public class ElasticService: ElasticClient
	{
		private ElasticClient _elastic;


		public ElasticService()
		{
			var nodes = new[] { "http://192.168.43.212:9200/" };
			
			var nodeUris = nodes.Select(n => new Uri(n));
			var connectionPool = new SniffingConnectionPool(nodeUris);

			var settings = new ConnectionSettings(connectionPool)
				.MaximumRetries(2)
				.DisableDirectStreaming(true)
				.SniffOnStartup(false)
				.SniffOnConnectionFault(false)
				.ThrowExceptions()
				.DefaultIndex("devdays")
				.DefaultTypeNameInferrer(def => def.FullName)
				.PingTimeout(new TimeSpan(1000));
			
			//check if we need to login to get connection
				_elastic =  new ElasticClient(settings);

		}

		public void IndexDocument<T>(T document) where T: class
		{
			_elastic.Index<T>(document);
		}

		public ElasticClient  GetClient()
		{
			return _elastic;
		}

		public ProjectModel  GetProject (string projectName)
		{
			var res = _elastic.Search<ProjectModel>(q => q.Query(qm => qm.Term("name.keyword", projectName)));
			var projectids = res.Hits.Select(m => m.Source).FirstOrDefault();
			return projectids;
		}
	}
}