using System;
using System.ComponentModel;
using System.IO;

namespace WSLatexCUI
{
    public class RunLatex
    {
        static string tempDir = 
            Environment.ExpandEnvironmentVariables("%USERPROFILE%\\.temp.wslatex");

        static public void Prepare()
        {
            Directory.CreateDirectory(tempDir);
            var logFile = new StreamWriter($"{tempDir}\\wslatex.output.log");
            Console.SetOut(logFile);
        }

        static string SaveTexToFile(string content)
        {
            string filename = "temp"; // TODO: Implement
            File.WriteAllText($"{tempDir}\\{filename}.tex", content);
            return filename;
        }
        
        static void GenerateDVIFromTex(string filename)
        {
            RunCommand.RunOnWSL(
                "pdflatex",
                $"-output-format dvi -shell-escape -interaction=batchmode {filename}.tex");
        }

        static void GenerateSVGFromDVI(string filename)
        {
            RunCommand.RunOnWSL(
                "dvisvgm", 
                $"--no-fonts -o {filename}.svg {filename}.dvi");
        }

        static public void GenerateSVGFromTex(string filename)
        {
            Environment.CurrentDirectory = tempDir;
            GenerateDVIFromTex(filename);
            GenerateSVGFromDVI(filename);
        }

        static public string GenerateSVGFromTexContent(string content)
        {
            string filename = SaveTexToFile(content);
            GenerateSVGFromTex(filename);
            return $"{tempDir}\\{filename}.svg";
        }
    }
}
