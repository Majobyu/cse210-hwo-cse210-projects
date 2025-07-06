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
            // Generar un número mágico aleatorio entre 1 y 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101); // 1 a 100

            int guess = -1;
            int guessCount = 0;

            Console.WriteLine("Bienvenido al juego 'Adivina mi Número'!");
            Console.WriteLine("Estoy pensando en un número entre 1 y 100.");

            while (guess != magicNumber)
            {
                Console.Write("¿Cuál es tu suposición? ");
                string guessString = Console.ReadLine();

                // Intentar convertir la entrada del usuario a un entero
                if (int.TryParse(guessString, out guess))
                {
                    guessCount++; // Incrementar el contador de suposiciones

                    if (guess < magicNumber)
                    {
                        Console.WriteLine("¡Más alto!");
                    }
                    else if (guess > magicNumber)
                    {
                        Console.WriteLine("¡Más bajo!");
                    }
                    else
                    {
                        Console.WriteLine($"¡Lo adivinaste! El número mágico era {magicNumber}.");
                        Console.WriteLine($"Te tomó {guessCount} suposiciones.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, introduce un número.");
                }
            }

            Console.Write("¿Quieres jugar de nuevo? (sí/no) ");
            string playAgainResponse = Console.ReadLine().ToLower();
            playAgain = (playAgainResponse == "sí" || playAgainResponse == "si");
            Console.WriteLine(); // Añadir una línea en blanco para mayor claridad
        }

        Console.WriteLine("¡Gracias por jugar!");
    }
}