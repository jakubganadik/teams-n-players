using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsNPlayers.Application.Individuals;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Application.Players
{
    public class Player
    {

        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public Guid IndividualId { get; set; }

        public Team? Team { get; set; }

        public Individual? Individual { get; set; }

        //public string TeamName { get; set; } = string.Empty;

        //public string PlayerName { get; set; } = string.Empty;

        public ushort ShirtNumber { get; set; }

        public string Position { get; set; } = string.Empty;
       
    }
}
