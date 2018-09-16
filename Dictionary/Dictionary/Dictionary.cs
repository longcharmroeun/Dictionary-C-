using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Dictionary
{
    public partial class Dictionary : Form
    {
        Data data;
        public string word { set; get; }
        public string definition { set; get; }
        public int index = 0;
        
        public Dictionary()
        {
            InitializeComponent();
            data = new Data();
        }

        private void Dictionary_Load(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Data));
                using (FileStream fs = new FileStream("data.xml", FileMode.Open))
                {
                    data = xs.Deserialize(fs) as Data;
                }
                if (data != null)
                {
                    for (int i = 0; i < data.Size; i++)
                    {
                        if (data.Dictionarys[i].Word != null)
                        {
                            toolStripTextBox1.AutoCompleteCustomSource.Add(data.Dictionarys[i].Word);
                            listBox1.Items.Add(data.Dictionarys[i].Word);
                            index++;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Dictionary_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileStream fs = null;
            XmlSerializer xs = new XmlSerializer(typeof(Data));

            using (fs = new FileStream("data.xml", FileMode.Create))
            {
                xs.Serialize(fs, data);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Editor editor = new Editor(this);
            editor.ShowDialog();
            if (word != null && definition != null)
            {
                data.Dictionarys[index].Word = word;
                data.Dictionarys[index].Definition = definition;
                toolStripTextBox1.AutoCompleteCustomSource.Add(word);
                listBox1.Items.Add(word);
                index++;
                data.Sort(index);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text = listBox1.SelectedIndex.ToString();
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < index)
            {
                richTextBox1.Text = data.Dictionarys[listBox1.SelectedIndex].Definition;
                toolStripButton2.Enabled = true;
                toolStripButton3.Enabled = true;
                removeToolStripMenuItem.Enabled = true;
                editeToolStripMenuItem.Enabled = true;
            }
            else
            {
                removeToolStripMenuItem.Enabled = false;
                editeToolStripMenuItem.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox1.FindString(toolStripTextBox1.Text);
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < index) richTextBox1.Text = data.Dictionarys[listBox1.SelectedIndex].Definition;
            else richTextBox1.Text = "Not Found!";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (toolStripButton2.Enabled)
            {
                int n = listBox1.SelectedIndex;
                data.RemoveAt(n, ref index);
                data.Sort(index);
                toolStripTextBox1.AutoCompleteCustomSource.RemoveAt(n);
                listBox1.Items.RemoveAt(n);
                richTextBox1.Text = null;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            Editor editor = new Editor(this, data.Dictionarys[i].Word, data.Dictionarys[i].Definition);
            editor.ShowDialog();
            if (word != null)
            {
                data.Dictionarys[i].Word = word;
                data.Dictionarys[i].Definition = definition;
                toolStripTextBox1.AutoCompleteCustomSource.RemoveAt(i);
                listBox1.Items.RemoveAt(i);
                toolStripTextBox1.AutoCompleteCustomSource.Add(word);
                listBox1.Items.Add(word);
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                toolStripButton4.PerformClick();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1.PerformClick();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2.PerformClick();
        }

        private void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3.PerformClick();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = null;
            for (int i = 0; i < index; i++)
            {
                richTextBox1.Text += $"({i}  {data.Dictionarys[i].Definition})           ";
            }
            FileStream fs = null;
            XmlSerializer xs = new XmlSerializer(typeof(Data));

            using (fs = new FileStream("data.xml", FileMode.Create))
            {
                xs.Serialize(fs, data);
            }
        }
    }
}
