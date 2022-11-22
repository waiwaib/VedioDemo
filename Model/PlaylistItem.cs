using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace VideoDemo
{
    public enum MediaType
    {
        LocalVideo,
        LocalAudio,
        Link,
        Unknown
    }

    public class PlaylistItem
    {
        public MediaType Type { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public bool IsNetworkResource { get; set; }

        public PlaylistItem() { }

        // The following constructor has parameters for two of the three
        // properties.
        public PlaylistItem( string path, string title, MediaType type)
        {
            FilePath = path;
            Title = title;
            Type = type;
        }


        string DisplayName()
        {
            return "";
        }

        DateTime ModifyDate()
        {
            return DateTime.Now;
        }

        string FolderPath()
        {
            return "";
        }

        bool isValid()
        {
            return true;
        }

        string Duration()
        {
            return "";
        }
    }
}
