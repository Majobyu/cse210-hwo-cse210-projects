// Entry.cs
public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public void Display()
    {
        Console.WriteLine($"Fecha: {Date}");
        Console.WriteLine($"Indicación: {Prompt}");
        Console.WriteLine($"Respuesta: {Response}");
        Console.WriteLine("--------------------");
    }
}