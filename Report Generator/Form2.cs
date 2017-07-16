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
    public partial class Form2 : Form
    {

        OpenFileDialog ofd = new OpenFileDialog();
        public String author, title, journal, volume, number, pages, year, publisher, note, month, edition;
        public String authorData, titleData, journalData, volumeData, numberData, pagesData, yearData, publisherData, noteData, monthData, editionData;
        String[] authors;
        private string pdfFilePath = "";
        public string path;

        //Add attorney
        private void button13_Click_1(object sender, EventArgs e)
        {
            AddAttorneyForm aafrm = new AddAttorneyForm();
            aafrm.Location = this.Location;
            //aafrm.SetWorkForm(this);
            aafrm.Show();
        }

        //Add physician 
        private void button14_Click(object sender, EventArgs e)
        {
            AddPhysicianForm apfrm = new AddPhysicianForm();
            apfrm.Location = this.Location;
            apfrm.Show();
        }

        //Continue Document button
        private void button2_Click(object sender, EventArgs e)
        {
            bool isNewDocument = false;
            Settings.Default.imerIsNew = false;
            Settings.Default.msrIsNew = false;

            WorkForm wkfrm = new WorkForm(isNewDocument);

            wkfrm.Location = this.Location;

            wkfrm.FormClosed += (s, args) => this.Close();
            wkfrm.Show();

            this.Hide();

            
        }

        //Archived Document button
        private void button3_Click(object sender, EventArgs e)
        {
            ArchiveDocumentForm adf = new ArchiveDocumentForm();
            adf.Location = this.Location;

            adf.FormClosed += (s, args) => this.Close();
            adf.Show();

            this.Hide();
        }

        public string referenceData;
        public int currentNumReferences;

        private void button11_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string referenceFilePath = ofd.FileName;
                if (referenceFilePath.Contains(".bib") || referenceFilePath.Contains(".txt"))
                {
                    referenceData = File.ReadAllText(referenceFilePath);
                    string apaReference = LoadReference(referenceData);
                    label1.Text = "REFERENCE: \n" + apaReference;
                }
                else
                {
                    label1.Text = "REFERENCE: \n Please select a .bib or .txt file.";
                }
            }
            
        }

        //Check PDF button click
        private void button12_Click(object sender, EventArgs e)
        {
            if (pdfFilePath.Contains(".pdf") || File.Exists(pdfFilePath))
            {
                //label2.Text = "PDF: " + pdfFilePath;
                System.Diagnostics.Process.Start(pdfFilePath);
            }
            else
            {
                label2.Text = "PDF: Please select a PDF file";
            }
        }

        

        public Form2()
        {
            InitializeComponent();
            
           

            panel1.Visible = false;
            //panel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Location = this.Location;
            this.Hide();
            frm3.FormClosed += (s, args) => this.Close();
            frm3.Show();
        }

        //Exit Document Button
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Text = "ARCHIVE LOCATION: \n" + Settings.Default["programFilesDirectory"].ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //panel2.Visible = false;
            //panel2.SendToBack();
            panel1.BringToFront();
            panel1.Visible = true;
            //panel1.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.SendToBack();
            panel1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pdfFilePath = ofd.FileName;
                if (pdfFilePath.Contains(".pdf"))
                {
                    label2.Text = "PDF: " + pdfFilePath;
                }
                else
                {
                    label2.Text = "PDF: Please select a PDF file";
                }
            }
            //ofd.ShowDialog();
        }

        //Copy reference from clipboard
        private void button6_Click(object sender, EventArgs e)
        {
            referenceData = Clipboard.GetText();
            String apaReference = LoadReference(referenceData);
            label1.Text = "REFERENCE: \n" + apaReference;

        }

        //Save to archive button
        private void button9_Click(object sender, EventArgs e)
        {
            //currentReferences
            if (File.Exists(pdfFilePath) && referenceData != null)
            {
                path = Settings.Default["programFilesDirectory"].ToString();
                currentNumReferences = (int)Settings.Default["currentReferences"];
                currentNumReferences = currentNumReferences + 1;
                //File.Copy(pdfFilePath, path + "\\PDF_files\\source" + currentNumReferences.ToString() + ".pdf");
                //char[] BAD_CHARS = new char[] { '!', '@', '#', '$', '%' }; // Possible characters to remove from file path. 
                string fixedTitle = titleData.Replace(":", "").Replace("!","").Replace("$","").Replace("%","").Replace("#","").Replace("@","");
                    //string.Concat(titleData.Split(BAD_CHARS, StringSplitOptions.RemoveEmptyEntries));
                Console.WriteLine(fixedTitle);
                if (File.Exists(path + "\\PDF_files\\" + authors[0] + ", " + fixedTitle + ".pdf") || File.Exists(path + "\\data\\" + authors[0] + ", " + fixedTitle + ".bib"))
                {
                    label2.Text = "PDF: It looks like you already linked this file. \n If there was a mistake with the old file, delete it first. :)";
                    label1.Text = "REFERENCE: ";
                }
                else
                {
                    //Console.WriteLine(path + "\\PDF_files\\" + authors[0] + ", " + titleData + ".pdf");
                    
                    File.Copy(pdfFilePath, path + "\\PDF_files\\" + authors[0] + ", " + fixedTitle + ".pdf");
                    File.WriteAllText(path + "\\data\\" + authors[0] + ", " + fixedTitle + ".bib", referenceData);
                    Settings.Default["currentReferences"] = currentNumReferences;
                    Settings.Default.Save();


                    pdfFilePath = "";
                    label2.Text = "PDF: Saved!";
                    label1.Text = "REFERENCE: ";
                }
                
            }
            else
            {
                label2.Text = "PDF: Please select a new file";
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // Choose Location button to initialize archive or find existing archive.
        private void button10_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string newArchiveDirectory = ofd.FileName;
                if (pdfFilePath.Contains(".pdf"))
                {
                    label2.Text = "PDF: " + pdfFilePath;
                }
                else
                {
                    label2.Text = "PDF: Please select a PDF file";
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    path = fbd.SelectedPath;
                    label3.Text = "ARCHIVE LOCATION: \n" + Settings.Default["programFilesDirectory"].ToString();
                    Settings.Default["programFilesDirectory"] = fbd.SelectedPath;
                    Settings.Default.Save();

                    if (!Directory.Exists(path + "\\PDF_Files")){
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


                }
                else
                {
                    label3.Text = "Please select a valid folder";
                }
            }
        }


        // LoadReference takes a bibtex file, parses it, and then returns an APA reference.
        public string LoadReference(string reference)
        {
            author= title= journal= volume= number= pages= year= publisher= note= month= edition = "";
            authorData = titleData= journalData= volumeData= numberData= pagesData= yearData= publisherData= noteData= monthData= editionData = "";
            //Console.WriteLine(reference);
            char symbol = '@'; // bibtext files all start with the @ symbol. Not a perfect way of detecting these files, but... you know.
            if(reference.Contains(symbol.ToString()))
            {
                string[] lines = reference.Split('\n');
                foreach (string ln in lines)
                {
                    if (ln.Contains('='))
                    {
                        string[] data = ln.Replace("\n","").Replace("\r","").Split('=');
                        if (data[0].Contains("author"))
                        {
                            author = data[0];
                            authorData = data[1].Replace("{", "")
                                                 .Replace("}", "");
                            authors = authorData.Replace(" and ","|").Split('|');
                            StringBuilder sb = new StringBuilder();
                            int i = 0;
                            int last = authors.Length - 1;
                            //Console.WriteLine(last);
                            foreach(string a in authors)
                            {
                                string finalName;
                                string[] names = a.Replace(", ","|").Split('|');
                                if(names.Length > 1)
                                {
                                    string first = names[1];
                                    first = first[0].ToString();
                                    finalName = names[0] + ", " + first;
                                    if (i == 0)
                                    {
                                        sb.Append(finalName);
                                    }
                                    else if (i == last)
                                    {
                                        sb.Append(" && " + finalName);
                                    }
                                    else
                                    {
                                        sb.Append(", " + finalName);
                                    }
                                   
                                }
                                else
                                {
                                    finalName = names[0];
                                    if (i == 0)
                                    {
                                        sb.Append(finalName);
                                    }
                                    else if (i == last)
                                    {
                                        sb.Append(" && " + finalName);
                                    }
                                    else
                                    {
                                        sb.Append(", " + finalName);
                                    }
                                }
                                
                                i++;
                                //Console.WriteLine(finalName);
                            }
                            authorData = sb.ToString();
                            //Console.WriteLine("Here: " + authorData);
                        }
                        else if (data[0].Contains("title"))
                        {
                            title = data[0];
                            titleData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(".", "")
                                                 .Replace(",", "");
                            Console.WriteLine("title: "+titleData);
                        }
                        else if (data[0].Contains("journal"))
                        {
                            journal = data[0];
                            journalData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("volume"))
                        {
                            volume = data[0];
                            volumeData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("number"))
                        {
                            number = data[0];
                            numberData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("pages"))
                        {
                            pages = data[0];
                            pagesData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("year"))
                        {
                            year = data[0];
                            yearData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("publisher"))
                        {
                            publisher = data[0];
                            publisherData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("note"))
                        {
                            note = data[0];
                            noteData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("month"))
                        {
                            month = data[0];
                            monthData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                        else if (data[0].Contains("edition"))
                        {
                            edition = data[0];
                            editionData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", "");
                        }
                    }
                    
                }


                String workingReference = ""; // This string will be used to build the final apa formatted junk.
                //authorData = titleData= journalData= volumeData= numberData= pagesData= yearData= publisherData= noteData= monthData= editionData = "";

                if (!authorData.Equals(""))
                {
                    workingReference = workingReference + authorData + " ";
                }
                if (!yearData.Equals(""))
                {
                    workingReference = workingReference + "(" + yearData + "). ";
                }
                if (!titleData.Equals(""))
                {
                    workingReference = workingReference + titleData + ". ";
                }
                if (!journalData.Equals(""))
                {
                    workingReference = workingReference + journalData + " ";
                }
                if (!volumeData.Equals(""))
                {
                    workingReference = workingReference + volumeData;
                }
                if (!numberData.Equals(""))
                {
                    workingReference = workingReference + "(" + numberData + "), ";
                }
                else
                {
                    workingReference = workingReference + ", ";
                }
                if (!pagesData.Equals(""))
                {
                    workingReference = workingReference + pagesData + ". ";
                }
                if (!publisherData.Equals(""))
                {
                    workingReference = workingReference + publisherData + ". ";
                }

                if (!workingReference.Equals(""))
                {
                    return workingReference;
                }
                else
                {
                    return "An error occured. BibTeX code not complete.";
                }

            }
            else
            {
                return "An error occured. BibTeX code not detected.";
                //Console.WriteLine("Not Bibtex");
            }

        }





    }
}
