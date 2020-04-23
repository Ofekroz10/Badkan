using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GitHubLink { get; set; }
    }
}
