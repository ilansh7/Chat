using System;
using System.Data;

namespace BusinessLogic
{
    public class NonDB_ChatLogic : BaseLogic
    {
        //[MessageId] [int] IDENTITY(1,1) NOT NULL,
        //[MessageText] [nvarchar](500) NULL,
        //[UserId] [int] NULL,
        //[SentDate] [datetime] NULL,
        //[RecepientId] [int] NULL,

        public DataTable dtMessages;
        public int id;
        NonDB_UsersLogic user;

        public NonDB_ChatLogic()
        {
            user = new NonDB_UsersLogic();
            dtMessages = new DataTable();

            dtMessages.Columns.Add("MessageId");
            dtMessages.Columns.Add("MessageText");
            dtMessages.Columns.Add("UserId");
            dtMessages.Columns.Add("SentDate");
            dtMessages.Columns.Add("RecepientId");

            ////Add blank row
            //DataRow row = dtMessages.NewRow();
            //dtMessages.Rows.InsertAt(row, 0);
            id = 1;
            object[] o1 = { id++, "Ilan Shchori", 1, DateTime.Now, null };
            dtMessages.Rows.Add(o1);
            object[] o2 = { id++, "Tal", 1, DateTime.Now, null };
            dtMessages.Rows.Add(o2);

        }

        public string InsertMessage(string loginName, string message, string receiver)
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "InsertMessage";
            //command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.AddWithValue("@loginName", loginName);
            //command.Parameters.AddWithValue("@receiver", receiver);
            //command.Parameters.AddWithValue("@message", message);

            //return Dal.ExecuteStoredProcedure(command);
            string userReceiver = "";
            int userid = int.Parse(user.GetUser(loginName, 1));
            if (!string.IsNullOrEmpty(receiver))
                userReceiver = user.GetUser(receiver, 1);

            DataRow row = dtMessages.NewRow();
            object[] o = { id++, message, userid, DateTime.Now, int.Parse(userReceiver) };
            dtMessages.Rows.Add(o);
            return id.ToString();
        }

        public DataTable GetMessages(int sender, int receiver, DateTime fromDate, DateTime toDate, string keywords)
        {
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "GetMessages";
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@sender", sender);
            //command.Parameters.AddWithValue("@receiver", receiver);
            //command.Parameters.AddWithValue("@fromDate", fromDate);
            //command.Parameters.AddWithValue("@toDate", toDate);
            //command.Parameters.AddWithValue("@keywords", keywords);

            //DataTable dt = Dal.GetTable(command);

            ////// insert Empty row at the top
            ////DataRow row = dt.NewRow();
            //////row[0] = "";
            //////row[1] = "";
            ////dt.Rows.InsertAt(row, 0);
            //return dt;
            return dtMessages;
        }
    }
}
