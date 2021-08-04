using System.Collections;
using System.Collections.Generic;
using Editor;
using PlasticGui.Help;
using UnityEditor;
using UnityEngine;

public class StartPoint
{
    public Rect Box;
    private Color _color;
    private Edge _edge;
    private bool _isUse;
    private int _number;
    private Vector2 _startMousePos;
    private NodeEvent _node;

    public NodeEvent Node => _node;
    public int Number => _number;

    public bool IsUse
    {
        get => _isUse;
        set => _isUse = value;
    }

    public StartPoint(NodeEvent node, Rect box, Color color, int number)
    {
        Box = box;
        _color = color;
        _isUse = false;
        _node = node;
        _number = number;
    }

    public void HandleEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (Box.Contains(e.mousePosition))
                {
                    _startMousePos 
                        = new Vector2(Box.center.x
                            , Box.center.y);
                    _edge = new Edge();
                }
                    
                break;
            case EventType.MouseDrag:
                if (e.button == 0 && _edge != null) 
                {
                    _edge.Set(_startMousePos, e.mousePosition);
                }
                
                break;
            case EventType.MouseUp:
                _edge = null;
                break;
        }
    }

    public void Show()
    {
        GUI.color = _color;
        GUI.Box(Box, "");
        if (_edge != null)
        {
            _edge.Show();
        }
    }
}