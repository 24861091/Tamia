using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public class _10岁 : AIPlayer
    {
        private 棋子[][] _positions = null;
        public override void GameStart(棋子[][] positions)
        {
            _positions = positions;
            DNA dna = new DNA("Base");
        }

        public override AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            return new AIResult();
        }
    }
}
