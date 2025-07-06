using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Muestra un mensaje de bienvenida.
        Console.WriteLine("Hello World! This is the Exercise1 Project.");
        Console.WriteLine(); // Agrega una línea en blanco para mejor legibilidad.

        // Solicita al usuario su primer nombre.
        Console.Write("What is your first name? ");
        // Lee la entrada del usuario y la guarda en la variable 'firstName'.
        string firstName = Console.ReadLine();

        // Solicita al usuario su apellido.
        Console.Write("What is your last name? ");
        // Lee la entrada del usuario y la guarda en la variable 'lastName'.
        string lastName = Console.ReadLine();

        // Muestra el nombre completo en el formato "Apellido, Nombre Apellido.".
        // Se utiliza la interpolación de cadenas ($"") para insertar las variables directamente en la cadena.
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}
