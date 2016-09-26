using Microsoft.Win32;
using MVVMDemoApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyWindowsMediaPlayer.View;

namespace MyWindowsMediaPlayer.ViewModel
{
    class ControlViewModel : ViewModelBase
    {
        private ScreenView media;

        #region fields
        private ICommand openCommand;
        private ICommand playCommand;
        private ICommand pauseCommand;
        private ICommand stopCommand;
        #endregion
        
        public ControlViewModel(ScreenView view)
        {
            this.media = view;
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
        #endregion

        private bool CanOpenFile()
        {
            return true;
        }

        private void OpenFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "All files (*.*)|*.*";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            openFile.Title = "Open...";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                foreach (string filename in openFile.FileNames)
                {
                    media.Source(filename);
                    media.Play();
                }
            }
        }

        private bool CanPlayFile()
        {
            return true;
        }

        private void PlayFile()
        {
            media.Play();
        }

        private bool CanPauseFile()
        {
            return true;
        }

        private void PauseFile()
        {
            media.Pause();
        }

        private bool CanStopFile()
        {
            return true;
        }

        private void StopFile()
        {
            media.Stop();
        }
    }
}
