using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoTesteZatec
{
    public partial class frmLogin : Form
    {
        Conexao conect = new Conexao();
        string sql;
        MySqlCommand cmd;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            conect.AbrirConexao();
            cmd = new MySqlCommand("SELECT * FROM usuarios WHERE usuario=@usuario AND senha=@senha", conect.conx);
            cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                frmMenu frm = new frmMenu();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("OPS, usuário ou senha inválido");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
