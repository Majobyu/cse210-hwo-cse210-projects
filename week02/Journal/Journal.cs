using System;
using System.Collections.Generic;
using System.IO;
// using System.Linq; // No es estrictamente necesario para las operaciones actuales en LoadFromFile

public class Journal
{
    private List<Entry> _entries;
    private List<string> _prompts; // Lista de indicaciones

    public Journal()
    {
        _entries = new List<Entry>();
        _prompts = new List<string>
        {
            "¿Quién fue la persona más interesante con la que interactué hoy?",
            "¿Cuál fue la mejor parte de mi día?",
            "¿Cómo vi la mano del Señor en mi vida hoy?",
            "¿Cuál fue la emoción más fuerte que sentí hoy?",
            "Si tuviera que hacer una cosa hoy, ¿qué sería?",
            "¿Qué aprendí hoy?", // Indicación adicional
            "¿Por qué estoy agradecido hoy?" // Indicación adicional
        };
    }

    public void AddNewEntry()
    {
        Random random = new Random();
        string randomPrompt = _prompts[random.Next(_prompts.Count)];

        Console.WriteLine($"\nIndicación: {randomPrompt}");
        Console.Write("Tu respuesta: ");
        string userResponse = Console.ReadLine();
        string currentDate = DateTime.Now.ToShortDateString(); // Formato de fecha simple

        Entry newEntry = new Entry(randomPrompt, userResponse, currentDate);
        _entries.Add(newEntry);
        Console.WriteLine("Entrada guardada con éxito.");
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nEl diario está vacío. No hay entradas para mostrar.");
            return;
        }

        Console.WriteLine("\n--- DIARIO COMPLETO ---");
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
        Console.WriteLine("--- FIN DEL DIARIO ---\n");
    }

    public void SaveToFile()
    {
        Console.Write("Ingresa el nombre del archivo para guardar (ej. mi_diario.txt): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                // Usamos '|~|' como separador para evitar conflictos con comas.
                foreach (Entry entry in _entries)
                {
                    outputFile.WriteLine($"{entry.Date}|~|{entry.Prompt}|~|{entry.Response}");
                }
            }
            Console.WriteLine($"Diario guardado en '{filename}' con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        Console.Write("Ingresa el nombre del archivo para cargar (ej. mi_diario.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine($"Error: El archivo '{filename}' no existe.");
            return;
        }

        try
        {
            _entries.Clear(); // Borra las entradas actuales antes de cargar nuevas
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                // Se usa el método Split con el separador como string directamente para mayor concisión.
                string[] parts = line.Split("|~|", StringSplitOptions.None);
                if (parts.Length == 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];
                    _entries.Add(new Entry(prompt, response, date));
                }
                else
                {
                    Console.WriteLine($"Advertencia: Línea con formato incorrecto omitida: {line}");
                }
            }
            Console.WriteLine($"Diario cargado desde '{filename}' con éxito. {_entries.Count} entradas cargadas.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
        }
    }
}
