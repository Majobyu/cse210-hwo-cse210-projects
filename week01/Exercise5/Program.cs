using System;

public class Exercise5
{
    public static void Main(string[] args)
    {
        // Llama a la función DisplayWelcome
        DisplayWelcome();

        // Llama a PromptUserName y guarda el nombre del usuario
        string userName = PromptUserName();

        // Llama a PromptUserNumber y guarda el número favorito del usuario
        int userNumber = PromptUserNumber();

        // Llama a SquareNumber y guarda el número al cuadrado
        int squaredNumber = SquareNumber(userNumber);

        // Llama a DisplayResult para mostrar el nombre del usuario y el número al cuadrado
        DisplayResult(userName, squaredNumber);
    }

    /// <summary>
    /// Muestra el mensaje de bienvenida al programa.
    /// </summary>
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    /// <summary>
    /// Solicita al usuario que ingrese su nombre y lo devuelve.
    /// </summary>
    /// <returns>El nombre del usuario como una cadena.</returns>
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    /// <summary>
    /// Solicita al usuario que ingrese su número favorito y lo devuelve.
    /// </summary>
    /// <returns>El número favorito del usuario como un entero.</returns>
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        // Usamos int.Parse para convertir la entrada de cadena a un entero.
        // En una aplicación de producción, sería recomendable agregar manejo de errores
        // para entradas no válidas (por ejemplo, TryParse).
        return int.Parse(Console.ReadLine());
    }

    /// <summary>
    /// Acepta un número entero y devuelve ese número al cuadrado.
    /// </summary>
    /// <param name="number">El número entero a cuadrar.</param>
    /// <returns>El número al cuadrado como un entero.</returns>
    static int SquareNumber(int number)
    {
        return number * number;
    }

    /// <summary>
    /// Muestra el nombre del usuario y el número al cuadrado.
    /// </summary>
    /// <param name="userName">El nombre del usuario.</param>
    /// <param name="squaredNumber">El número al cuadrado.</param>
    static void DisplayResult(string userName, int squaredNumber)
    {
        Console.WriteLine($"{userName}, the square of your number is {squaredNumber}");
    }
}