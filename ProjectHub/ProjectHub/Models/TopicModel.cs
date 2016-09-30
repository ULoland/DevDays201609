﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;

namespace ProjectHub.Models
{
    public class TopicModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
		[Keyword]
		public string KewordName => Name;

        public string Description { get; set; }

    }
}