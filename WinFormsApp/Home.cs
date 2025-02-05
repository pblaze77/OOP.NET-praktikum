using Dao;
using Dao.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text.Json;
using static Dao.Models.Squads;
using System.DirectoryServices;
using System.Drawing.Text;

namespace WinFormsApp
{
    public partial class Home : Form
    {
        IFilesRepo repo;
        IWebRepo webRepo;
        List<Team> data;

        public Home()
        {
            TestForFile();
            repo.InitializeSettings();
            webRepo = RepoFactory.GetWebRepo();
            SetLanguage(repo);
            InitializeComponent();

            FormClosing += new FormClosingEventHandler(Home_FormClosing);
        }

        private void TestForFile()
        {
            repo = RepoFactory.GetRepo();

            if (!repo.FileExists())
            {
                new StartSettings().ShowDialog();
            }
        }

        private void SetLanguage(IFilesRepo repo)
        {
            var cultureCode = repo.GetSettings()[1] switch
            {
                0 => "hr-HR",
                1 => "",
                _ => Thread.CurrentThread.CurrentCulture.Name
            };

        }

        private async void Home_Load(object sender, EventArgs e)
        {
            pBar.Hide();
            HomeRefresh();
        }

        private void RemoveFavsFromRegulars()
        {
            foreach (Label favPlayer in panel2.Controls)
            {
                foreach (Label player in panel1.Controls)
                {
                    if (player.Text == favPlayer.Text)
                    {
                        panel1.Controls.Remove(player);
                    }
                }
            }
        }

        private async void LoadTeamPlayers()
        {
            List<Player> players = await GetTeamPlayers();

            panel1.Controls.Clear();

            foreach (var player in players)
            {
                Label label = new Label();

                label.Width = 150;

                label.Text = player.Name;

                label.Cursor = Cursors.Hand;

                label.MouseEnter += Label_MouseEnter;

                label.MouseLeave += Label_MouseLeave;

                label.MouseDown += Label_MouseDown;

                label.MouseMove += Label_MouseMove;

                label.MouseUp += Label_MouseUp;

                panel1.Controls.Add(label);
            }


            RemoveFavsFromRegulars();
        }

        private bool isDraging;
        private Point dragStartPoint;

        private void Label_MouseUp(object? sender, MouseEventArgs e)
        {
            if (!isDraging)
            {
                Label_Click(sender, e);
            }
            dragStartPoint = Point.Empty;
            isDraging = false;
        }

        private void Label_MouseMove(object? sender, MouseEventArgs e)
        {
            if (sender is Label label && e.Button == MouseButtons.Left)
            {
                var dx = Math.Abs(e.X - dragStartPoint.X);
                var dy = Math.Abs(e.Y - dragStartPoint.Y);

                if (dx >= SystemInformation.DragSize.Width || dy >= SystemInformation.DragSize.Height)
                {
                    isDraging = true;
                    DoDragDrop(label, DragDropEffects.Move);
                }
            }
        }

        private async void Label_MouseDown(object? sender, MouseEventArgs e)
        {
            dragStartPoint = e.Location;
            isDraging = false;
        }

        Label temp;

        private async void Label_Click(object? sender, EventArgs e)
        {
            if (sender is Label label)
            {
                var player = await SelectedPlayer(label);

                playeruc1.lbNameOutput.Text = player.Name;
                playeruc1.lbCaptainOutput.Text = player.Captain.ToString();
                playeruc1.lbPositionOutput.Text = player.Position.ToString();
                playeruc1.lbShirtNumberOutput.Text = player.ShirtNumber.ToString();

                var tempFavs = repo.GetFavourites();
                playeruc1.lbFavouritOutput.Text = tempFavs.Contains(label.Text) ? "\u2713" : "\u00D7";

                playeruc1.pbPlayer.ImageLocation = imagesRepo.GetImage(player.Name, player.ShirtNumber.ToString());

                temp = label;
            }
        }

