using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public class Cli : IClie
    {
        public Process Cmd { get; set; }
        private bool Working;
        public string path;
        private string defaultPath;

        public Cli(string path)
        {
            Cmd = new Process();
            Working = false;
            this.path = path;
            this.defaultPath = path;
        }

        public void MakeDir(string DirName)
        {
            string command = CliCommands.MakeDirCommand + DirName;
            this.StartProcess(command);
            path = path+DirName;
        }

        public void GitClone(string eror, string gitUrl)
        {
            string command = CliCommands.GitCloneCommandWithLog(eror, gitUrl);
            this.StartProcess(command);
        }

        public void DeleteAllDir(string DirName)
        {
            this.path = defaultPath;
            string command = CliCommands.DelDirCommand + DirName;
            StartProcess(command);
        }

        private void StartProcess(string command)
        {
            Working = true;
            Cmd.StartInfo.FileName = "cmd.exe";
            Cmd.StartInfo.RedirectStandardInput = true;
            Cmd.StartInfo.RedirectStandardOutput = true;
            Cmd.StartInfo.CreateNoWindow = true;
            Cmd.StartInfo.UseShellExecute = false;
            Cmd.Start();

            if(path!= "")
                Cmd.StandardInput.WriteLine(CliCommands.CdCommand+path);

            Cmd.StandardInput.WriteLine(command);
            Cmd.StandardInput.Flush();
            Cmd.StandardInput.Close();
            Cmd.WaitForExit();
            Working = false;
           
        }

        public void CompileAll(string erorFile)
        {
            StartProcess(CliCommands.CompileCsWithLogCommand(erorFile));
        }

        public void Run(string erorFile, string outFile)
        {
            StartProcess(CliCommands.RunTestWithLogCommand(erorFile,outFile));
        }

        public string GetContent(string fPath)
        {
            string text = System.IO.File.ReadAllText(fPath);
            return text;
        }

        public string GetPath()
        {
            return path;
        }
    }
}
