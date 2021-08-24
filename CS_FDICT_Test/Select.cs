using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CS_FDICT_Test
{
    class Select
    {
        Dictionary<string, int> fDictionary;
        List<string> texts;
        string path;
        string text;

        public Select()
        {
            fDictionary = new Dictionary<string, int>();
            texts = new List<string>();
        }

        public void TXTSelect(string path)
        {
            this.path = path;
            DirectoryInfo d1 = new DirectoryInfo(path);
            if (d1.Exists == true)
            {
                FileInfo[] files = d1.GetFiles("*.txt");
                foreach (FileInfo current in files)
                {
                    texts.Add(current.FullName);
                }
            }
            else throw new Exception("ERROR: path entered incorrectly");
        }

        public void TXTProcess()
        {
            if (texts.Count != 0)
            {
                foreach (var s in texts)
                {
                    string tempStr = null;
                    try
                    {

                        tempStr = File.ReadAllText(s);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    WORDSelect(tempStr);
                }
            }
            else throw new Exception("ERROR: no text files selected");
            
        }

        public void WORDSelect(string text)
        {
            string word = null;
            char[] separators = new char[] { ' ', ';', ':', '"', '-', '\'', '\\', ',', '\t', '\n', '\r', '.', '!', '?', '…', '(', ')' };

            string[] SUBs = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sub in SUBs)
            {
                word = sub;
                if (fDictionary.ContainsKey(word))
                {
                    fDictionary[word]++;
                }
                else
                {
                    fDictionary.Add(word, 1);
                }
                
            }
        }

        public void Display()
        {
            foreach (var s in fDictionary)
            {
                Console.WriteLine(s.Key+"-------"+s.Value);
            }
        }

        public void Sort()
        {
            List<(int, string)> temp = new List<(int, string)> ();
            foreach (var s in fDictionary)
            {
                temp.Add((s.Value, s.Key));
            }

            temp.Sort();
            fDictionary.Clear();
            foreach ((int, string) s in temp)
            {
                fDictionary.Add(s.Item2, s.Item1);
            }
        }

        public void Save(string path)
        {
            StreamWriter stream = new StreamWriter(path, false, Encoding.Default);
            foreach (var s in fDictionary)
            {
                stream.WriteLine(s.Key + "............................" + s.Value);
            }
            stream.Close();
        }
             
    }
}
