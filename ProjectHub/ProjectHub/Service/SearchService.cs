using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectHub.Models;

namespace ProjectHub.Service
{
	public class SearchService
	{

		private ElasticService _elasticService;

		public SearchService()
		{
			_elasticService = new ElasticService();
		}

		public List<dynamic> Search(string query, Dictionary<string, string> filters = null)
		{

			var qr =
				_elasticService.GetClient()
					.Search<dynamic>(q => q.AllTypes().Query(qm => qm.Match(qmm => qmm.Query(query).Field("_all"))));

			return qr.Hits.ToList<dynamic>();
		}

	}
}