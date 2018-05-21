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

        public static string[] ListFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(@path);
            FileInfo[] Files = d.GetFiles();
            string[] ret = new string[Files.Length];
            int cnt = 0;
            foreach (FileInfo file in Files)
            {
                ret[cnt] = file.Name;
                cnt++;
            }
            return ret;
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
