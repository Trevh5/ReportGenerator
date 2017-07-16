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
    public partial class ArchiveDocumentForm : Form
    {

        private string archiveDirectory;
        public ArchiveDocumentForm()
        {
            InitializeComponent();

            archiveDirectory = Settings.Default["programFilesDirectory"].ToString() + "\\doc_archive\\";
            string[] fileNames = Directory.GetFiles(archiveDirectory);
            foreach (string fName in fileNames)
            {
                listBox1.Items.Add(Path.GetFileName(fName));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedReference = listBox1.SelectedItem.ToString();
            if (File.Exists(archiveDirectory + selectedReference))
            {
                
                string[] fileNameParts = selectedReference.Split('_');
                string clientDir = Settings.Default["programFilesDirectory"].ToString() + "\\Exported\\" + fileNameParts[0] + "_" + fileNameParts[1] + "_Documents\\" + fileNameParts[2] + "_" + fileNameParts[3] + "_" + fileNameParts[4] + "_" + fileNameParts[5].Replace(".txt","");
                Console.WriteLine(clientDir);
                if (Directory.Exists(clientDir))
                {
                    Console.WriteLine("Archive enter");
                    Settings.Default["msrIntroduction"] = File.ReadAllText(clientDir + "\\archiveData\\msrIntroduction.txt");
                    Settings.Default["msrRecordsReviewed"] = File.ReadAllText(clientDir + "\\archiveData\\msrRecordsReviewed.txt");
                    Settings.Default["msrHistory"] = File.ReadAllText(clientDir + "\\archiveData\\msrHistory.txt");
                    Settings.Default["msrReviewOfRecords"] = File.ReadAllText(clientDir + "\\archiveData\\msrReviewOfRecords.txt");
                    Settings.Default["msrOngoingMedicalProblems"] = File.ReadAllText(clientDir + "\\archiveData\\msrOngoingMedicalProblems.txt");
                    Settings.Default["msrMedicalChargesToDate"] = File.ReadAllText(clientDir + "\\archiveData\\msrMedicalChargesToDate.txt");
                    Settings.Default["msrDiscussion"] = File.ReadAllText(clientDir + "\\archiveData\\msrDiscussion.txt");
                    Settings.Default["msrFutureCareAndCosts"] = File.ReadAllText(clientDir + "\\archiveData\\msrFutureCareAndCosts.txt");
                    Settings.Default["msrSummary"] = File.ReadAllText(clientDir + "\\archiveData\\msrSummary.txt");
                    Settings.Default["msrReferences"] = File.ReadAllText(clientDir + "\\archiveData\\msrReferences.txt");

                    Settings.Default["imerHPI"] = File.ReadAllText(clientDir + "\\archiveData\\imerHPI.txt");
                    Settings.Default["imerCurrentSymptoms"] = File.ReadAllText(clientDir + "\\archiveData\\imerCurrentSymptoms.txt");
                    Settings.Default["imerMedicalRecordReview"] = File.ReadAllText(clientDir + "\\archiveData\\imerMedicalRecordReview.txt");
                    Settings.Default["imerPhysicalExamination"] = File.ReadAllText(clientDir + "\\archiveData\\imerPhysicalExamination.txt");
                    Settings.Default["imerAssessment"] = File.ReadAllText(clientDir + "\\archiveData\\imerAssessment.txt");
                    Settings.Default["imerReferences"] = File.ReadAllText(clientDir + "\\archiveData\\imerReferences.txt");

                    Settings.Default["socBriefHistoryOfEvents"] = File.ReadAllText(clientDir + "\\archiveData\\socBriefHistoryOfEvents.txt");
                    Settings.Default["socQuestionOfSubstandardCare"] = File.ReadAllText(clientDir + "\\archiveData\\socQuestionOfSubstandardCare.txt");
                    Settings.Default["socReferences"] = File.ReadAllText(clientDir + "\\archiveData\\socReferences.txt");

                    Settings.Default["rqrMainPage"] = File.ReadAllText(clientDir + "\\archiveData\\rqrMainPage.txt");
                    Settings.Default["rqrReferences"] = File.ReadAllText(clientDir + "\\archiveData\\rqrReferences.txt");

                    Settings.Default["msrIsNew"] = false;
                    Settings.Default["imerIsNew"] = false;
                    Settings.Default["socIsNew"] = false;
                    Settings.Default["rqrIsNew"] = false;

                    WorkForm wkfrm = new WorkForm(Settings.Default.msrIsNew);

                    wkfrm.Location = this.Location;

                    wkfrm.FormClosed += (s, args) => this.Close();
                    wkfrm.Show();

                    this.Hide();
                }

            }
        }

        private void LoadArchivedDocument(string name)
        {

        }

        //Menu button
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Location = this.Location;
            this.Hide();
            frm2.FormClosed += (s, args) => this.Close();
            frm2.Show();
        }
    }
}
