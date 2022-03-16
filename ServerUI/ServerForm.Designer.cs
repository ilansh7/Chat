namespace ServerUI
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.tabServer = new System.Windows.Forms.TabControl();
            this.tabCurrUser = new System.Windows.Forms.TabPage();
            this.lvConnections = new System.Windows.Forms.ListView();
            this.NickName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.lvHistory = new System.Windows.Forms.ListView();
            this.HistoryLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabUsersList = new System.Windows.Forms.TabPage();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.txtSelectedUserName = new System.Windows.Forms.TextBox();
            this.txtSelectedUserId = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lvUsersList = new System.Windows.Forms.ListView();
            this.IsActive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.User_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LoginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastConnection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.btnSearchMsg = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbReceiver = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSender = new System.Windows.Forms.ComboBox();
            this.gvMessages = new System.Windows.Forms.DataGridView();
            this.lblUserLogin = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtUserLogin = new System.Windows.Forms.TextBox();
            this.tabServer.SuspendLayout();
            this.tabCurrUser.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.tabUsersList.SuspendLayout();
            this.tabMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.tabCurrUser);
            this.tabServer.Controls.Add(this.tabHistory);
            this.tabServer.Controls.Add(this.tabUsersList);
            this.tabServer.Controls.Add(this.tabMessages);
            this.tabServer.Location = new System.Drawing.Point(12, 12);
            this.tabServer.Name = "tabServer";
            this.tabServer.SelectedIndex = 0;
            this.tabServer.Size = new System.Drawing.Size(520, 439);
            this.tabServer.TabIndex = 1;
            // 
            // tabCurrUser
            // 
            this.tabCurrUser.Controls.Add(this.lvConnections);
            this.tabCurrUser.Location = new System.Drawing.Point(4, 22);
            this.tabCurrUser.Name = "tabCurrUser";
            this.tabCurrUser.Padding = new System.Windows.Forms.Padding(3);
            this.tabCurrUser.Size = new System.Drawing.Size(512, 413);
            this.tabCurrUser.TabIndex = 0;
            this.tabCurrUser.Text = "Current User";
            this.tabCurrUser.UseVisualStyleBackColor = true;
            // 
            // lvConnections
            // 
            this.lvConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NickName,
            this.Status,
            this.Time});
            this.lvConnections.Font = new System.Drawing.Font("Arial", 10F);
            this.lvConnections.Location = new System.Drawing.Point(15, 16);
            this.lvConnections.Name = "lvConnections";
            this.lvConnections.Size = new System.Drawing.Size(451, 369);
            this.lvConnections.TabIndex = 0;
            this.lvConnections.UseCompatibleStateImageBehavior = false;
            this.lvConnections.View = System.Windows.Forms.View.Details;
            // 
            // NickName
            // 
            this.NickName.Text = "Nick Name";
            this.NickName.Width = 100;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 110;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Time.Width = 200;
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.lvHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(512, 413);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // lvHistory
            // 
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HistoryLine});
            this.lvHistory.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lvHistory.Location = new System.Drawing.Point(14, 15);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(472, 375);
            this.lvHistory.TabIndex = 0;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            // 
            // HistoryLine
            // 
            this.HistoryLine.Text = "History";
            this.HistoryLine.Width = 450;
            // 
            // tabUsersList
            // 
            this.tabUsersList.Controls.Add(this.txtUserLogin);
            this.tabUsersList.Controls.Add(this.txtUserId);
            this.tabUsersList.Controls.Add(this.lblUserId);
            this.tabUsersList.Controls.Add(this.lblUserLogin);
            this.tabUsersList.Controls.Add(this.btnDeleteUser);
            this.tabUsersList.Controls.Add(this.txtSelectedUserName);
            this.tabUsersList.Controls.Add(this.txtSelectedUserId);
            this.tabUsersList.Controls.Add(this.btnRefresh);
            this.tabUsersList.Controls.Add(this.lvUsersList);
            this.tabUsersList.Location = new System.Drawing.Point(4, 22);
            this.tabUsersList.Name = "tabUsersList";
            this.tabUsersList.Size = new System.Drawing.Size(512, 413);
            this.tabUsersList.TabIndex = 2;
            this.tabUsersList.Text = "Users List";
            this.tabUsersList.UseVisualStyleBackColor = true;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(425, 3);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(71, 24);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // txtSelectedUserName
            // 
            this.txtSelectedUserName.Location = new System.Drawing.Point(391, 5);
            this.txtSelectedUserName.Name = "txtSelectedUserName";
            this.txtSelectedUserName.Size = new System.Drawing.Size(15, 20);
            this.txtSelectedUserName.TabIndex = 8;
            this.txtSelectedUserName.Visible = false;
            // 
            // txtSelectedUserId
            // 
            this.txtSelectedUserId.Location = new System.Drawing.Point(404, 5);
            this.txtSelectedUserId.Name = "txtSelectedUserId";
            this.txtSelectedUserId.Size = new System.Drawing.Size(15, 20);
            this.txtSelectedUserId.TabIndex = 7;
            this.txtSelectedUserId.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(330, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(55, 24);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Search";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lvUsersList
            // 
            this.lvUsersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IsActive,
            this.User_Id,
            this.UserName,
            this.LoginName,
            this.LastConnection});
            this.lvUsersList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lvUsersList.FullRowSelect = true;
            this.lvUsersList.Location = new System.Drawing.Point(20, 33);
            this.lvUsersList.Name = "lvUsersList";
            this.lvUsersList.Size = new System.Drawing.Size(476, 364);
            this.lvUsersList.TabIndex = 5;
            this.lvUsersList.UseCompatibleStateImageBehavior = false;
            this.lvUsersList.View = System.Windows.Forms.View.Details;
            this.lvUsersList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvUsersList_ColumnClick);
            this.lvUsersList.Click += new System.EventHandler(this.lvUsersList_Click);
            // 
            // IsActive
            // 
            this.IsActive.Text = "";
            this.IsActive.Width = 30;
            // 
            // User_Id
            // 
            this.User_Id.Text = "ID";
            this.User_Id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.User_Id.Width = 40;
            // 
            // UserName
            // 
            this.UserName.Text = "User Name";
            this.UserName.Width = 119;
            // 
            // LoginName
            // 
            this.LoginName.Text = "Login Name";
            this.LoginName.Width = 110;
            // 
            // LastConnection
            // 
            this.LastConnection.Text = "Last Connected";
            this.LastConnection.Width = 111;
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.btnSearchMsg);
            this.tabMessages.Controls.Add(this.label5);
            this.tabMessages.Controls.Add(this.txtKeywords);
            this.tabMessages.Controls.Add(this.label4);
            this.tabMessages.Controls.Add(this.label3);
            this.tabMessages.Controls.Add(this.dtFromDate);
            this.tabMessages.Controls.Add(this.dtToDate);
            this.tabMessages.Controls.Add(this.label2);
            this.tabMessages.Controls.Add(this.cbReceiver);
            this.tabMessages.Controls.Add(this.label1);
            this.tabMessages.Controls.Add(this.cbSender);
            this.tabMessages.Controls.Add(this.gvMessages);
            this.tabMessages.Location = new System.Drawing.Point(4, 22);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Size = new System.Drawing.Size(512, 413);
            this.tabMessages.TabIndex = 3;
            this.tabMessages.Text = "Messages";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // btnSearchMsg
            // 
            this.btnSearchMsg.Location = new System.Drawing.Point(421, 5);
            this.btnSearchMsg.Name = "btnSearchMsg";
            this.btnSearchMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSearchMsg.TabIndex = 24;
            this.btnSearchMsg.Text = "Search";
            this.btnSearchMsg.UseVisualStyleBackColor = true;
            this.btnSearchMsg.Click += new System.EventHandler(this.btnSearchMsg_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "KeyWords";
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(72, 60);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(322, 20);
            this.txtKeywords.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "From";
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFromDate.Location = new System.Drawing.Point(72, 34);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(110, 20);
            this.dtFromDate.TabIndex = 19;
            this.dtFromDate.Value = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtToDate.Location = new System.Drawing.Point(284, 34);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(110, 20);
            this.dtToDate.TabIndex = 18;
            this.dtToDate.Value = new System.DateTime(2015, 12, 31, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Receiver";
            // 
            // cbReceiver
            // 
            this.cbReceiver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReceiver.FormattingEnabled = true;
            this.cbReceiver.Location = new System.Drawing.Point(284, 7);
            this.cbReceiver.Name = "cbReceiver";
            this.cbReceiver.Size = new System.Drawing.Size(110, 21);
            this.cbReceiver.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Sender";
            // 
            // cbSender
            // 
            this.cbSender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSender.FormattingEnabled = true;
            this.cbSender.Location = new System.Drawing.Point(72, 7);
            this.cbSender.Name = "cbSender";
            this.cbSender.Size = new System.Drawing.Size(110, 21);
            this.cbSender.TabIndex = 14;
            // 
            // gvMessages
            // 
            this.gvMessages.AllowUserToAddRows = false;
            this.gvMessages.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMessages.Location = new System.Drawing.Point(13, 86);
            this.gvMessages.Name = "gvMessages";
            this.gvMessages.Size = new System.Drawing.Size(483, 324);
            this.gvMessages.TabIndex = 13;
            // 
            // lblUserLogin
            // 
            this.lblUserLogin.AutoSize = true;
            this.lblUserLogin.Location = new System.Drawing.Point(150, 9);
            this.lblUserLogin.Name = "lblUserLogin";
            this.lblUserLogin.Size = new System.Drawing.Size(58, 13);
            this.lblUserLogin.TabIndex = 18;
            this.lblUserLogin.Text = "User Login";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(20, 9);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(38, 13);
            this.lblUserId.TabIndex = 19;
            this.lblUserId.Text = "UserId";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(60, 5);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(64, 20);
            this.txtUserId.TabIndex = 23;
            // 
            // txtUserLogin
            // 
            this.txtUserLogin.Location = new System.Drawing.Point(210, 5);
            this.txtUserLogin.Name = "txtUserLogin";
            this.txtUserLogin.Size = new System.Drawing.Size(99, 20);
            this.txtUserLogin.TabIndex = 24;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 479);
            this.Controls.Add(this.tabServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.tabServer.ResumeLayout(false);
            this.tabCurrUser.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.tabUsersList.ResumeLayout(false);
            this.tabUsersList.PerformLayout();
            this.tabMessages.ResumeLayout(false);
            this.tabMessages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMessages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabServer;
        private System.Windows.Forms.TabPage tabCurrUser;
        private System.Windows.Forms.ListView lvConnections;
        protected internal System.Windows.Forms.ColumnHeader NickName;
        protected internal System.Windows.Forms.ColumnHeader Status;
        protected internal System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.ColumnHeader HistoryLine;
        private System.Windows.Forms.TabPage tabUsersList;
        private System.Windows.Forms.TextBox txtSelectedUserName;
        private System.Windows.Forms.TextBox txtSelectedUserId;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView lvUsersList;
        private System.Windows.Forms.ColumnHeader IsActive;
        private System.Windows.Forms.ColumnHeader User_Id;
        private System.Windows.Forms.ColumnHeader UserName;
        private System.Windows.Forms.ColumnHeader LoginName;
        private System.Windows.Forms.ColumnHeader LastConnection;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.Button btnSearchMsg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbReceiver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSender;
        private System.Windows.Forms.DataGridView gvMessages;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblUserLogin;
        private System.Windows.Forms.TextBox txtUserLogin;
    }
}