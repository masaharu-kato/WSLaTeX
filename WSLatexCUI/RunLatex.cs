using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSLatexCUI
{
    public class RunLatex
    {
        static string SaveTexToFile(string content)
        {
            string filename = "temp"; // TODO: Implement
            File.WriteAllText($"{filename}.tex", content);
            return "temp";
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
            GenerateDVIFromTex(filename);
            GenerateSVGFromDVI(filename);
        }

        static public void GenerateSVGFromTexContent(string content)
        {
            string filename = SaveTexToFile(content);
            GenerateSVGFromTex(filename);
        }
    }
}
