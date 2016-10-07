using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string html = "<html><head></head><body style='background-color: grey; '>Some body text</body></html>";

            webBrowser1.Navigate("about:blank");
            while (webBrowser1.Document == null || webBrowser1.Document.Body == null)
                Application.DoEvents();
            webBrowser1.Document.OpenNew(true).Write(html);
        }
    }
}
