using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_FDICT_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Select sel = new Select();
            Console.WriteLine("Enter source folder name:");
            string sourcePath = Console.ReadLine();
            try
            {
                sel.TXTSelect(sourcePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Data.Count);
            }
            try
            {
                sel.TXTProcess();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Data.Count);
            }
           // sel.Display();
            sel.Sort();
            //sel.Display();
            Console.WriteLine("Enter resulting file name:");
            string targetPath = Console.ReadLine();
            sel.Save(targetPath);
            //sel.Load();
        }
    }
}
