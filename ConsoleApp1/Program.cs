using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
//https://www.infoworld.com/article/3185447/c-sharp/how-to-work-with-filesystemwatcher-in-c.html
//https://www.geeksforgeeks.org/c-sharp-arraylist-class/
namespace MonitorIntelFolderActivity
{
    internal class Program
	//class Program
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]

        public static extern int MessageBox(IntPtr h, string m, string c, int type);


        //Make sure your Main method (in Program.cs) is defined as:
        //Then args is an array containing the command-line arguments.
        public static void Main(string[] args)
    {
        Run();
    }
       
	 
    private static void Run()
        {
string[] args = Environment.GetCommandLineArgs();

            // If a directory is not specified, exit program.

            if (args == null) //   
            {
             
            }
            else if (args.Length == 1)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: Watcher.exe (path1 path2)");
                // Console.ReadKey();
                MessageBox((IntPtr)0, "bad", "My Message Box", 0);

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
                return;
            }

            else if (args.Length != 3)
            {
                Console.WriteLine("Usage: Watcher.exe (path1 path2)");
                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
                return;
            }
            else if (args.Length == 3)
            {
                Console.Write("args length is ");
                Console.WriteLine(args.Length);
                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];
                    Console.Write("args index ");
                    Console.Write(i); // Write index
                    Console.Write(" is [");
                    Console.Write(argument); // Write string
                    Console.WriteLine("]");
                }
          
                MonitorDirectory(args[1], args[2]);
                Console.ReadKey();
            }

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


        //  MonitorDirectory(args[1]);

       
        }


        // private static void MonitorDirectory(string _arg1) // une seule instance
      private static void MonitorDirectory(string v, string v1)
          //  private static void MonitorDirectory(string v)
          //System.IO will be used for FileSystemWatcher and Directory.
        //System.Collections for the ArrayList
       {

            List<string> lstFolders = new List<string>();   //C# program that uses List, foreach-loop
          // lstFolders.Add(@"C:\Intel\Logs");
           // lstFolders.Add(@"C:\Intel\");
			 lstFolders.Add(v);
             lstFolders.Add(v1);
            ArrayList aFileWatcherInstance = new ArrayList();  //use System.Collections for <ArrayList>;
            System.Console.WriteLine("..");
            foreach (string sMonitorFolder in lstFolders)
            {
               
             
                if (Directory.Exists(sMonitorFolder))   //Only if Directory Exists
                {
                    System.IO.FileSystemWatcher oMultipleFileWatcher;  // Create a new FileSystemWatcher and set its properties.
                    oMultipleFileWatcher = new FileSystemWatcher();   

       
                  {  //Set the path that you want to monitor. 
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
           
        }

        private static void FSWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {

            // string sLog = e.ChangeType+ " ---- " + e.Name;
            // Specify what is done when a file is renamed.
            // string sLog = ($"File: {e.OldFullPath} renamed to {e.FullPath}");
            string sLog = ($"File <{e.OldName}> renamed to <{e.Name}>");
            AppendText(sLog);
        }

        private static void FSWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            //A file has been deleted from the monitor directory.
           
            string sLog = e.ChangeType+ " ---- " + e.Name;
            AppendText(sLog);
        }

        private static void FSWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            
            if (e.Name== "Nouveau document texte.txt")
            {
				
                string sLog =  e.ChangeType+ " ---- " +e.Name; AppendText(sLog);
            }
              else
            { // Console.WriteLine("File created: {0}", e.Name);
                string sLog = e.ChangeType+ " ---- "  +e.FullPath;  AppendText(sLog);}
          
        }

        private static void AppendText(string sLog)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Convert.ToString(DateTime.Now) +" - " + sLog);
            MessageBox((IntPtr)0, (Convert.ToString(DateTime.Now) + " - " + sLog), "My Message Box", 0);

        }
    }
}
