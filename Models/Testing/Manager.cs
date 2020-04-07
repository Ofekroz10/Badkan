using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public class Manager : IManager
    {
        public readonly string outFile;
        public readonly string erorFile;
        public string DirName { get; set; }

        private IClie cli;

        public Manager(string dirName, string outFile = "output.txt", string erorFile = "eror.txt")
        {
            this.DirName = dirName;
            this.outFile = outFile;
            this.erorFile = erorFile;
            this.cli = new Cli("C:\\");
            this.cli.MakeDir(dirName);
        }

        public string GetCurOutput()
        {
            string path = cli.GetPath() + "\\" + outFile;
            return cli.GetContent(path);
        }

        public string GetCurEror()
        {
            string path = cli.GetPath() + "\\" + erorFile;
            return cli.GetContent(path);
        }

        public void Run()
        {
            cli.Run(erorFile, outFile);
        }

        public void Compile()
        {
            int fCount = CountFileInDir();
            cli.CompileAll(erorFile);
            int fCountAfter = CountFileInDir();
            if (fCount == fCountAfter)
                throw new CompileExcaption(); 


        }

        public void MakeDir()
        {
            cli.MakeDir(DirName);
        }

        public void DeleteDir()
        {
            cli.DeleteAllDir(DirName);
        }

        public void GitClone(string url)
        {
            int preClone = CountFileInDir();
            cli.GitClone(erorFile, url);
            int afterClone = CountFileInDir();
            if (preClone == afterClone)
                throw new GitCloneException();

        }

        private int CountFileInDir()
        {
            string temp = cli.GetPath();
            int fCount = Directory.GetFiles(cli.GetPath() ,
                "*", SearchOption.TopDirectoryOnly).Length;
            return fCount;
        }
    }
}
