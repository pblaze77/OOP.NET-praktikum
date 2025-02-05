using Dao.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dao
{
    internal class WebRepo : IWebRepo
    {
        RestClient client = new RestClient("https://worldcup-vua.nullbit.hr/");
        IFilesRepo repo = RepoFactory.GetRepo();


        public async Task<List<Player>> GetPlayers(string fifaCode)
        {

            var request = repo.GetSettings()[0] switch
            {
                0 => new RestRequest($"/men/matches/country?fifa_code={fifaCode}"),
                1 => new RestRequest($"/women/matches/country?fifa_code={fifaCode}"),
                _ => new RestRequest("/men/matches/country?fifa_code=KOR")
            };

            var response = await client.ExecuteGetAsync(request);

            var data = JsonConvert.DeserializeObject<List<Squads>>(response.Content);

            var match = data.FirstOrDefault();

            var homeTeamStats = match?.HomeTeam.Code == fifaCode
                ? match.home_team_statistics
                : match?.AwayTeam.Code == fifaCode
                    ? match.away_team_statistics
                    : match?.home_team_statistics;

            var startingEleven = homeTeamStats?.starting_eleven.Select(player => new Player
            {
                Captain = player.captain,
                Name = player.name,
                Position = player.position,
                ShirtNumber = player.shirt_number
            }).ToList() ?? new List<Player>();

            var substitutes = homeTeamStats?.substitutes.Select(player => new Player
            {
                Captain = player.captain,
                Name = player.name,
                Position = player.position,
                ShirtNumber = player.shirt_number
            }).ToList() ?? new List<Player>();

            var allPlayers = startingEleven.Concat(substitutes).ToList();

            return allPlayers;

        }
        public async Task<List<Team>> GetChampionshipTeams()
        {
            var request = repo.GetSettings()[0] switch
            {
                0 => new RestRequest("/men/teams/results"),
                1 => new RestRequest("/women/teams/results"),
                _ => new RestRequest("/men/teams/results")
            };

            var response = await client.ExecuteGetAsync<Team>(request);

            var data = JsonConvert.DeserializeObject<List<Team>>(response.Content);

            data.ForEach(item => item.DisplayName = $"{item.Country} ({item.FifaCode})");

            return data;
        }

        public async Task<List<Squads>> GetEnemies(string fifaCode)
        {
            var request = repo.GetSettings()[0] switch
            {
                0 => new RestRequest($"/men/matches/country?fifa_code={fifaCode}"),
                1 => new RestRequest($"/women/matches/country?fifa_code={fifaCode}"),
                _ => new RestRequest("/men/matches/country?fifa_code=KOR")
            };

            var response = await client.ExecuteGetAsync(request);

            var data = JsonConvert.DeserializeObject<List<Squads>>(response.Content);

            return data;
        }

        public async Task<List<Player>> GetStartingEleven(string fifaCode)
        {
            var request = repo.GetSettings()[0] switch
            {
                0 => new RestRequest($"/men/matches/country?fifa_code={fifaCode}"),
                1 => new RestRequest($"/women/matches/country?fifa_code={fifaCode}"),
                _ => new RestRequest("/men/matches/country?fifa_code=KOR")
            };

            var response = await client.ExecuteGetAsync(request);

            var data = JsonConvert.DeserializeObject<List<Squads>>(response.Content);

            var match = data.FirstOrDefault();

            var homeTeamStats = match?.HomeTeam.Code == fifaCode
                ? match.home_team_statistics
                : match?.AwayTeam.Code == fifaCode
                    ? match.away_team_statistics
                    : match?.home_team_statistics;

            var startingElevenplayers = homeTeamStats?.starting_eleven.Select(player => new Player
            {
                Captain = player.captain,
                Name = player.name,
                Position = player.position,
                ShirtNumber = player.shirt_number
            }).ToList() ?? new List<Player>();

            return startingElevenplayers;
        }
    }
}
