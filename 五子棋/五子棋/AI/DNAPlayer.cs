using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public class DNAPlayer : AIPlayer
    {
        private 棋子[][] _positions = null;
        private DNA _dna = null;

        public override void GameStart(棋子[][] positions)
        {
            _positions = positions;
            _dna = new DNA("DNAPlayer/" + Name, Name);
        }

        public override AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            AIResult result = new AIResult();
            
            return result;
        }
    }
}
