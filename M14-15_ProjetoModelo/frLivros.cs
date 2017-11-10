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
using System.IO;

namespace M14_15_ProjetoModelo
{
    public partial class frLivros : Form
    {
        public frLivros()
        {
            InitializeComponent();
            
            // Update table
            updateTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validate form inputs
            string name = textBox2.Text;
            int year = int.Parse(textBox3.Text);
            DateTime date = dateTimePicker1.Value;
            decimal price = decimal.Parse(textBox5.Text);

            // Copy image to images directory
            string imageName = DateTime.Now.Ticks.ToString();
            string imagesFolder = Application.UserAppDataPath;

            if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);

            string[] extention = lbCapa.Text.Split('.');
            string fullDirectory = imagesFolder + "\\" + imageName + "." + extention[extention.Length - 1];
            File.Copy(lbCapa.Text, fullDirectory);

            // INSERT INTO
            string sql = "INSERT INTO Livros(nome, ano, data_aquisicao, preco, capa, estado) VALUES(@nome, @ano, @data_aquisicao, @preco, @capa, @estado)";

            // Parameters
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter(){ ParameterName = "@nome", SqlDbType = SqlDbType.VarChar, Value = name },
                new SqlParameter(){ ParameterName = "@ano", SqlDbType = SqlDbType.Int, Value = year },
                new SqlParameter(){ ParameterName = "@data_aquisicao", SqlDbType = SqlDbType.Date, Value = date },
                new SqlParameter(){ ParameterName = "@preco", SqlDbType = SqlDbType.Decimal, Value = price },
                new SqlParameter(){ ParameterName = "@capa", SqlDbType = SqlDbType.VarChar, Value = fullDirectory },
                new SqlParameter(){ ParameterName = "@estado", SqlDbType = SqlDbType.Bit, Value = true }
            };

            // Exectute SQL Command
            DB.Instance.ExecSQL(sql, parameters);

            // Update table
            updateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result =  fileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            lbCapa.Text = fileDialog.FileName;
            pictureBox1.Image = Image.FromFile(lbCapa.Text);
        }

        public void updateTable()
        {
            dataGridView1.DataSource = DB.Instance.ExecQuery("SELECT * FROM Livros");
        }
    }
}