        private async Task<Player> SelectedPlayer(Label label)
        {
            team = comboBox1.SelectedItem as Team;
            var players = await webRepo.GetPlayers(team.FifaCode);

            return players.FirstOrDefault(player => player.Name == label.Text) ?? players[0];
        }

        private void Label_MouseLeave(object? sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.White;
        }

        private void Label_MouseEnter(object? sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.LightBlue;
        }

        bool isFirstFetchSkiped = false;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (isFirstFetchSkiped && comboBox1.SelectedItem is Team)
            {
                panel1.Controls.Clear();
                panel2.Controls.Clear();

                repo.ClearFavourites();

                LoadTeamPlayers();
            }

            repo.SetFavouriteTeam(team);
        }

        private Team team;
        private async Task<List<Player>> GetTeamPlayers()
        {

            team = comboBox1.SelectedItem as Team;
            var players = await webRepo.GetPlayers(team.FifaCode);

            if (!isFirstFetchSkiped)
            {
                isFirstFetchSkiped = true;
            }

            return players;

        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSettings settings = new StartSettings();

            settings.FormClosed += Settings_FormClosed;

            settings.ShowDialog();



        }

        private void Settings_FormClosed(object? sender, FormClosedEventArgs e)
        {
            HomeRefresh();
        }


        private async void HomeRefresh()
        {

            AllControlsRedraw();
            isFirstFetchSkiped = false;

            data = await webRepo.GetChampionshipTeams();
            comboBox1.DataSource = data;
            comboBox1.ValueMember = "DisplayName";

            var favPlayers = repo.GetFavourites();

            if (panel2.Controls.Count != favPlayers.Count)
            {
                foreach (var player in favPlayers)
                {
                    var label = new Label
                    {
                        Width = 150,
                        Text = player,
                        Cursor = Cursors.Hand
                    };

                    label.MouseEnter += Label_MouseEnter;
                    label.MouseLeave += Label_MouseLeave;
                    label.MouseDown += Label_MouseDown;
                    label.MouseMove += Label_MouseMove;
                    label.MouseUp += Label_MouseUp;

                    panel2.Controls.Add(label);
                    favourites.Add(label);
                }
            }

            var favTeam = repo.GetFavouriteTeam();
            var temporary = comboBox1.Items.OfType<Team>().FirstOrDefault(team => team.DisplayName == favTeam);

            if (temporary != null)
            {
                comboBox1.SelectedItem = temporary;
                panel1.Controls.Clear();
                LoadTeamPlayers();
                return;
            }

            var firstTeam = comboBox1.Items.OfType<Team>().FirstOrDefault();

            if (firstTeam != null)
            {
                comboBox1.SelectedItem = firstTeam;
                repo.SetFavouriteTeam(firstTeam);
                LoadTeamPlayers();
            }
        }

        private void AllControlsRedraw()
        {
            Controls.Clear();
            InitializeComponent();
            pBar.Hide();
        }

        private string selectedImagePath;
        IImagesRepo imagesRepo = RepoFactory.GetImagesRepo();

