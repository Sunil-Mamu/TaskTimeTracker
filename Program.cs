using System;
using System.Collections.Generic;
using System.IO;

namespace TaskTimeTracker
{
    class Program
    {
        static List<Task> taskList = new();
        const string FilePath = "tasklog.txt";

        static void Main()
        {
            LoadTasks();
            while (true)
            {
                Console.WriteLine("\n1. Add Task\n2. Show Tasks\n3. Show Total Hours\n4. Save & Exit");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask(); break;
                    case "2":
                        ShowTasks(); break;
                    case "3":
                        ShowTotalHours(); break;
                    case "4":
                        SaveTasks(); return;
                    default:
                        Console.WriteLine("Invalid option."); break;
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Task name: ");
            var name = Console.ReadLine();

            Console.Write("Hours spent: ");
            if (double.TryParse(Console.ReadLine(), out double hours))
            {
                taskList.Add(new Task(name, hours));
                Console.WriteLine("Task added.");
            }
            else
                Console.WriteLine("Invalid number.");
        }

        static void ShowTasks()
        {
            if (taskList.Count == 0) Console.WriteLine("No tasks logged.");
            else
                taskList.ForEach(t => Console.WriteLine(t));
        }

        static void ShowTotalHours()
        {
            double total = 0;
            taskList.ForEach(t => total += t.Hours);
            Console.WriteLine($"Total: {total} hours");
        }

        static void SaveTasks()
        {
            using StreamWriter writer = new(FilePath);
            foreach (var task in taskList)
                writer.WriteLine($"{task.Name}|{task.Hours}");
            Console.WriteLine("Tasks saved.");
        }

        static void LoadTasks()
        {
            if (!File.Exists(FilePath)) return;

            foreach (var line in File.ReadAllLines(FilePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && double.TryParse(parts[1], out double hrs))
                    taskList.Add(new Task(parts[0], hrs));
            }
        }
    }
}
