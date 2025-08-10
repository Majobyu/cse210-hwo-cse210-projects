using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MindfulnessApp
{
    // Abstract base class for activities with shared behavior
    abstract class ActivityBase
    {
        private int duration;
        private string activityName;
        private string description;

        protected ActivityBase(string name, string description)
        {
            this.activityName = name;
            this.description = description;
        }

        // Start the activity flow
        public void Execute()
        {
            Console.Clear();
            AskDuration();
            ShowStartingMessage();
            RunActivity();
            ShowEndingMessage();
        }

        // Request and validate the duration input
        private void AskDuration()
        {
            int seconds = 0;
            bool valid = false;
            do
            {
                Console.Write("How many seconds will this session last? ");
                string input = Console.ReadLine();
                valid = int.TryParse(input, out seconds) && seconds > 0;
                if (!valid)
                {
                    Console.WriteLine("Please enter a positive whole number.");
                }
            } while (!valid);
            duration = seconds;
        }

        protected int GetDuration() => duration;
        protected string GetName() => activityName;
        protected string GetDescription() => description;

        // Common starting message with countdown animation
        protected void ShowStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Activity: {activityName}");
            Console.WriteLine(description);
            Console.WriteLine($"Duration: {duration} seconds");
            Console.WriteLine("Get ready to begin...");
            CountdownAnimation(3);
        }

        // Common ending message with countdown animation
        protected void ShowEndingMessage()
        {
            Console.WriteLine("\nGood job! You have completed the activity.");
            Console.WriteLine($"Activity finished: {activityName}");
            Console.WriteLine($"Elapsed time: {duration} seconds.");
            CountdownAnimation(3);
        }

        // Simple countdown animation for pauses in seconds
        protected void CountdownAnimation(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\rStarting in {i} seconds... ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Spinner animation for reflection pauses
        protected void SpinnerAnimation(int durationSeconds)
        {
            char[] symbols = { '|', '/', '-', '\\' };
            int index = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (timer.ElapsedMilliseconds < durationSeconds * 1000)
            {
                Console.Write($"\r{symbols[index++ % symbols.Length]} Reflecting... ");
                Thread.Sleep(200);
            }
            Console.WriteLine();
        }

        // Timer animation for breathing pauses
        protected void TimerAnimation(int durationSeconds)
        {
            for (int i = durationSeconds; i > 0; i--)
            {
                Console.Write($"\rTime: {i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Abstract method for the activity-specific execution
        protected abstract void RunActivity();
    }

    // Breathing activity class
    class BreathingActivity : ActivityBase
    {
        public BreathingActivity()
            : base("Breathing",
                  "This activity will help you relax by walking you through slow inhales and exhales. Clear your mind and focus on your breathing.")
        {
        }

        protected override void RunActivity()
        {
            int duration = GetDuration();
            int elapsed = 0;
            bool inhale = true;

            Console.WriteLine("\nLet's begin!\n");

            while (elapsed < duration)
            {
                if (inhale)
                    Console.WriteLine("Inhale...");
                else
                    Console.WriteLine("Exhale...");
                inhale = !inhale;

                int pauseDuration = 4; // seconds for inhale/exhale cycle

                for (int i = pauseDuration; i > 0; i--)
                {
                    Console.Write($"\rTime: {i} ");
                    Thread.Sleep(1000);
                    elapsed++;
                    if (elapsed >= duration)
                        break;
                }
                Console.WriteLine();
            }
        }
    }

    // Reflection activity class
    class ReflectionActivity : ActivityBase
    {
        private List<string> prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What do you like most about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity()
            : base("Reflection",
                  "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other areas of your life.")
        { }

        protected override void RunActivity()
        {
            int duration = GetDuration();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Random rand = new Random();

            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine("\nLet's begin reflecting!\n");
            Console.WriteLine(prompt);
            Console.WriteLine();

            // Loop to randomly ask questions with pauses until duration expires
            while (stopwatch.ElapsedMilliseconds < duration * 1000)
            {
                string question = questions[rand.Next(questions.Count)];
                Console.WriteLine(question + " (Reflect)");
                SpinnerAnimation(4);
                if (stopwatch.ElapsedMilliseconds >= duration * 1000)
                    break;
            }
        }
    }

    // Listing activity class
    class ListingActivity : ActivityBase
    {
        private List<string> prompts = new List<string>
        {
            "Who are people you appreciate?",
            "What are your personal strengths?",
            "Who have you helped this week?",
            "When have you felt the Spirit this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
            : base("Listing",
                  "This activity will help you reflect on the good things in your life by having you list as many items as you can in a certain area.")
        { }

        protected override void RunActivity()
        {
            int duration = GetDuration();
            Random rand = new Random();

            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine("\nPrompt: " + prompt);
            Console.WriteLine("Prepare to think...");
            CountdownAnimation(3);

            Console.WriteLine("\nStart writing your responses. (Enter as many items as you can. Press Enter after each. To finish early, just press Enter on a blank line.)");
            List<string> items = new List<string>();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (timer.ElapsedMilliseconds < duration * 1000)
            {
                if (Console.KeyAvailable)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;
                    items.Add(line.Trim());
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            Console.WriteLine($"\nTime's up or finished early! Number of items entered: {items.Count}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Mindfulness Application.");
                Console.WriteLine("Select an activity:");
                Console.WriteLine("1. Breathing");
                Console.WriteLine("2. Reflection");
                Console.WriteLine("3. Listing");
                Console.WriteLine("4. Exit");
                Console.Write("Enter the number of your choice: ");

                string choice = Console.ReadLine();

                ActivityBase activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("Thank you for using the Mindfulness application. Goodbye!");
                        Thread.Sleep(2000);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        Thread.Sleep(2000);
                        break;
                }

                if (activity != null)
                {
                    activity.Execute();
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
