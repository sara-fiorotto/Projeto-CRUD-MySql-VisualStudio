using MySql.Data.MySqlClient;
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
    public partial class frmCadastroUsuario : Form
    {
        Conexao conect = new Conexao();
        string sql;
        public frmCadastroUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "" || txtSenha.Text.ToString() == "")
            {
                MessageBox.Show("OPS! Digite o nome e a senha!");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            conect.AbrirConexao();
            sql = "INSERT INTO usuarios(usuario, senha) VALUES(@usuario, @senha)";

            MySqlCommand cmd = new MySqlCommand(sql, conect.conx);
            cmd.Parameters.AddWithValue("@usuario", txtNome.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);

            cmd.ExecuteNonQuery();
            conect.FecharConexao();
            MessageBox.Show("Registro Salvo com sucesso", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNome.Text = "";
            txtSenha.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtSenha.Text = "";
        }
    }


}
