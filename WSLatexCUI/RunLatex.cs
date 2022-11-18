using System;
using System.ComponentModel;
using System.IO;

namespace WSLatexCUI
{
    public class RunLatex
    {
        static string tempDir = 
            Environment.ExpandEnvironmentVariables("%USERPROFILE%\\.temp.wslatex");
        static string tempName = "temp";

        static public void Prepare(string _tempName = null)
        {
            if (_tempName != null) tempName = _tempName;
            Directory.CreateDirectory(tempDir);
            var logFile = new StreamWriter($"{tempDir}\\wslatex.output.log");
            Console.SetOut(logFile);
        }

        static void SaveTexToFile(string content)
        {
            File.WriteAllText($"{tempDir}\\{tempName}.tex", content);
        }
        
        static void GenerateDVIFromTex()
        {
            RunCommand.RunOnWSL(
                "pdflatex",
                $"-output-format dvi -shell-escape -interaction=batchmode {tempName}.tex");
        }

        static void GenerateSVGFromDVI()
        {
            RunCommand.RunOnWSL(
                "dvisvgm", 
                $"--no-fonts -o {tempName}.svg {tempName}.dvi");
        }

        static public void GenerateSVGFromTex()
        {
            Environment.CurrentDirectory = tempDir;
            GenerateDVIFromTex();
            GenerateSVGFromDVI();
        }

        static public string GenerateSVGFromTexContent(string content)
        {
            SaveTexToFile(content);
            GenerateSVGFromTex();
            string outPath = $"{tempDir}\\{tempName}.svg";
            if (File.Exists(outPath)) return outPath;
            return null;
        }

        static public void ClearTempFiles()
        {
            System.IO.File.Delete($"{tempDir}\\{tempName}.aux");
            System.IO.File.Delete($"{tempDir}\\{tempName}.dvi");
            System.IO.File.Delete($"{tempDir}\\{tempName}.log");
            System.IO.File.Delete($"{tempDir}\\{tempName}.svg");
            System.IO.File.Delete($"{tempDir}\\{tempName}.tex");
        }
    }
}
