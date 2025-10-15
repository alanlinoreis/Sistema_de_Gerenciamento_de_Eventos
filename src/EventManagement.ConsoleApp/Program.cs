using System;
using EventManagement.Domain.Entities;

namespace EventManagement.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            PrintHeader("SISTEMA DE GERENCIAMENTO DE EVENTOS");

            RunSpeakerExamples();
            RunVenueExamples();
            RunEventExamples();
            RunErrorExamples();

            Console.WriteLine("\n✅ Demonstração concluída com sucesso.\n");
        }

        // Exibe cabeçalho visual
        private static void PrintHeader(string title)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine($"  {title}");
            Console.WriteLine("==============================================\n");
        }

        private static void RunSpeakerExamples()
        {
            Console.WriteLine("🧑‍🏫 PALESTRANTES");

            var speaker1 = new Speaker(1, "João Silva", "joao@email.com");
            speaker1.SetBiography("Especialista em C# e .NET");
            speaker1.Company = "Tech Corp";
            Console.WriteLine($"• {speaker1}");

            var speaker2 = new Speaker(2, "Maria Souza", "maria@dev.com");
            speaker2.SetBiography(null);
            Console.WriteLine($"• {speaker2}\n");
        }

        private static void RunVenueExamples()
        {
            Console.WriteLine("🏛️ LOCAIS");

            var venue = new Venue(1, "Centro de Convenções", "Av. Principal, 100", 500);
            venue.SetDescription("Espaço moderno com ar-condicionado e som ambiente");
            Console.WriteLine($"• {venue}");
            Console.WriteLine($"• Local padrão: {Venue.Default}\n");
        }

        private static void RunEventExamples()
        {
            Console.WriteLine("🎟️ EVENTOS");

            var evento = new Event(1, ".NET Conference 2025", new DateTime(2025, 12, 15), TimeSpan.FromHours(8));
            evento.SetEventCode("NETCONF2025");
            evento.SetDescription("Conferência anual sobre tecnologias .NET");
            evento.AssignMainSpeaker(new Speaker(1, "João Silva", "joao@email.com"));

            Console.WriteLine($"• {evento}");
            Console.WriteLine($"  Local: {evento.Venue}");
            Console.WriteLine($"  Palestrante: {evento.MainSpeaker?.FullName ?? "A definir"}\n");
        }

        private static void RunErrorExamples()
        {
            Console.WriteLine("⚠️ TESTES DE ERRO");

            Try(() => new Speaker(0, "Fulano", "email@email.com"), "SpeakerId inválido");
            Try(() => new Speaker(3, "Ana", "sem-arroba"), "E-mail inválido");
            Try(() => new Event(2, "Evento Antigo", DateTime.Now.AddDays(-1), TimeSpan.FromHours(1)), "Data no passado");
            Try(() => new Event(3, "Evento Curto", DateTime.Now.AddDays(2), TimeSpan.FromMinutes(10)), "Duração menor que 30 minutos");

            Console.WriteLine();
        }

        // Método auxiliar para exibir erros formatados
        private static void Try(Action action, string description)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"• {description}: {ex.Message}");
            }
        }
    }
}
