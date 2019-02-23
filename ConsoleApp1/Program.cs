using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://www.infoworld.com/article/3185447/c-sharp/how-to-work-with-filesystemwatcher-in-c.html
namespace MonitorIntelFolderActivity
{
    class Program
    {
     
       
        static void Main(string[] args)
        {
           
           /* une seule instance
           string str_dirpath = @"C:\Intel\Logs"; 
            //C# program that uses List, foreach-loop
           List<string> list = new List<string>();
           list.Add(str_dirpath);
           foreach (string prime in list)  // Loop through List with foreach.
           {System.Console.WriteLine(prime);
           }
            //The directory path is passed as an argument to the method.
            //  MonitorDirectory(str_dirpath); 
            */ 
            MonitorDirectory();
            Console.ReadKey();
        }


        // private static void MonitorDirectory(string _arg1) // une seule instance
        private static void MonitorDirectory()
        //System.IO will be used for FileSystemWatcher and Directory.
        //System.Collections for the ArrayList
        {
            List<string> lstFolders = new List<string>();   //C# program that uses List, foreach-loop
            ArrayList aFileWatcherInstance = new ArrayList();  //use System.Collections for <ArrayList>;
            lstFolders.Add(@"C:\Intel\Logs");
            lstFolders.Add(@"C:\Intel\");

            foreach (string sMonitorFolder in lstFolders)
            {

                //Only if Directory Exists
                if (Directory.Exists(sMonitorFolder))
            {

                 System.Console.WriteLine(sMonitorFolder);
                 System.IO.FileSystemWatcher oFileWatcher = new FileSystemWatcher(); 
                 oFileWatcher.Path = sMonitorFolder;  //Set the path that you want to monitor.
                 oFileWatcher.Filter = "*.log";  //Set the Filter Expression., only watch log files.


                // Add event handlers.
                 oFileWatcher.Created += Program.FSWatcher_Created;  // |->  method  
                 oFileWatcher.Deleted += Program.FSWatcher_Deleted;  // |->  method
                 oFileWatcher.Renamed += Program.FSWatcher_Renamed;  // |->  method
                // Begin watching.
                 oFileWatcher.EnableRaisingEvents = true;

                //Add a new instance of FileWatcher 
                 aFileWatcherInstance.Add(oFileWatcher);
                }

            }

        }

        private static void FSWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
          
            string sLog = "File renamed: " + e.Name;
            AppendText(sLog);
        }

        private static void FSWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            //A file has been deleted from the monitor directory.
           
            string sLog = "File Deleted: " + e.Name;
            AppendText(sLog);
        }

        private static void FSWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
           // Console.WriteLine("File created: {0}", e.Name);
         
        }
        private static void AppendText(string sLog)
        {
        Console.WriteLine(Convert.ToString(DateTime.Now) +" - " + sLog);
        }
    }
}
