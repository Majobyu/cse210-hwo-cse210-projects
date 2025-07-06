using System;

public class Program
{
    public static void Main(string[] args)
    {
        PlayGuessingGame();
    }

    public static void PlayGuessingGame()
    {
        bool playAgain = true;

        while (playAgain)
        {
            Random random = new Random();
            int magicNumber = random.Next(1, 101); // Generates a number from 1 to 100

            int guess = -1;
            int guessCount = 0;

            Console.WriteLine("Welcome to the 'Guess My Number' game!");
            Console.WriteLine("I'm thinking of a number between 1 and 100.");

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                string guessString = Console.ReadLine();

                if (int.TryParse(guessString, out guess))
                {
                    guessCount++;

                    if (guess < magicNumber)
                    {
                        Console.WriteLine("Higher!");
                    }
                    else if (guess > magicNumber)
                    {
                        Console.WriteLine("Lower!");
                    }
                    else
                    {
                        Console.WriteLine($"You guessed it! The magic number was {magicNumber}.");
                        Console.WriteLine($"It took you {guessCount} guesses.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            Console.Write("Do you want to play again? (yes/no) ");
            string playAgainResponse = Console.ReadLine().ToLower();
            playAgain = (playAgainResponse == "yes"); // Check for "yes"
            Console.WriteLine();
        }

        Console.WriteLine("Thanks for playing!");
    }
}