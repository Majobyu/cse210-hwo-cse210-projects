using System;
using System.Collections.Generic;
using System.IO;
// using System.Linq; // Not strictly necessary for current operations in LoadFromFile

public class Journal
{
    private List<Entry> _entries;
    private List<string> _prompts; // List of prompts

    public Journal()
    {
        _entries = new List<Entry>();
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing to do today, what would it be?",
            "What did I learn today?", // Additional prompt
            "What am I grateful for today?" // Additional prompt
        };
    }

    public void AddNewEntry()
    {
        Random random = new Random();
        string randomPrompt = _prompts[random.Next(_prompts.Count)];

        Console.WriteLine($"\nPrompt: {randomPrompt}");
        Console.Write("Your response: ");
        string userResponse = Console.ReadLine();
        string currentDate = DateTime.Now.ToShortDateString(); // Simple date format

        Entry newEntry = new Entry(randomPrompt, userResponse, currentDate);
        _entries.Add(newEntry);
        Console.WriteLine("Entry saved successfully.");
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nThe journal is empty. No entries to display.");
            return;
        }

        Console.WriteLine("\n--- FULL JOURNAL ---");
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
        Console.WriteLine("--- END OF JOURNAL ---\n");
    }

    public void SaveToFile()
    {
        Console.Write("Enter the filename to save (e.g., my_journal.txt): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                // We use '|~|' as a separator to avoid conflicts with commas.
                foreach (Entry entry in _entries)
                {
                    outputFile.WriteLine($"{entry.Date}|~|{entry.Prompt}|~|{entry.Response}");
                }
            }
            Console.WriteLine($"Journal saved to '{filename}' successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        Console.Write("Enter the filename to load (e.g., my_journal.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine($"Error: The file '{filename}' does not exist.");
            return;
        }

        try
        {
            _entries.Clear(); // Clears current entries before loading new ones
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                // The Split method is used with the separator as a string directly for conciseness.
                string[] parts = line.Split("|~|", StringSplitOptions.None);
                if (parts.Length == 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];
                    _entries.Add(new Entry(prompt, response, date));
                }
                else
                {
                    Console.WriteLine($"Warning: Malformed line skipped: {line}");
                }
            }
            Console.WriteLine($"Journal loaded from '{filename}' successfully. {_entries.Count} entries loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}

// Assuming the Entry class exists as it's used in Journal.
// If not provided, a basic implementation is given here:
public class Entry
{
    public string Prompt { get; }
    public string Response { get; }
    public string Date { get; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public void Display()
    {
        Console.WriteLine($"\nDate: {Date}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
    }
}

// Program.cs
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Journal Program!");
        Journal myJournal = new Journal();
        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine("\n--- JOURNAL MENU ---");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
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
                    Console.WriteLine("Thank you for using the journal program! See you soon!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a number from 1 to 5.");
                    break;
            }
        }
    }
}

/*
* Exceeding requirements:
* 1. Two additional prompts were added to the list for more variety:
* - "What did I learn today?"
* - "What am I grateful for today?"
* This helps diversify daily reflections and can encourage greater creativity in responses.
* 2. The separator for saving/loading files has been explicitly defined as "|~|"
* to minimize the risk of collisions with user content, although it does not handle
* complex character escapes like a standard CSV. This is a simple step
* towards greater robustness in storage.
*/
