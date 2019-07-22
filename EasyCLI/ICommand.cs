namespace EasyCli
{
    public delegate ICommandResult CommandMethod(ICli cli, IArgumentsInfo argInfo);

    public interface ICommand
    {
        string[] Names { get; }
        CommandMethod Method { get; }
        string[] Description { get; }
    }
}
