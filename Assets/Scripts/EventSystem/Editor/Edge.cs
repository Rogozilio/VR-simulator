using System;
using System.Collections.Generic;
using PlasticGui.Help;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Editor
{
    [Serializable]
    public class Edge
    {
        private List<List<Vector2>> _allEdges = new List<List<Vector2>>();
        private List<Vector2> _edge;

        private float _offsetX;
        private float _offsetY;
        private double _size = 1;
        private Color _color = Color.yellow;

        public void Set(Vector2 start, Vector2 end)
        {
            Vector2 center = start + (end - start) / 2f;
            _edge = new List<Vector2>();
            _offsetX = center.x;
            _offsetY = center.y;
            GetSize(end.y - center.y);

            for (float x = start.x; x < end.x - 1; x++)
            {
                _edge.Add(new Vector2(x, Arctg(x)));
            }
        }

        private void GetSize(double y)
        {
            _size = (y) / Math.Atan(y);
            if (y < 0)
            {
                _size = -_size;
            }
            else if (y == 0)
            {
                _size = 0.00001f;
            }
        }

        private float Arctg(float x)
        {
            return (float) ((Math.Atan(x - _offsetX)
                             + _offsetY / _size) * _size);
        }

        public void Show()
        {
            if (_edge != null)
            {
                Handles.BeginGUI();
                Handles.color = _color;
                Vector2[] edge = _edge.ToArray();
                for (int i = 0; i < _edge.Count - 1; i++)
                {
                    Handles.DrawLine(edge[i]
                        , edge[i + 1], 2);
                }

                //Рисует круг в начальной точке линии
                if (_edge.Count > 3)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        Handles.DrawWireDisc(edge[0]
                            , Vector3.forward, i, 2);
                    }
                }

                Handles.EndGUI();
            }
        }
    }
}