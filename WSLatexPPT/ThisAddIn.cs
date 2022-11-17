using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using System.Windows.Forms;
using WSLatexCUI;

namespace WSLatexPPT
{
    public partial class ThisAddIn
    {
        public Microsoft.Office.Tools.CustomTaskPane pane;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            RunLatex.Prepare();
            var mainUserControl = new MainUserControl();
            this.pane = CustomTaskPanes.Add(mainUserControl, "WSLaTex");
            pane.Width = 320;
            pane.Visible = true;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO で生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
