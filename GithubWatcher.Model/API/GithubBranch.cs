using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubWatcher.Model.API
{
    public class GithubBranch
    {
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
   
}