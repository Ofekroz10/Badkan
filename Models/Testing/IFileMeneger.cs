using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public interface IFileMeneger
    {
        public void MakeDir(string DirName);
        public void GitClone(string eror, string gitUrl);
        public void DeleteAllDir(string DirName);
        public string GetContent(string fPath);
        public string GetPath();
    }
}
