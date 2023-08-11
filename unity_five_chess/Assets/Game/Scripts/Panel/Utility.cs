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
            return new Vector2(_origin_point.x + (i) * _x_axis, _origin_point.y + (j) * _y_axis);
        }


    }

}
