namespace RushCodingExercise.Interfaces
{
    public interface IConsoleService
    {
        string ReadLine();
        ConsoleKeyInfo ReadKey();
        void WriteLine(string input);
    }
}