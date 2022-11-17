﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using WSLatexCUI;
using System.Diagnostics;
using System.Reflection.Emit;

namespace WSLatexPPT
{
    public partial class MainUserControl : UserControl
    {
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
            Enabled = false;
            //PowerPoint.Shape textBox = cslide.Shapes.AddTextbox(
            //    Office.MsoTextOrientation.msoTextOrientationHorizontal,
            //    0, 0, 480, 320);
            //textBox.TextFrame.TextRange.InsertAfter(textBox1.Text);
            var app = Globals.ThisAddIn.Application;
            var window = app.ActiveWindow;
            PowerPoint.Slide cslide = window.View.Slide;

            var outPath = RunLatex.GenerateSVGFromTexContent(textBox1.Text);
            if(outPath == null)
            {
                label1.Text = "Failed to process.";
            }
            else
            {
                label1.Text = "";
                cslide.Shapes.AddPicture(
                    outPath,
                    Office.MsoTriState.msoFalse,
                    Office.MsoTriState.msoTrue,
                    0 /* window.Selection.ShapeRange.Left */,
                    0 /* window.Selection.ShapeRange.Top */
                );
            }
            RunLatex.ClearTempFiles();
            Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
