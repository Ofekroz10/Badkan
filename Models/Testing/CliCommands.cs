using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Testing
{
    public static class CliCommands
    {
        public const string MakeDirCommand = "mkdir ";
        public const string GitCloneCommand = "git clone ";
        public const string DelDirCommand = "rd /s /q ";
        public const string ContentCommand = "type ";
        public const string CdCommand = "cd ";
        public const string GitOnlyFile = " .";
        

        private const string CompileCsCommand = "csc *.cs";

        public static string CompileCsWithLogCommand(string eror)
        {
            return CompileCsCommand + " > " + eror;
        }

        public static string RunTestWithLogCommand(string eror, string output, string fileName = "Test.exe")
        {
            return fileName + " > " + output + " 2> " + eror;
        } 
        
        public static string GitCloneCommandWithLog(string eror, string url)
        {
            return GitCloneCommand + url + GitOnlyFile;
        }

        public static string DelAllFiles(string dirName)
        {
            return "del /f /s /q " + dirName + " 1>nul";
        }



        
    }
}
