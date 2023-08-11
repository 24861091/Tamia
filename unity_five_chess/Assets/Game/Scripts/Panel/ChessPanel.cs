using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace 五子棋
{
    public class ChessPanel : MonoBehaviour
    {
        private Grid _grid = new Grid();
        private void Start()
        {
            _grid.Draw(10);
        }


    }

}
