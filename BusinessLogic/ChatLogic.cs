using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogic
{
    public class ChatLogic : BaseLogic
    {
        //      public void InsertMessage(string loginName, string message, string receiver)
        //      {
        ////          ]
        ////    ,[]
        ////    ,[]
        ////    ,[]
        ////FROM [Chat].[dbo].[]
        //          SqlCommand command = new SqlCommand();
        //          UsersLogic ul = new UsersLogic();
        //          string UserId = ul.GetUser(loginName, 1);
        //          command.CommandText = "INSERT INTO Messages(UserId, SentDate, RecepientId, MessageText) VALUES(@userId, GETDATE(), @messageText)";
        //          command.Parameters.AddWithValue("@userName", userName);
        //          command.Parameters.AddWithValue("@messageText", receiver);
        //          Dal.ExecuteInsertUpdateDelete(command);
        //      }

        public string InsertMessage(string loginName, string message, string receiver)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "InsertMessage";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@loginName", loginName);
            command.Parameters.AddWithValue("@receiver", receiver);
            command.Parameters.AddWithValue("@message", message);

            return Dal.ExecuteStoredProcedure(command);
        }

        public DataTable GetMessages(int sender, int receiver, DateTime fromDate, DateTime toDate, string keywords)
        {
            SqlCommand command = new SqlCommand();
            //command.CommandText = "SELECT MessageId, UserId, MessageText, SentDate, RecepientId FROM Messages";
            command.CommandText = "GetMessages";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@sender", sender);
            command.Parameters.AddWithValue("@receiver", receiver);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            command.Parameters.AddWithValue("@keywords", keywords);

            DataTable dt = Dal.GetTable(command);

            //// insert Empty row at the top
            //DataRow row = dt.NewRow();
            ////row[0] = "";
            ////row[1] = "";
            //dt.Rows.InsertAt(row, 0);
            return dt;
        }


    }
}
