// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading; // For Thread.Sleep

// This program helps users memorize scripture passages
// by progressively hiding random words.

// Features that go beyond basic requirements:
// 1. Scripture Library: The program can handle multiple Scriptures
//    and selects one at random for the memorization session.
// 2. Hide only unhidden words: When selecting words to hide,
//    the program ensures that only visible words are selected,
//    which improves the memorization experience.

public class Program
{
    public static void Main(string[] args)
    {
        // Initialize a scripture library
        List<Scripture> scriptureLibrary = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("Philippians", 4, 13), "I can do all things through Christ which strengtheneth me."),
            new Scripture(new Reference("Psalms", 23, 1), "The Lord is my shepherd; I shall not want.")
        };

        // Select a random Scripture from the library
        Random random = new Random();
        Scripture currentScripture = scriptureLibrary[random.Next(scriptureLibrary.Count)];

        string userInput = "";
        while (userInput.ToLower() != "quit" && !currentScripture.IsCompletelyHidden())
        {
            Console.Clear(); // Clear the console screen
            Console.WriteLine(currentScripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit:");

            userInput = Console.ReadLine();

            if (userInput.ToLower() != "quit")
            {
                currentScripture.HideRandomWords(3); // Hide 3 words at a time
            }
        }

        Console.Clear();
        Console.WriteLine(currentScripture.GetDisplayText()); // Display the final Scripture
        Console.WriteLine("\nProgram finished! Keep practicing!");
        Thread.Sleep(2000); // Pause for 2 seconds before exiting
    }
}

// Scripture.cs
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    // Constructor for the Scripture class
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        // Split the text into words and create Word objects
        foreach (string wordText in text.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries))
        {
            _words.Add(new Word(wordText));
        }
    }

    // Hides a specified number of random words that are not yet hidden
    public void HideRandomWords(int count)
    {
        Random random = new Random();
        List<Word> unhiddenWords = _words.Where(w => !w.IsHidden()).ToList();

        // If there are no unhidden words or the count is greater than available words,
        // adjust the count to avoid errors and hide all remaining.
        if (unhiddenWords.Count == 0)
        {
            return; // All words are already hidden
        }

        int wordsToHide = Math.Min(count, unhiddenWords.Count);

        for (int i = 0; i < wordsToHide; i++)
        {
            int indexToHide = random.Next(unhiddenWords.Count);
            unhiddenWords[indexToHide].Hide();
            unhiddenWords.RemoveAt(indexToHide); // Remove to avoid hiding the same word twice in the same round
        }
    }

    // Gets the full display text of the Scripture, including the reference and words (hidden or not)
    public string GetDisplayText()
    {
        string scriptureText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} {scriptureText}";
    }

    // Checks if all words in the Scripture are hidden
    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}

// Reference.cs
public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse; // Optional, for verse ranges

    // Constructor for a single verse reference (e.g., John 3:16)
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse; // The end verse is the same as the start verse
    }

    // Constructor for a verse range (e.g., Proverbs 3:5-6)
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    // Gets the text representation of the reference
    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}

// Word.cs
public class Word
{
    private string _text;
    private bool _isHidden;

    // Constructor for the Word class
    public Word(string text)
    {
        _text = text;
        _isHidden = false; // By default, the word is not hidden
    }

    // Hides the word
    public void Hide()
    {
        _isHidden = true;
    }

    // Shows the word (useful if an "unhide" function were desired)
    public void Show()
    {
        _isHidden = false;
    }

    // Checks if the word is hidden
    public bool IsHidden()
    {
        return _isHidden;
    }

    // Gets the display text of the word (underscores if hidden, or the original text)
    public string GetDisplayText()
    {
        if (_isHidden)
        {
            // Replace with underscores of the same length as the original word
            return new string('_', _text.Length);
        }
        else
        {
            return _text;
        }
    }
}
