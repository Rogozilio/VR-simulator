using System;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    [Serializable]
    public class FinishPoint
    {
        public Rect Box;
        private Color _color;
        private Edge _edge;
        private bool _isUse;
        private NodeEvent _node;

        public NodeEvent Node => _node;
        public bool IsUse
        {
            get => _isUse;
            set
            {
                _isUse = value;
                if (_isUse)
                {
                    _color = Color.yellow;
                }
                else
                {
                    _color = Color.black;
                }
            }
        }

        public FinishPoint(Rect box, Color color, NodeEvent node)
        {
            Box = box;
            _color = color;
            _node = node;
        }

        public void Show()
        {
            Handles.color = _color;
            GUI.color = Color.red;
            GUI.Box(Box, "");
            for (int i = 1; i < 4; i++)
            {
                Handles.DrawWireDisc(Box.center
                    , Vector3.forward, i, 2);
            }
        }
    }
}