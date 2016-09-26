using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using System.IO;
using System.Windows.Media.Imaging;

namespace MyWindowsMediaPlayer.Model
{
    public class MediaInfo
    {
        public MediaInfo(string mediaPath)
        {
            TagLib.File tagFile = TagLib.File.Create(@mediaPath);
            this.Path = mediaPath;
            this.Titre = tagFile.Tag.Title;
            this.Titre = ((this.Titre == null) ? System.IO.Path.GetFileNameWithoutExtension(@Path) : this.Titre);
            if (mediaPath.EndsWith(".mp3") || mediaPath.EndsWith(".ogg")
                    || mediaPath.EndsWith(".flac") || mediaPath.EndsWith(".wav"))
            {
                this.Artiste = tagFile.Tag.Performers.FirstOrDefault();
                this.Genre = tagFile.Tag.Album;
                this.Artiste = ((this.Artiste == null) ? "Unknow" : this.Artiste);
                this.Genre = ((this.Genre == null) ? "Unknow" : this.Genre);
                this.toDisplay = ((tagFile.Tag.Track != 0) ? (tagFile.Tag.Track + ". ") : "") + this.Titre + " - " + this.Artiste + " - " + this.Genre;
            }
            else
                this.toDisplay = this.Titre;
        }

        public string Path { get; set; }
        public string Titre { get; set; }
        public string Artiste { get; set; }
        public string Genre { get; set; }
        public string toDisplay { get; set; }
    }
    public class Explorer
    {
        private int key = -1;
        private ObservableCollection<MediaInfo> list;
        private String name;

        public void addToList(String content)
        {
            try
            {
                if (File.Exists(content))
                    this.list.Add(new MediaInfo(content));
            }
            catch (Exception e)
            {
            }
        }

        public String getPathFromKey(int key)
        {
            return list[key].Path;
        }

        public void removeToList(String content)
        {
            for (int i = this.list.Count - 1; i >= 0; i--)
            {
                if (this.list[i].Path == content)
                {
                    this.list.RemoveAt(i);
                    return;
                }
            }
        }

        public int getKey()
        {
            return this.key;
        }

        public void setKey(String path)
        {
            int i = 0;

            foreach (var media in this.list)
            {
                if (path == media.Path)
                {
                    this.key = i;
                    return;
                }
                ++i;
            }
        }

        public void setKeyUp()
        {
             this.key = this.key + 1;
        }

        public Boolean canKeyUp()
        {
            if (this.key + 1 < list.Count())
                return true;
            return false;
        }

        public void setKeyDown()
        {
            this.key = this.key - 1;
        }

        public Boolean canKeyDown()
        {
            if (this.key > 0)
                return true;
            return false;
        }


        public String getName()
        {
            return this.name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public ObservableCollection<MediaInfo> getList()
        {
            return this.list;
        }

        public void loadExplorer(String name, List<String> playlist)
        {
            this.key = -1;
            this.name = name;
            this.list.Clear();
            foreach (String path in playlist)
                this.addToList(path);
        }

        public void filterBy(String filter)
        {
            IEnumerable<MediaInfo> sortList =
                from media in list
                orderby ((filter.EndsWith("Title")) ? media.Titre : (filter.EndsWith("Artist")) ? media.Artiste : media.Genre)
                select media;

            foreach (var media in sortList)
            {
                removeToList(media.Path);
                addToList(media.Path);
            }
        }

        public void sortBySearch(String search)
        {
            IEnumerable<MediaInfo> sortList =
                from media in list
                orderby media.Titre.StartsWith(search) descending
                select media;

            foreach (var media in sortList)
            {
                removeToList(media.Path);
                addToList(media.Path);
            }
        }

        public Explorer ()
        {
            this.list = new ObservableCollection<MediaInfo>();
        }

    }
}
