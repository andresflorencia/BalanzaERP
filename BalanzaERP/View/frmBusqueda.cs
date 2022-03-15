using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalanzaERP.View
{
    public partial class frmBusqueda : Form
    {
        public String codigo = "";
        public frmBusqueda()
        {
            InitializeComponent();
        }

        private void frmBusqueda_Load(object sender, EventArgs e)
        {
        }

        private void dgvBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                codigo = dgvBusqueda.CurrentRow.Cells[0].Tag.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void dgvBusqueda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                codigo = dgvBusqueda.CurrentRow.Cells[0].Tag.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
