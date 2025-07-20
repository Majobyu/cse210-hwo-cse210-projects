// Program.cs
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Diary Program!");
        Journal myJournal = new Journal();
        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine("\n--- DIARY MENU ---");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Show the diary");
            Console.WriteLine("3. Save the diary to a file");
            Console.WriteLine("4. Load the diary from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    myJournal.AddNewEntry();
                    break;
                case "2":
                    myJournal.DisplayJournal();
                    break;
                case "3":
                    myJournal.SaveToFile();
                    break;
                case "4":
                    myJournal.LoadFromFile();
                    break;
                case "5":
                    Console.WriteLine("Thank you for using the diary program! See you soon!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a number from 1 to 5.");
                    break;
            }
        }
    }
}

/*
* Exceeding the requirements:
* 1. Two additional prompts were added to the list for greater variety:
* - "What did I learn today?"
* - "What am I grateful for today?"
* This helps diversify daily reflections and can encourage greater creativity in responses.
* 2. The separator for saving/loading files has been explicitly defined as "|~|"
* to minimize the risk of collisions with user content, although it does not handle
* complex character escaping like a standard CSV. This is a simple step
* towards greater robustness in storage.
*/
