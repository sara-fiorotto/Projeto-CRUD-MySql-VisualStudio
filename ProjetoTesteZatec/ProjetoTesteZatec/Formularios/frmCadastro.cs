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
using MySql.Data.MySqlClient;
using System.Drawing;

namespace ProjetoTesteZatec
{
    public partial class frmPrincipal : Form
    {
        Conexao conect = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id, foto, cpfantigo;
        bool alteroufoto = false;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ListarGrid();
            LimpaFoto();
           
        }

        private void FormataGD()
        {
            grid.Columns[0].HeaderText = "id";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Endereço";
            grid.Columns[3].HeaderText = "CPF";
            grid.Columns[4].HeaderText = "Telefone";
            grid.Columns[5].HeaderText = "Foto";

            grid.Columns[0].Visible = false;
            grid.Columns[5].Visible = false;
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitaBotoes();
            btnSalvar.Enabled = false;
            btnNovo.Enabled = false;

            HabilitaCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtCPF.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtTel.Text = grid.CurrentRow.Cells[4].Value.ToString();

            cpfantigo = grid.CurrentRow.Cells[3].Value.ToString();

            if (e.RowIndex > -1)
            {
                byte[] imgagem = (byte[])grid.Rows[e.RowIndex].Cells[5].Value;

                MemoryStream ms = new MemoryStream(imgagem);

                imagemBox.Image = Image.FromStream(ms);
            }
            else
            {
                imagemBox.Image = Properties.Resources.ícone_de_perfil_avatar_padrão_imagem_usuário_mídia_social_210115353;
            }
        }
        private void ListarGrid()
        {
            conect.AbrirConexao();
            sql = "SELECT * FROM clientes ORDER BY NOME ASC";
            cmd = new MySqlCommand(sql, conect.conx);

            //o MySqlAdapter é a ponte entre o programa e o banco de dados para o recebimento de alterações como o UPDATE para o preenchimento de um DataSet/Tabela..
            MySqlDataAdapter da = new MySqlDataAdapter();

            //Passa uma instrução para executar um comando no objeto Criado,  que são usados para preencher o DataSet e atualizar o banco de dados do SQL Server.
            da.SelectCommand = cmd;

            //criando uma tabela e alimentando ela com as informações obtidas no objeto Da.
            DataTable dt = new DataTable();
            da.Fill(dt);

            //Grid Recebendo a tabela
            grid.DataSource = dt;

            conect.FecharConexao();
            LimpaFoto();
            FormataGD();
        }
        //Limpa os textos das TextBox do fomulário
        private void LimparTextBox()
        {
            txtCPF.Text = "";
            txtEndereco.Text = "";
            txtNome.Text = "";
            txtTel.Text = "";
        }

        //Desabilita todos os botões
        private void DesabilitaBotoes()
        {
            btnCancelar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = false;
            btnFoto.Enabled = false;
        }

        //Habilita todos os botões
        private void HabilitaBotoes()
        {
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnAlterar.Enabled = true;
            btnFoto.Enabled = true;
        }

        //Habilita todos os campos de TextBox
        private void HabilitaCampos()
        {
            txtNome.Enabled = true;
            txtCPF.Enabled = true;
            txtEndereco.Enabled = true;
            txtTel.Enabled = true;

        }

        //Desabilita todos os campos de TextBox
        private void DesabilitaCampos()
        {
            txtNome.Enabled = false;
            txtCPF.Enabled = false;
            txtEndereco.Enabled = false;
            txtTel.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaFoto();
            HabilitaCampos();
            txtNome.Focus();

            HabilitaBotoes();
            btnNovo.Enabled = false;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaFoto();
            LimparTextBox();
            DesabilitaBotoes();
            DesabilitaCampos();
            btnNovo.Enabled = true;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("OPS! Digite o nome!");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }

            if (txtCPF.Text == "   .   .   -" || txtCPF.Text.Length <14)
            {
                MessageBox.Show("OPS! Digite seu CPF!");
                txtCPF.Text = "";
                return;
            }

            conect.AbrirConexao();

            //Usando a variavel sql como auxiliar para guardar a informção do comando para o base de dados (seu índice no banco, e valores a serem guardados)
            sql = "INSERT INTO clientes(nome, endereco, cpf, telefone, foto) VALUES(@nome, @endereco, @cpf, @telefone, @foto)";

            //Instanciando um comando no MySql passando respectivamente o parametro da variavel sql e a conexão que está na classe Conexão
            cmd = new MySqlCommand(sql, conect.conx);

