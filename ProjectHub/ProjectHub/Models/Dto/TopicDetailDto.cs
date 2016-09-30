using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHub.Models.Dto
{
    public class TopicDetailDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<PostModel> Posts { get; set; }
    }
}