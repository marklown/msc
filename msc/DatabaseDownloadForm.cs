using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace msc
{
    public partial class DatabaseDownloadForm : Form
    {
        private WebClient client = new WebClient();

        public DatabaseDownloadForm()
        {
            InitializeComponent();
            label1.Text = "Could not find database file stars.db, downloading...";
            DownloadDatabase();
        }


        private void DownloadDatabase()
        {
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadComplete);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileAsync(new Uri("https://www.dropbox.com/s/4pjh9vq3jqo2670/stars.db?dl=1"), "./stars.db");
        }

        private void DownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            label1.Text = "Download complete!";
            btnOk.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            client.CancelAsync();
            label1.Text = "Download cancelled";
        }
    }
}
