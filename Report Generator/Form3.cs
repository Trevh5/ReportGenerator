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
using System.Timers;

namespace Report_Generator
{
    public partial class Form3 : Form
    {

        string path;

        public Form3()
        {
            InitializeComponent();

            path = Settings.Default["programFilesDirectory"].ToString() + "\\attorney_data\\";
            string[] fileNames = Directory.GetFiles(path);
            foreach (string fName in fileNames)
            {
                listBox1.Items.Add(Path.GetFileName(fName.Replace(".txt", "")));
            }

            path = Settings.Default["programFilesDirectory"].ToString() + "\\physician_data\\";
            string[] fileNames2 = Directory.GetFiles(path);
            foreach (string fName in fileNames2)
            {
                listBox2.Items.Add(Path.GetFileName(fName.Replace(".txt", "")));
            }
        }

        


        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Location = this.Location;
            this.Hide();
            frm2.FormClosed += (s, args) => this.Close();
            frm2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            if(listBox1.SelectedIndex != -1 || listBox2.SelectedIndex != -1)
            {
                Settings.Default["attorneyFirstName"] = listBox1.SelectedItem.ToString().Split(' ')[0];
                Settings.Default["attorneyLastName"] = listBox1.SelectedItem.ToString().Split(' ')[1];
                Settings.Default["clientFirstName"] = textBox4.Text;
                Settings.Default["clientLastName"] = textBox3.Text;
                Settings.Default["attorneyAddress"] = File.ReadAllText(Settings.Default["programFilesDirectory"].ToString() + "\\attorney_data\\" + listBox1.SelectedItem.ToString() + ".txt");
                Settings.Default["physicianSignature"] = File.ReadAllText(Settings.Default["programFilesDirectory"].ToString() + "\\physician_data\\" + listBox2.SelectedItem.ToString() + ".txt");
                Settings.Default.Save();

                bool isNewDocument = true;

                Settings.Default["msrIsNew"] = true;
                Settings.Default["imerIsNew"] = true;
                Settings.Default["socIsNew"] = true;
                Settings.Default["rqrIsNew"] = true;
                Settings.Default.Save();
            if (radioButton1.Checked)
            {
                WorkForm wkfrm = new WorkForm(Settings.Default.msrIsNew);

                wkfrm.Location = this.Location;

                wkfrm.FormClosed += (s, args) => this.Close();
                wkfrm.Show();

                this.Hide();
            }
            else if (radioButton2.Checked)
            {
                WorkForm2 wkfrm2 = new WorkForm2(Settings.Default.imerIsNew);

                wkfrm2.Location = this.Location;

                wkfrm2.FormClosed += (s, args) => this.Close();
                wkfrm2.Show();

                this.Hide();
            }
            else if (radioButton3.Checked)
            {
                WorkForm3 wkfrm3 = new WorkForm3(Settings.Default.socIsNew);

                wkfrm3.Location = this.Location;

                wkfrm3.FormClosed += (s, args) => this.Close();
                wkfrm3.Show();

                this.Hide();
            }
            else if (radioButton4.Checked)
            {
                WorkForm4 wkfrm4 = new WorkForm4(Settings.Default.rqrIsNew);

                wkfrm4.Location = this.Location;

                wkfrm4.FormClosed += (s, args) => this.Close();
                wkfrm4.Show();

                this.Hide();
            }

        }
        else
        {
            MessageBox.Show("Please SELECT an attorney and a Physician");
        }
                
            
            
        }
    }
}
