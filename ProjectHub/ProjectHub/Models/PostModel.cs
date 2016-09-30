using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHub.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string PostText { get; set; }

        public virtual ProjectModel Project { get; set; }
        public virtual List<TopicModel> Topics { get; set; }

    }
}