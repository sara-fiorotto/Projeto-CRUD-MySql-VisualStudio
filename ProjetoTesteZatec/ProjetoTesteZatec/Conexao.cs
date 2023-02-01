using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ProjetoTesteZatec
{
    class Conexao
    {
        //Linha de acesso que contem as informações do banco de dados
        public string conec = "SERVER=localhost; DATABASE=aula; UID=root; PWD=; PORT=";


        //String do tipo MySql que instância a conexão com a base.
        public MySqlConnection conx = null;


        //Abre conexão
        public void AbrirConexao()
        {
            try
            {
                conx = new MySqlConnection(conec); //Nova instância da váriavel conx
                conx.Open(); //Conexão aberta
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no Servidor: "+ex.Message); //Menssagem de erro para caso haja falha na conexão
            }
        }

        public void FecharConexao()
        {
            try
            {
                conx = new MySqlConnection(conec);
                conx.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro no Servidor: " + ex.Message);
            }
        }


    }
}
