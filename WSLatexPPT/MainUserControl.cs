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
using Office = Microsoft.Office.Core;
using WSLatexCUI;

namespace WSLatexPPT
{
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
        }

        private void MainUserControl_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //PowerPoint.Slide cslide = Globals.ThisAddIn.Application.ActiveWindow.View.Slide;
            //PowerPoint.Shape textBox = cslide.Shapes.AddTextbox(
            //    Office.MsoTextOrientation.msoTextOrientationHorizontal,
            //    0, 0, 480, 320);
            //textBox.TextFrame.TextRange.InsertAfter(textBox1.Text);
            WSLatexCUI.RunLatex.GenerateSVGFromTexContent(textBox1.Text);
            textBox1.Text = "";
        }
    }
}
