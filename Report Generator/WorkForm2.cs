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
    public partial class WorkForm2 : Form
    {
        string attorneyAddress;
        bool isNew;
        ReferenceLibraryForm rlfrm;
        public WorkForm2(bool isNewReport)
        {
            isNew = isNewReport;
            InitializeComponent();

            if (isNewReport)
            {
                Settings.Default.imerIsNew = false;
                Settings.Default.Save();
                attorneyAddress = Settings.Default["attorneyAddress"].ToString();

                richTextBox1.Text = "Independent Medical Exam Rebuttal \n\n\n\n" + DateTime.Today.Date.ToString("MMMM d, yyyy") + "\n\n" + attorneyAddress + "\n\n" ;
                richTextBox1.Text = richTextBox1.Text + "Dear " + Settings.Default["attorneyFirstName"].ToString() + " " + Settings.Default["attorneyLastName"].ToString() + ",\n\n\n"+ "History of Present Illness: \n\n";
                richTextBox1.Text = richTextBox1.Text + "CLIENT: " + Settings.Default["clientFirstName"].ToString() + " " + Settings.Default["clientLastName"].ToString() + "\n";


                richTextBox2.Text = "Current Symptoms: \n\n";
                richTextBox3.Text = "Medical Record Review: \n\n";
                richTextBox4.Text = "Physical Examination: \n\n";
                richTextBox5.Text = "Assessment: \n\n" + "\n\n\n\n" + Settings.Default["physicianSignature"].ToString();
                richTextBox6.Text = "References: \n\n";


                SaveTextBoxes();
            }
            else
            {
                Console.WriteLine("Load");
                LoadTextBoxes();

            }
        }

        //Save button
        private void button6_Click(object sender, EventArgs e)
        {
            SaveTextBoxes();
        }

        private void LoadTextBoxes()
        {
            richTextBox1.Text = Settings.Default["imerHPI"].ToString();
            richTextBox2.Text = Settings.Default["imerCurrentSymptoms"].ToString();
            richTextBox3.Text = Settings.Default["imerMedicalRecordReview"].ToString();
            richTextBox4.Text = Settings.Default["imerPhysicalExamination"].ToString();
            richTextBox5.Text = Settings.Default["imerAssessment"].ToString();
            richTextBox6.Text = Settings.Default["imerReferences"].ToString();

        }

        private void SaveTextBoxes()
        {
            Console.WriteLine("Saving Document");
            Settings.Default["imerHPI"] = richTextBox1.Text;
            Settings.Default["imerCurrentSymptoms"] = richTextBox2.Text;
            Settings.Default["imerMedicalRecordReview"] = richTextBox3.Text;
            Settings.Default["imerPhysicalExamination"] = richTextBox4.Text;
            Settings.Default["imerAssessment"] = richTextBox5.Text;
            Settings.Default["imerReferences"] = richTextBox6.Text;


            Settings.Default.Save();
        }

        //Go to medical report button
        private void button3_Click(object sender, EventArgs e)
        {
            if(rlfrm != null)
            {
                rlfrm.Close();
            }
            
            SaveTextBoxes();
            WorkForm wkfrm = new WorkForm(Settings.Default.msrIsNew);

            wkfrm.Location = this.Location;

            wkfrm.FormClosed += (s, args) => this.Close();
            wkfrm.Show();

            this.Hide();
        }

        //Menu Button
        private void button1_Click(object sender, EventArgs e)
        {
            SaveTextBoxes();
            Form2 frm2 = new Form2();
            frm2.Location = this.Location;
            frm2.FormClosed += (s, args) => this.Close();
            frm2.Show();
            this.Hide();
        }

        //Add citation button
        private void button2_Click(object sender, EventArgs e)
        {
            rlfrm = new ReferenceLibraryForm();
            rlfrm.Location = this.Location;
            rlfrm.SetWorkForm(null,this , null,null);
            rlfrm.Show();
        }

        //to standard of care button
        private void button4_Click(object sender, EventArgs e)
        {
            if (rlfrm != null)
            {
                rlfrm.Close();
            }

            SaveTextBoxes();
            WorkForm3 wkfrm3 = new WorkForm3(Settings.Default.socIsNew);

            wkfrm3.Location = this.Location;

            wkfrm3.FormClosed += (s, args) => this.Close();
            wkfrm3.Show();

            this.Hide();
        }

        //To medical research question report
        private void button5_Click(object sender, EventArgs e)
        {
            if (rlfrm != null)
            {
                rlfrm.Close();
            }

            SaveTextBoxes();
            WorkForm4 wkfrm4 = new WorkForm4(Settings.Default.rqrIsNew);

            wkfrm4.Location = this.Location;

            wkfrm4.FormClosed += (s, args) => this.Close();
            wkfrm4.Show();

            this.Hide();
        }


        //export button
        private void button7_Click(object sender, EventArgs e)
        {
            ExportAndSaveAll();
        }


        private void ExportAndSaveAll()
        {
            SaveTextBoxes();

            string archivePath = Settings.Default["programFilesDirectory"].ToString();
            if (!Directory.Exists(archivePath + "\\Exported"))
            {
                Directory.CreateDirectory(archivePath + "\\Exported");
            }
            //Make the attorney folder
            if (!Directory.Exists(archivePath + "\\Exported\\" + Settings.Default["attorneyLastName"].ToString() + "_" + Settings.Default["attorneyFirstName"].ToString() + "_Documents\\"))
            {
                Directory.CreateDirectory(archivePath + "\\Exported\\" + Settings.Default["attorneyLastName"].ToString() + "_" + Settings.Default["attorneyFirstName"].ToString() + "_Documents\\");
            }

            if (Directory.Exists(archivePath + "\\Exported\\" + Settings.Default["attorneyLastName"].ToString() + "_" + Settings.Default["attorneyFirstName"].ToString() + "_Documents\\"))
            {
                //Export all of the documents and save the pdfs with it.

                //Make the folder to place documents.
                Console.WriteLine(Settings.Default["attorneyLastName"].ToString());
                Console.WriteLine(archivePath + "\\Exported\\" + Settings.Default["attorneyLastName"].ToString() + "_" + Settings.Default["attorneyFirstName"].ToString() + "_Documents\\" + Settings.Default["clientLastName"].ToString() + "_" + DateTime.Today.ToString("MM_dd_yyyy") + "\\");
                string clientDir = archivePath + "\\Exported\\" + Settings.Default["attorneyLastName"].ToString() + "_" + Settings.Default["attorneyFirstName"].ToString() + "_Documents\\" + Settings.Default["clientLastName"].ToString() + "_" + DateTime.Today.ToString("MM_dd_yyyy") + "\\";
                if (!Directory.Exists(clientDir))
                {
                    Directory.CreateDirectory(clientDir);
                }

                if (Directory.Exists(clientDir))
                {
                    Directory.CreateDirectory(clientDir + "msrReportReferences");
                    Directory.CreateDirectory(clientDir + "imerReportReferences");
                    Directory.CreateDirectory(clientDir + "socReportReferences");
                    Directory.CreateDirectory(clientDir + "rqrReportReferences");
                    string msrReport;
                    msrReport = Settings.Default["msrIntroduction"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrRecordsReviewed"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrHistory"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrReviewOfRecords"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrOngoingMedicalProblems"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrMedicalChargesToDate"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrDiscussion"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrFutureCareAndCosts"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrSummary"].ToString();
                    msrReport = msrReport + "\n\n" + Settings.Default["msrReferences"].ToString();
                    string imerReport;
                    imerReport = Settings.Default["imerHPI"].ToString();
                    imerReport = imerReport + "\n\n" + Settings.Default["imerCurrentSymptoms"].ToString();
                    imerReport = imerReport + "\n\n" + Settings.Default["imerMedicalRecordReview"].ToString();
                    imerReport = imerReport + "\n\n" + Settings.Default["imerPhysicalExamination"].ToString();
                    imerReport = imerReport + "\n\n" + Settings.Default["imerAssessment"].ToString();
                    imerReport = imerReport + "\n\n" + Settings.Default["imerReferences"].ToString();
                    string socReport;
                    socReport = Settings.Default["socBriefHistoryOfEvents"].ToString();
                    socReport = socReport + "\n\n" + Settings.Default["socQuestionOfSubstandardCare"].ToString();
                    socReport = socReport + "\n\n" + Settings.Default["socReferences"].ToString();
                    string rqrReport;
                    rqrReport = Settings.Default["rqrMainPage"].ToString();
                    rqrReport = rqrReport + "\n\n" + Settings.Default["rqrReferences"].ToString();


                    //Save archive files that can be reimported later.
                    if (!Directory.Exists(archivePath + "\\doc_archive\\"))
                    {
                        Directory.CreateDirectory(archivePath + "\\doc_archive\\");
                    }

                    if (Directory.Exists(archivePath + "\\doc_archive\\"))
                    {
                        if (!Directory.Exists(clientDir + "\\archiveData\\"))
                        {
                            Directory.CreateDirectory(clientDir + "\\archiveData\\");
                        }

                        File.WriteAllText(archivePath + "\\doc_archive\\" + Settings.Default["attorneyLastName"].ToString() + "_" + Settings.Default["attorneyFirstName"].ToString() + "_" + Settings.Default["clientLastName"].ToString() + "_" + DateTime.Today.ToString("MM_dd_yyyy") + ".txt", "data");

                        File.WriteAllText(clientDir + "\\archiveData\\msrIntroduction.txt", Settings.Default["msrIntroduction"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrRecordsReviewed.txt", Settings.Default["msrRecordsReviewed"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrHistory.txt", Settings.Default["msrHistory"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrReviewOfRecords.txt", Settings.Default["msrReviewOfRecords"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrOngoingMedicalProblems.txt", Settings.Default["msrOngoingMedicalProblems"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrMedicalChargesToDate.txt", Settings.Default["msrMedicalChargesToDate"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrDiscussion.txt", Settings.Default["msrDiscussion"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrFutureCareAndCosts.txt", Settings.Default["msrFutureCareAndCosts"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrSummary.txt", Settings.Default["msrSummary"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\msrReferences.txt", Settings.Default["msrReferences"].ToString());

                        File.WriteAllText(clientDir + "\\archiveData\\imerHPI.txt", Settings.Default["imerHPI"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\imerCurrentSymptoms.txt", Settings.Default["imerCurrentSymptoms"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\imerMedicalRecordReview.txt", Settings.Default["imerMedicalRecordReview"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\imerPhysicalExamination.txt", Settings.Default["imerPhysicalExamination"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\imerAssessment.txt", Settings.Default["imerAssessment"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\imerReferences.txt", Settings.Default["imerReferences"].ToString());

                        File.WriteAllText(clientDir + "\\archiveData\\socBriefHistoryOfEvents.txt", Settings.Default["socBriefHistoryOfEvents"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\socQuestionOfSubstandardCare.txt", Settings.Default["socQuestionOfSubstandardCare"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\socReferences.txt", Settings.Default["socReferences"].ToString());

                        File.WriteAllText(clientDir + "\\archiveData\\rqrMainPage.txt", Settings.Default["rqrMainPage"].ToString());
                        File.WriteAllText(clientDir + "\\archiveData\\rqrReferences.txt", Settings.Default["rqrReferences"].ToString());


                    }

                    // Write all reports to .doc files
                    File.WriteAllText(clientDir + "msrReport.txt", msrReport);
                    File.WriteAllText(clientDir + "imerReport.txt", imerReport);
                    File.WriteAllText(clientDir + "socReport.txt", socReport);
                    File.WriteAllText(clientDir + "rqrReport.txt", rqrReport);

                    //Copy PDF files over

                    string referencesDirectory = Settings.Default["programFilesDirectory"].ToString() + "\\data\\";
                    string[] fileNames = Directory.GetFiles(referencesDirectory);
                    foreach (string fName in fileNames)
                    {

                        string selectedReference = Path.GetFileName(fName);
                        Console.WriteLine("Selected: " + selectedReference);
                        if (File.Exists(referencesDirectory + selectedReference))
                        {
                            string bibFile = File.ReadAllText(referencesDirectory + selectedReference);
                            string refTitle = LoadTitle(bibFile);
                            Console.WriteLine(refTitle);

                            //if the title is found in the references, copy the pdf
                            if (msrReport.Contains(refTitle))
                            {
                                File.Copy(Settings.Default["programFilesDirectory"].ToString() + "\\PDF_Files\\" + selectedReference.Replace(".bib", ".pdf"), clientDir + "msrReportReferences\\" + selectedReference.Replace(".bib", ".pdf"));
                            }
                            if (imerReport.Contains(refTitle))
                            {
                                File.Copy(Settings.Default["programFilesDirectory"].ToString() + "\\PDF_Files\\" + selectedReference.Replace(".bib", ".pdf"), clientDir + "imerReportReferences\\" + selectedReference.Replace(".bib", ".pdf"));
                            }
                            if (socReport.Contains(refTitle))
                            {
                                File.Copy(Settings.Default["programFilesDirectory"].ToString() + "\\PDF_Files\\" + selectedReference.Replace(".bib", ".pdf"), clientDir + "socReportReferences\\" + selectedReference.Replace(".bib", ".pdf"));
                            }
                            if (rqrReport.Contains(refTitle))
                            {
                                File.Copy(Settings.Default["programFilesDirectory"].ToString() + "\\PDF_Files\\" + selectedReference.Replace(".bib", ".pdf"), clientDir + "rqrReportReferences\\" + selectedReference.Replace(".bib", ".pdf"));
                            }

                        }
                    }
                }


            }





        }


        public string LoadTitle(string reference)
        {
            string title = "";
            string titleData = "";
            //Console.WriteLine(reference);
            char symbol = '@'; // bibtext files all start with the @ symbol. Not a perfect way of detecting these files, but... you know.
            if (reference.Contains(symbol.ToString()))
            {
                string[] lines = reference.Split('\n');
                foreach (string ln in lines)
                {
                    if (ln.Contains('='))
                    {
                        string[] data = ln.Replace("\n", "").Replace("\r", "").Split('=');
                        if (data[0].Contains("title"))
                        {
                            title = data[0];
                            titleData = data[1].Replace("{", "")
                                                 .Replace("}", "")
                                                 .Replace(".", "")
                                                 .Replace(",", "");
                            Console.WriteLine("title: " + titleData);
                        }

                    }

                }

                if (!titleData.Equals(""))
                {
                    return titleData;
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
