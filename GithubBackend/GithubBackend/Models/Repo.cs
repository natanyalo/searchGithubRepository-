using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubBackend.Models
{
    public class Repo
    {
        public string id { get; set; }
        public Owner owner { get; set; }
        public string full_name { get; set; }
    }
}
