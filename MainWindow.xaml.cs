using Dao;
using Dao.Models;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFilesRepo repo;
        IWebRepo webRepo;
        List<Team> data;

        public MainWindow()
        {
            TestForFile();
            repo.InitializeSettingsforWpf();
            webRepo = RepoFactory.GetWebRepo();
            SetLanguage(repo);
            InitializeComponent();

            switch (repo.GetSettingsforWpf()[2])
            {
                case 0:
                    this.Width = 800;
                    this.Height = 450;
                    break;
                case 1:
                    this.Width = 1024;
                    this.Height = 650;
                    break;
                case 2:
                    this.WindowState = WindowState.Maximized;
                    break;
                default:
                    this.Width = 800;
                    this.Height = 450;
                    break;
            }
        }

        Team team = new Team();
        Team enemy = new Team();
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        { MainWindowRefresh();}

        private async void FillCbAgainst()
        {
            List<Squads> matches = await webRepo.GetEnemies(team.FifaCode);

            List<Team> enemies = new List<Team>();

            foreach (Team tempTeam in data)
            {
                if (tempTeam.Country != team.Country)
                {
                    foreach (Squads squad in matches)
                    {
                        if (squad.HomeTeam.Country == tempTeam.Country || squad.AwayTeam.Country == tempTeam.Country && tempTeam.Country != team.Country)
                        {
                            enemies.Add(tempTeam);
                        }
                    }
                }

            }

            cbAgainst.ItemsSource = enemies;

            cbAgainst.DisplayMemberPath = "DisplayName";

            cbAgainst.SelectedIndex = 0;

        }

        private void TestForFile()
        {
            repo = RepoFactory.GetRepo();

            if (!repo.FileExistsforWpf())
            {
                var settings = new Settings();

                settings.ShowDialog();
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

            var cultureInfo = new System.Globalization.CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

        }

        private void btnFavouriteInfo_Click(object sender, RoutedEventArgs e)
        {
            TeamInfo teamInfo = new TeamInfo();
            string[] values = { team.Country,
                team.FifaCode,
                team.GamesPlayed.ToString(),
                team.Wins.ToString(),
                team.Losses.ToString(),
                team.Draws.ToString(),
                team.Goals.ToString(),
                team.GoalsAgainst.ToString(),
                team.GoalDifference.ToString() };
            teamInfo.SetLabelValues(values);
            teamInfo.Show();
        }

        private void cbFavourite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            team = cbFavourite.SelectedItem as Team;
            repo.SetFavouriteTeam(team);
            FillCbAgainst();
            LoadPlayers(team);
            gridField.Children.Clear();
            MakeStartingFromationTeam(team);
        }


        private async void MakeStartingFromationTeam(Team team)
        {
            List<Player> startingEleven = await webRepo.GetStartingEleven(team.FifaCode);

            int defCounter = 0;
            int midCounter = 0;
            int attCounter = 0;

            foreach (Player player in startingEleven)
            {
                PlayerUCWpf playerUC = new PlayerUCWpf();

                playerUC.lbName.Content = player.Name;
                playerUC.lbNumber.Content = player.ShirtNumber.ToString();

                string playerNameNum = player.Name + "-" + player.ShirtNumber.ToString() + ".png";
                string imagePath = repo.GetPlayerPicture(playerNameNum);


                Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);


                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = imageUri;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                playerUC.playerPic.Source = bitmap;

                playerUC.MouseDown += (sender, e) => OnPlayerMouseDown(sender, e, player);


                if (player.Position == "Goalie")
                {
                    Grid.SetRow(playerUC, 7);
                    Grid.SetColumn(playerUC, 2);
                }
                else if (player.Position == "Defender")
                {
                    int[] defenderColumns = { 2, 1, 3, 0, 4 };
                    int col = defCounter < defenderColumns.Length ? defenderColumns[defCounter] : defenderColumns[^1];
                    Grid.SetRow(playerUC, 6);
                    Grid.SetColumn(playerUC, col);
                    defCounter++;
                }
                else if (player.Position == "Midfield")
                {
                    int[] midfieldColumns = { 2, 1, 3, 0, 4 };
                    int col = midCounter < midfieldColumns.Length ? midfieldColumns[midCounter] : midfieldColumns[^1];
                    Grid.SetRow(playerUC, 5);
                    Grid.SetColumn(playerUC, col);
                    midCounter++;
                }
                else if (player.Position == "Forward")
                {
                    int[] forwardColumns = { 2, 1, 3, 0, 4 };
                    int col = attCounter < forwardColumns.Length ? forwardColumns[attCounter] : forwardColumns[^1];
                    Grid.SetRow(playerUC, 4);
                    Grid.SetColumn(playerUC, col);
                    attCounter++;
                }

                
                gridField.Children.Add(playerUC);

            }


        }

        private void btnAgainstInfo_Click(object sender, RoutedEventArgs e)
        {
            TeamInfo teamInfo = new TeamInfo();
            string[] values = { enemy.Country,
                enemy.FifaCode,
                enemy.GamesPlayed.ToString(),
                enemy.Wins.ToString(),
                enemy.Losses.ToString(),
                enemy.Draws.ToString(),
                enemy.Goals.ToString(),
                enemy.GoalsAgainst.ToString(),
                enemy.GoalDifference.ToString() };
            teamInfo.SetLabelValues(values);
            teamInfo.Show();
        }

        private async void cbAgainst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            enemy = cbAgainst.SelectedItem as Team;

            if (enemy is null)
            {
                await Task.Delay(3000);
                enemy = cbAgainst.SelectedItem as Team;
            }
            GetResult();
            LoadPlayers(enemy);
            gridField.Children.Clear();
            MakeStartingFromationTeam(team);
            MakeStartingFromationEnemy(enemy);
        }

        private async void MakeStartingFromationEnemy(Team enemy)
        {
            List<Player> startingEleven = await webRepo.GetStartingEleven(enemy.FifaCode);

            int defCounter = 0;
            int midCounter = 0;
            int attCounter = 0;

            foreach (Player player in startingEleven)
            {
                PlayerUCWpf playerUC = new PlayerUCWpf();

                playerUC.lbName.Content = player.Name;
                playerUC.lbNumber.Content = player.ShirtNumber.ToString();

                string playerNameNum = player.Name + "-" + player.ShirtNumber.ToString() + ".png";
                string imagePath = repo.GetPlayerPicture(playerNameNum);


                Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);


                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = imageUri;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                playerUC.playerPic.Source = bitmap;

                playerUC.MouseDown += (sender, e) => OnPlayerMouseDownEnemy(sender, e, player);

                var columnPositions = new Dictionary<string, int[]>
{
                  { "Goalie", new[] { 2 } },
                  { "Defender", new[] { 2, 1, 3, 0, 4 } },
                  { "Midfield", new[] { 2, 1, 3, 0, 4 } },
                  { "Forward", new[] { 2, 1, 3, 0, 4 } }
                            };

                var positionCounters = new Dictionary<string, int>
{
                     { "Defender", defCounter },
                     { "Midfield", midCounter },
                       { "Forward", attCounter }
                        };

                if (player.Position == "Goalie")
                {
                    Grid.SetRow(playerUC, 0);
                    Grid.SetColumn(playerUC, columnPositions["Goalie"][0]);
                }
                else if (columnPositions.ContainsKey(player.Position))
                {
                    var counter = positionCounters[player.Position];
                    var columns = columnPositions[player.Position];
                    var col = counter < columns.Length ? columns[counter] : columns[^1];

                    var row = player.Position switch
                    {
                        "Defender" => 1,
                        "Midfield" => 2,
                        "Forward" => 3,
                        _ => 0
                    };

                    Grid.SetRow(playerUC, row);
                    Grid.SetColumn(playerUC, col);

                    positionCounters[player.Position]++;
                }

                gridField.Children.Add(playerUC);

                
                defCounter = positionCounters["Defender"];
                midCounter = positionCounters["Midfield"];
                attCounter = positionCounters["Forward"];



            }
        }


         
        private async void OnPlayerMouseDown(object sender, MouseButtonEventArgs e, Player player)
        {
            List<Squads> matchesInfo = await webRepo.GetEnemies(team.FifaCode);
            PlayerInfo playerInfo = new PlayerInfo();
            List<TeamEvent> teamTemp = null;
            foreach (Squads squad in matchesInfo)
            {
                if (squad.home_team_statistics.country == team.Country && squad.away_team_statistics.country == enemy.Country)
                {
                    teamTemp = squad.HomeTeamEvents;
                }
                else if (squad.away_team_statistics.country == team.Country && squad.home_team_statistics.country == enemy.Country)
                {
                    teamTemp = squad.AwayTeamEvents;
                }

            }

            int scoredGoals = 0;
            int yellowCards = 0;

            foreach (TeamEvent teamEvent in teamTemp)
            {
                if (player.Name == teamEvent.Player && teamEvent.TypeOfEvent == "goal" || teamEvent.TypeOfEvent == "goal-penalty")
                {
                    scoredGoals++;
                }

                if (player.Name == teamEvent.Player && teamEvent.TypeOfEvent == "yellow-card")
                {
                    yellowCards++;
                }
            }


            string[] values =
            {
                player.Name,
                player.ShirtNumber.ToString(),
                player.Position,
                player.Captain.ToString(),
                scoredGoals.ToString(),
                yellowCards.ToString()
            };

            playerInfo.SetLabelValues(values);

            string playerNameNum = player.Name + "-" + player.ShirtNumber.ToString() + ".png";
            string imagePath = repo.GetPlayerPicture(playerNameNum);


            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);


            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = imageUri;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            playerInfo.imgPlayer.Source = bitmap;

            playerInfo.ShowDialog();

        }

        private async void OnPlayerMouseDownEnemy(object sender, MouseButtonEventArgs e, Player player)
        {
            List<Squads> matchesInfo = await webRepo.GetEnemies(team.FifaCode);
            PlayerInfo playerInfo = new PlayerInfo();
            List<TeamEvent> teamTemp = null;
            foreach (Squads squad in matchesInfo)
            {
                if (squad.home_team_statistics.country == enemy.Country && squad.away_team_statistics.country == team.Country)
                {
                    teamTemp = squad.HomeTeamEvents;
                }
                else if (squad.away_team_statistics.country == enemy.Country && squad.home_team_statistics.country == team.Country)
                {
                    teamTemp = squad.AwayTeamEvents;
                }

            }

            int scoredGoals = 0;
            int yellowCards = 0;

            foreach (TeamEvent teamEvent in teamTemp)
            {
                if (player.Name == teamEvent.Player && teamEvent.TypeOfEvent == "goal" || teamEvent.TypeOfEvent == "goal-penalty")
                {
                    scoredGoals++;
                }

                if (player.Name == teamEvent.Player && teamEvent.TypeOfEvent == "yellow-card")
                {
                    yellowCards++;
                }
            }


            string[] values =
            {
                player.Name,
                player.ShirtNumber.ToString(),
                player.Position,
                player.Captain.ToString(),
                scoredGoals.ToString(),
                yellowCards.ToString()
            };

            playerInfo.SetLabelValues(values);

            string playerNameNum = player.Name + "-" + player.ShirtNumber.ToString() + ".png";
            string imagePath = repo.GetPlayerPicture(playerNameNum);


            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);


            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = imageUri;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            playerInfo.imgPlayer.Source = bitmap;

            playerInfo.ShowDialog();
        }


        private async void LoadPlayers(Team teamTemp)
        {
            var players = await webRepo.GetPlayers(teamTemp.FifaCode);

            var targetPanel = teamTemp == team ? panelFavourite : panelEnemy;
            var alignment = teamTemp == team ? HorizontalAlignment.Right : HorizontalAlignment.Left;

            targetPanel.Children.Clear();

            foreach (var player in players)
            {
                var label = new Label
                {
                    Content = player.Name,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalContentAlignment = alignment,
                    Foreground = new SolidColorBrush(Colors.White)
                };

                targetPanel.Children.Add(label);
            }
        }

        private async void lbResult_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private async void GetResult()
        {
            var matches = await webRepo.GetEnemies(team.FifaCode);
            Specs favourite = null;
            Specs enemyT = null;

            foreach (var squad in matches)
            {
                if (squad.home_team_statistics.country == team.Country && squad.away_team_statistics.country == enemy.Country)
                {
                    favourite = squad.HomeTeam;
                    enemyT = squad.AwayTeam;
                    break; // Match found, no need to continue
                }
                else if (squad.away_team_statistics.country == team.Country && squad.home_team_statistics.country == enemy.Country)
                {
                    favourite = squad.AwayTeam;
                    enemyT = squad.HomeTeam;
                    break; // Match found, no need to continue
                }
            }

            if (favourite != null && enemyT != null)
            {
                lbResult.Content = $"{favourite.Goals} : {enemyT.Goals}";
            }
            else
            {
                lbResult.Content = "Result not found.";
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();

            settings.Closed += Settings_Closed;

            Hide();

            settings.ShowDialog();
            
            Close();

        }

        private void Settings_Closed(object? sender, EventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }

        private async void MainWindowRefresh()
        {
            data = await webRepo.GetChampionshipTeams();

            cbFavourite.ItemsSource = data;

            cbFavourite.DisplayMemberPath = "DisplayName";


            if (!repo.TeamFileExistsforWpf())
            {
                string winFormsTeam = repo.WinFormsFavouriteTeam();

                bool notInChampionship = true;

                foreach (Team teamChoice in cbFavourite.Items)
                {
                    if (teamChoice.DisplayName == winFormsTeam)
                    {
                        cbFavourite.SelectedItem = teamChoice;
                        team = teamChoice;
                        repo.SetFavouriteTeamforWpf(teamChoice);
                        notInChampionship = false;
                    }
                }

                if (notInChampionship)
                {
                    MessageBox.Show("Favourite team is not in selected championship", "Favourite team error");
                    cbFavourite.SelectedIndex = 0;
                    repo.SetFavouriteTeamforWpf(cbFavourite.SelectedItem as Team);
                }

            }
            else
            {
                string teamName = repo.GetFavouriteTeam();


                foreach (Team temp in cbFavourite.Items)
                {
                    if (temp.DisplayName == teamName)
                    {
                        team = temp;
                    }
                }

                if (cbFavourite.Items.Contains(team))
                {
                    cbFavourite.SelectedItem = team;
                }
                else
                {
                    MessageBox.Show("Favourite team is not in selected championship", "Favourite team error");
                    cbFavourite.SelectedIndex = 0;
                }

            }


            FillCbAgainst();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CustomBox customBox = new CustomBox();

            
            var result = ShowCustomDialog(customBox);

            if (result)
            {
                Application.Current.Shutdown();
            }
            else
            {
               e.Cancel = true;
            }
        }

        private bool ShowCustomDialog(CustomBox customBox)
        {
            customBox.ShowDialog();
            return (customBox as CustomBox)?.UserDecision ?? false;
        }
    }

}