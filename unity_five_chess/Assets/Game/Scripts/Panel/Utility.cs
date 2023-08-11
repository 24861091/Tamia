using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


namespace 五子棋
{
    public static partial class Utility
    {

        private static Vector2 _origin_point = Vector2.zero;
        private static float _x_axis = 1f;
        private static float _y_axis = 1f;

        public static Vector2 CoordToWorld(KeyValuePair<int, int> v2)
        {
            int i = v2.Key;
            int j = v2.Value;

            return new Vector2(_origin_point.x + (i + j) * _x_axis / 2f,
                _origin_point.y + (j - i) * _y_axis / 2f);
        }

        public static KeyValuePair<int, int> WorldToCoord(Vector2 v2)
        {
            double x = v2.x;
            double y = v2.y;

            x = x - _origin_point.x;
            y = y - _origin_point.y;


            double X = x / _x_axis - y / _y_axis;
            double Y = x / _x_axis + y / _y_axis;

            X = Mathf.Ceil((float)X);
            Y = Mathf.Floor((float)Y);

            return new KeyValuePair<int, int>((int)X, (int)Y);
        }

    }

}
