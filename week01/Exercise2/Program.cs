using System;

class GradeCalculator
{
    static void Main(string[] args)
    {
        // Prompt the user to enter their grade percentage
        Console.Write("Please enter your grade percentage: ");
        string userInput = Console.ReadLine();
        int gradePercentage;

        // Validate that the input is an integer
        if (!int.TryParse(userInput, out gradePercentage))
        {
            Console.WriteLine("Invalid input. Please enter an integer for the percentage.");
            return; // Exit the program if the input is not valid
        }

        string letter = ""; // Variable to store the letter grade
        string sign = "";   // Variable to store the sign (+ or -)

        // Determine the letter grade
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Logic to determine the sign (+ or -), handling special cases
        if (letter != "F") // F grades do not have +/-
        {
            int lastDigit = gradePercentage % 10; // Get the last digit of the percentage

            if (lastDigit >= 7)
            {
                if (letter == "A")
                {
                    // There is no A+, so no sign is added
                    sign = ""; 
                }
                else
                {
                    sign = "+";
                }
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            // If the last digit is between 3 and 6, no sign is added, so 'sign' remains empty.
        }
        
        // Print the final grade
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Determine if the user passed or failed the course
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep striving for next time.");
        }
    }
}
