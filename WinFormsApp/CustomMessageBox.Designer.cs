namespace WinFormsApp
{
    partial class CustomMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomMessageBox));
            lbQuestion = new Label();
            btnYes = new Button();
            btnNo = new Button();
            SuspendLayout();
            // 
            // lbQuestion
            // 
            resources.ApplyResources(lbQuestion, "lbQuestion");
            lbQuestion.Name = "lbQuestion";
            // 
            // btnYes
            // 
            resources.ApplyResources(btnYes, "btnYes");
            btnYes.Name = "btnYes";
            btnYes.UseVisualStyleBackColor = true;
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            resources.ApplyResources(btnNo, "btnNo");
            btnNo.Name = "btnNo";
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += btnNo_Click;
            // 
            // CustomMessageBox
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Controls.Add(lbQuestion);
            Name = "CustomMessageBox";
            Load += CustomMessageBox_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbQuestion;
        private Button btnYes;
        private Button btnNo;
    }
}