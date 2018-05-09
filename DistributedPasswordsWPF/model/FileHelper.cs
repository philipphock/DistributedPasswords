using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class FileHelper
    {
        private const string DB_FOLDER = "assets/pypassDatabase";
        private const string KEYS_FOLDER = "assets/pypassKeys";

        public static void readFile()
        { 
        }

        private static void ListFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(@path);
            FileInfo[] Files = d.GetFiles(); 
            string str = "";
            foreach (FileInfo file in Files)
            {
                Console.WriteLine(file);
            }
        }

        public static void ListDatabaseFiles()
        {
            ListFiles(DB_FOLDER);
        }
    }
}


/**
 * 
DirectoryInfo d = new DirectoryInfo(@"D:\Test");//Assuming Test is your Folder
FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
string str = "";
foreach(FileInfo file in Files )
{
  str = str + ", " + file.Name;
}
 * */
