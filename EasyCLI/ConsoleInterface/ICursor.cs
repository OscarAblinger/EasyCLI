namespace EasyCli.ConsoleInterface
{
    public interface ICursor
    {
        int CursorLeft { get; set; }
        int CursorTop { get; set; }
        int CursorSize { get; set; }
        int CursorVisible { get; set; }
    }
}