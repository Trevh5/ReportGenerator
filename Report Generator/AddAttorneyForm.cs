using Report_Generator.Properties;
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

namespace Report_Generator
{
    public partial class AddAttorneyForm : Form
    {
        public string path;

        public AddAttorneyForm()
        {
            InitializeComponent();
            path = Settings.Default["programFilesDirectory"].ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(!textBox1.Text.Equals("First") && !textBox2.Text.Equals("Last") && !textBox3.Text.Equals(""))
            {
                string attorneyName = textBox1.Text + " " + textBox2.Text;
                string attorneyAddress = textBox3.Text;
                if (!File.Exists(path + "\\attorney_data\\" + attorneyName + ".txt"))
                {
                    File.WriteAllText(path + "\\attorney_data\\" + attorneyName + ".txt", attorneyAddress);
                    textBox1.Text = "First";
                    textBox2.Text = "Last";
                    textBox3.Text = "Saved!";
                }
                else
                {
                    textBox3.Text = "It looks like there is already an attorney by that name in the database :(";
                }

            }
        }
    }
}
