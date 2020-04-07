using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public interface ICompiler
    {
        public void CompileAll(string erorFile);
        public void Run(string erorFile, string outputFile);
    }
}
