using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace WinFormAppBackGroundWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void StartAsyncButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("StartAsyncButton_Click - thread id: " + Thread.CurrentThread.ManagedThreadId);
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("backgroundWorker1_DoWork - thread id: " + Thread.CurrentThread.ManagedThreadId);
            var worker = sender as BackgroundWorker;
            if (worker == null) return;

            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                Thread.Sleep(500);
                worker.ReportProgress(i * 10);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("backgroundWorker1_ProgressChanged - thread id: " + Thread.CurrentThread.ManagedThreadId);
            resultLabel.Text = (e.ProgressPercentage + @"%");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("backgroundWorker1_RunWorkerCompleted - thread id: " + Thread.CurrentThread.ManagedThreadId);
            if (e.Cancelled)
                resultLabel.Text = @"Canceled!";
            else if (e.Error != null)
                resultLabel.Text = @"Error: " + e.Error.Message;
            else
                resultLabel.Text = @"done!";
        }

        private void CancelAsyncButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CancelAsyncButton_Click - thread id: " + Thread.CurrentThread.ManagedThreadId);
            if (backgroundWorker1.WorkerSupportsCancellation)
                backgroundWorker1.CancelAsync();
        }
    }
}
