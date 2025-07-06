using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int numberInput = -1;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (numberInput != 0)
        {
            Console.Write("Enter number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out numberInput))
            {
                if (numberInput != 0)
                {
                    numbers.Add(numberInput);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        int sum = numbers.Sum();
        Console.WriteLine($"The sum is: {sum}");

        if (numbers.Count > 0)
        {
            double average = numbers.Average();
            Console.WriteLine($"The average is: {average}");

            int maxNumber = numbers.Max();
            Console.WriteLine($"The largest number is: {maxNumber}");
        }
        else
        {
            Console.WriteLine("The list is empty, cannot calculate average or find the largest number.");
        }

        List<int> positiveNumbers = numbers.Where(n => n > 0).ToList();
        if (positiveNumbers.Count > 0)
        {
            int smallestPositive = positiveNumbers.Min();
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("No positive numbers in the list.");
        }

        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}