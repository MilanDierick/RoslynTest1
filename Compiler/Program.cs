using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Compiler
{
    static class Program
    {
        internal static void Main(string[] args)
        {
            string path = @"E:\dev\Projects\RoslynTest\RoslynTest\RoslynTest.sln";
            var properties = new Dictionary<string, string>() {};
            var msws = MSBuildWorkspace.Create(properties);

            msws.WorkspaceFailed += (s, e) =>
            {
                Console.WriteLine($"Workspace failed with: {e.Diagnostic}");
            };

            var soln = msws.OpenSolutionAsync(path).Result;
            var comp = soln.Projects.First(x => x.Name == "RoslynTest").GetCompilationAsync().Result;
            var errs = comp.GetDiagnostics().Where(n => n.Severity == DiagnosticSeverity.Error).ToList();

            comp.Emit(@"E:\dev\Projects\RoslynTest\RoslynTest\out\RoslynConsole.exe");

            Console.WriteLine(".exe is created!");
            Console.ReadLine();
        }
    }
}
