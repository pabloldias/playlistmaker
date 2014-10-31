using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PlaylistMaker
{
    public class Playlist
    {
        private List<Mp3Directory> directories;
        private List<String> filenames = new List<String>();
        private List<String> extensions = new List<String>();

        private string directory;
        private string filename;

        public Playlist() { }

        public Playlist(string _directory, string _filename) {

            directory = _directory;
            filename = _filename;

        }

        public void Make() {

            directories = new Mp3DirectoryList(directory).GetList();
            SaveFile();
        
        }

        private void SaveFile()
        {
            extensions.AddRange(new string[] { "*.mp3", "*.m4a" });

            foreach (Mp3Directory dir in directories)
            {
                bool foundExt = false;
                foreach (string ext in extensions)
                {
                    foreach (string f in Directory.GetFiles(dir.FullPath, ext))
                    {
                        filenames.Add(f);
                        foundExt = true;
                    }
                    if (foundExt) { break; }
                }
                if (!foundExt)
                {
                    throw new Exception(String.Format("O diretório {0} não possui arquivos de áudio.", dir.FullPath));
                }
            }

            File.WriteAllLines(filename, filenames.ToArray(), Encoding.UTF8);
        }

        public string[] GetFilenames()
        {
            return filenames.ToArray();
        }
    }
}
