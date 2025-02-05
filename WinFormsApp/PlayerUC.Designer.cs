namespace WinFormsApp
{
    partial class PlayerUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerUC));
            lbName = new Label();
            lbShirtNumber = new Label();
            lbPosition = new Label();
            lbCaptain = new Label();
            lbFavourite = new Label();
            lbNameOutputlbNameOutput = new Label();
            pbPlayer = new PictureBox();
            lbNameOutput = new Label();
            lbShirtNumberOutput = new Label();
            lbPositionOutput = new Label();
            lbCaptainOutput = new Label();
            lbFavouritOutput = new Label();
            ((System.ComponentModel.ISupportInitialize)pbPlayer).BeginInit();
            SuspendLayout();
            // 
            // lbName
            // 
            resources.ApplyResources(lbName, "lbName");
            lbName.Name = "lbName";
            lbName.Click += lbName_Click;
            // 
            // lbShirtNumber
            // 
            resources.ApplyResources(lbShirtNumber, "lbShirtNumber");
            lbShirtNumber.Name = "lbShirtNumber";
            // 
            // lbPosition
            // 
            resources.ApplyResources(lbPosition, "lbPosition");
            lbPosition.Name = "lbPosition";
            // 
            // lbCaptain
            // 
            resources.ApplyResources(lbCaptain, "lbCaptain");
            lbCaptain.Name = "lbCaptain";
            // 
            // lbFavourite
            // 
            resources.ApplyResources(lbFavourite, "lbFavourite");
            lbFavourite.Name = "lbFavourite";
            // 
            // lbNameOutputlbNameOutput
            // 
            resources.ApplyResources(lbNameOutputlbNameOutput, "lbNameOutputlbNameOutput");
            lbNameOutputlbNameOutput.Name = "lbNameOutputlbNameOutput";
            // 
            // pbPlayer
            // 
            pbPlayer.Image = Properties.Resources._default;
            resources.ApplyResources(pbPlayer, "pbPlayer");
            pbPlayer.Name = "pbPlayer";
            pbPlayer.TabStop = false;
            pbPlayer.Click += pbPlayer_Click;
            // 
            // lbNameOutput
            // 
            resources.ApplyResources(lbNameOutput, "lbNameOutput");
            lbNameOutput.Name = "lbNameOutput";
            // 
            // lbShirtNumberOutput
            // 
            resources.ApplyResources(lbShirtNumberOutput, "lbShirtNumberOutput");
            lbShirtNumberOutput.Name = "lbShirtNumberOutput";
            // 
            // lbPositionOutput
            // 
            resources.ApplyResources(lbPositionOutput, "lbPositionOutput");
            lbPositionOutput.Name = "lbPositionOutput";
            // 
            // lbCaptainOutput
            // 
            resources.ApplyResources(lbCaptainOutput, "lbCaptainOutput");
            lbCaptainOutput.Name = "lbCaptainOutput";
            // 
            // lbFavouritOutput
            // 
            resources.ApplyResources(lbFavouritOutput, "lbFavouritOutput");
            lbFavouritOutput.Name = "lbFavouritOutput";
            // 
            // PlayerUC
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lbFavouritOutput);
            Controls.Add(lbCaptainOutput);
            Controls.Add(lbPositionOutput);
            Controls.Add(lbShirtNumberOutput);
            Controls.Add(lbNameOutput);
            Controls.Add(pbPlayer);
            Controls.Add(lbNameOutputlbNameOutput);
            Controls.Add(lbFavourite);
            Controls.Add(lbCaptain);
            Controls.Add(lbPosition);
            Controls.Add(lbShirtNumber);
            Controls.Add(lbName);
            Name = "PlayerUC";
            Load += PlayerUC_Load;
            ((System.ComponentModel.ISupportInitialize)pbPlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label lbName;
        public Label lbShirtNumber;
        public Label lbPosition;
        public Label lbCaptain;
        public Label lbFavourite;
        public Label lbNameOutputlbNameOutput;
        public PictureBox pbPlayer;
        public Label lbNameOutput;
        public Label lbShirtNumberOutput;
        public Label lbPositionOutput;
        public Label lbCaptainOutput;
        public Label lbFavouritOutput;
    }
}
