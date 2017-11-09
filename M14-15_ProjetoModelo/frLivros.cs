using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            DB db = new DB();

            // Validate form inputs
            string name = textBox2.Text;
            int year = int.Parse(textBox3.Text);
            DateTime date = dateTimePicker1.Value;
            decimal price = decimal.Parse(textBox5.Text);

            // Copy image to images directory


            // INSERT INTO
            string sql = $"INSERT INTO Livros(nome, ano, data_aquisicao, preco) VALUES('{name}', {year}, '{date}', {price})";
            db.ExecSQL(sql);
        }
    }
}
