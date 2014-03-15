using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader
{
    public class Manga
    {
        public string Name;
        public string DLFormat;
        public Dictionary<int, int> Chapters = new Dictionary<int, int>();

        public Manga(string name, string dlformat, Dictionary<int, int> chapters)
        {
            Name = name;
            DLFormat = dlformat;
            Chapters = chapters;
        }
    }
}
