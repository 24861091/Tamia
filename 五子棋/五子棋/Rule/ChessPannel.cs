using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    class ChessPannel
    {
        public 棋子 [][] positions;
        private int sizeX;
        private int sizeY;
        private List<Position> blackList = new List<Position>();
        private List<Position> whiteList = new List<Position>();

        private List<Position> _blackList = new List<Position>();
        private List<Position> _whiteList = new List<Position>();

        public List<Position> BlackList
        {
            get
            {
                _blackList.Clear();
                _blackList.AddRange(blackList);
                return _blackList;
            }
        }
        public List<Position> WhiteList
        {
            get
            {
                _whiteList.Clear();
                _whiteList.AddRange(whiteList);
                return _whiteList;
            }
        }


        private 棋子[][] _positions;
        public 棋子[][] Positions
        {
            get
            {
                for(int i= 0; i < _positions.Length; i ++)
                {
                    for(int j = 0; j < _positions[0].Length; j ++)
                    {
                        _positions[i][j] = positions[i][j];
                    }
                }

                return _positions;
            }
        }

        private List<Position> mark = new List<Position>();
        public List<Position> Mark
        {
            get
            {
                return mark;
            }
        }

        public void Initialize(int sizeX, int sizeY)
        {
            positions = null;
            positions = new 棋子[sizeX][];
            _positions = new 棋子[sizeX][];
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            for(int i = 0; i < sizeX; i ++)
            {
                positions[i] = new 棋子[sizeY];
                _positions[i] = new 棋子[sizeY];
                for (int j = 0; j < sizeY; j++)
                {
                    positions[i][j] = 棋子.无;
                    _positions[i][j] = 棋子.无;
                }
            }
            blackList.Clear();
            whiteList.Clear();

            mark.Add(new Position());
        }
        public void Clear()
        {
            Initialize(sizeX, sizeY);
        }
        public 棋子 GetSide(int x, int y)
        {
            if (x < 0 || x >= sizeX) return 棋子.无;
            if (y < 0 || y >= sizeY) return 棋子.无;
            return positions[x][y];
        }

        public void MakeStep(int x, int y, 棋子 side)
        {
            if (x < 0 || x >= sizeX) return;
            if (y < 0 || y >= sizeY) return;

            positions[x][y] = side;
            if(side == 棋子.白子)
            {
                whiteList.Add(new Position(x,y));
            }
            else if(side == 棋子.黑子)
            {
                blackList.Add(new Position(x,y));
            }
            mark[0] = new Position(x, y);
        }

        public bool Has(int x, int y)
        {
            if (x < 0 || x >= sizeX) return false;
            if (y < 0 || y >= sizeY) return false;

            return positions[x][y] != 棋子.无;
        }
    }
    public struct Position
    {
        public int X;
        public int Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
