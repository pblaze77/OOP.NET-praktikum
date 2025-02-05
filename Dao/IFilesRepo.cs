using Dao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public interface IFilesRepo
    {
        int[] GetSettings();
        void SetSettings(int[] settings);
        void InitializeSettings();
        bool FileExists();
        string GetPlayerPicture(string playerNameNum);
        List<string> GetFavourites();
        string GetFavouriteTeam();
        void SetFavourites(string favourite);
        void SetFavouriteTeam(Team selectedItem);
        void ClearFavouriteTeam();
        void ClearFavourites();
        void RemoveFromFavourites(string text);

        //WPF----------------------------------------------------------
        void InitializeSettingsforWpf();
        int[] GetSettingsforWpf();
        void SetSettingsforWpf(int[] settings);
        bool FileExistsforWpf();
        void SetFavouriteTeamforWpf(Team selectedItem);
        bool TeamFileExistsforWpf();
        string WinFormsFavouriteTeam();
        //WPF----------------------------------------------------------
    }
}
