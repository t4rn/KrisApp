namespace KrisApp.DataModel.Interfaces
{
    public interface ISessionService
    {
        void AddToSession(SessionItem itemName, object value);

        T GetFromSession<T>(SessionItem itemName);

        void ClearSession();
    }

    public enum SessionItem
    {
        User,
        Uod
    }
}
