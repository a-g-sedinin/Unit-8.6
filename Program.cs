using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime bday)
        {
            Name = name;
            Group = group;
            DateOfBirth = bday;

        }
    }
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
        static string GetFile()
        {
            Console.WriteLine("Input correct path to file, please.");
            string path = Console.ReadLine();
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    path = fileInfo.FullName;
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


            return GetFile();
        }
        static void DeleteUnused(DirectoryInfo path)
        {
            DirectoryInfo[] dirs = path.GetDirectories();
            Console.WriteLine($"\n--->>> Directories <<<---\n Total directories in root: {dirs.Length}");
            foreach (DirectoryInfo dir in dirs)
            {


                if (DateTime.Now - dir.LastAccessTime >= TimeSpan.FromMinutes(30))
                {

                    try
                    {
                        dir.Delete(true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            FileInfo[] files = path.GetFiles();
            Console.WriteLine($"\n--->>> Files <<<---\n Total files in root: {files.Length}");
            foreach (FileInfo file in files)
            {
                if (DateTime.Now - file.LastAccessTime >= TimeSpan.FromMinutes(30))
                {

                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            }
        }
        static long CalcSpace(DirectoryInfo path)
        {
            long space = 0;
            FileInfo[] files = path.GetFiles();
            
            foreach (FileInfo file in files)
            {
                try
                {
                 space += file.Length;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            DirectoryInfo[] dirs = path.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {
                try
                {
                    space += CalcSpace(dir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
         return space;
        }
        static void Task1()
        { 
            Console.Clear();
            Console.WriteLine("Task #1 has been choosen.");
            DirectoryInfo path = new DirectoryInfo(GetPath());
            DeleteUnused(path);
            Console.ReadKey();
        }
        static void Task2()
        {
            Console.Clear();
            Console.WriteLine("Task #2 has been choosen.");
            DirectoryInfo path = new DirectoryInfo( GetPath());
            
            
            Console.WriteLine($"Directory size = {CalcSpace(path)} byte(s)");
            Console.ReadKey();

        }
        static void Task3()
        {
            Console.Clear();
            Console.WriteLine("Task #3 has been choosen.");
            DirectoryInfo path = new DirectoryInfo(GetPath());
            long initialSpace = CalcSpace(path);
            DeleteUnused(path);
            long currentSpace = CalcSpace(path);
            long deletedSpace = initialSpace - currentSpace;
            Console.WriteLine($"\n Initial size was: {initialSpace} byte(s) \n Byte(s) deleted: {deletedSpace} \n Current space is: {currentSpace} byte(s) ");
            Console.ReadKey();


        }
        static void Task4()
        {
            Console.Clear();
            Console.WriteLine("Task #4 has been choosen.");
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream fs = new FileStream(GetFile(), FileMode.OpenOrCreate);
            {
                try
                {
                    Student[] st = (Student[]) formatter.Deserialize(fs);
                    string destopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    DirectoryInfo dirinfo = new DirectoryInfo(destopPath + "/Students/");
                    if (!dirinfo.Exists) dirinfo.Create();
                    
                    string dirFullName = dirinfo.FullName;
                    
                    foreach (Student stud in st)
                    {
                        string filePath = dirFullName + stud.Group + ".txt";
                        string strToWrite = stud.Name + ", " + stud.DateOfBirth.ToString();
                        if (!File.Exists(filePath))
                        {
                            using (StreamWriter sw = File.CreateText(filePath));
                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo(filePath);
                            using (StreamWriter sw = fileInfo.AppendText())
                            {
                                sw.WriteLine(strToWrite);
                            }
                        
                        }
                        
                    }
                    Console.WriteLine("Sorting complete.");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


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
                    Task4();
                    break;
                default:

                    ChooseTask();
                    break;
            }


        }


    }
}
