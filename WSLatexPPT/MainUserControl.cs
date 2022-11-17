using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using WSLatexCUI;
using System.Diagnostics;
using System.Reflection.Emit;
using Microsoft.Office.Core;

namespace WSLatexPPT
{
    public partial class MainUserControl : UserControl
    {
        float last_posx = 0;
        float last_posy = 0;
        float last_sizex = 0;
        float last_sizey = 0;
        
        public MainUserControl()
        {
            var logFile = new System.IO.StreamWriter("output.log.txt");
            Console.SetOut(logFile);
            InitializeComponent();
        }

        private void MainUserControl_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //PowerPoint.Shape textBox = cslide.Shapes.AddTextbox(
            //    Office.MsoTextOrientation.msoTextOrientationHorizontal,
            //    0, 0, 480, 320);
            //textBox.TextFrame.TextRange.InsertAfter(textBox1.Text);
            PowerPoint.Application app;
            PowerPoint.DocumentWindow window;
            PowerPoint.Slide cslide;
            try { 
                app = Globals.ThisAddIn.Application;
                window = app.ActiveWindow;
                cslide = window.View.Slide;
                if(cslide == null) throw new Exception("Current slide is empty.");
            }
            catch
            {
                label1.Text = "Please select a slide.";
                return;
            }

            Enabled = false;
            var outPath = RunLatex.GenerateSVGFromTexContent(textBox1.Text);
            if(outPath == null)
            {
                label1.Text = "Failed to process.";
            }
            else
            {
                var posx = last_posx;
                var posy = last_posy + last_sizey * 1.2f;
                try {
                    var range = window.Selection.ShapeRange;
                    posx = range.Left;
                    posy = range.Top + range.Height * 1.2f;
                }
                catch {}
                label1.Text = "";
                var shape = cslide.Shapes.AddPicture(
                    outPath,
                    MsoTriState.msoFalse, MsoTriState.msoTrue,
                    posx, posy
                );
                shape.ScaleHeight(2.0f, MsoTriState.msoTrue);
                shape.Select();

                last_posx = posx; last_posy = posy;
                last_sizex = shape.Width; last_sizey = shape.Height;
            }
            RunLatex.ClearTempFiles();
            Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
        }
    }
}
