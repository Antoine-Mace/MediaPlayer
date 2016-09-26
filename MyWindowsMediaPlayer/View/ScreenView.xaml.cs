using System.Drawing;
using Microsoft.Win32;
using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;

namespace MyWindowsMediaPlayer.View
{
    /// <summary>
    /// Logique d'interaction pour ScreenView.xaml
    /// </summary>
    public partial class ScreenView : UserControl
    {
        private String mediaPath = "";
        private String[] files;
        private int mediaTime;
        private int mediaActualPos;
        private int mediaActualFile;

        public ScreenView()
        {
            InitializeComponent();
            mediaActualFile = 0;
        }

        internal void Source(String path)
        {
            if (path != null)
            {
                mediaPath = path;
                media.Source = new Uri(path, UriKind.RelativeOrAbsolute);
                //files[mediaActualFile],
            }
        }

        internal void Play()
        {
            if (media.Source != null)
                media.Play();
        }


        internal void Pause()
        {
            if (media.Source != null)
                media.Pause();
        }

        internal void Stop()
        {
            if (media.Source != null)
            {
                mediaTime = 0;
                timeSlider.Value = 0;
                media.Source = null;
                media.Stop();
            }
        }

        private void Element_Mediaopened(object sender, RoutedEventArgs e)
        {
            Play();
            timeSlider.Maximum = media.NaturalDuration.TimeSpan.TotalMilliseconds;
            mediaTime = (int)media.NaturalDuration.TimeSpan.TotalMilliseconds;
            mediaActualPos = (int)media.Position.TotalMilliseconds;
        }

        private void Element_Mediaended(object sender, RoutedEventArgs e)
        {

            Stop();
            if (files != null && files.Length > mediaActualFile + 1)
            {
                mediaActualFile++;
                media.Source = new Uri(files[mediaActualFile]);
                Play();
            }
        }

        private void MediaTimeChanged(object sender, RoutedEventArgs e)
        {
            TimeSpan time = new TimeSpan(0, 0, 0, 0, (int)timeSlider.Value);
            media.Position = time;
            mediaActualPos = (int)media.Position.TotalMilliseconds;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i;

            while (true)
            {
                if (this.media != null && mediaTime != 0)
                {
                    i = mediaActualPos / mediaTime * 100;
                    if (i <= 0)
                        i = 1;
                    while (i < 100 && mediaTime > 0)
                    {
                        if (i == 1)
                            (sender as BackgroundWorker).ReportProgress(i);
                        else
                            (sender as BackgroundWorker).ReportProgress(i);
                        Thread.Sleep((this.mediaTime) / 100);
                        i++;
                    }
                    if (mediaTime < 0)
                        (sender as BackgroundWorker).ReportProgress(0);
                    this.mediaTime = 0;
                }
            }


        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress.Value = e.ProgressPercentage;
        }
    }
}
