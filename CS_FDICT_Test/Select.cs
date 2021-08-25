using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CS_FDICT_Test
{
    public class Select
    {
        Dictionary<string, int> fDictionary;
        List<string> texts;
        string path;
        string resultPath;
        string text;

        public List<string> Texts { get { return texts; } }

        public Dictionary<string, int> Dictionary { get { return fDictionary; } }


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

                        StreamReader stream = new StreamReader(s);
                        string line;
                        line = stream.ReadLine();

                        while (line != null)
                        {
                            tempStr += line;
                            line = stream.ReadLine();
                        }
                        stream.Close();
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
            char[] separators = new char[] { ' ', ';', ':', '\'', '"', '‘', '“', '’', '”', '<', '>',
                                             '_', '$', '#', '/', '[', ']', '-', '\'', '\\', ',',
                                            '\t', '\n', '\r', '.', '!', '?', '…', '(', ')', '—', '—'};

            string[] SUBs = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sub in SUBs)
            {
                if (sub == "How?")
                {
                    Console.WriteLine(text);
                    Console.ReadKey();
                }
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
            resultPath = path;
            StreamWriter stream = new StreamWriter(resultPath, false);
            foreach (var s in fDictionary)
            {
                if (s.Key == "how") Console.WriteLine("!!!");
                stream.WriteLine(s.Key + " ............................ " + s.Value);
            }
            stream.Close();
        }

        public void Load()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            StreamReader stream = new StreamReader(resultPath);
            string line;
            line = stream.ReadLine();

            while (line != null)
            {
                char[] separators = new char[] { '.', ' '};
                string[] SUBs = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (SUBs[0] == "How?") Console.WriteLine(line);
                temp.Add(SUBs[0], Convert.ToInt32(SUBs[1]));

                line = stream.ReadLine();
            }
            stream.Close();

            foreach (var item in fDictionary)
            {
                if (temp.ContainsKey(item.Key))
                {
                    if (temp[item.Key] != item.Value)
                    {
                        Console.WriteLine(item.Key + "..." + item.Value);
                    }
                }
                else Console.WriteLine(item.Key + "..." + item.Value);

            }
        }
             
    }
}
