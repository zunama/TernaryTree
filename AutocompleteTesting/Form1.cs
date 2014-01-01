using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TernaryTree;

namespace AutocompleteTesting
{
    public partial class Form1 : Form
    {
        AutocompleteDictionary dict = new AutocompleteDictionary();

        IList<string> possibleWords = new List<string>();

        public Form1()
        {
            InitializeComponent();

            string[] lines = System.IO.File.ReadAllLines(@"WordList.txt");
            foreach (string line in lines)
                dict.AddWord(line);
        }

        private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            outputWords.Text = "";

            if (!string.IsNullOrEmpty(textBoxInput.Text))
            {
                possibleWords = dict.GetPossibleWords(textBoxInput.Text);
                outputWords.Text = string.Format("({0})", possibleWords.Count);

                listBox1.DataSource = null;
                listBox1.DataSource = possibleWords;
            }
        }
    }
}
