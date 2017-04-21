namespace KrisApp.DataModel.Interfaces
{
    public interface ILogger
    {
        void Error(string format, params object[] args);

        void Debug(string format, params object[] args);
    }
}
