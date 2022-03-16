using System.Configuration;

using DataAccessLayer;

namespace BusinessLogic
{
    public abstract class BaseLogic
    {
        protected DAL Dal;

        protected BaseLogic()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ChatDB"].ConnectionString;
            Dal = new DAL(connectionString);
        }
    }
}
