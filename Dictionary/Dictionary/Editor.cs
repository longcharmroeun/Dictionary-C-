using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dictionary
{
    public partial class Editor : Form
    {
        Dictionary dictionary;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("Word must input", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            else
            {
                dictionary.word = textBox1.Text;
                dictionary.definition = richTextBox1.Text;
                this.Close();
            }
        }

        public Editor(Dictionary dictionary = null, string word = null, string definition = null)
        {
            InitializeComponent();
            this.dictionary = dictionary;
            if (word != null && definition != null)
            {
                textBox1.Text = word;
                richTextBox1.Text = definition;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                richTextBox1.Focus();
            }
        }
    }
}
