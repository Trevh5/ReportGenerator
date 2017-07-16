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
    public partial class InitializeArchiveForm : Form
    {

        public string path;
        public InitializeArchiveForm()
        {
            InitializeComponent();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void button10_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    path = fbd.SelectedPath;
                    label4.Text = "ARCHIVE LOCATION: \n" + fbd.SelectedPath;
                    //Settings.Default["programFilesDirectory"] = fbd.SelectedPath;
                }
                else
                {
                    label4.Text = "ARCHIVE LOCATION: \n" + "Please select a valid folder";
                }
            }
        }

        //Save location button
        private void button11_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(path);
            if (Directory.Exists(path))
            {
                Settings.Default["programFilesDirectory"] = path;
                Settings.Default.Save();

                if(!Directory.Exists(path + "\\PDF_Files")){
                    Directory.CreateDirectory(path + "\\PDF_Files");
                }
                if (!Directory.Exists(path + "\\data")){
                    Directory.CreateDirectory(path + "\\data");
                    
                }
                if (!Directory.Exists(path + "\\attorney_data"))
                {
                    Directory.CreateDirectory(path + "\\attorney_data");
                }
                if (!Directory.Exists(path + "\\physician_data"))
                {
                    Directory.CreateDirectory(path + "\\physician_data");
                }


                Form2 frm2 = new Form2();
                frm2.Location = this.Location;
                frm2.FormClosed += (s, args) => this.Close();
                frm2.Show();
                this.Hide();
            }
            else
            {
                label4.Text = "ARCHIVE LOCATION: \n" + "Please select a valid folder";
            }
        }
    }
}
