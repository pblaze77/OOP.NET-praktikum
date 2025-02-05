using Dao.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Dao
{
    internal class TextRepo : IFilesRepo
    {
        private string PATH = @".\settings.txt";

        private static int champions;
        private static int language;


        public void InitializeSettings()
        {
            var lines = File.ReadAllLines(PATH);

            if (!lines.Any()) return;

            var indexes = lines[1].Split(";");

            champions = int.Parse(indexes[0]);
            language = int.Parse(indexes[1]);

        }

        public int[] GetSettings()
        {
            int[] settings = { champions, language };

            return settings;
        }

        public void SetSettings(int[] settings)
        {
            if (!File.Exists(PATH))
            {
                CreateFile();
            }
            else
            {
                FillFile(settings);
            }

        }

        private void FillFile(int[] settings)
        {
            File.WriteAllText(PATH, "Championship;Language\n" + settings[0].ToString() + ';' + settings[1].ToString());
        }

        private void CreateFile()
        {
            File.Create(PATH).Close();

            File.WriteAllText(PATH, "Championship;Language\n0;0");


        }

        public bool FileExists()
        {
            return File.Exists(PATH);
        }


        private string PATHFAVS = @".\favourites.txt";


        public List<string> GetFavourites()
        {
            if (File.Exists(PATHFAVS))
            {
                return File.ReadAllLines(PATHFAVS).ToList();
            }

            CreateFileFavourites(PATHFAVS);
            return new List<string>();

        }

        public void SetFavourites(string favourite)
        {
            if (File.Exists(PATHFAVS))
            {
               
                FillFileFavourites(favourite);
            }
            else
            {
                CreateFileFavourites(PATHFAVS);
                FillFileFavourites(favourite);
            }
        }

        private void FillFileFavourites(string favourite)
        {
            using (StreamWriter sw = new StreamWriter(PATHFAVS, true))
            {
                sw.WriteLine($"{favourite}");
            }
        }

        private void CreateFileFavourites(string path)
        {
            File.Create(path).Close();

        }


        private string PATHTEAM = @".\team.txt";

        public void SetFavouriteTeam(Team selectedItem)
        {
            if (!File.Exists(PATHTEAM))
            {
                CreateFileFavourites(PATHTEAM);
            }

            FillFileTeam(selectedItem);
        }

        private void FillFileTeam(Team selectedItem)
        {
            if (selectedItem == null) 
            {
                return;
            }
            else
            File.WriteAllText(PATHTEAM, $"{selectedItem.DisplayName}");
        }

        public string GetFavouriteTeam()
        {
            var temp = File.ReadAllLines(PATHTEAM);

            if (!temp.Any())
            {
                File.WriteAllText(PATHTEAM, "Croatia (CRO)");
                return File.ReadAllText(PATHTEAM);
            }

            return temp[0];
        }

        
        public void ClearFavourites()
        {
            File.Delete(PATHFAVS);
        }

        public void ClearFavouriteTeam()
        {
            File.Delete(PATHTEAM);
        }

        public void RemoveFromFavourites(string text)
        {
            List<string> lines = File.ReadAllLines(PATHFAVS).ToList();

            if (lines.Contains(text))
            {
                lines.Remove(text);
                File.WriteAllLines(PATHFAVS, lines);
            }
        }


        //WPF--------------------------------------------------------------------------------------------------------------------
        private string pathWinFormsSettingsRelative = @"..\..\..\..\WinFormsApp\bin\Debug\net8.0-windows\settings.txt";
        private string pathWinFormsSettingsAbsolute;

        private string pathWinFormsTeam = @"..\..\..\..\WinFormsApp\bin\Debug\net8.0-windows\team.txt";

        private string pathWinFormsPics = @"..\..\..\..\WinFormsApp\bin\Debug\net8.0-windows\images";


        private string PATHWPF = @".\settingsWpf.txt";

        private string PATHTEAMWPF = @".\team.txt";

        private static int size;
        //WPF--------------------------------------------------------------------------------------------------------------------
        public void InitializeSettingsforWpf()
        {
            string[] lines = File.ReadAllLines(PATHWPF);

            string[] indexes = lines[1].Split(";");

            champions = int.Parse(indexes[0]);

            language = int.Parse(indexes[1]);

            size = int.Parse(indexes[2]);

        }

        public string[] PullSettings()
        {
            pathWinFormsSettingsAbsolute = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathWinFormsSettingsRelative));

            var lines = File.ReadAllLines(pathWinFormsSettingsAbsolute);

            if (!lines.Any())
            {
                return new[] { "0", "0" };
            }

            var indexes = lines[1].Split(';');
            return new[] { indexes[0], indexes[1] };

        }


        public int[] GetSettingsforWpf()
        {

            int[] settings = { champions, language, size};

            return settings;
        }

        public void SetSettingsforWpf(int[] settings)
        {
            if (!File.Exists(PATHWPF))
            {
                CreateFileforWpf();
            }

            FillFileforWpf(settings);

        }

        private void FillFileforWpf(int[] settings)
        {
            File.WriteAllText(PATHWPF, $"Championship;Language;WindowSize\n{settings[0]};{settings[1]};{settings[2]}");
            File.WriteAllText(pathWinFormsSettingsRelative, $"Championship;Language\n{settings[0]};{settings[1]}");
        }

        private void CreateFileforWpf()
        {
            File.Create(PATHWPF).Close();

            File.WriteAllText(PATHWPF, "Championship;Language;WindowSize\n" + PullSettings()[0] + ";" + PullSettings()[1] + ";0");


        }

        public bool FileExistsforWpf()
        {
            return File.Exists(PATHWPF);
        }

        public void SetFavouriteTeamforWpf(Team selectedItem)
        {
            if (!File.Exists(PATHTEAMWPF))
            {
                FillFileTeam(selectedItem);
                return;
            }

            FillFileTeamforWpf(selectedItem);
        }

        private void FillFileTeamforWpf(Team selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }
            else
            File.WriteAllText(PATHTEAMWPF, $"{selectedItem.DisplayName}");
        }

        public string WinFormsFavouriteTeam()
        {
            var temp = File.ReadAllLines(pathWinFormsTeam);

            if (!temp.Any())
            {
                File.WriteAllText(PATHTEAM, "Croatia (CRO)");
                return File.ReadAllText(PATHTEAM);
            }

            return temp[0];
        }

        public bool TeamFileExistsforWpf()
        {
            return File.Exists(PATHTEAM);
        }

        public string GetPlayerPicture(string playerNameNum)
        {
            var solutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            var defaultPath = Path.Combine(solutionDirectory, @"Images\default.png");
            var imagePath = Path.Combine(pathWinFormsPics, playerNameNum);

            return File.Exists(imagePath) ? imagePath : defaultPath;
        }


    }
}
