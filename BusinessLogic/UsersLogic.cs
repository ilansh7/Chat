using System.Data;
using System.Data.SqlClient;

namespace BusinessLogic
{
    public class UsersLogic : BaseLogic
    {
        public DataTable GetAllUsers()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT UserId, LoginName FROM Users order by LoginName";
            DataTable dt = Dal.GetTable(command);

            // insert Empty row at the top
            DataRow row = dt.NewRow();
            //row[0] = "";
            //row[1] = "";
            dt.Rows.InsertAt(row, 0);
            return dt;
        }

        public string GetUser(string loginName, int IsConnected)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT UserId FROM Users where LoginName = @loginName and IsConnected = @IsConnected";
            command.Parameters.AddWithValue("@loginName", loginName);
            command.Parameters.AddWithValue("@IsConnected", IsConnected);
            return Dal.ExecuteScalar(command);
        }

        public DataTable GetUsersData(int id, string login)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "GetUsers";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", id);
            command.Parameters.AddWithValue("@userLogin", login);

            DataTable dt = Dal.GetTable(command);

            return dt;
        }

        public DataTable GetConnectedUsers()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT IsConnected, UserId, UserName, LoginName, LastConnectionDate FROM Users order by LastConnectionDate asc";// where IsConnected = 1";
            DataTable dt = Dal.GetTable(command);
            // insert Empty row at the top
            //DataRow row = dt.NewRow();
            //dt.Rows.InsertAt(row, 0);
            return dt;
        }

        public DataTable GetUsersList(bool fullList)
        {
            SqlCommand command = new SqlCommand();
            if (fullList)
                command.CommandText = "SELECT UserId, LoginName FROM Users order by UserName";
            else
                command.CommandText = "SELECT UserId, LoginName FROM Users where IsConnected = 1 order by UserName";
            DataTable dt = Dal.GetTable(command);
            // insert Empty row at the top
            DataRow row = dt.NewRow();
            dt.Rows.InsertAt(row, 0);
            return dt;
        }

        public void InsertUser(string userName, string loginName)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO Users(UserName, LoginName) VALUES(@userName, @loginName)";
            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@loginName", loginName);
            Dal.ExecuteInsertUpdateDelete(command);
        }

        public void WriteConnection(int userId, bool isActive)
        {
            SqlCommand command = new SqlCommand();
            string ConnectStr = "UPDATE Users SET LastConnectionDate = GETDATE(), IsConnected = 1 where UserId = @UserId";
            string DisConnectStr = "UPDATE Users SET IsConnected = 0 where UserId = @UserId";
            command.CommandText = (isActive) ? ConnectStr : DisConnectStr;
            command.Parameters.AddWithValue("@UserId", userId);
            Dal.ExecuteInsertUpdateDelete(command);
        }

        public void DeleteUser(int userId)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM Users where UserId = @UserId";
            command.Parameters.AddWithValue("@UserId", userId);
            Dal.ExecuteInsertUpdateDelete(command);
        }

        public void ResetConnection()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE Users SET IsConnected = 0";
            Dal.ExecuteInsertUpdateDelete(command);
        }

    }
}
