using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace M14_15_ProjetoModelo
{
    public partial class frLivros : Form
    {
        public frLivros()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validate form inputs
            string name = textBox2.Text;
            int year = int.Parse(textBox3.Text);
            DateTime date = dateTimePicker1.Value;
            decimal price = decimal.Parse(textBox5.Text);

            // Copy image to images directory


            // INSERT INTO
            string sql = "INSERT INTO Livros(nome, ano, data_aquisicao, preco) VALUES(@nome, @ano, @data_aquisicao, @preco)";

            // Parameters
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter(){ ParameterName = "@nome", SqlDbType = SqlDbType.VarChar, Value = name },
                new SqlParameter(){ ParameterName = "@ano", SqlDbType = SqlDbType.Int, Value = year },
                new SqlParameter(){ ParameterName = "@data_aquisicao", SqlDbType = SqlDbType.Date, Value = date },
                new SqlParameter(){ ParameterName = "@preco", SqlDbType = SqlDbType.Decimal, Value = price }
            };

            DB.Instance.ExecSQL(sql, parameters);
        }
    }
}
