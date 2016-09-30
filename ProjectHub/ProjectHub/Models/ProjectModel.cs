using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHub.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<PostModel> Posts { get; set; }
        public virtual List<TopicModel> Topics { get; set; }


    }
}