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
    public partial class ReferenceLibraryForm : Form
    {
        public String author, title, journal, volume, number, pages, year, publisher, note, month, edition;
        private WorkForm wkfrm;
           private WorkForm2 wkfrm2;
        private WorkForm3 wkfrm3;
        private WorkForm4 wkfrm4;
        string apaRef;




        public String authorData, titleData, journalData, volumeData, numberData, pagesData, yearData, publisherData, noteData, monthData, editionData;
        String[] authors;

        private string archiveDirectory;
        public ReferenceLibraryForm()
        {
            InitializeComponent();

            archiveDirectory = Settings.Default["programFilesDirectory"].ToString() + "\\data\\";
            string[] fileNames = Directory.GetFiles(archiveDirectory);
            foreach (string fName in fileNames)
            {
                listBox1.Items.Add(Path.GetFileName(fName));
            }
        }

        private void ReferenceLibraryForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //Add to bibliography
        private void button1_Click(object sender, EventArgs e)
        {
            string selectedReference = listBox1.SelectedItem.ToString();
            if (File.Exists(archiveDirectory + selectedReference))
            {
                string bibFile = File.ReadAllText(archiveDirectory + selectedReference);
                apaRef = LoadReference(bibFile);
                Clipboard.SetText("(" + authors[0].Split(',')[0] + ", " + yearData + ")");
            }

            Console.WriteLine(selectedReference);
            if(wkfrm != null)
            {
                wkfrm.richTextBox10.Text = wkfrm.richTextBox10.Text + apaRef + "\n\n";

                string refs = wkfrm.richTextBox10.Text.Replace("References:","");
                
                string[] references = refs.Replace("\n\n","|").Split('|');
                Array.Sort(references);
                wkfrm.richTextBox10.Text = "References: \n\n";
                foreach (string r in references)
                {
                    bool hasLetters = r.Any(x => char.IsLetter(x));
                    if (hasLetters)
                    {
                        //Console.WriteLine("output: " + r);
                        wkfrm.richTextBox10.Text = wkfrm.richTextBox10.Text + r + "\n\n";
                    }
                    
                }
                
            }
            else if(wkfrm2 != null)
            {
                wkfrm2.richTextBox6.Text = wkfrm2.richTextBox6.Text + apaRef + "\n\n";

                string refs = wkfrm2.richTextBox6.Text.Replace("References:", "");
                //Console.WriteLine("refs: " + refs);
                string[] references = refs.Replace("\n\n", "|").Split('|');
                Array.Sort(references);
                wkfrm2.richTextBox6.Text = "References: \n\n";
                foreach (string r in references)
                {
                    bool hasLetters = r.Any(x => char.IsLetter(x));
                    if (hasLetters)
                    {
                        //Console.WriteLine("output: " + r);
                        wkfrm2.richTextBox6.Text = wkfrm2.richTextBox6.Text + r + "\n\n";
                    }

                }
            }
            else if (wkfrm3 != null)
            {
                wkfrm3.richTextBox6.Text = wkfrm3.richTextBox6.Text + apaRef + "\n\n";

                string refs = wkfrm3.richTextBox6.Text.Replace("References:", "");
                //Console.WriteLine("refs: " + refs);
                string[] references = refs.Replace("\n\n", "|").Split('|');
                Array.Sort(references);
                wkfrm3.richTextBox6.Text = "References: \n\n";
                foreach (string r in references)
                {
                    bool hasLetters = r.Any(x => char.IsLetter(x));
                    if (hasLetters)
                    {
                        //Console.WriteLine("output: " + r);
                        wkfrm3.richTextBox6.Text = wkfrm3.richTextBox6.Text + r + "\n\n";
                    }

                }
            }
            else if (wkfrm4 != null)
            {
                wkfrm4.richTextBox6.Text = wkfrm4.richTextBox6.Text + apaRef + "\n\n";

                string refs = wkfrm4.richTextBox6.Text.Replace("References:", "");
                //Console.WriteLine("refs: " + refs);
                string[] references = refs.Replace("\n\n", "|").Split('|');
                Array.Sort(references);
                wkfrm4.richTextBox6.Text = "References: \n\n";
                foreach (string r in references)
                {
                    bool hasLetters = r.Any(x => char.IsLetter(x));
                    if (hasLetters)
                    {
                        //Console.WriteLine("output: " + r);
                        wkfrm4.richTextBox6.Text = wkfrm4.richTextBox6.Text + r + "\n\n";
                    }

                }
            }

        }

        public void SetWorkForm(WorkForm w, WorkForm2 w2,WorkForm3 w3,WorkForm4 w4)
        {
            wkfrm = w;
            wkfrm2 = w2;
            wkfrm3 = w3;
            wkfrm4 = w4;

        }

        //Add to clipboard button
        private void button2_Click(object sender, EventArgs e)
        {
            string selectedReference = listBox1.SelectedItem.ToString();
            if (File.Exists(archiveDirectory + selectedReference))
            {
                string bibFile = File.ReadAllText(archiveDirectory + selectedReference);
                apaRef = LoadReference(bibFile);
                Clipboard.SetText("(" + authors[0].Split(',')[0] + ", " + yearData + ")");
            }

            Console.WriteLine(selectedReference);
        }

        // LoadReference takes a bibtex file, parses it, and then returns an APA reference. THIS IS A SECOND COPY OF THIS METHOD FROM FORM2
        public string LoadReference(string reference)
        {
            author = title = journal = volume = number = pages = year = publisher = note = month = edition = "";
            authorData = titleData = journalData = volumeData = numberData = pagesData = yearData = publisherData = noteData = monthData = editionData = "";
            //Console.WriteLine(reference);
            char symbol = '@';
            if (reference.Contains(symbol.ToString()))
            {
                string[] lines = reference.Split('\n');
                foreach (string ln in lines)
                {
                    if (ln.Contains('='))
                    {
                        string[] data = ln.Replace("\n", "").Replace("\r", "").Split('=');
                        if (data[0].Contains("author"))
                        {
                            author = data[0];
                            authorData = data[1].Replace("{", "")
                                                 .Replace("}", "");
                            authors = authorData.Replace(" and ", "|").Split('|');
                            StringBuilder sb = new StringBuilder();
                            int i = 0;
                            int last = authors.Length - 1;
                            //Console.WriteLine(last);
                            foreach (string a in authors)
                            {
                                string finalName;
                                string[] names = a.Replace(", ", "|").Split('|');
                                if (names.Length > 1)
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
                                        sb.Append(" & " + finalName);
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
                                        sb.Append(" & " + finalName);
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
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("journal"))
                        {
                            journal = data[0];
                            journalData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("volume"))
                        {
                            volume = data[0];
                            volumeData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("number"))
                        {
                            number = data[0];
                            numberData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("pages"))
                        {
                            pages = data[0];
                            pagesData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("year"))
                        {
                            year = data[0];
                            yearData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("publisher"))
                        {
                            publisher = data[0];
                            publisherData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("note"))
                        {
                            note = data[0];
                            noteData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("month"))
                        {
                            month = data[0];
                            monthData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
                        }
                        else if (data[0].Contains("edition"))
                        {
                            edition = data[0];
                            editionData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(",", ""); ;
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
