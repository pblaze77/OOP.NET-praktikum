namespace WinFormsApp
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            rangListToolStripMenuItem = new ToolStripMenuItem();
            comboBox1 = new ComboBox();
            lbFavouriteRepresentation = new Label();
            lbPlayer = new Label();
            lbFavouritePlayers = new Label();
            lbPlayers = new Label();
            btnTransferToFavs = new Button();
            playeruc1 = new PlayerUC();
            panel1 = new FlowLayoutPanel();
            btnAddPicture = new Button();
            panel2 = new FlowLayoutPanel();
            btnRemoveFromFavs = new Button();
            pBar = new ProgressBar();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, rangListToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(settingsToolStripMenuItem, "settingsToolStripMenuItem");
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // rangListToolStripMenuItem
            // 
            rangListToolStripMenuItem.Name = "rangListToolStripMenuItem";
            resources.ApplyResources(rangListToolStripMenuItem, "rangListToolStripMenuItem");
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            resources.ApplyResources(comboBox1, "comboBox1");
            comboBox1.Name = "comboBox1";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lbFavouriteRepresentation
            // 
            resources.ApplyResources(lbFavouriteRepresentation, "lbFavouriteRepresentation");
            lbFavouriteRepresentation.Name = "lbFavouriteRepresentation";
            // 
            // lbPlayer
            // 
            resources.ApplyResources(lbPlayer, "lbPlayer");
            lbPlayer.Name = "lbPlayer";
            // 
            // lbFavouritePlayers
            // 
            resources.ApplyResources(lbFavouritePlayers, "lbFavouritePlayers");
            lbFavouritePlayers.Name = "lbFavouritePlayers";
            // 
            // lbPlayers
            // 
            resources.ApplyResources(lbPlayers, "lbPlayers");
            lbPlayers.Name = "lbPlayers";
            // 
            // btnTransferToFavs
            // 
            resources.ApplyResources(btnTransferToFavs, "btnTransferToFavs");
            btnTransferToFavs.Name = "btnTransferToFavs";
            btnTransferToFavs.UseVisualStyleBackColor = true;
            btnTransferToFavs.Click += btnTransferToFavs_Click;
            // 
            // playeruc1
            // 
            resources.ApplyResources(playeruc1, "playeruc1");
            playeruc1.BorderStyle = BorderStyle.FixedSingle;
            playeruc1.Name = "playeruc1";
            // 
            // panel1
            // 
            panel1.AllowDrop = true;
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Name = "panel1";
            panel1.DragDrop += Panel_DragDrop;
            panel1.DragEnter += Panel_DragEnter;
            // 
            // btnAddPicture
            // 
            resources.ApplyResources(btnAddPicture, "btnAddPicture");
            btnAddPicture.Name = "btnAddPicture";
            btnAddPicture.UseVisualStyleBackColor = true;
            btnAddPicture.Click += btnAddPicture_Click;
            // 
            // panel2
            // 
            panel2.AllowDrop = true;
            resources.ApplyResources(panel2, "panel2");
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Name = "panel2";
            panel2.DragDrop += Panel_DragDrop;
            panel2.DragEnter += Panel_DragEnter;
            // 
            // btnRemoveFromFavs
            // 
            resources.ApplyResources(btnRemoveFromFavs, "btnRemoveFromFavs");
            btnRemoveFromFavs.Name = "btnRemoveFromFavs";
            btnRemoveFromFavs.UseVisualStyleBackColor = true;
            btnRemoveFromFavs.Click += btnRemoveFromFavs_Click;
            // 
            // pBar
            // 
            resources.ApplyResources(pBar, "pBar");
            pBar.Name = "pBar";
            // 
            // Home
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pBar);
            Controls.Add(btnRemoveFromFavs);
            Controls.Add(panel2);
            Controls.Add(btnAddPicture);
            Controls.Add(panel1);
            Controls.Add(playeruc1);
            Controls.Add(btnTransferToFavs);
            Controls.Add(lbPlayers);
            Controls.Add(lbFavouritePlayers);
            Controls.Add(lbPlayer);
            Controls.Add(lbFavouriteRepresentation);
            Controls.Add(comboBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Home";
            Load += Home_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem rangListToolStripMenuItem;
        private ComboBox comboBox1;
        private Label lbFavouriteRepresentation;
        private Label lbPlayer;
        private Label lbFavouritePlayers;
        private Label lbPlayers;
        private Button btnTransferToFavs;
        private PlayerUC playeruc1;
        private FlowLayoutPanel panel1;
        private Button btnAddPicture;
        private FlowLayoutPanel panel2;
        private Button btnRemoveFromFavs;
        private ProgressBar pBar;
    }
}