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

        public void Show(Texture2D texture)
        {
            Handles.color = _color;
            GUIStyle style = new GUIStyle();
            style.normal.background = texture;
            GUI.Box(Box, "", style);
            for (int i = 1; i < 4; i++)
            {
                Handles.DrawWireDisc(Box.center
                    , Vector3.forward, i, 2);
            }
        }
    }
}