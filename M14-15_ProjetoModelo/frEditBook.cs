using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace M14_15_ProjetoModelo
{
    public partial class frEditBook : Form
    {
        string currentCover;

        public frEditBook(int bookId)
        {
            InitializeComponent();

            // Get data from DB
            string sql = $"SELECT * FROM Livros WHERE nlivro = {bookId}";
            DataTable book = DB.Instance.ExecQuery(sql);

            // Populate form
            label7.Text = bookId.ToString();
            textBox2.Text = book.Rows[0][1].ToString();
            textBox3.Text = book.Rows[0][2].ToString();
            dateTimePicker1.Value = DateTime.Parse(book.Rows[0][3].ToString());
            textBox3.Text = book.Rows[0][4].ToString();
            if (File.Exists(book.Rows[0][5].ToString())) pictureBox1.Image = Image.FromFile(book.Rows[0][5].ToString());
            currentCover = book.Rows[0][5].ToString();
            lbCapa.Text = currentCover;
        }

        private void btn_choose_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            lbCapa.Text = fileDialog.FileName;
            pictureBox1.Image = Image.FromFile(lbCapa.Text);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
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
            string sql = "UPDATE Livros SET nome = @nome, ano = @ano, data_aquisicao = @data_aquisicao, preco = @preco, capa = @capa WHERE nlivro = @nlivro";

            // Parameters
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter(){ ParameterName = "@nome", SqlDbType = SqlDbType.VarChar, Value = name },
                new SqlParameter(){ ParameterName = "@ano", SqlDbType = SqlDbType.Int, Value = year },
                new SqlParameter(){ ParameterName = "@data_aquisicao", SqlDbType = SqlDbType.Date, Value = date },
                new SqlParameter(){ ParameterName = "@preco", SqlDbType = SqlDbType.Decimal, Value = price },
                new SqlParameter(){ ParameterName = "@capa", SqlDbType = SqlDbType.VarChar, Value = fullDirectory },
                new SqlParameter(){ ParameterName = "@nlivro", SqlDbType = SqlDbType.Int, Value = int.Parse(label7.Text)}
            };

            // Exectute SQL Command
            DB.Instance.ExecSQL(sql, parameters);

            // Delete current cover
            pictureBox1.Image = null;
            GC.Collect();
            try
            {
                File.Delete(currentCover);
            }
            catch (Exception error)
            {
                Console.Write(error.Message);
            }
        }
    }
}
