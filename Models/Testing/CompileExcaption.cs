using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public class CompileExcaption : Exception
    {
        public CompileExcaption()
            :base("Compiled field!")
        {

        }
    }
}
