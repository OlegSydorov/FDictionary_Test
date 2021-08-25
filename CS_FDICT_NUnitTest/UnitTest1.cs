using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using CS_FDICT_Test;
using System.IO;


namespace CS_FDICT_NUnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        string path="e:\\text";
        [Test]
        public void TestTXTSelect1()
        {
            Select s = new Select();
            int count = 0;
           
            DirectoryInfo data = new DirectoryInfo(path);
           
            if (!data.Exists)
            {
                Assert.That(() => s.TXTSelect(path), Throws.Exception, "Does not throw exception!");
            }
        }
        [Test]
        public void TestTXTSelect2()
        {
            Select s = new Select();
            int count = 0;
            s.TXTSelect(path);

            DirectoryInfo data = new DirectoryInfo(path);
            if (data.Exists == true)
            {
                FileInfo[] files = data.GetFiles();
                foreach (FileInfo current in files)
                {
                    if (current.Extension == ".txt") count++;
                }

                if (count != s.Texts.Count) Assert.Fail("ERROR: wrong number of selected .txt files!");
            }
            
        }
        [Test]
        public void TestTXTSelect3()
        {
            Select s = new Select();
            s.TXTSelect(path);

            foreach(string file in s.Texts)
            {
                string extension = file.Substring(file.Length - 4);
                if (extension!=".txt") Assert.Fail("ERROR: wrong files selected!");
            }

        }
        [Test]
        public void TestWORDSelect1()
        {
            DirectoryInfo data = new DirectoryInfo(path);
            if (data.Exists == true)
            {
                HashSet<string> set = new HashSet<string>();
                FileInfo[] files = data.GetFiles("*.txt");
                foreach (FileInfo current in files)
                {
                    string text=null;
                    StreamReader stream = new StreamReader(current.FullName);
                    string line;
                    line = stream.ReadLine();

                    while (line != null)
                    {
                        text += line;
                        line = stream.ReadLine();
                    }
                    stream.Close();

                    char[] separators = new char[]{ ' ', ';', ':', '\'', '"', '‘', '“', '’', '”', '<', '>',
                                             '_', '$', '#', '/', '[', ']', '-', '\'', '\\', ',',
                                            '\t', '\n', '\r', '.', '!', '?', '…', '(', ')', '—', '—'};

                    string[] SUBs = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in SUBs) set.Add(word);
                }

                Select s = new Select();
                s.TXTSelect(path);
                s.TXTProcess();

                Assert.That(() => s.Dictionary.Count == set.Count, "ERROR: wrong number of words selected form texts!");
            }
        }
        [Test]
        public void TestWORDSelect2()
        {
            DirectoryInfo data = new DirectoryInfo(path);
            if (data.Exists == true)
            {
                HashSet<string> set = new HashSet<string>();
                FileInfo[] files = data.GetFiles("*.txt");
                foreach (FileInfo current in files)
                {
                    string text = null;
                    StreamReader stream = new StreamReader(current.FullName);
                    string line;
                    line = stream.ReadLine();

                    while (line != null)
                    {
                        text += line;
                        line = stream.ReadLine();
                    }
                    stream.Close();
                    char[] separators = new char[] { ' ', ';', ':', '\'', '"', '‘', '“', '’', '”', '<', '>',
                                             '_', '$', '#', '/', '[', ']', '-', '\'', '\\', ',',
                                            '\t', '\n', '\r', '.', '!', '?', '…', '(', ')', '—', '—'};

                    string[] SUBs = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in SUBs) set.Add(word);
                }

                Select s = new Select();
                s.TXTSelect(path);
                s.TXTProcess();

                foreach (string word in set)
                {
                    if (!s.Dictionary.ContainsKey(word))
                        Assert.Fail("ERROR: not all words selected from texts!");
                }
            }
        }
        [Test]
        public void testWORDSelect3()
        {
            DirectoryInfo data = new DirectoryInfo(path);
            if (data.Exists == true)
            {
                HashSet<string> set = new HashSet<string>();
                FileInfo[] files = data.GetFiles("*.txt");
                foreach (FileInfo current in files)
                {
                    string text = null;
                    StreamReader stream = new StreamReader(current.FullName);
                    string line;
                    line = stream.ReadLine();

                    while (line != null)
                    {
                        text += line;
                        line = stream.ReadLine();
                    }
                    stream.Close();
                    char[] separators = new char[] { ' ', ';', ':', '\'', '"', '‘', '“', '’', '”', '<', '>',
                                             '_', '$', '#', '/', '[', ']', '-', '\'', '\\', ',',
                                            '\t', '\n', '\r', '.', '!', '?', '…', '(', ')', '—', '—'};

                    string[] SUBs = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in SUBs) set.Add(word);
                }

                Select s = new Select();
                s.TXTSelect(path);
                s.TXTProcess();

                foreach (var item in s.Dictionary)
                {
                    if (!set.Contains(item.Key))
                        Assert.Fail("ERROR: alien words selected from texts!");
                }
            }
        }
        [Test]
        public void testWORDSelect4()
        {
            DirectoryInfo data = new DirectoryInfo(path);
            if (data.Exists == true)
            {
                List<string> list = new List<string>();
                FileInfo[] files = data.GetFiles("*.txt");
                foreach (FileInfo current in files)
                {
                    string text = null;
                    StreamReader stream = new StreamReader(current.FullName);
                    string line;
                    line = stream.ReadLine();

                    while (line != null)
                    {
                        text += line;
                        line = stream.ReadLine();
                    }
                    stream.Close();
                    char[] separators = new char[]{ ' ', ';', ':', '\'', '"', '‘', '“', '’', '”', '<', '>',
                                             '_', '$', '#', '/', '[', ']', '-', '\'', '\\', ',',
                                            '\t', '\n', '\r', '.', '!', '?', '…', '(', ')', '—', '—'};

                    string[] SUBs = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in SUBs) list.Add(word);
                }

                Select s = new Select();
                s.TXTSelect(path);
                s.TXTProcess();

                foreach (var item in s.Dictionary)
                {
                    List<string> words = list.FindAll(x => x == item.Key);
                    if (words.Count != item.Value) Assert.Fail($"ERROR: word count for {item.Key} is incorrect");
                }
            }
        }
        [Test]
        public void TestSort1()
        {
            Select s = new Select();
            s.TXTSelect(path);
            s.TXTProcess();

            int[] arr = new int[s.Dictionary.Count];
            int n = 0;
            foreach (var item in s.Dictionary)
            {
                arr[n] = item.Value;
                n++;
            }
            int min;
            int max;
            GetMinMax(arr, out min, out max);
            s.Sort();
            int n1 = 0;
            foreach (var item in s.Dictionary)
            {
                if (n1 == 0 && item.Value != min)
                {
                    Assert.Fail("ERROR: wrong least frequent word frequency count!");
                }
                else if (n1 == s.Dictionary.Count && item.Value != max)
                {
                    Assert.Fail("ERROR: wrong most frequent word frequency count!");
                }
                n1++;
            }


        }

        static void GetMinMax(int[] array, out int Min, out int Max)
        {
            Min = array[0];
            Max = array[0];
            foreach (var a in array)
            {
                if (a < Min) Min = a;
                if (a > Max) Max = a;
            }

        }
        [Test]
        public void TestSort2()
        {
            Select s = new Select();
            s.TXTSelect(path);
            s.TXTProcess();
            int[] arr = new int[s.Dictionary.Count];
            int n = 0;
            foreach (var item in s.Dictionary)
            {
                arr[n] = item.Value;
                n++;
            }
            SortArr(arr);
            int n1 = 0;
            s.Sort();
            foreach (var item in s.Dictionary)
            {
                if (arr[n1] != item.Value) Assert.Fail("ERROR: Sorting incorrect!");
                n1++;
            }
        }

        public void SortArr(int[] array)
        {
            if (array == null)
                return;
            QSort(array, 0, array.Length - 1);
        }
        void QSort(int[] array, int start, int end)
        {
            if (start >= end) return;
            int i = start, j = end;

            int baseElementIndex = start + (end - start) / 2;

            while (i < j)
            {
                int value = array[baseElementIndex];

                while (i < baseElementIndex && (array[i] <= value)) i++;

                while (j > baseElementIndex && (array[j] >= value)) j--;

                if (i < j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    if (i == baseElementIndex) baseElementIndex = j;
                    else if (j == baseElementIndex) baseElementIndex = i;
                }
            }

            QSort(array, start, baseElementIndex);
            QSort(array, baseElementIndex + 1, end);
        }
        [Test]
        public void TestSave1()
        {
            //List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            //DirectoryInfo data = new DirectoryInfo(path);
            //if (data.Exists == true)
            //{
            //    FileInfo[] files = data.GetFiles("*.txt");
            //    foreach (FileInfo current in files)
            //    {
            //        list1.Add(current.FullName);
            //    }
            //}

            Select s = new Select();
            s.TXTSelect(path);
            s.TXTProcess();
            s.Sort();
            string filePath = path + "\\result.txt";
            s.Save(filePath);

            DirectoryInfo data1 = new DirectoryInfo(path);
            if (data1.Exists == true)
            {
                FileInfo[] files = data1.GetFiles("*.txt");
                foreach (FileInfo current in files)
                {
                    list2.Add(current.FullName);
                }
            }

            Assert.That(() => list2.Contains(filePath), "ERROR: result file not recorded!");
        }
        [Test]
        public void TestSave2()
        {
            Select s = new Select();
            s.TXTSelect(path);
            s.TXTProcess();
            s.Sort();
            string filePath = path + "\\result.txt";
            s.Save(filePath);

            Dictionary<string, int> temp = new Dictionary<string, int>();
            StreamReader stream = new StreamReader(filePath);
            string line;
            line = stream.ReadLine();

            while (line != null)
            {
                char[] separators = new char[] { '.', ' ' };
                string[] SUBs = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                
                temp.Add(SUBs[0], Convert.ToInt32(SUBs[1]));

                line = stream.ReadLine();
            }
            stream.Close();

            foreach (var item in s.Dictionary )
            {
                if (temp.ContainsKey(item.Key))
                {
                    if (temp[item.Key] != item.Value)
                    {
                       Assert.Fail("ERROR: wrongly saved word frequency!");
                    }
                }
                else Assert.Fail("ERROR: wrongly saved word!");

            }



        }











    }
}
