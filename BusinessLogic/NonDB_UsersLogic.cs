using System;
using System.Data;

namespace BusinessLogic
{
    public class NonDB_UsersLogic : BaseLogic
    {
        public DataTable dtUsers;
        public int id;

        public NonDB_UsersLogic()
        {
            dtUsers = new DataTable();

            dtUsers.Columns.Add("UserId");
            dtUsers.Columns.Add("UserName");
            dtUsers.Columns.Add("LoginName");
            dtUsers.Columns.Add("LastConnectionDate");
            dtUsers.Columns.Add("IsConnected");

            //Add blank row
            DataRow row = dtUsers.NewRow();
            dtUsers.Rows.InsertAt(row, 0);
            id = 1;
            object[] o1 = { id++, "Ilan Shchori", "ilanSh7", DateTime.Now, false };
            dtUsers.Rows.Add(o1);
            object[] o2 = { id++, "Tal", "Mushinka", DateTime.Now, false };
            dtUsers.Rows.Add(o2);
            object[] o3 = { id++, "Yuval", "Bubichka", DateTime.Now, false };
            dtUsers.Rows.Add(o3);
            object[] o4 = { id++, "פלוני אלמוני", "JohnDow", DateTime.Now, false };
            dtUsers.Rows.Add(o4);
            object[] o5 = { id++, "משה אופניק", "Ufnik", DateTime.Now, false };
            dtUsers.Rows.Add(o5);

        }

        public DataTable GetAllUsers()
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "SELECT UserId, LoginName FROM Users order by LoginName";
            //DataTable dt = Dal.GetTable(command);

            //// insert Empty row at the top
            //DataRow row = dt.NewRow();
            ////row[0] = "";
            ////row[1] = "";
            //dt.Rows.InsertAt(row, 0);
            //return dt;
            DataTable dt = dtUsers.Clone();
            dt.Columns.Remove("UserName");
            dt.Columns.Remove("LastConnectionDate");
            dt.Columns.Remove("IsConnected");
            return dt;
        }

        public string GetUser(string loginName, int isConnected)
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "SELECT UserId FROM Users where LoginName = @loginName and IsConnected = @isConnected";
            //command.Parameters.AddWithValue("@loginName", loginName);
            //command.Parameters.AddWithValue("@isConnected", isConnected);
            //return Dal.ExecuteScalar(command);
            foreach (DataRow row in dtUsers.Rows)
            {
                if (row["LoginName"].ToString() == loginName && int.Parse(row["IsConnected"].ToString()) == isConnected)
                    return row["UserId"].ToString();
            }
            return "";
        }

        public DataTable GetConnectedUsers()
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "SELECT IsConnected, UserId, UserName, LoginName, LastConnectionDate FROM Users";// where IsConnected = 1";
            //DataTable dt = Dal.GetTable(command);
            //// insert Empty row at the top
            ////DataRow row = dt.NewRow();
            ////dt.Rows.InsertAt(row, 0);
            //return dt;
            return dtUsers;
        }

        public DataTable GetUsersList(bool fullList)
        {
            //SqlCommand command = new SqlCommand();
            //if (fullList)
            //    command.CommandText = "SELECT UserId, LoginName FROM Users order by UserName";
            //else
            //    command.CommandText = "SELECT UserId, LoginName FROM Users where IsConnected = 1 order by UserName";
            //DataTable dt = Dal.GetTable(command);
            //// insert Empty row at the top
            //DataRow row = dt.NewRow();
            //dt.Rows.InsertAt(row, 0);
            //return dt;
            DataTable dt = dtUsers.Clone();
            dt.Columns.Remove("UserName");
            dt.Columns.Remove("LastConnectionDate");
            dt.Columns.Remove("IsConnected");
            if (fullList)
                return dt;
            else
            {
                DataTable dt1 = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["IsConnected"].ToString() == "1")
                        dt1.Rows[i].Delete();
                }
                return dt1;
            }
        }

        public void InsertUser(string userName, string loginName)
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "INSERT INTO Users(UserName, LoginName) VALUES(@userName, @loginName)";
            //command.Parameters.AddWithValue("@userName", userName);
            //command.Parameters.AddWithValue("@loginName", loginName);
            //Dal.ExecuteInsertUpdateDelete(command);
            DataRow row = dtUsers.NewRow();
            object[] o = { id++, userName, loginName, DateTime.MinValue, 0 };
            dtUsers.Rows.Add(o);
        }

        public void WriteConnection(int userId, bool isActive)
        {
            //SqlCommand command = new SqlCommand();
            //string ConnectStr = "UPDATE Users SET LastConnectionDate = GETDATE(), IsConnected = 1 where UserId = @UserId";
            //string DisConnectStr = "UPDATE Users SET IsConnected = 0 where UserId = @UserId";
            //command.CommandText = (isActive) ? ConnectStr : DisConnectStr;
            //command.Parameters.AddWithValue("@UserId", userId);
            //Dal.ExecuteInsertUpdateDelete(command);
            foreach (DataRow row in dtUsers.Rows)
            {
                if (row["UserId"].ToString() == userId.ToString())
                    if (isActive)
                    {
                        row["LastConnectionDate"] = DateTime.Now;
                        row["IsConnected"] = 1;
                    }
                    else
                        row["IsConnected"] = 0;
            }
        }

        public void DeleteUser(int userId)
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "DELETE FROM Users where UserId = @UserId";
            //command.Parameters.AddWithValue("@UserId", userId);
            //Dal.ExecuteInsertUpdateDelete(command);
            int index = -1;
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if (dtUsers.Rows[i]["UserId"].ToString() == userId.ToString())
                    index = i;
            }
            if (index >= 0)
                dtUsers.Rows[index].Delete();
        }

        public void ResetConnection()
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "UPDATE Users SET IsConnected = 0";
            //Dal.ExecuteInsertUpdateDelete(command);
            foreach (DataRow row in dtUsers.Rows)
            {
                row["IsConnected"] = 0;
            }
        }

    }
}
