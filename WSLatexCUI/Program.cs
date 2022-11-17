using System;
using System.Diagnostics;

namespace WSLatexCUI // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunLatex.GenerateSVGFromTex(args[0]);
        }
    }
}