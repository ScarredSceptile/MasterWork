namespace ReviewRanking
{
    partial class Ranking
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
            ReviewLeft=new TextBox();
            ReviewRight=new TextBox();
            MethodProgress=new Label();
            Clicks=new Label();
            time=new Label();
            CompareMethod=new Label();
            GroupProgress=new Label();
            ProgressBtn=new Button();
            GPTInput=new TextBox();
            SuspendLayout();
            // 
            // ReviewLeft
            // 
            ReviewLeft.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            ReviewLeft.Location=new Point(50, 126);
            ReviewLeft.Multiline=true;
            ReviewLeft.Name="ReviewLeft";
            ReviewLeft.ReadOnly=true;
            ReviewLeft.ScrollBars=ScrollBars.Vertical;
            ReviewLeft.Size=new Size(567, 594);
            ReviewLeft.TabIndex=0;
            // 
            // ReviewRight
            // 
            ReviewRight.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            ReviewRight.Location=new Point(818, 126);
            ReviewRight.Multiline=true;
            ReviewRight.Name="ReviewRight";
            ReviewRight.ReadOnly=true;
            ReviewRight.ScrollBars=ScrollBars.Vertical;
            ReviewRight.Size=new Size(567, 594);
            ReviewRight.TabIndex=1;
            // 
            // MethodProgress
            // 
            MethodProgress.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            MethodProgress.AutoSize=true;
            MethodProgress.Location=new Point(688, 468);
            MethodProgress.Name="MethodProgress";
            MethodProgress.Size=new Size(38, 15);
            MethodProgress.TabIndex=5;
            MethodProgress.Text="label1";
            // 
            // Clicks
            // 
            Clicks.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            Clicks.AutoSize=true;
            Clicks.Location=new Point(684, 149);
            Clicks.Name="Clicks";
            Clicks.Size=new Size(38, 15);
            Clicks.TabIndex=6;
            Clicks.Text="label1";
            // 
            // time
            // 
            time.AutoSize=true;
            time.Location=new Point(689, 37);
            time.Name="time";
            time.Size=new Size(36, 15);
            time.TabIndex=7;
            time.Text="Time:";
            // 
            // CompareMethod
            // 
            CompareMethod.AutoSize=true;
            CompareMethod.Location=new Point(688, 438);
            CompareMethod.Name="CompareMethod";
            CompareMethod.Size=new Size(38, 15);
            CompareMethod.TabIndex=8;
            CompareMethod.Text="label1";
            // 
            // GroupProgress
            // 
            GroupProgress.AutoSize=true;
            GroupProgress.Location=new Point(684, 524);
            GroupProgress.Name="GroupProgress";
            GroupProgress.Size=new Size(38, 15);
            GroupProgress.TabIndex=9;
            GroupProgress.Text="label1";
            // 
            // ProgressBtn
            // 
            ProgressBtn.Location=new Point(661, 341);
            ProgressBtn.Name="ProgressBtn";
            ProgressBtn.Size=new Size(107, 71);
            ProgressBtn.TabIndex=10;
            ProgressBtn.Text="Progress";
            ProgressBtn.UseVisualStyleBackColor=true;
            ProgressBtn.Click+=ProgressBtn_Click;
            // 
            // GPTInput
            // 
            GPTInput.Location=new Point(641, 299);
            GPTInput.Name="GPTInput";
            GPTInput.Size=new Size(146, 23);
            GPTInput.TabIndex=11;
            // 
            // Ranking
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(1444, 828);
            Controls.Add(GPTInput);
            Controls.Add(ProgressBtn);
            Controls.Add(GroupProgress);
            Controls.Add(CompareMethod);
            Controls.Add(time);
            Controls.Add(Clicks);
            Controls.Add(MethodProgress);
            Controls.Add(ReviewRight);
            Controls.Add(ReviewLeft);
            Name="Ranking";
            Text="Ranking";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ReviewLeft;
        private TextBox ReviewRight;
        private Label MethodProgress;
        private Label Clicks;
        private Label time;
        private Label CompareMethod;
        private Label GroupProgress;
        private Button ProgressBtn;
        private TextBox GPTInput;
    }
}