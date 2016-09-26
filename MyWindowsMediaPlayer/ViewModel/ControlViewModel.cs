using System.Drawing;
using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using MyWindowsMediaPlayer.View;
using MyWindowsMediaPlayer.Model;
using System.Windows.Threading;


namespace MyWindowsMediaPlayer.ViewModel
{
    class ControlViewModel : ViewModelBase
    {
        private ScreenView media;
        private MainWindow mainView;
        private ExplorerView explorerView;
        private Explorer explorer;
        private Boolean isFullscreen;
        private Boolean isPlay = false;
        private Boolean isPause = false;
        private List<String> extensions;
        private Double rightMargin;
        private Double leftMargin;
        private Double topMargin;
        private Double bottomMargin;
        private DispatcherTimer timer = new DispatcherTimer();

        #region fields
        private ICommand fullscreenCommand;
        private ICommand openCommand;
        private ICommand playCommand;
        private ICommand pauseCommand;
        private ICommand stopCommand;
        private ICommand prevCommand;
        private ICommand nextCommand;
        #endregion

        public ControlViewModel(MainWindow mainView, ScreenView view, List<String> extensions, Explorer explorer, ExplorerView explorerView)
        {
            this.explorerView = explorerView;
            this.extensions = extensions;
            this.media = view;
            this.mainView = mainView;
            this.mainView.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(HandleEsc);
            this.explorer = explorer;
            this.isFullscreen = false;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (CanStopFile() == false && CanNextFile() && this.explorer.getName() != null && this.media.getSource() == null)
            {
                this.explorer.setKeyUp();
                this.explorerView.listMedia.SelectedValue
                    = this.explorer.getPathFromKey(this.explorer.getKey());
            }
        }

        private void HandleEsc(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape && isFullscreen)
                FullscreenMode();
        }

        #region properties
        public ICommand OpenCommand
        {
            get
            {
                if (this.openCommand == null)
                    this.openCommand = new RelayCommand(() => this.OpenFile(), () => this.CanOpenFile());
                return this.openCommand;
            }
        }

        public ICommand PlayCommand
        {
            get
            {
                if (this.playCommand == null)
                    this.playCommand = new RelayCommand(() => this.PlayFile(), () => this.CanPlayFile());
                return this.playCommand;
            }
        }

        public ICommand PauseCommand
        {
            get
            {
                if (this.pauseCommand == null)
                    this.pauseCommand = new RelayCommand(() => this.PauseFile(), () => this.CanPauseFile());
                return this.pauseCommand;
            }
        }

        public ICommand StopCommand
        {
            get
            {
                if (this.stopCommand == null)
                    this.stopCommand = new RelayCommand(() => this.StopFile(), () => this.CanStopFile());
                return this.stopCommand;
            }
        }

        public ICommand PrevCommand
        {
            get
            {
                if (this.prevCommand == null)
                    this.prevCommand = new RelayCommand(() => this.PrevFile(), () => this.CanPrevFile());
                return this.prevCommand;
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if (this.nextCommand == null)
                    this.nextCommand = new RelayCommand(() => this.NextFile(), () => this.CanNextFile());
                return this.nextCommand;
            }
        }

        public ICommand FullscreenCommand
        {
            get
            {
                if (this.fullscreenCommand == null)
                    this.fullscreenCommand = new RelayCommand(() => this.FullscreenMode());
                return this.fullscreenCommand;
            }
        }
        #endregion

        public void doubleClickControl()
        {
            FullscreenMode();
        }

