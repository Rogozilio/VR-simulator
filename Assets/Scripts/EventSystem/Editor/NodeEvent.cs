using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR.InteractionSystem;

namespace Editor
{
    [Serializable]
    public class NodeEvent
    {
        private Rect _box;
        private Rect _boxName;
        private Rect _boxFinish;
        private Rect[] _boxStart;
        private Rect[] _boxCondition;
        private Rect[] _boxAction;

        private float _width = 120f;
        private float _heightString = 20f;

        private string _name;
        private string[] _textCondition;
        private string[] _textAction;

        private Node _data;
        private StartPoint[] _startEdge;
        private FinishPoint _finishEdge;

        private bool _isBoxMove = false;

        public Rect Box => _box;
        public Node Data => _data;
        public StartPoint[] Start => _startEdge;
        public FinishPoint Finish => _finishEdge;


        public NodeEvent(Node data, Vector2 position)
        {
            _data = data;
            InitBox(position);
        }

        private void InitBox(Vector2 position)
        {
            InitBoxName(_data.Name, position);
            InitBoxConditions(_data.GetConditions(), position);
            InitBoxFinish(position);
            _box = new Rect(position
                , new Vector2(_width
                    , _heightString * (_boxCondition.Length + _boxAction.Length + 1)));
        }

        private void InitBoxName(string name, Vector2 position)
        {
            _name = name;
            _boxName = new Rect(position, new Vector2(_width, _heightString));
        }

        private void InitBoxConditions(List<Condition> conditions, Vector2 position)
        {
            if (conditions.Count > 0)
            {
                _textCondition = new string[conditions.Count];
                _boxCondition = new Rect[conditions.Count];
                _boxStart = new Rect[conditions.Count];
                _startEdge = new StartPoint[conditions.Count];

                for (int i = 0; i < conditions.Count; i++)
                {
                    Rect box = new Rect(
                        position + new Vector2(_width * 0.2f, _heightString * (i + 1))
                        , new Vector2(_width * 0.6f, _heightString));
                    _boxCondition[i] = box;
                    _textCondition[i] = conditions.ToArray()[i].Text;
                    InitBoxStart(box.position + new Vector2(box.width, 0), i);
                }
            }
            else
            {
                Rect box = new Rect(
                    position + new Vector2(_width * 0.2f, _heightString * 1)
                    , new Vector2(_width * 0.6f, _heightString));
                _boxCondition = new Rect[1];
                _textCondition = new string[1] {""};
                _boxCondition[0] = box;
                InitBoxStart(box.position + new Vector2(box.width, 0));
            }
        }

        private void InitBoxStart(Vector2 position, int i)
        {
            _boxStart[i] = new Rect(position + Vector2.one * 2,
                new Vector2(_width * 0.2f - 4, _heightString - 4));
            _startEdge[i] = new StartPoint(this, _boxStart[i], new Color(0, 41, 4, 1), i + 1);
        }

        private void InitBoxStart(Vector2 position)
        {
            _boxStart = new Rect[1];
            _startEdge = new StartPoint[1];
            _boxStart[0] = new Rect(position + Vector2.one * 2,
                new Vector2(_width * 0.2f - 4, _heightString - 4));
            _startEdge[0] = new StartPoint(this, _boxStart[0], new Color(0, 41, 4, 1), 0 + 1);
        }

        private void InitBoxFinish(Vector2 position)
        {
            if (_data.Conditions.Count != 0)
            {
                _boxFinish = new Rect(position + new Vector2(0, _boxName.height) + Vector2.one * 2,
                    new Vector2(_width * 0.2f - 4, _heightString * _boxCondition.Length - 4));
            }
            else
            {
                _boxFinish = new Rect(position + new Vector2(0, _boxName.height) + Vector2.one * 2,
                    new Vector2(_width * 0.2f - 4, _heightString - 4));
            }

            _finishEdge = new FinishPoint(_boxFinish, Color.black, this);

            _boxAction = new Rect[_data.GetActions().Count];
            _textAction = new string[_data.GetActions().Count];

            for (int i = 0; i < _data.GetActions().Count; i++)
            {
                InitBoxAction(position + new Vector2(0, _boxName.height + _boxFinish.height), i);
                _textAction[i] = _data.GetActions()[i].Method.Name;
            }
        }

        private void InitBoxAction(Vector2 position, int i)
        {
            Vector2 delta = new Vector2(0, _heightString * i + 4);
            Vector2 size = new Vector2(_width, _heightString);
            _boxAction[i] = new Rect(position + delta, size);
        }

        private void MoveBoxes(Event e)
        {
            _box.position += e.delta;
            _boxName.position += e.delta;

            for (int i = 0; i < _boxCondition.Length; i++)
            {
                _boxCondition[i].position += e.delta;
            }

            for (int i = 0; i < _boxAction.Length; i++)
            {
                _boxAction[i].position += e.delta;
            }

            _startEdge.ForEach(x => x.Box.position += e.delta);
            _finishEdge.Box.position += e.delta;
        }

        public void HandleEvents(Event e, bool isMoveAllBoxs)
        {
            _startEdge.ForEach(x => x.HandleEvents(e));
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (_boxName.Contains(e.mousePosition))
                    {
                        _isBoxMove = true;
                    }

                    break;
                case EventType.MouseUp:
                    _isBoxMove = false;
                    break;
                case EventType.MouseDrag:
                    if (isMoveAllBoxs)
                    {
                        MoveBoxes(e);
                    }
                    else if (_isBoxMove)
                    {
                        MoveBoxes(e);
                    }

                    break;
            }
        }

        private void ShowBox(Rect box, string name, Color color, int depth = 1)
        {
            GUI.depth = depth;
            GUI.color = color;
            GUI.Box(box, name);
        }

        private void ShowBox(Rect[] boxs, string[] name, Color color, int depth = 1)
        {
            GUI.depth = depth;
            GUI.color = color;
            for (int i = 0; i < boxs.Length; i++)
            {
                GUI.Box(boxs[i], name[i]);
            }
        }

        public void Show()
        {
            ShowBox(_box, "", new Color(255, 255, 255), 0);
            ShowBox(_boxName, _name, new Color(255, 255, 255));
            ShowBox(_boxCondition, _textCondition, new Color(200, 205, 255));
            ShowBox(_boxAction, _textAction, Color.cyan);
            _startEdge.ForEach(x => x.Show());
            _finishEdge.Show();
        }

        public NodeEvent Delete()
        {
            foreach (var node in Node.Nodes.Values)
            {
                if (node == _data)
                    node.IsUse = false;
            }

            return this;
        }
    }
}