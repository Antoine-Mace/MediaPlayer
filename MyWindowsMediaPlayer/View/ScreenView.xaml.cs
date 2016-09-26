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
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace MyWindowsMediaPlayer.View
{
    public partial class ScreenView : UserControl
    {
        private Boolean mediaPlayerIsPlaying = false;
        private Boolean userIsDraggingSlider = false;
        private Boolean isFullscreen = false;
        private DispatcherTimer timer = new DispatcherTimer();

        public ScreenView()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            timeSlider.Value = 0;
            timeSlider.Maximum = 0;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((media.Source != null) && (media.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                timeSlider.Minimum = 0;
                timeSlider.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
                timeSlider.Value = media.Position.TotalSeconds;
            }
        }

        internal void Source(String path)
        {
            if (path != null)
                media.Source = new Uri(path, UriKind.RelativeOrAbsolute);
        }

        public String getSource()
        {
            if (media.Source != null)
                return media.Source.AbsoluteUri;
            return null;
        }

        internal void Play()
        {
            if (media.Source != null)
            {
                timer.Start();
                mediaPlayerIsPlaying = true;
                media.Play();
            }
        }


        internal void Pause()
        {
            if (media.Source != null)
            {
                timer.Stop();
                mediaPlayerIsPlaying = false;
                media.Pause();
            }
        }

        internal void Stop()
        {
                mediaPlayerIsPlaying = false;
                timeSlider.Value = 0;
                timeSlider.Maximum = 0;
                currentTimeTextBlock.Text = "00:00:00";
                media.Source = null;
                timer.Stop();
                media.Stop();
        }

        private void Element_Mediaended(object sender, RoutedEventArgs e)
        {
            String mediaPath = ((media.Source != null) ? media.Source.AbsoluteUri : null);

            if (!mediaPath.EndsWith(".jpg") && !mediaPath.EndsWith(".png")
                && !mediaPath.EndsWith(".ico") && !mediaPath.EndsWith(".jpeg")
                && !mediaPath.EndsWith(".bmp"))
            {
                Stop();
            }
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            media.Position = TimeSpan.FromSeconds(timeSlider.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaPlayerIsPlaying)
                currentTimeTextBlock.Text = TimeSpan.FromSeconds(timeSlider.Value).ToString(@"hh\:mm\:ss");
        }

        private void ProgressBar(object sender, MouseWheelEventArgs e)
        {
            if (mediaPlayerIsPlaying)
            {
                timeSlider.Value += (e.Delta > 0) ? 1 : -1;
                media.Position = TimeSpan.FromSeconds(timeSlider.Value);
                currentTimeTextBlock.Text = TimeSpan.FromSeconds(timeSlider.Value).ToString(@"hh\:mm\:ss");
            }
        }

        private void VolumeBar(object sender, MouseWheelEventArgs e)
        {
            media.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void clickControl(object sender, MouseButtonEventArgs e)
        {
            if (mediaPlayerIsPlaying)
                Pause();
            else if (media != null)
                Play();
        }

        private void mouseEventEnter(object sender, MouseEventArgs e)
        {
            this.navBar.Opacity = 0.8;
        }

        public void setNavBar(Boolean isFullscreen)
        {
            if (isFullscreen == true)
                this.navBar.Opacity = 0;
            else
                this.navBar.Opacity = 0.8;
            this.isFullscreen = isFullscreen;
        }

        private void mouseEventLeave(object sender, MouseEventArgs e)
        {
            if (isFullscreen)
                this.navBar.Opacity = 0;
        }
    }
}
