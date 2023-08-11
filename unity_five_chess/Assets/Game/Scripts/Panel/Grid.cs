using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace 五子棋
{
    public class Grid 
    {
        private GameObject root;

        private Terrain owner;
        private float z;
        private Color defaultColor;
        private Color axisColor;
        
        
        public Grid(float z = 0f, GameObject root = null)
        {            
            this.root = new GameObject("Grid");
            this.root.transform.position = new Vector3();
            this.root.transform.localScale = Vector3.one;
            this.root.transform.rotation = new Quaternion();
            if (root != null)
            {
                this.root.transform.parent = root.transform;
            }
            
            this.z = z;

            defaultColor = new Color(0.1f, 0.1f, 0.1f);
            axisColor = new Color(0.8f, 0.8f, 0.8f);
        }
        
        public void Draw(int n)
        {
            for (int i = -n; i <= n; i ++)
            {
                KeyValuePair<int,int> s = new KeyValuePair<int, int>(i, -n);
                KeyValuePair<int,int> e = new KeyValuePair<int, int>(i, n);
                
                Vector3 start = Utility.CoordToWorld(s);
                Vector3 end = Utility.CoordToWorld(e);
                
                start = new Vector3(start.x , start.y, z);
                end = new Vector3(end.x, end.y, z);
                CreateLine(start, end, i == 0);
            }
            
            for (int i = -n; i <= n; i ++)
            {
                KeyValuePair<int,int> s = new KeyValuePair<int, int>(-n, i);
                KeyValuePair<int,int> e = new KeyValuePair<int, int>(n, i);
            
                Vector3 start = Utility.CoordToWorld(s);
                Vector3 end = Utility.CoordToWorld(e);
                
                start = new Vector3(start.x, start.y, z);
                end = new Vector3(end.x, end.y, z);
                CreateLine(start, end, i == 0);
            }

        }
        public void DestroyAllLines()
        {
            if(root != null)
            {
                for(int i = 0, ie = root.transform.childCount; i < ie; i ++)
                {
                    GameObject.DestroyImmediate(root.transform.GetChild(0).gameObject);
                }
            }
        }

        public void ReDraw()
        {
            DestroyAllLines();
            Draw(1);
        }

        private Line CreateLine(Vector3 v1, Vector3 v2, bool isAxis = false)
        {
            GameObject go = new GameObject();
            go.transform.parent = root.transform;
            LineRenderer renderer = go.AddComponent<LineRenderer>();

            Line line = new Line(renderer, new Vector3[] { v1, v2 }, isAxis ? axisColor : defaultColor);
            return line;
        }
        
        public void Show(bool show)
        {
            if (root != null)
            {
                root.SetActive(show);
            }
        }

        public void SetOffset(Vector3 offset)
        {
            root.transform.localPosition = offset;
        }

    }

}
