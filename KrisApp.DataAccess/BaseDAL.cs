namespace KrisApp.DataAccess
{
    public abstract class BaseDAL
    {
        protected string csKris;

        public BaseDAL(string cs)
        {
            csKris = cs;
        }
    }
}