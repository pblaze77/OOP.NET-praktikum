using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Models
{
    public class Squads
    {
        [JsonProperty("home_team")]
        public Specs? HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public Specs? AwayTeam { get; set; }

        [JsonProperty("home_team_statistics")]
        public Team_Statistics home_team_statistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public Team_Statistics away_team_statistics { get; set; }

        [JsonProperty("home_team_country")]
        public string HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country")]
        public string AwayTeamCountry { get; set; }

        [JsonProperty("home_team_events")]
        public List<TeamEvent> HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public List<TeamEvent> AwayTeamEvents { get; set; }
    }

    public partial class TeamEvent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type_of_event")]
        public string TypeOfEvent { get; set; }

        [JsonProperty("player")]
        public string Player { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }

    public partial class Specs
    {
        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("goals")]
        public long? Goals { get; set; }

        [JsonProperty("penalties")]
        public long? Penalties { get; set; }
    }

    public class Team_Statistics
    {
        public string country { get; set; }
        public Starting_Eleven[] starting_eleven { get; set; }
        public Substitute[] substitutes { get; set; }
    }

    public class Starting_Eleven
    {
        public string name { get; set; }
        public bool captain { get; set; }
        public int shirt_number { get; set; }
        public string position { get; set; }
    }

    public class Substitute
    {
        public string name { get; set; }
        public bool captain { get; set; }
        public int shirt_number { get; set; }
        public string position { get; set; }
    }


}
