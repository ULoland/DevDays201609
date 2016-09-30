using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHub.Models
{
    public class PostModel
    {
        public string Id { get; set; }
        public string Heading { get; set; }
        public string PostText { get; set; }

        public virtual List<ProjectModel> Project { get; set; }
        public virtual List<TopicModel> Topics { get; set; }

    }
}