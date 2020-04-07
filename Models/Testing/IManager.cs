using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public interface IManager
    {
        public string GetCurOutput();
        public string GetCurEror();
        public void Run();
        public void Compile();
        public void MakeDir();
        public void DeleteDir();
        public void GitClone(string url);
    }
}
