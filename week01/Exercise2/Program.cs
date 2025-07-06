using System;

class GradeCalculator
{
    static void Main(string[] args)
    {
        Console.Write("Por favor, introduce tu porcentaje de calificación: ");
        string userInput = Console.ReadLine();
        int gradePercentage;

        // Validar que la entrada sea un número entero
        if (!int.TryParse(userInput, out gradePercentage))
        {
            Console.WriteLine("Entrada inválida. Por favor, introduce un número entero para el porcentaje.");
            return; // Sale del programa si la entrada no es válida
        }

        string letter = "";
        string sign = "";

        // Determinar la letra de la calificación
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

        // Lógica para determinar el signo (+ o -), manejando los casos especiales
        if (letter != "F") // Las calificaciones F no tienen +/-
        {
            int lastDigit = gradePercentage % 10;

            if (lastDigit >= 7)
            {
                if (letter == "A")
                {
                    // No hay A+, así que no se añade ningún signo
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
            // Si el último dígito está entre 3 y 6, no se añade ningún signo, por lo que 'sign' permanece vacío.
        }
        
        // Imprimir la calificación final
        Console.WriteLine($"Tu calificación es: {letter}{sign}");

        // Determinar si el usuario aprobó o reprobó el curso
        if (gradePercentage >= 70)
        {
            Console.WriteLine("¡Felicidades! Aprobaste el curso.");
        }
        else
        {
            Console.WriteLine("¡No te desanimes! Sigue esforzándote para la próxima vez.");
        }
    }
}
