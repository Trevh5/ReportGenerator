﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Report_Generator.Properties;
using System.IO;

namespace Report_Generator
{
    public partial class Form1 : Form
    {
        Form2 frm2;

        public string programDirectory;

        public Form1()
        {
            frm2 = new Form2();
            frm2.Hide();
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form2 frm = new Form2();

            programDirectory = Settings.Default["programFilesDirectory"].ToString();
            if (!Directory.Exists(programDirectory))
            {
                InitializeArchiveForm IAF = new InitializeArchiveForm();
                IAF.Location = this.Location;

                IAF.FormClosed += (s, args) => this.Close();
                IAF.Show();
                this.Hide();


            }
            else
            {
                frm2.Location = this.Location;
                frm2.FormClosed += (s, args) => this.Close();
                frm2.Show();
                this.Hide();
            }

        }
    }
}
