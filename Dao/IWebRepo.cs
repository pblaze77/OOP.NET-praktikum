using Dao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public interface IWebRepo
    {
        Task<List<Player>> GetPlayers(string fifaCode);
        Task<List<Team>> GetChampionshipTeams();
        Task<List<Player>> GetStartingEleven(string fifaCode);
        Task<List<Squads>> GetEnemies(string fifaCode);
    }
}
