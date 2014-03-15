using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;

namespace MangaDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<string> MangaList = new List<string>();
        public bool cancel = false;
        

        private void Form1_Shown(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "/mangaList.txt");

            string[] nameURL = {};
            Dictionary<int, int> chapters = new Dictionary<int, int>();

            for (int i = 0; i < lines.Length; ++i)
            {
                for (int j = 0; j < lines[i].Split('\t').Length - 1; ++j)
                {
                    MangaList.Add(lines[i].Split('\t')[j]);
                    listBox1.Items.Add(lines[i].Split('\t')[j]);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < MangaList.Count - 1; ++i)
            {
                if (MangaList[i].ToLower().Contains(textBox1.Text.ToLower()))
                {
                    listBox1.Items.Add(MangaList[i]);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int pageFails = 0;
            int chapterFails = 0;
            int pagesTotal = 0;
            int chaptersTotal = 0;
            using (WebClient webClient = new WebClient())
            {
                for (int chapters = 0; chapters >= 0; ++chapters)
                {
                    for(int pages = 1; pages >= 0; ++pages)
                    {
                        try
                        {
                            string url = "http://r1.goodmanga.net/images/manga/" + listBox1.SelectedItem.ToString().Replace(' ', '_').Replace(".", "").Replace("/", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").ToLower() + "/" + chapters + "/" + pages + ".jpg";
                            string tempURL = url.Replace("{page}", pages.ToString());
                            System.IO.Directory.CreateDirectory("\\manga\\" + listBox1.SelectedItem.ToString().Replace(".", "").Replace("/", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "") + "\\" + chapters);
                            webClient.DownloadFile(tempURL, "\\manga\\" + listBox1.SelectedItem.ToString().Replace(".", "").Replace("/", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "") + "\\" + chapters + "\\" + pages + ".jpg");
                            label2.Text = chapters.ToString();
                            label3.Text = pages.ToString();
                            Refresh();
                        }
                        catch
                        {
                            if (pages == 1)
                            {
                                chapterFails++;
                            }
                            else
                            {
                                pageFails++;
                            }
                            break;
                        }
                        if (pageFails >= 1) { pageFails = 0; pagesTotal = pages - 1; break; };
                    }
                    if (chapters != 0 && chapterFails > 1) { chapterFails = 0; chaptersTotal = chapters - 1; break; }
                }
            }
        }
    }
}
