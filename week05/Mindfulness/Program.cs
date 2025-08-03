using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MindfulnessApp
{
    // Clase base abstracta para manejar comportamiento común
    abstract class ActividadBase
    {
        private int duracion;
        private string nombreActividad;
        private string descripcion;

        protected ActividadBase(string nombre, string descripcion)
        {
            this.nombreActividad = nombre;
            this.descripcion = descripcion;
        }

        // Método para iniciar la actividad
        public void Ejecutar()
        {
            Console.Clear();
            PedirDuracion();
            MostrarMensajeInicio();
            EjecutarActividad();
            MostrarMensajeFinal();
        }

        // Método para pedir duración con validación repetitiva
        private void PedirDuracion()
        {
            int segundos = 0;
            bool valido = false;
            do
            {
                Console.Write("¿Cuántos segundos durará la sesión? ");
                string input = Console.ReadLine();
                valido = int.TryParse(input, out segundos) && segundos > 0;
                if (!valido)
                {
                    Console.WriteLine("Por favor, ingresa un número entero positivo.");
                }
            } while (!valido);
            duracion = segundos;
        }

        protected int GetDuracion() => duracion;
        protected string GetNombre() => nombreActividad;
        protected string GetDescripcion() => descripcion;

        // Mensaje de inicio común con animación de cuenta regresiva
        protected void MostrarMensajeInicio()
        {
            Console.Clear();
            Console.WriteLine($"Actividad: {nombreActividad}");
            Console.WriteLine(descripcion);
            Console.WriteLine($"Duración: {duracion} segundos");
            Console.WriteLine("Prepárate para comenzar...");
            AnimacionCuentaRegresiva(3);
        }

        // Mensaje final común con animación de cuenta regresiva
        protected void MostrarMensajeFinal()
        {
            Console.WriteLine("\n¡Buen trabajo! Has completado la actividad.");
            Console.WriteLine($"Actividad completada: {nombreActividad}");
            Console.WriteLine($"Tiempo transcurrido: {duracion} segundos.");
            AnimacionCuentaRegresiva(3);
        }

        // Animación simple de cuenta regresiva en segundos
        protected void AnimacionCuentaRegresiva(int segundos)
        {
            for (int i = segundos; i > 0; i--)
            {
                Console.Write($"\rComenzando en {i} segundos... ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Animación de ruleta giratoria para pausas reflexivas
        protected void AnimacionRuleta(int duracionSegs)
        {
            char[] simbolos = { '|', '/', '-', '\\' };
            int index = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < duracionSegs * 1000)
            {
                Console.Write($"\r{simbolos[index++ % simbolos.Length]} Reflexionando... ");
                Thread.Sleep(200);
            }
            Console.WriteLine();
        }

        // Animación tipo temporizador para pausas en cuenta regresiva
        protected void AnimacionTemporizador(int duracionSegs)
        {
            for (int i = duracionSegs; i > 0; i--)
            {
                Console.Write($"\rTiempo: {i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Método abstracto para la lógica específica de cada actividad
        protected abstract void EjecutarActividad();
    }

    // Actividad de Respiración
    class ActividadRespiracion : ActividadBase
    {
        public ActividadRespiracion()
            : base("Respiración",
                  "Esta actividad te ayudará a relajarte caminando mientras inhalas y exhalas lentamente. Despeja tu mente y concéntrate en tu respiración.")
        {
        }

        protected override void EjecutarActividad()
        {
            int duracion = GetDuracion();
            int tiempoTranscurrido = 0;
            bool inhalar = true;

            Console.WriteLine("\n¡Comienza la sesión!\n");

            // Usamos ciclos de inhalar y exhalar con pausas contando segundos
            while (tiempoTranscurrido < duracion)
            {
                if (inhalar)
                    Console.WriteLine("Inhala...");
                else
                    Console.WriteLine("Exhala...");
                inhalar = !inhalar;

                int pausa = 4; // duración de cada respiración/exhalación en segundos

                for (int i = pausa; i > 0; i--)
                {
                    Console.Write($"\rTiempo: {i} ");
                    Thread.Sleep(1000);
                    tiempoTranscurrido++;
                    if (tiempoTranscurrido >= duracion)
                        break;
                }
                Console.WriteLine();
            }
        }
    }

    // Actividad de Reflexión
    class ActividadReflexion : ActividadBase
    {
        private List<string> indicaciones = new List<string>
        {
            "Piensa en una ocasión en la que defendiste a otra persona.",
            "Piensa en una ocasión en la que hiciste algo realmente difícil.",
            "Piensa en una ocasión en la que ayudaste a alguien necesitado.",
            "Piensa en una ocasión en la que hiciste algo verdaderamente desinteresado."
        };

        private List<string> preguntas = new List<string>
        {
            "¿Por qué fue significativa esta experiencia para usted?",
            "¿Alguna vez has hecho algo parecido?",
            "¿Cómo empezaste?",
            "¿Cómo te sentiste cuando lo terminaste?",
            "¿Qué hizo que esta vez fuera diferente a otras cuando no tuviste tanto éxito?",
            "¿Qué es lo que más te gusta de esta experiencia?",
            "¿Qué podrías aprender de esta experiencia que pueda aplicarse a otras situaciones?",
            "¿Qué aprendiste sobre ti mismo a través de esta experiencia?",
            "¿Cómo puedes mantener esta experiencia en la mente en el futuro?"
        };

        public ActividadReflexion() : base("Reflexión",
            "Esta actividad te ayudará a reflexionar sobre los momentos de tu vida en los que has demostrado fortaleza y resiliencia. Esto te ayudará a reconocer el poder que tienes y cómo puedes usarlo en otros aspectos de tu vida.")
        { }

        protected override void EjecutarActividad()
        {
            int duracion = GetDuracion();
            Stopwatch reloj = new Stopwatch();
            reloj.Start();

            Random r = new Random();

            // Mostrar indicación escogida al azar
            string indicacion = indicaciones[r.Next(indicaciones.Count)];
            Console.WriteLine("\n¡Comienza la reflexión!\n");
            Console.WriteLine(indicacion);
            Console.WriteLine();

            // Mostramos preguntas aleatorias y pausas con animación ruleta
            while (reloj.ElapsedMilliseconds < duracion * 1000)
            {
                string pregunta = preguntas[r.Next(preguntas.Count)];
                Console.WriteLine(pregunta + " (Reflexiona)");
                AnimacionRuleta(4); // pausa con animación ruleta (4 seg)
                if (reloj.ElapsedMilliseconds >= duracion * 1000)
                    break;
            }
        }
    }

    // Actividad de Listado
    class ActividadListado : ActividadBase
    {
        private List<string> indicaciones = new List<string>
        {
            "¿Quiénes son las personas que aprecias?",
            "¿Cuáles son tus fortalezas personales?",
            "¿A quiénes has ayudado esta semana?",
            "¿Cuándo has sentido el Espíritu Santo este mes?",
            "¿Quiénes son algunos de tus héroes personales?"
        };

        public ActividadListado() : base("Listado",
            "Esta actividad te ayudará a reflexionar sobre las cosas buenas de tu vida al hacerte enumerar tantas cosas como puedas en un área determinada.")
        { }

        protected override void EjecutarActividad()
        {
            int duracion = GetDuracion();
            Random r = new Random();

            // Elegir y mostrar indicación
            string indicacion = indicaciones[r.Next(indicaciones.Count)];
            Console.WriteLine("\nIndicador: " + indicacion);
            Console.WriteLine("Prepárate para pensar...");
            AnimacionCuentaRegresiva(3);

            Console.WriteLine("\nComienza a escribir tus respuestas. (Ingresa tantos elementos como puedas. Presiona Enter después de cada uno. Cuando quieras terminar, solo presiona Enter en blanco o espera que termine el tiempo.)");
            List<string> elementos = new List<string>();

            Stopwatch reloj = new Stopwatch();
            reloj.Start();

            while (reloj.ElapsedMilliseconds < duracion * 1000)
            {
                // Para no bloquear, preguntamos si hay línea disponible
                // En consola normal es difícil detectar entrada sin bloquear,
                // por eso se usa esta técnica básica: esperar una entrada con timeout limitado
                if (Console.KeyAvailable)
                {
                    string linea = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(linea))
                        break; // entró línea vacía, terminamos antes de tiempo
                    elementos.Add(linea.Trim());
                }
                else
                {
                    Thread.Sleep(100); // esperar para no usar CPU al 100%
                }
            }

            Console.WriteLine($"\n¡Tiempo terminado o finalizado! Cantidad de elementos escritos: {elementos.Count}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("Bienvenido a la aplicación de Mindfulness.");
                Console.WriteLine("Seleccione una actividad:");
                Console.WriteLine("1. Respiración");
                Console.WriteLine("2. Reflexión");
                Console.WriteLine("3. Listado");
                Console.WriteLine("4. Salir");
                Console.Write("Ingrese el número de su elección: ");

                string opcion = Console.ReadLine();

                ActividadBase actividad = null;

                switch (opcion)
                {
                    case "1":
                        actividad = new ActividadRespiracion();
                        break;
                    case "2":
                        actividad = new ActividadReflexion();
                        break;
                    case "3":
                        actividad = new ActividadListado();
                        break;
                    case "4":
                        salir = true;
                        Console.WriteLine("Gracias por usar la aplicación Mindfulness. ¡Hasta luego!");
                        Thread.Sleep(2000);
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intenta de nuevo.");
                        Thread.Sleep(2000);
                        break;
                }

                if (actividad != null)
                {
                    actividad.Ejecutar();
                    Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }
            }
        }
    }
}