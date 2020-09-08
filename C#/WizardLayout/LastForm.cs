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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace WizardLayout
{
    public partial class LastForm : Form
    {
        StringBuilder strbld = new StringBuilder();
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
                processCaller.FileName = @"..\..\qa.exe";//Give your file downloader binary path.
                processCaller.Arguments = $@" -sharable {hash} -progress -json";
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
           
            strbld.Append(e.Text.Trim());
            //valiadting string builder for Json.
            if (validateJson(strbld.ToString())){
                //Convert string builder to JSON object
                dynamic json = JObject.Parse(strbld.ToString());
                this.richTextBox2.AppendText(json+ Environment.NewLine);
                if (json.ContainsKey("data"))
                {
                    //Showing percentage in progress bar
                    this.progressBar1.Value = json.data.percentage;
                    //Showing Doenloaded and total size.
                    this.download.Text = json.data.downloaded + " / " + json.data.total_size;
                }
                strbld = new StringBuilder();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string downlaodPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Process.Start("explorer.exe", $@"{downlaodPath}");
        }
        private bool validateJson(String str)
        {
            try
            {
                JToken token = JObject.Parse(str);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