        private void btnAddPicture_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(playeruc1.lbNameOutput.Text))
            {
                using var fileDialog = new OpenFileDialog
                {
                    Filter = "Image files (*.png) | *.png",
                    AddExtension = true,
                    Title = repo.GetSettings()[1] == 0
                        ? "Dodaj sliku igrača"
                        : "Add player picture"
                };

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var imagesDirectory = Path.Combine(".", "images");
                    Directory.CreateDirectory(imagesDirectory);

                    var fileName = $"{playeruc1.lbNameOutput.Text}-{playeruc1.lbShirtNumberOutput.Text}{Path.GetExtension(fileDialog.FileName)}";
                    var imagePath = Path.Combine(imagesDirectory, fileName);

                    using var imageStream = fileDialog.OpenFile();
                    Image.FromStream(imageStream).Save(imagePath);

                    selectedImagePath = fileName;

                    SetPicture();
                }
            }
            else
            {
                var message = repo.GetSettings()[1] == 0
                    ? "Igrač mora biti odabran kako bi postavili sliku!"
                    : "Player must be selected to set the picture!";

                var title = repo.GetSettings()[1] == 0
                    ? "Postavi sliku greška"
                    : "Set image error";

                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetPicture()
        {
            if (selectedImagePath != null)
            {
                playeruc1.pbPlayer.ImageLocation = selectedImagePath;
            }
        }

        List<Label> favourites = new List<Label>();
        private async void btnTransferToFavs_Click(object sender, EventArgs e)
        {

            Player player = await SelectedPlayer(temp);

            Label newFavorite = new Label();

            newFavorite.Width = 150;
            newFavorite.Text = player.Name;

            newFavorite.Cursor = Cursors.Hand;

            newFavorite.MouseEnter += Label_MouseEnter;

            newFavorite.MouseLeave += Label_MouseLeave;

            newFavorite.MouseMove += Label_MouseMove;

            newFavorite.MouseDown += Label_MouseDown;

            newFavorite.MouseUp += Label_MouseUp;

            newFavorite.Click += Label_Click;


            if (panel2.Controls.Count == 3)
            {
                MessageBox.Show("Max 3 favourite players!", "Favourites error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsFavouriteAlready(newFavorite, favourites))
            {
                MessageBox.Show("Player is already a favourite!", "Favourites error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            panel2.Controls.Add(newFavorite);

            foreach (Control label in panel1.Controls)
            {
                if (label.Text == player.Name)
                {
                    panel1.Controls.Remove(label);
                }
            }

            favourites.Add(newFavorite);


            repo.SetFavourites(newFavorite.Text);

        }


        private bool IsFavouriteAlready(Label newFavorite, List<Label> favourites)
        {
            foreach (Label label in favourites)
            {

                if (label.Text == newFavorite.Text)
                {
                    return true;
                }

            }
            return false;
        }


        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
        e.Effect = e.Data.GetDataPresent(typeof(Label))
        ? DragDropEffects.Move
        : DragDropEffects.None;
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {

            if (!e.Data.GetDataPresent(typeof(Label))) return;

            var label = e.Data.GetData(typeof(Label)) as Label;
            var targetPanel = sender as Panel;

            if (targetPanel == panel2 && panel2.Controls.Count == 3)
            {
                MessageBox.Show("Max 3 favourite players!", "Favourites error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (label != null && targetPanel != null)
            {
                var targetPoint = targetPanel.PointToClient(new Point(e.X, e.Y));
                label.Location = targetPoint;

                if (targetPanel == panel2)
                    repo.SetFavourites(label.Text);

                if (targetPanel == panel1)
                    repo.RemoveFromFavourites(label.Text);

                label.Parent.Controls.Remove(label);
                targetPanel.Controls.Add(label);
            }
        }

        private async void btnRemoveFromFavs_Click(object sender, EventArgs e)
        {

            var player = await SelectedPlayer(temp);
            var oldFavorite = temp;

            if (!favourites.Contains(oldFavorite))
            {
                MessageBox.Show("Selected player is not a favourite!", "Favourites error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var control in panel2.Controls.OfType<Label>().ToList())
            {
                if (control.Text == player.Name)
                {
                    favourites.Remove(control);
                    repo.RemoveFromFavourites(control.Text);
                    panel2.Controls.Remove(control);
                }
            }

            oldFavorite.Width = 150;
            oldFavorite.Text = player.Name;
            oldFavorite.Cursor = Cursors.Hand;

       
            oldFavorite.MouseEnter += Label_MouseEnter;
            oldFavorite.MouseLeave += Label_MouseLeave;
            oldFavorite.MouseMove += Label_MouseMove;
            oldFavorite.MouseDown += Label_MouseDown;
            oldFavorite.MouseUp += Label_MouseUp;
            oldFavorite.Click += Label_Click;

            panel1.Controls.Add(oldFavorite);

        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            using var customMessageBox = new CustomMessageBox();
            var result = customMessageBox.ShowDialog();

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

    }

}

