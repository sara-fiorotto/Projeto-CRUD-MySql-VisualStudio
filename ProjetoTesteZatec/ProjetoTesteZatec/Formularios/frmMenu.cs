using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoTesteZatec
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            frm.ShowDialog();

        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroUsuario frm = new frmCadastroUsuario();
            frm.ShowDialog();

        }

        private void relatorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Relatorio.frmRelatorio1 frm = new Relatorio.frmRelatorio1();
            frm.ShowDialog();
        }

        private void oProjetoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formularios.frmSobre frm = new Formularios.frmSobre();
            frm.ShowDialog();
        }
    }
}
