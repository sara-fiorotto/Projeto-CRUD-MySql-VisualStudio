using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoTesteZatec.Relatorio
{
    public partial class frmRelatorio1 : Form
    {
        public frmRelatorio1()
        {
            InitializeComponent();
        }

        private void frmRelatorio1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'aulaDataSet.produtos'. Você pode movê-la ou removê-la conforme necessário.
            this.produtosTableAdapter.Fill(this.aulaDataSet.produtos);
            // TODO: esta linha de código carrega dados na tabela 'aulaDataSet.clientes'. Você pode movê-la ou removê-la conforme necessário.
            this.clientesTableAdapter.Fill(this.aulaDataSet.clientes);
            // TODO: esta linha de código carrega dados na tabela 'aulaDataSet.usuarios'. Você pode movê-la ou removê-la conforme necessário.
            this.usuariosTableAdapter.Fill(this.aulaDataSet.usuarios);

            this.reportViewer1.RefreshReport();
        }
    }
}
