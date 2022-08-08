using System;
using System.IO;

namespace Unit_8._6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChooseTask();
        }

        static string GetPath()
        {
            Console.WriteLine("Input correct path to directory, please.");
            string path = Console.ReadLine();
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists)
                {
                    path = dirInfo.FullName;
                    return path;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Path is incorrect.");
                    
                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
            
            return GetPath();
        }

        static void Task1()
        { 
            Console.Clear();
            Console.WriteLine("Task #1 has been choosen.");
            string path = GetPath();
            string[] dirs = Directory.GetDirectories(path);
            Console.WriteLine($"\n--->>> Directories <<<---\n Total directories {dirs.Length}");
            foreach (string dir in dirs)
            {


                if (   DateTime.Now - Directory.GetLastAccessTime(dir) >= TimeSpan.FromMinutes(30)         )
                {
                    
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dir);
                        dirInfo.Delete(true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            string[] files = Directory.GetFiles(path);
            Console.WriteLine($"\n--->>> Files <<<---\n Total files {files.Length}");
            foreach (string file in files)
            {
                if (DateTime.Now - File.GetLastAccessTime(file) >= TimeSpan.FromMinutes(30))
                {
                    
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        fileInfo.Delete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            }

        Console.ReadLine();
        }
        static void Task2()
        {
            Console.Clear();
            Console.WriteLine("Task #2 has been choosen.");
            string path = GetPath();




        }
        static void Task3()
        {
            Console.Clear();
            Console.WriteLine("Task #3 has been choosen.");
        }
        static void Task4()
        {
            Console.Clear();
            Console.WriteLine("Task #4 has been choosen.");
        }
        static void ChooseTask()
        {
            Console.Clear();
            Console.WriteLine("Choose task \n 1 - Task #1 \n 2 - Task #2 \n 3 - Task #3 \n 4 - Task #4");



            switch (Console.ReadLine())
            {
                case "1":
                    Task1();
                    break;

                case "2":
                    Task2();
                    break;

                case "3":
                    Task3();
                    break;
                case "4":
                    Task3();
                    break;
                default:

                    ChooseTask();
                    break;
            }


        }


    }
}
