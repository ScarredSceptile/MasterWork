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
            LeftChoice=new Button();
            RightChoice=new Button();
            EqualValue=new Button();
            MethodProgress=new Label();
            Clicks=new Label();
            time=new Label();
            CompareMethod=new Label();
            GroupProgress=new Label();
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
            // LeftChoice
            // 
            LeftChoice.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            LeftChoice.Font=new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            LeftChoice.Location=new Point(641, 385);
            LeftChoice.Name="LeftChoice";
            LeftChoice.Size=new Size(41, 39);
            LeftChoice.TabIndex=2;
            LeftChoice.Text=">";
            LeftChoice.UseVisualStyleBackColor=true;
            LeftChoice.Click+=LeftChoice_Click;
            // 
            // RightChoice
            // 
            RightChoice.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            RightChoice.Font=new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            RightChoice.Location=new Point(735, 385);
            RightChoice.Name="RightChoice";
            RightChoice.Size=new Size(41, 39);
            RightChoice.TabIndex=3;
            RightChoice.Text="<";
            RightChoice.UseVisualStyleBackColor=true;
            RightChoice.Click+=RightChoice_Click;
            // 
            // EqualValue
            // 
            EqualValue.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            EqualValue.Font=new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            EqualValue.Location=new Point(688, 385);
            EqualValue.Name="EqualValue";
            EqualValue.Size=new Size(41, 39);
            EqualValue.TabIndex=4;
            EqualValue.Text="=";
            EqualValue.UseVisualStyleBackColor=true;
            EqualValue.Click+=EqualValue_Click;
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
            // Ranking
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(1444, 828);
            Controls.Add(GroupProgress);
            Controls.Add(CompareMethod);
            Controls.Add(time);
            Controls.Add(Clicks);
            Controls.Add(MethodProgress);
            Controls.Add(EqualValue);
            Controls.Add(RightChoice);
            Controls.Add(LeftChoice);
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
        private Button LeftChoice;
        private Button RightChoice;
        private Button EqualValue;
        private Label MethodProgress;
        private Label Clicks;
        private Label time;
        private Label CompareMethod;
        private Label GroupProgress;
    }
}