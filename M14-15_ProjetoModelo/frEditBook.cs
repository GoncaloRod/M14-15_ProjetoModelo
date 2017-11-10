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
    public partial class frEditBook : Form
    {
        public frEditBook(int bookId)
        {
            InitializeComponent();

            // Get data from DB
            string sql = $"SELECT * FORM Livros WHERE nlivro = {bookId}";
            DataTable book = DB.Instance.ExecQuery(sql);

            // Populate form
            label7.Text = bookId.ToString();
            textBox2.Text = book.Rows[0][1].ToString();
            textBox3.Text = book.Rows[0][2].ToString();
            dateTimePicker1.Value = DateTime.Parse(book.Rows[0][3].ToString());
            textBox3.Text = book.Rows[0][4].ToString();
            pictureBox1.Image = Image.FromFile(book.Rows[0][5].ToString());
        }
    }
}
