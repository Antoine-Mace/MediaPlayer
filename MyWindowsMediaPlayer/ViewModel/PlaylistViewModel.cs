using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyWindowsMediaPlayer.Model;
using System.Windows.Input;
using System.Windows.Controls;
using System.IO;

namespace MyWindowsMediaPlayer.ViewModel
{
    class PlaylistViewModel : ViewModelBase
    {
        private Playlist pl_manager;
        private Explorer explorer;

        #region fields
        private readonly ObservableCollection<String> pl_names;
        private String pl_name;
        private String new_pl_name;
        private ICommand AddPlCommand;
        private ICommand RemovePlCommand;
        #endregion
        //get list click on a playlist -> get the playlists paths list on model and callback ExplorerViewModel with this list
        public PlaylistViewModel(Playlist playlist, Explorer explorer)
        {
            this.pl_manager = playlist;
            this.explorer = explorer;
            this.pl_names = new ObservableCollection<string>();
            List<String> pl_names = new List<String>();
            Playlist pl_manager = new Playlist();
            pl_names = pl_manager.getAllPlaylists();
            pl_names.ForEach(delegate(string name)
            {
                this.pl_names.Add(name);
            });

        }

        #region properties
        public ObservableCollection<String> playlists_names
        {
            get { return this.pl_names; }
        }

        public String getSelectedPlaylist
        {
            get { return this.pl_name; }
            set
            {
                Playlist pl_manager = new Playlist();
                List<String> paths = pl_manager.getPlaylistPaths(value);
                this.explorer.loadExplorer(value, paths);
                this.pl_name = value;
            }
        }

        public String PlaylistSetName
        {
            get { return this.new_pl_name; }
            set
            {
                this.new_pl_name = value;
                Playlist pl_manager = new Playlist();
                pl_manager.createPlaylist(value);
                pl_manager.savePlaylist();
                this.pl_names.Add(value);
            }
        }

        public ICommand AddPlaylistCommand
        {
            get
            {
                if (this.AddPlCommand == null)
                    this.AddPlCommand = new RelayCommand(() => this.AddPl());
                return this.AddPlCommand;
            }
        }

        public ICommand RemovePlaylistCommand
        {
            get
            {
                if (this.RemovePlCommand == null)
                    this.RemovePlCommand = new RelayCommand(() => this.removePl(this.pl_name));
                return this.RemovePlCommand;

            }
        }
        #endregion

        public void removePl(String pl_name)
        {
            if (pl_name != null && pl_name != "" && File.Exists(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml"))
            {
                Console.WriteLine(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml");
                try
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml");
                    this.pl_names.Remove(pl_name);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void AddPl()
        {

        }
    }
}
