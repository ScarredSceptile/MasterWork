namespace ReviewRanking
{
    partial class Menu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1=new Label();
            DbConnection=new Label();
            ChangeDbBtn=new Button();
            CourseSelecter=new ComboBox();
            StartRanking=new Button();
            FixSavedData=new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            label1.AutoSize=true;
            label1.Location=new Point(183, 332);
            label1.Name="label1";
            label1.Size=new Size(123, 15);
            label1.TabIndex=0;
            label1.Text="Database Connection:";
            // 
            // DbConnection
            // 
            DbConnection.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            DbConnection.AutoSize=true;
            DbConnection.Location=new Point(312, 332);
            DbConnection.Name="DbConnection";
            DbConnection.Size=new Size(38, 15);
            DbConnection.TabIndex=1;
            DbConnection.Text="label2";
            // 
            // ChangeDbBtn
            // 
            ChangeDbBtn.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            ChangeDbBtn.Location=new Point(201, 242);
            ChangeDbBtn.Name="ChangeDbBtn";
            ChangeDbBtn.Size=new Size(119, 48);
            ChangeDbBtn.TabIndex=2;
            ChangeDbBtn.Text="Change DB";
            ChangeDbBtn.UseVisualStyleBackColor=true;
            ChangeDbBtn.Click+=ChangeDbBtn_Click;
            // 
            // CourseSelecter
            // 
            CourseSelecter.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            CourseSelecter.FormattingEnabled=true;
            CourseSelecter.Location=new Point(12, 196);
            CourseSelecter.Name="CourseSelecter";
            CourseSelecter.Size=new Size(513, 23);
            CourseSelecter.TabIndex=3;
            // 
            // StartRanking
            // 
            StartRanking.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            StartRanking.Location=new Point(134, 80);
            StartRanking.Name="StartRanking";
            StartRanking.Size=new Size(260, 83);
            StartRanking.TabIndex=4;
            StartRanking.Text="Start ranking with selected course";
            StartRanking.UseVisualStyleBackColor=true;
            StartRanking.Click+=StartRanking_Click;
            // 
            // FixSavedData
            // 
            FixSavedData.Location=new Point(12, 288);
            FixSavedData.Name="FixSavedData";
            FixSavedData.Size=new Size(118, 56);
            FixSavedData.TabIndex=5;
            FixSavedData.Text="Fix Saved Data";
            FixSavedData.UseVisualStyleBackColor=true;
            FixSavedData.Click+=FixSavedData_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(537, 356);
            Controls.Add(FixSavedData);
            Controls.Add(StartRanking);
            Controls.Add(CourseSelecter);
            Controls.Add(ChangeDbBtn);
            Controls.Add(DbConnection);
            Controls.Add(label1);
            Name="Menu";
            Text="Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label DbConnection;
        private Button ChangeDbBtn;
        private ComboBox CourseSelecter;
        private Button StartRanking;
        private Button FixSavedData;
    }
}