using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PlaylistMaker
{
    public class Mp3DirectoryList
    {
        private List<Mp3Directory> list = new List<Mp3Directory>();

        public Mp3DirectoryList(string dir)
        {
            Read(dir);
            // ordenar de trás pra frente primeiro, para evitar que as bandas no fim do alfabeto fiquem juntas
            list.Reverse(); 
            Sort();
            // inverter novamente, e prossegue com a ordenação do começo do alfabeto
            list.Reverse(); 
            Sort();
        }

        private void Read(string dir)
        {
            try
            {
                foreach (string d1 in Directory.GetDirectories(dir))
                {
                    foreach (string d2 in Directory.GetDirectories(d1))
                    {
                        string[] fullDir = d2.Split('\\');
                        Mp3Directory mp3Dir = new Mp3Directory();

                        mp3Dir.Artist = fullDir[fullDir.Length - 2];
                        mp3Dir.Album = fullDir[fullDir.Length - 1];
                        mp3Dir.FullPath = d2;

                        list.Add(mp3Dir);
                    }
                }
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void Sort()
        {

            for (int i = 0; i < list.Count - 1; i++) 
            { 
                
                Mp3Directory dir;
                Mp3Directory nextDir;

                dir = list[i];
                nextDir = list[i + 1];

                if (dir.Artist == nextDir.Artist)
                {
                    // puxar os 2 artistas que estiverem na frente
                    int j = i + 1;

                    int inicio = 0;
                    int fim = 0;

                    int changes = 0;
                    while (changes <= 2)
                    {

                        if ((j + 1) > (list.Count - 1))
                        {
                            break;
                        }

                        if (list[j].Artist != list[j + 1].Artist)
                        {
                            changes++;
                        }

                        j++;

                        if (changes == 1)
                        {
                            inicio = j;
                        }

                        if (changes == 3)
                        {
                            fim = j - 1;
                        }

                    }

                    if (fim > inicio)
                    {
                        for (int k = inicio; k <= fim; k++)
                        {
                            Mp3Directory dirK = list[k];
                            list.RemoveAt(k);
                            list.Insert(k - (inicio - (i + 1)), dirK);
                        }
                    }
                    else {
                        break;
                    }
                }

            }

        }


        public List<Mp3Directory> GetList()
        {
            return list;
        }
    }
}
