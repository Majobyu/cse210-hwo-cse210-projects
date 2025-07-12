// Program.cs
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("¡Bienvenido al Programa de Diario!");
        Journal myJournal = new Journal();
        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine("\n--- MENÚ DEL DIARIO ---");
            Console.WriteLine("1. Escribir una nueva entrada");
            Console.WriteLine("2. Mostrar el diario");
            Console.WriteLine("3. Guardar el diario en un archivo");
            Console.WriteLine("4. Cargar el diario desde un archivo");
            Console.WriteLine("5. Salir");
            Console.Write("Elige una opción: ");

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    myJournal.AddNewEntry();
                    break;
                case "2":
                    myJournal.DisplayJournal();
                    break;
                case "3":
                    myJournal.SaveToFile();
                    break;
                case "4":
                    myJournal.LoadFromFile();
                    break;
                case "5":
                    Console.WriteLine("¡Gracias por usar el programa de diario! ¡Hasta pronto!");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Por favor, elige un número del 1 al 5.");
                    break;
            }
        }
    }
}

/*
* Superando los requisitos:
* 1. Se agregaron dos indicaciones adicionales a la lista para mayor variedad:
* - "¿Qué aprendí hoy?"
* - "¿Por qué estoy agradecido hoy?"
* Esto ayuda a diversificar las reflexiones diarias y puede fomentar una mayor creatividad en las respuestas.
* 2. El separador para guardar/cargar archivos se ha definido explícitamente como "|~|"
* para minimizar el riesgo de colisiones con el contenido del usuario, aunque no maneja
* escapes de caracteres complejos como un CSV estándar. Este es un paso simple
* hacia una mayor robustez en el almacenamiento.
*/