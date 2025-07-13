using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationBuilder_All_In_One
{
    public partial class SplashScreen : Form
    {
        private Timer timer;
        public SplashScreen()
        {
            InitializeComponent();
            this.TransparencyKey = (BackColor);
        }
    }
}
