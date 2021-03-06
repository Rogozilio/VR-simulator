using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;
using Object = UnityEngine.Object;

namespace Editor
{
    public class MainWindowEventSystem : EditorWindow
    {
        private List<NodeEvent> _nodeEvents;

        private StartPoint _startPoint;
        private FinishPoint _finishPoint;
        private Edge _edge = new Edge();
        private bool _isMoveAllBoxs = false;

        [MenuItem("Window/EventSystem")]
        public static MainWindowEventSystem OpenMainWindowEdit()
        {
            return GetWindow<MainWindowEventSystem>("Event System");
        }

        private void OnEnable()
        {
            _nodeEvents = new List<NodeEvent>();

            NodeEditor.AddInEditor();

            Node[] newNodes = MyAssetBundle.Load("nodes");
            if (newNodes != null)
            {
                foreach (var node in newNodes)
                {
                    if (node.IsUse)
                        OnAddNode(Node.Nodes[node.name], node.EditorPosition);
                }
            }
            else
            {
                throw new Exception("Nodes empty or not found");
            }
        }

        private void OnGUI()
        {
            EditorGUIUtility.AddCursorRect(
                new Rect(0, 0, Screen.width, Screen.height),
                (_isMoveAllBoxs) ? MouseCursor.Pan : MouseCursor.Arrow);


            HandleEvent(Event.current);

            ShowNode();
            ShowEdge();

            Repaint();
        }

        private void HandleEvent(Event e)
        {
            foreach (var node in _nodeEvents)
            {
                if (node != null)
                {
                    node.HandleEvents(e, _isMoveAllBoxs);
                    switch (e.type)
                    {
                        case EventType.KeyDown:
                            if (e.keyCode == KeyCode.Delete
                                && node.Box.Contains(e.mousePosition))
                            {
                                node.Delete();
                                _nodeEvents.Remove(node);

                                return;
                            }

                            break;
                        case EventType.MouseDown:
                            foreach (var start in node.Start)
                            {
                                if (start.Box.Contains(e.mousePosition))
                                {
                                    _startPoint = start;
                                    start.IsUse = true;
                                    return;
                                }
                            }

                            _startPoint = null;
                            break;
                        case EventType.MouseUp:
                            if (node.Finish.Box.Contains(e.mousePosition)
                                && _startPoint != null && _startPoint.Node != node)
                            {
                                _finishPoint = node.Finish;
                                AddLinkNode(_startPoint, _finishPoint);
                                _startPoint = null;
                            }

                            break;
                    }
                }
            }

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                    {
                        OpenContextMenu(e.mousePosition);
                    }

                    break;
                case EventType.KeyDown:
                    if (e.control && e.keyCode == KeyCode.S)
                    {
                        foreach (var node in Node.Nodes)
                        {
                            EditorUtility.SetDirty(node.Value);
                        }

                        MyAssetBundle.Save();
                        Debug.Log("Save");
                    }

                    if (e.keyCode == KeyCode.Space && e.isKey)
                    {
                        _isMoveAllBoxs = true;
                    }

                    break;
                case EventType.KeyUp:
                    if (e.keyCode == KeyCode.Space && e.isKey)
                    {
                        _isMoveAllBoxs = false;
                    }

                    break;
            }
        }

        private void OpenContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            foreach (var node in Node.Nodes.Values)
            {
                if (!node.IsUse)
                {
                    genericMenu.AddItem(new GUIContent(node.name)
                        , false, () => OnAddNode(node, mousePosition));
                }
                else
                {
                    genericMenu.AddItem(new GUIContent(node.name + "(Used)")
                        , false, func: null);
                }
            }

            genericMenu.ShowAsContext();
        }

        private void OnAddNode(Node node, Vector2 mousePosition)
        {
            node.IsUse = true;
            node.EditorPosition = mousePosition;
            NodeEvent nodeEvent = new NodeEvent(node, mousePosition);
            _nodeEvents.Add(nodeEvent);
        }

        private void ShowNode()
        {
            foreach (var node in _nodeEvents)
            {
                node?.Show();
            }
        }

        private void AddLinkNode(StartPoint start, FinishPoint finish)
        {
            start.Node.Data.NextNode[start.Number - 1] = finish.Node.Data.name;
            if (finish.Node.Data.PrevNode != null &&
                !finish.Node.Data.PrevNode.Contains(start.Node.Data.name))
                finish.Node.Data.PrevNode.Add(start.Node.Data.name);
        }

        private void ShowEdge()
        {
            foreach (var node in _nodeEvents)
            {
                for (int i = 0; i < node?.Data.NextNode.Length; i++)
                {
                    if (node.Data.NextNode[i] != null)
                    {
                        Vector2 start = node.Start[i].Box.center;
                        foreach (var node2 in _nodeEvents)
                        {
                            if (node2.Data.name == node.Data.NextNode[i])
                            {
                                Vector2 finish = _nodeEvents.First
                                    (x => x.Data.name == node2.Data.name).Finish.Box.center;
                                _edge.Set(start, finish);
                                _edge.Show();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var node in Node.Nodes.Values)
            {
                node.IsUse = false;
            }
        }
    }
}