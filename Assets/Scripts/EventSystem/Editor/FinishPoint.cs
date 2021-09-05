using System;
using System.Linq;
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
        private NodeEvent _node;

        public NodeEvent Node => _node;

        public FinishPoint(Rect box, NodeEvent node)
        {
            Box = box;
            _color = Color.black;
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
            
            if (_node.Data.PrevNode.Count > 0)
            {
                _color = new Color32(1, 189, 232, 255);;
            }
            else
            {
                _color = Color.black;
            }
        }
    }
}