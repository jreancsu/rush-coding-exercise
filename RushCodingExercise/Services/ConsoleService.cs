using RushCodingExercise.Interfaces;

namespace RushCodingExercise.Services
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }

        public string ReadLine()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }
    }
}
