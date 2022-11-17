using System;
using System.Diagnostics;

namespace WSLatexCUI
{
    internal class RunCommand
    {
        static public void Run(string appName, string args)
        {
            Console.WriteLine($"Run command: {appName} {args}");
            
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = appName;
            psInfo.Arguments = args;
            psInfo.CreateNoWindow = true;
            psInfo.UseShellExecute = false;
            psInfo.RedirectStandardOutput = true;

            var p = Process.Start(psInfo);
            if (p == null) throw new Exception("Failed to start command.");
            p.OutputDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
            p.Close();
        }

        static public void RunOnWSL(string appName, string args)
        {
            Run("wsl", "-- " + appName + " " + args);
        }
    }
}
