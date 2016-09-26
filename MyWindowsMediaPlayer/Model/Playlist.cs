using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace MyWindowsMediaPlayer.Model
{
    [XmlRoot("MediaContent")]
    public class MediaContent
    {
        [XmlElement("content_path")]
        public string content_path { get; set; }
        [XmlElement("content_name")]
        public string content_name { get; set; }

        public MediaContent() { }

    }

    [XmlRoot("Playlist")]
    public class Playlist
    {
        [XmlArray(ElementName = "list")]
        public List<MediaContent> content_list { get; set; }

        [XmlElement(ElementName = "name")]
        public String name { get; set; }


        public Playlist()
        {
            this.content_list = new List<MediaContent>();
        }

        public void removePl(String pl_name)
        {
            if (pl_name != null && pl_name != "" && File.Exists(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml"))
            {
                Console.WriteLine(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml");
                try
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml");
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public List<String> getAllPlaylists()
        {
            List<String> pl_names = new List<string>();

            string[] dirs = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                pl_names.Add(Path.GetFileNameWithoutExtension(dir));
            }
            return pl_names;
        }

        public List<String> getPlaylistPaths(string pl_filename)
        {
            List<String> pl_paths = new List<string>();
            if (File.Exists(Directory.GetCurrentDirectory() + @"\" + pl_filename + ".xml"))
            {
                StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + pl_filename + ".xml");
                XmlSerializer x = new XmlSerializer(typeof(Playlist));
                Playlist pl_obj = (Playlist)x.Deserialize(reader);

                pl_obj.content_list.ForEach(delegate(MediaContent unique_content)
                {
                    pl_paths.Add(unique_content.content_path);
                });
                reader.Close();
            }

            return pl_paths;
        }

        public void createPlaylist(string name)
        {
            this.name = name + ".xml";
        }

        public void addContentToPlaylist(MediaContent content)
        {
            this.content_list.Add(content);
        }


        public void savePlaylist()
        {
            Console.WriteLine("name of playlist" + this.name);
            if (this.name != null)
            {
                try
                {
                    TextWriter writer = new StreamWriter(this.name);
                    List<MediaContent> pl_obj = this.content_list;
                    XmlSerializer x = new XmlSerializer(typeof(Playlist));
                    x.Serialize(writer, this);
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void addContentToPl(String pl_name, String file_path)
        {

            if (File.Exists(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml"))
            {
                StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml");
                XmlSerializer x = new XmlSerializer(typeof(Playlist));
                Playlist pl_obj = (Playlist)x.Deserialize(reader);
                reader.Close();
                MediaContent media = new MediaContent();
                media.content_name = System.IO.Path.GetFileNameWithoutExtension(file_path);
                media.content_path = file_path;
                pl_obj.content_list.Add(media);
                this.removePl(pl_name);

                try
                {
                    TextWriter writer = new StreamWriter(pl_name + ".xml");
                    List<MediaContent> pl_ob = pl_obj.content_list;
                    x.Serialize(writer, pl_obj);
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void removeContentFromPl(String pl_name, String file_path)
        {

            if (File.Exists(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml"))
            {
                StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + pl_name + ".xml");
                XmlSerializer x = new XmlSerializer(typeof(Playlist));
                Playlist pl_obj = (Playlist)x.Deserialize(reader);
                reader.Close();
                for (int i = 0; i < pl_obj.content_list.Count(); i++)
                {
                    if (pl_obj.content_list[i].content_name == System.IO.Path.GetFileNameWithoutExtension(file_path) && pl_obj.content_list[i].content_path == file_path)
                        pl_obj.content_list.RemoveAt(i);
                }
                this.removePl(pl_name);
                try
                {
                    TextWriter writer = new StreamWriter(pl_name + ".xml");
                    x.Serialize(writer, pl_obj);
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
