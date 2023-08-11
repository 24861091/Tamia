using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace 五子棋
{
    public static class ChessPanel
    {

        private static Grid _grid = new Grid();
        public static void DrawPanel()
        {
            _grid.Draw(7);
        }


    }

}
