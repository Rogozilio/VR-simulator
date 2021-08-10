using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

namespace Editor
{
    public class MainWindowEventSystem : EditorWindow
    {
        private NodeEvent[] _nodeEvents = new NodeEvent[Node.Nodes.Count];

        private int[,] _linkNodes
            = new int[Node.Nodes.Count, Node.Nodes.Count];


        private StartPoint _startPoint;
        private FinishPoint _finishPoint;
        private Edge _edge = new Edge();

        [MenuItem("Window/EventSystem")]
        public static MainWindowEventSystem OpenMainWindowEdit()
        {
            return GetWindow<MainWindowEventSystem>("Event System");
        }

        private void OnEnable()
        {
            bool isNodesNull;
            //Удаление нод из словаря при существовании null нод
            do
            {
                isNodesNull = false;
                foreach (var node in Node.Nodes)
                {
                    int key = (node.Value == null) ? node.Key : -1;
                    if (key > -1)
                    {
                        for (; key < Node.Nodes.Count - 1; key++)
                        {
                            Node.Nodes[key] = Node.Nodes[key + 1];
                            Node.Nodes[key].Number = key;
                        }

                        Node.Nodes.Remove(Node.Nodes.Count - 1);
                        //Debug.Log("Delete node " + node.Value.Name);
                        isNodesNull = true;
                        break;
                    }
                }
            } while (isNodesNull);

        }

        private void OnGUI()
        {
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
                    node.HandleEvents(e);
                    switch (e.type)
                    {
                        case EventType.KeyDown:
                            if (e.keyCode == KeyCode.Delete
                                && node.Box.Contains(e.mousePosition))
                            {
                                int value = Node.Nodes
                                    .FirstOrDefault(x => x.Value == node.Data).Key;
                                node.Delete();
                                _nodeEvents[value] = null;
                                foreach (int index in Node.Nodes.Keys)
                                {
                                    if (_linkNodes[value, index] > 0)
                                    {
                                        _nodeEvents[index].Finish.IsUse = false;
                                    }

                                    _linkNodes[index, value] = 0;
                                    _linkNodes[value, index] = 0;
                                }

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
                                node.Finish.IsUse = true;
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
                        Debug.Log("Save");
                        SaveLoad system = new SaveLoad();
                        system.SaveData(_linkNodes);
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
                    genericMenu.AddItem(new GUIContent(node.Name)
                        , false, () => OnAddNode(node, mousePosition));
                }
                else
                {
                    genericMenu.AddItem(new GUIContent(node.Name + "(Used)")
                        , false, func: null);
                }
            }

            genericMenu.ShowAsContext();
        }

        private void OnAddNode(Node node, Vector2 mousePosition)
        {
            node.IsUse = true;
            NodeEvent nodeEvent = new NodeEvent(node, mousePosition);
            _nodeEvents[Node.Nodes.FirstOrDefault(x
                => x.Value == nodeEvent.Data).Key] = nodeEvent;
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
            int i = Node.Nodes.FirstOrDefault(x => x.Value == start.Node.Data).Key;
            int j = Node.Nodes.FirstOrDefault(x => x.Value == finish.Node.Data).Key;

            _linkNodes[i, j] = start.Number;
        }

        private void ShowEdge()
        {
            for (int i = 0; i < _linkNodes.GetLength(0); i++)
            {
                for (int j = 0; j < _linkNodes.GetLength(1); j++)
                {
                    if (_linkNodes[i, j] > 0)
                    {
                        _edge.Set(_nodeEvents[i].Start[_linkNodes[i, j] - 1].Box.center
                            , _nodeEvents[j].Finish.Box.center);
                        _nodeEvents[j].Finish.IsUse = true;
                        _edge.Show();
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