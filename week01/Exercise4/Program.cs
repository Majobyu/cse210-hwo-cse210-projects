using System;
using System.Collections.Generic;
using System.Linq; // Necesario para .Average(), .Max(), .Min(), .Where(), .OrderBy()

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

        // Requisitos Básicos (repetidos para mantener el contexto)
        int sum = numbers.Sum(); // Usamos .Sum() de LINQ para mayor brevedad
        Console.WriteLine($"The sum is: {sum}");

        if (numbers.Count > 0)
        {
            double average = numbers.Average(); // Usamos .Average() de LINQ
            Console.WriteLine($"The average is: {average}");

            int maxNumber = numbers.Max(); // Usamos .Max() de LINQ
            Console.WriteLine($"The largest number is: {maxNumber}");
        }
        else
        {
            Console.WriteLine("The list is empty, cannot calculate average or find the largest number.");
        }

        // Desafío de Estiramiento

        // 1. Encontrar el número positivo más pequeño
        // Filtramos solo los números positivos y luego encontramos el mínimo entre ellos
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

        // 2. Ordenar la lista y mostrarla
        numbers.Sort(); // Ordena la lista en su lugar
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}