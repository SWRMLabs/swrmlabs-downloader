using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;

namespace WizardLayout
{
    public partial class LastForm : Form
    {
        
        public LastForm()
        {
            InitializeComponent();
            LastForm_Load();

        }
        private void LastForm_Load()
        {
            try
            {
                if (CheckForInternetConnection())
                {
                    this.button1.Enabled = true;

                }
                else
                {
                    this.button1.Enabled = false;
                    this.richTextBox1.Text = "Internet Not Avaliable";
                }
            }
            catch(Exception e)
            {
                this.richTextBox1.Text = e.Message;
            }
           
        }
        private ProcessCaller processCaller;
      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (processCaller != null)
            {
                processCaller.Cancel();
            }
            this.Close();
            
        }

      

       

        /// <summary>
        /// Handles the events of processCompleted & processCanceled
        /// </summary>
        private void processCompleted(object sender, EventArgs e)
        {
            this.button1.Enabled = true;
        }
        private void processCanceled(object sender, EventArgs e)
        {

        }
        private static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.button1.Enabled = false;
                this.Cursor = Cursors.AppStarting;
                string hash = richTextBox1.Text.Trim();//reading the file has from Textbox
                processCaller = new ProcessCaller(this);//Creating a ProcessCaller to execute command
                string downlaodPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//File path Variable
                processCaller.FileName = @"PathToDownloader";//Give your file downloader binary path.
                processCaller.Arguments = $@" -sharable {hash} -dst {downlaodPath} -progress";
                processCaller.StdErrReceived += new DataReceivedHandler(writeStreamInfo); //Stdout error receiver
                processCaller.StdOutReceived += new DataReceivedHandler(writeStreamInfo);// Stdout Data Receiver
                processCaller.Completed += new EventHandler(processCompleted);//process complete handeler
                processCaller.Cancelled += new EventHandler(processCanceled);//process cancle handeler
                this.richTextBox2.Text = "Connecting to SWRM Labs CDN..." + Environment.NewLine;
                processCaller.Start();
            }
            catch (Exception ex)
            {
                this.richTextBox2.Text = ex.Message;

            }
        }
        /// <summary>
        /// Handles the events of StdErrReceived and StdOutReceived.
        /// </summary>
        /// <remarks>
        /// If stderr were handled in a separate function, it could possibly
        /// be displayed in red in the richText box, but that is beyond 
        /// the scope of this demo.
        /// </remarks>
        private void writeStreamInfo(object sender, DataReceivedEventArgs e)
        {
            string tempVal = e.Text;
            tempVal = tempVal.Remove(tempVal.Length - 1);
            tempVal = tempVal.Substring(9).Trim();
            if (Int32.TryParse(tempVal, out int numValue))
                progressBar1.Value = numValue;
            this.richTextBox2.AppendText(e.Text + Environment.NewLine);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string downlaodPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Process.Start("explorer.exe", $@"{downlaodPath}");
        }
    }
}
