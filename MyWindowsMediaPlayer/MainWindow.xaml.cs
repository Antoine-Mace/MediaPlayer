using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyWindowsMediaPlayer.Model;
using MyWindowsMediaPlayer.ViewModel;


namespace MyWindowsMediaPlayer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlViewModel controlViewModel;
        private bool isFullscreen = false;
        public MainWindow()
        {
            InitializeComponent();
            List <String> extensions = new List<String>() {".mp4", ".avi", ".mkv", ".flv"
                , ".flac", ".mp3", "wav", ".ogg", ".jpg", ".jpeg", ".png", ".ico", ".bmp" };
            Playlist playlist = new Playlist();
            Explorer explorer = new Explorer();
            this.controlViewModel = new ControlViewModel(this, this.ScreenView, extensions, explorer, this.ExplorerView);
            PlaylistViewModel playlistViewModel = new PlaylistViewModel(playlist, explorer);
            ExplorerViewModel explorerViewModel = new ExplorerViewModel(explorer, playlist, this.ScreenView, extensions, this.controlViewModel);

            this.PlaylistView.DataContext = playlistViewModel;
            this.ControlView.DataContext = controlViewModel;
            this.ExplorerView.DataContext = explorerViewModel;
        }

        private void doubleClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
                this.controlViewModel.doubleClickControl();
        }

        private void mouseControlEventEnter(object sender, MouseEventArgs e)
        {
            this.ControlView.Opacity = 0.8;
        }

        public void setIsFullscreen(Boolean isFullscreen)
        {
            if (isFullscreen == true)
                this.ControlView.Opacity = 0;
            else
                this.ControlView.Opacity = 0.8;
            this.isFullscreen = isFullscreen;
        }

        private void mouseControlEventLeave(object sender, MouseEventArgs e)
        {
            if (isFullscreen)
                this.ControlView.Opacity = 0;
        }

        private void hideExplorer(object sender, RoutedEventArgs e)
        {
            this.ScreenView.Margin = new Thickness(this.ScreenView.Margin.Left, ((this.ScreenView.Margin.Left == 0) ? 18 : 23), 0, ((this.ScreenView.Margin.Left == 0) ? 0 : 5));
            this.ControlView.Margin = new Thickness(this.ControlView.Margin.Left, ((this.ScreenView.Margin.Left == 0) ? 18 : 23), 0, 0);
            this.MinHeight = ((this.ScreenView.Margin.Left == 0) ? 177 : 187);
            this.MinWidth = ((this.ScreenView.Margin.Left == 0) ? 450 : 655);
        }

        private void hidePlaylists(object sender, RoutedEventArgs e)
        {
            this.ScreenView.Margin = new Thickness(0, ((this.ScreenView.Margin.Right == 0) ? 18 : 23), this.ScreenView.Margin.Right, ((this.ScreenView.Margin.Right == 0) ? 0 : 5));
            this.ControlView.Margin = new Thickness(0, ((this.ScreenView.Margin.Right == 0) ? 18 : 23), this.ScreenView.Margin.Right, 0);
            this.MinHeight = ((this.ScreenView.Margin.Right == 0) ? 177 : 187);
            this.MinWidth = ((this.ScreenView.Margin.Right == 0) ? 450 : 655);
        }

        private void showExplorer(object sender, RoutedEventArgs e)
        {
            if (this.ScreenView.Margin.Right != 200)
            {
                this.ScreenView.Margin = new Thickness(this.ScreenView.Margin.Left, 23, 200, 5);
                this.ControlView.Margin = new Thickness(this.ControlView.Margin.Left, 23, 200, 0);
                this.MinHeight = 187;
                this.MinWidth = ((this.ScreenView.Margin.Left == 0) ? 655 : 850);
            }
        }

        private void showPlaylists(object sender, RoutedEventArgs e)
        {
            if (this.ScreenView.Margin.Left != 200)
            {
                this.ScreenView.Margin = new Thickness(200, 23, this.ScreenView.Margin.Right, 5);
                this.ControlView.Margin = new Thickness(200, 23, this.ScreenView.Margin.Right, 0);
                this.MinHeight = 187;
                this.MinWidth = ((this.ScreenView.Margin.Right == 0) ? 655 : 850);
            }
        }
        private void fullscreenMode(object sender, RoutedEventArgs e)
        {
            this.controlViewModel.FullscreenMode();
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            this.controlViewModel.OpenFile();
        }

        private void openDirectory(object sender, RoutedEventArgs e)
        {
            this.controlViewModel.OpenDirectory();
        }
    }
}
