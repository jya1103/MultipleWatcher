using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://www.infoworld.com/article/3185447/c-sharp/how-to-work-with-filesystemwatcher-in-c.html
//https://www.geeksforgeeks.org/c-sharp-arraylist-class/
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
            lstFolders.Add(@"C:\Intel\Logs");
            lstFolders.Add(@"C:\Intel\");

            ArrayList aFileWatcherInstance = new ArrayList();  //use System.Collections for <ArrayList>;
            System.Console.WriteLine("..");
            foreach (string sMonitorFolder in lstFolders)
            {
               

                //Only if Directory Exists
                if (Directory.Exists(sMonitorFolder))
            {
               
              
                 System.IO.FileSystemWatcher oMultipleFileWatcher;
                 oMultipleFileWatcher = new FileSystemWatcher();    //the object go into scope now

                    //Set the path that you want to monitor. 
                    oMultipleFileWatcher.Path = sMonitorFolder;
                    
                    //Set the Filter Expression., only watch log files.
                 oMultipleFileWatcher.Filter = "*.*";  

                    // Add event handlers.
                 oMultipleFileWatcher.Created += Program.FSWatcher_Created;  // |->  method  
                 oMultipleFileWatcher.Deleted += Program.FSWatcher_Deleted;  // |->  method
                 oMultipleFileWatcher.Renamed += Program.FSWatcher_Renamed;  // |->  method
                    
                    // Begin watching.
                 oMultipleFileWatcher.EnableRaisingEvents = true;


                    //Creating multiple instances of one object
                    //Multiple calls to oMultipleFileWatcher() would keep adding objects to the list with this design.
                    //https://stackoverflow.com/questions/35232565/create-multiple-instances-of-an-object-c-sharp
                    //https://dzone.com/articles/different-ways-of-creating-list-of-objects-in-c
                    //Add a new instance  
                    aFileWatcherInstance.Add(oMultipleFileWatcher);

                

                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine(sMonitorFolder);

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
            
            if (e.Name== "Nouveau document texte.txt")
            {
                string sLog = "Nouveau document texte !"; AppendText(sLog);
            }
              else
            { // Console.WriteLine("File created: {0}", e.Name);
                string sLog = "File created: " + e.Name;  AppendText(sLog);}
          
        }

        private static void AppendText(string sLog)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Convert.ToString(DateTime.Now) +" - " + sLog);
        }
    }
}
