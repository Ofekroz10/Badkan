using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public class GitCloneException : Exception
    {
        public GitCloneException()
            : base("Git clone field!")
        {

        }
    }
}
