using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    class Players
    {
        private List<ChessPlayer> players = new List<ChessPlayer>();
        public void Register(ChessPlayer player )
        {
            if(player != null && !players.Contains(player))
            {
                players.Add(player);
            }
        }

        public ChessPlayer [] ChessPlayers
        {
            get
            {
                return players.ToArray();
            }
        }

    }
}
