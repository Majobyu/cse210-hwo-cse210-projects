using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Needed for .ToList() in LoadFromFile

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
            "If I had to do one thing today, what would it be?",
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
            Console.WriteLine("\nThe journal is empty. There are no entries to display.");
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
        Console.Write("Enter the filename to save (e.g. my_journal.txt): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                // Using '|~|' as a separator to avoid conflicts with commas.
                foreach (Entry entry in _entries)
                {
                    outputFile.WriteLine($"{entry.Date}|~|{entry.Prompt}|~|{entry.Response}");
                }
            }
            Console.WriteLine($"Journal saved successfully to '{filename}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving the file: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        Console.Write("Enter the filename to load (e.g. my_journal.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine($"Error: The file '{filename}' does not exist.");
            return;
        }

        try
        {
            _entries.Clear(); // Clear current entries before loading new ones
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] parts = line.Split(new string[] { "|~|" }, StringSplitOptions.None);
                if (parts.Length == 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];
                    _entries.Add(new Entry(prompt, response, date));
                }
                else
                {
                    Console.WriteLine($"Warning: Skipped line with incorrect format: {line}");
                }
            }
            Console.WriteLine($"Journal loaded successfully from '{filename}'. {_entries.Count} entries loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading the file: {ex.Message}");
        }
    }
}
