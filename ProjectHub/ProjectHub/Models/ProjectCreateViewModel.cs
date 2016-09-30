using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHub.Models
{
    public class ProjectCreateViewModel
    {
        public ProjectModel projectModel {get;set;}
        public List<TopicModel> availableTopics { get; set; }
        public List<PostModel> posts { get; set; }
    }
}