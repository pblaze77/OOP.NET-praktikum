namespace WinFormsApp
{
    partial class StartSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartSettings));
            lbChampsionship = new Label();
            cbChampionship = new ComboBox();
            cbLanguage = new ComboBox();
            lbLanguage = new Label();
            btnApply = new Button();
            btnNext = new Button();
            SuspendLayout();
            // 
            // lbChampsionship
            // 
            resources.ApplyResources(lbChampsionship, "lbChampsionship");
            lbChampsionship.Name = "lbChampsionship";
            // 
            // cbChampionship
            // 
            cbChampionship.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChampionship.FormattingEnabled = true;
            cbChampionship.Items.AddRange(new object[] { resources.GetString("cbChampionship.Items"), resources.GetString("cbChampionship.Items1") });
            resources.ApplyResources(cbChampionship, "cbChampionship");
            cbChampionship.Name = "cbChampionship";
            cbChampionship.SelectedIndexChanged += cbChampionship_SelectedIndexChanged;
            // 
            // cbLanguage
            // 
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { resources.GetString("cbLanguage.Items"), resources.GetString("cbLanguage.Items1") });
            resources.ApplyResources(cbLanguage, "cbLanguage");
            cbLanguage.Name = "cbLanguage";
            cbLanguage.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lbLanguage
            // 
            resources.ApplyResources(lbLanguage, "lbLanguage");
            lbLanguage.Name = "lbLanguage";
            // 
            // btnApply
            // 
            resources.ApplyResources(btnApply, "btnApply");
            btnApply.Name = "btnApply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnNext
            // 
            resources.ApplyResources(btnNext, "btnNext");
            btnNext.Name = "btnNext";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // StartSettings
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnNext);
            Controls.Add(btnApply);
            Controls.Add(cbLanguage);
            Controls.Add(lbLanguage);
            Controls.Add(cbChampionship);
            Controls.Add(lbChampsionship);
            Name = "StartSettings";
            Load += StartSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbChampsionship;
        private ComboBox cbChampionship;
        private ComboBox cbLanguage;
        private Label lbLanguage;
        private Button btnApply;
        private Button btnNext;
    }
}
