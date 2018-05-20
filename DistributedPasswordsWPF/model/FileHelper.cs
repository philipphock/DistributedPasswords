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

        

        public static void WriteHeader()
        {
            Path.Combine(Settings.KEYS_PATH, "header");
        }
        public static void WriteHash()
        {
            Path.Combine(Settings.KEYS_PATH, "hash");
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
        //    ListFiles(DB_FOLDER);
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
