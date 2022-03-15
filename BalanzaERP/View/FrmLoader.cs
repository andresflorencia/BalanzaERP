using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalanzaERP.View
{
    public partial class FrmLoader : Form
    {
        public FrmLoader()
        {
            InitializeComponent();
        }

        private void FrmLoader_Load(object sender, EventArgs e)
        {
            //imgLoader.Load(Path.Combine(Application.StartupPath, @"images\loading.gif"));
            //imgLoader.Location = new Point(this.Width / 2 - imgLoader.Width / 2, this.Height / 2 - imgLoader.Height / 2);
            //lblMessage.Location = new Point(this.Width / 2 - lblMessage.Width / 2, this.Height / 2 - lblMessage.Height / 2);

        }

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }
    }
}