        public void FullscreenMode()
        {
            if (!isFullscreen)
            {
                this.mainView.WindowStyle = WindowStyle.None;
                this.mainView.WindowState = WindowState.Maximized;
                this.leftMargin = this.mainView.ScreenView.Margin.Left;
                this.rightMargin = this.mainView.ScreenView.Margin.Right;
                this.bottomMargin = this.mainView.ScreenView.Margin.Bottom;
                this.topMargin = this.mainView.ScreenView.Margin.Top;
                this.mainView.ScreenView.Margin = new Thickness(0, 0, 0, 0);
                this.mainView.ControlView.Margin = new Thickness(0, 0, 0, 0);
                this.mainView.setIsFullscreen(true);
                this.media.setNavBar(true);
                this.isFullscreen = true;
            }
            else
            {
                this.mainView.WindowStyle = WindowStyle.SingleBorderWindow;
                this.mainView.WindowState = WindowState.Normal;
                this.mainView.ScreenView.Margin = new Thickness(this.leftMargin, this.topMargin, this.rightMargin, this.bottomMargin);
                this.mainView.ControlView.Margin = new Thickness(this.leftMargin, this.topMargin, this.rightMargin, 0);
                this.mainView.setIsFullscreen(false);
                this.media.setNavBar(false);
                this.isFullscreen = false;
            }
        }

        private bool CanOpenFile()
        {
            return true;
        }

        public void OpenFile()
        {
            System.Windows.Forms.OpenFileDialog openFile
            = new System.Windows.Forms.OpenFileDialog();
            Boolean isPlayable = false;

            openFile.Filter = "All files (*.*)|*.*";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            openFile.Title = "Open...";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                foreach (string filename in openFile.FileNames)
                {
                    foreach (string extension in this.extensions)
                        if (System.IO.Path.GetExtension(@openFile.FileNames[0]) == extension)
                            isPlayable = true;
                    if (isPlayable)
                    {
                        StopFile();
                        this.explorer.loadExplorer(null, new List<String> { openFile.FileNames[0] });
                        this.explorerView.listMedia.SelectedValue
                            = this.explorer.getPathFromKey(0);
                    }
                    else
                        return;
                }
            }
        }

        private static void AddFiles(string path, List<string> files)
        {
            try
            {
                Directory.GetFiles(path)
                    .ToList()
                    .ForEach(s => files.Add(s));

                Directory.GetDirectories(path)
                    .ToList()
                    .ForEach(s => AddFiles(s, files));
            }
            catch (UnauthorizedAccessException ex)
            {
            }

        }

        public void OpenDirectory()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                List<string> files = new List<string>();
                List<String> listPaths = new List<string>();
                try { AddFiles(fbd.SelectedPath, files); }
                catch (NotSupportedException a) { }
                 IEnumerable<String> listStrings 
                     = from path in files
                       where path.EndsWith(".mp4") 
                       || path.EndsWith(".avi")
                       || path.EndsWith(".mkv")
                       || path.EndsWith(".flv")
                       || path.EndsWith(".flac")
                       || path.EndsWith(".mp3")
                       || path.EndsWith(".ogg")
                       || path.EndsWith(".jpg")
                       || path.EndsWith(".png")
                       || path.EndsWith(".ico")
                       || path.EndsWith(".jpeg")
                       || path.EndsWith(".bmp")
                       select path;

                foreach (var media in listStrings)
                    listPaths.Add(media);
                this.explorer.loadExplorer(null, listPaths);
            }
        }

        private bool CanPlayFile()
        {
                return isPlay;
        }

        public void PlayFile()
        {
            media.Play();
            this.isPlay = false;
            this.isPause = true;
        }

        private bool CanPauseFile()
        {
                return isPause;
        }

        private void PauseFile()
        {
            media.Pause();
            this.isPause = false;
            this.isPlay = true;
        }

        private bool CanStopFile()
        {
            if (this.media.getSource() != null)
                return true;
            return false;
        }

        private void StopFile()
        {
            media.Stop();
            this.media.Source(null);
            this.isPause = false;
            this.isPlay = false;
        }

        private bool CanPrevFile()
        {
            return this.explorer.canKeyDown();
        }

        private void PrevFile()
        {
            this.explorer.setKeyDown();
            this.explorerView.listMedia.SelectedValue
                = this.explorer.getPathFromKey(this.explorer.getKey());
        }

        private bool CanNextFile()
        {
            return this.explorer.canKeyUp();
        }

        private void NextFile()
        {
            this.explorer.setKeyUp();
            this.explorerView.listMedia.SelectedValue
                = this.explorer.getPathFromKey(this.explorer.getKey());
        }
    }
}
