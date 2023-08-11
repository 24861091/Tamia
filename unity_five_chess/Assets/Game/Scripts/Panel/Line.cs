using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace 五子棋
{
    public class Line
    {
        private LineRenderer lineRenderer = new LineRenderer();
        private Vector3[] points;
        private Color color;
        private float lineWidth = 0.05f;
        private Material mateial;
        
        public Line(LineRenderer renderer, Vector3[] vs, Color color)
        {
            this.lineRenderer = renderer;
            this.points = vs;
            this.color = color;
            this.mateial = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
            Reset();
        }

        public void Reset()
        {
            if (lineRenderer != null && points != null)
            {
                lineRenderer.useWorldSpace = false;
                lineRenderer.positionCount = points.Length;
                lineRenderer.material = this.mateial;
                lineRenderer.startColor = this.color;
                lineRenderer.endColor = this.color;
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
                lineRenderer.SetPositions(points);
            }
        }

        public void Enable()
        {
            if(lineRenderer != null)
            {
                lineRenderer.gameObject.SetActive(true);
            }
        }

        public void Disable()
        {
            if (lineRenderer != null)
            {
                lineRenderer.gameObject.SetActive(false);
            }
        }

    }

}
