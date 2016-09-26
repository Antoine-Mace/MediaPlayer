using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MyWindowsMediaPlayer.View;
using MyWindowsMediaPlayer.Model;
using Microsoft.Win32;


    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>

namespace MyWindowsMediaPlayer.ViewModel
{
    class ExplorerViewModel : ViewModelBase
    {
        private ScreenView view;
        private Playlist playlist;
        private List<String> extensions;
        private ControlViewModel control;

        #region fields
        private readonly Explorer explorer;
        private string selectedMedia;
        private string selectedFilter;
        private ICommand addCommand;
        private ICommand removeCommand;
        #endregion

        public ExplorerViewModel(Explorer explorer, Playlist playlist, ScreenView view, List<String> extensions, ControlViewModel control)
        {
            this.extensions = extensions;
            this.explorer = explorer;
            this.playlist = playlist;
            this.view = view;
            this.control = control;
        }

        #region properties
        public ICommand AddCommand
        {
            get
            {
                if (this.addCommand == null)
                    this.addCommand = new RelayCommand(() => this.AddMediaToPl(), () => this.CanModify());
                return this.addCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (this.removeCommand == null)
                    this.removeCommand = new RelayCommand(() => this.RemoveMediaToPl(), () => this.CanModify());
                return this.removeCommand;
            }
        }

        public ObservableCollection<MediaInfo> Explorer
        {
            get { return this.explorer.getList(); }
        }

        public string SelectedMedia
        {
            get
            {
                return selectedMedia; 
            } 
            set 
            { 
                this.selectedMedia = value;
                this.explorer.setKey(value);
                this.view.Source(value);
                this.control.PlayFile();
            } 
        }

        public string SelectedFilter
        {
            get
            {
                return selectedFilter;
            }
            set
            {
                this.selectedFilter = value;
                filterBy();
            }
        }

         public string SearchText
        {
            set
            {
                this.explorer.sortBySearch(value);
            }
        }

        #endregion

        public Boolean CanModify()
         {
             if (this.explorer.getName() == null)
                 return false;
             return true;
         }

        private void AddMediaToPl()
        {
            OpenFileDialog openFile = new OpenFileDialog();
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
                        this.playlist.addContentToPl(this.explorer.getName(), openFile.FileNames[0]);
                        this.explorer.addToList(openFile.FileNames[0]);
                    }
                    else
                        return;
                }
            }
        }

        private void RemoveMediaToPl()
        {
            this.playlist.removeContentFromPl(this.explorer.getName(), this.selectedMedia);
            this.explorer.removeToList(this.selectedMedia);
        }

        private void filterBy()
        {
            this.explorer.filterBy(this.SelectedFilter);
        }
    }
}