            //Passando os parametros usados na variavel sql, adicionando seu nome e respectivo valor
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTel.Text);
            cmd.Parameters.AddWithValue("@foto", img());

            //verificação de cpf
            MySqlCommand cmdVerifica = new MySqlCommand("SELECT * FROM clientes WHERE cpf=@cpf", conect.conx);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            cmdVerifica.Parameters.AddWithValue("@cpf", txtCPF.Text);
            da.SelectCommand = cmdVerifica;
            da.Fill(dt);
            if(dt.Rows.Count > 0) 
            {
                MessageBox.Show("Erro, CPF já cadastrado!", "ERRO",MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCPF.Text = "";
                txtCPF.Focus();
                return; 
            }


            //Comando que executa a Query, NÃO FUNCIONA SEM ELE!
            cmd.ExecuteNonQuery();
            conect.FecharConexao();

            LimparTextBox();
            DesabilitaBotoes();
            DesabilitaCampos();
            LimpaFoto();
            alteroufoto = false;
            btnNovo.Enabled = true;

            MessageBox.Show("Registro Salvo com sucesso", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListarGrid();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Fique alegre com os pequenos códigos
            if (MessageBox.Show("Deseja Excluir o Registro", "Deletar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conect.AbrirConexao();
                sql = "DELETE from clientes WHERE id=@id";
                cmd = new MySqlCommand(sql, conect.conx);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                conect.FecharConexao();
                MessageBox.Show("Registro Excluido com sucesso", "Deletado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                    
            
            LimparTextBox();
            DesabilitaBotoes();
            DesabilitaCampos();
            ListarGrid();
            btnNovo.Enabled = true;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("OPS! Digite o nome!");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }

            if (txtCPF.Text == "   .   .   -" || txtCPF.Text.Length < 14)
            {
                MessageBox.Show("OPS! Digite seu CPF!");
                txtCPF.Text = "";
                return;
            }

            conect.AbrirConexao();

            if (alteroufoto == true)
            {
                sql = "UPDATE clientes SET nome=@nome, cpf=@cpf, endereco=@endereco, telefone=@telefone, foto=@foto WHERE id=@id";
                cmd = new MySqlCommand(sql, conect.conx);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTel.Text);
                cmd.Parameters.AddWithValue("@foto", img());
            }
            else if(alteroufoto == false)
            {
                sql = "UPDATE clientes SET nome=@nome, cpf=@cpf, endereco=@endereco, telefone=@telefone WHERE id=@id";
                cmd = new MySqlCommand(sql, conect.conx);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTel.Text);
            }

            if(txtCPF.Text != cpfantigo)
            {
                MySqlCommand cmdVerifica = new MySqlCommand("SELECT * FROM clientes WHERE cpf=@cpf", conect.conx);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();

                cmdVerifica.Parameters.AddWithValue("@cpf", txtCPF.Text);
                da.SelectCommand = cmdVerifica;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Erro, CPF já cadastrado!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCPF.Text = "";
                    txtCPF.Focus();
                    return;
                }
            }

            cmd.ExecuteNonQuery();
            conect.FecharConexao();

            LimparTextBox();
            DesabilitaBotoes();
            DesabilitaCampos();

            alteroufoto = false;
            btnNovo.Enabled = true;
            MessageBox.Show("Registro Alterado com sucesso", "Editado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListarGrid();
        }


        

        private void BuscarNome()
        {
            conect.AbrirConexao();
            sql = "SELECT * FROM clientes WHERE nome LIKE @nome ORDER BY nome ASC"; //LIKE é usado para valores aproximados não necessariamento valor 100% igual
            cmd = new MySqlCommand(sql, conect.conx);
            cmd.Parameters.AddWithValue("@nome", txtBuscar.Text + "%");//Todo valor passado para o LIKE precisa acompanhar %

            MySqlDataAdapter da = new MySqlDataAdapter(); //Atualizando o DataGrid á partir do comando passado CMD 
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;

            conect.FecharConexao();
            FormataGD();

        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            //carregando imagem e guardando o caminho em foto
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Imagens(*.png; *.jpg) | *.png; *.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString();
                imagemBox.ImageLocation = foto;
                alteroufoto = true;
            }
            else { alteroufoto = false; }

        }

        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
        }

        private byte[] img()
        {
            byte[] img_byte = null;

            //acessando o caminho do arquivo, abrindo e fazendo a leitura do mesmo
            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read);

            //leitura de binários
            BinaryReader br = new BinaryReader(fs);

            img_byte = br.ReadBytes((int)fs.Length);
            return img_byte;
        }

        private void LimpaFoto()
        {
            imagemBox.Image = Properties.Resources.ícone_de_perfil_avatar_padrão_imagem_usuário_mídia_social_210115353;
            foto = "ft/perfil.jpg";
        }
    }
}
