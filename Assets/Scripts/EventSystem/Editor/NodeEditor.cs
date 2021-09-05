using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEditor;
using Valve.Newtonsoft.Json.Utilities;
using Valve.VR.InteractionSystem;

[CustomEditor(typeof(Node))]
[CanEditMultipleObjects]
public class NodeEditor : UnityEditor.Editor
{
    private Node _node;

    private int _indexGameObj = 0;
    private int _indexScript = 0;
    private List<int> _indexProperty;
    private List<int> _indexAction;

    private string[] _optionsGameObj;
    private string[] _optionsScript;
    private string[] _optionsProperty;
    private string[] _optionsAction;

    private MonoBehaviour[] _gameObjects;
    private MonoBehaviour[] _scripts;
    private PropertyInfo[] _property;
    private List<MethodInfo> _action;

    private void OnEnable()
    {
        _node = target as Node;
        _indexProperty = new List<int>();
        _indexAction = new List<int>();
        _action = new List<MethodInfo>();
        SetGameObjects();
        if (!string.IsNullOrEmpty(_node.NameGameObject))
        {
            _indexGameObj = Array.IndexOf(_optionsGameObj, _node.NameGameObject);
            SetScripts();

            if (!string.IsNullOrEmpty(_node.NameScript))
            {
                _indexScript = Array.IndexOf(_optionsScript, _node.NameScript);
                SetProperty();
                foreach (var property in _node.NameProperty)
                {
                    _indexProperty.Add(Array.IndexOf(_optionsProperty, property));
                }

                SetAction();
                foreach (var action in _node.NameAction)
                {
                    _indexAction.Add(Array.IndexOf(_optionsAction, action));
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _node = target as Node;

        ShowGameObjects();

        GUILayout.Space(20);
        if (GUILayout.Button("Save", GUILayout.Height(30f)))
        {
            SetMarker(_node);
            if (_node.Conditions.Count == 0)
                _node.NextNode = new[] {""};
            else
                _node.NextNode = new string[_node.Conditions.Count];

            for (int i = 0; i < _node.Conditions.Count; i++)
            {
                if (_indexProperty[i] > 0)
                    _node.NameProperty[i] = _property[_indexProperty[i] - 1].Name;
                else
                    _node.NameProperty[i] = "";
            }

            for (int i = 0; i < _node.NameAction.Count; i++)
            {
                if (_indexAction[i] > 0)
                    _node.NameAction[i] = _action[_indexAction[i] - 1].Name;
                else
                    _node.NameAction[i] = "";
                _node.SetActions(_action[_indexAction[i] - 1]);
            }

            EditorUtility.SetDirty(_node);
            AddInEditor();
        }
    }

    private void SetGameObjects()
    {
        _gameObjects = new MonoBehaviour[FindObjectsOfType(typeof(MonoBehaviour)).Length];
        _optionsGameObj
            = new string[_gameObjects.Length + 1];
        for (int i = 0; i < _gameObjects.Length; i++)
        {
            _gameObjects[i] = FindObjectsOfType(typeof(MonoBehaviour))[i] as MonoBehaviour;
            _optionsGameObj[i + 1] = _gameObjects[i].name;
        }
    }

    private void SetScripts()
    {
        _node.NameGameObject = _gameObjects[_indexGameObj - 1].name;
        _scripts =
            _gameObjects[_indexGameObj - 1].GetComponents<MonoBehaviour>();
        _optionsScript = new string[_scripts.Length + 1];
        for (int i = 0; i < _scripts.Length; i++)
        {
            _optionsScript[i + 1] = _scripts[i].GetType().ToString();
        }
    }

    private void SetProperty()
    {
        _property = _scripts[_indexScript - 1].GetType().GetProperties(BindingFlags.DeclaredOnly |
            BindingFlags.Public | BindingFlags.Instance);
        List<string> optionsProperty = new List<string>();
        optionsProperty.Add(null);
        foreach (var property in _property)
        {
            optionsProperty.Add(property.Name);
        }

        _optionsProperty = optionsProperty.ToArray();
    }

    private void SetAction()
    {
        MethodInfo[] action = _scripts[_indexScript - 1].GetType()
            .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        foreach (MethodInfo act in action)
        {
            if (!act.Name.StartsWith("set_") && !act.Name.StartsWith("get_"))
                _action.Add(act);
        }

        string[] optionsMethod = new string[_action.Count + 1];
        optionsMethod[0] = null;
        for (int i = 1; i < optionsMethod.Length; i++)
        {
            optionsMethod[i] = _action[i - 1].Name;
        }

        _optionsAction = optionsMethod;
    }

    private void ShowGameObjects()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("GameObject: ", GUILayout.Width(EditorGUIUtility.labelWidth));
        _indexGameObj =
            EditorGUILayout.Popup(_indexGameObj, _optionsGameObj);
        GUILayout.EndHorizontal();
        ShowScripts();
    }

    private void ShowScripts()
    {
        if (_indexGameObj != 0)
        {
            SetScripts();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Script: ", GUILayout.Width(EditorGUIUtility.labelWidth));
            _indexScript =
                EditorGUILayout.Popup(_indexScript, _optionsScript);
            GUILayout.EndHorizontal();
            if (_indexScript != 0)
            {
                ShowProperty();
                ShowAction();
            }
        }
    }

    private void ShowProperty()
    {
        GUILayoutOption[] options = {GUILayout.MaxWidth(100f)};
        GUILayout.Space(20);
        EditorGUI.indentLevel++;
        GUILayout.Label("CONDITIONS: ");
        _node.NameScript = _scripts[_indexScript - 1].GetType().Name;
        for (int i = 0; i < _node.Conditions.Count; i++)
        {
            GUILayout.BeginHorizontal();

            SetProperty();

            _indexProperty[i] =
                EditorGUILayout.Popup(_indexProperty[i], _optionsProperty, options);
            _node.Conditions[i].Value.NameProperty = _node.NameProperty[i];
            _node.Conditions[i].Value.Opertor
                = (Operator) EditorGUILayout.EnumPopup(_node.Conditions[i].Value.Opertor,
                    options);
            _node.Conditions[i].Value.Value
                = EditorGUILayout.FloatField(_node.Conditions[i].Value.Value, options);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _node.Conditions.RemoveAt(i);
                _indexProperty.RemoveAt(i);
                _node.NameProperty.RemoveAt(i);
            }

            if (i < _node.Conditions.Count - 1)
            {
                GUILayout.Box("", GUILayout.Width(40f));
            }
            else if (GUILayout.Button("Add", GUILayout.Width(40)))
            {
                _node.SetCondition(_node.NameProperty.ToString(), Operator.Equally, 1);
                _node.NameProperty.Add("");
                _indexProperty.Add(0);
            }

            GUILayout.EndHorizontal();
        }

        if (_node.Conditions.Count == 0 &&
            GUILayout.Button("Add condition"))
        {
            _node.SetCondition(_node.NameProperty.ToString(), Operator.Equally, 1);
            _node.NameProperty.Add("");
            _indexProperty.Add(0);
        }

        EditorGUI.indentLevel--;
    }

    private void ShowAction()
    {
        GUILayout.Space(20);
        EditorGUI.indentLevel++;
        GUILayout.Label("ACTION: ");
        SetAction();
        
        for (int i = 0; i < _indexAction.Count; i++)
        {
            GUILayout.BeginHorizontal();
            _indexAction[i] = EditorGUILayout.Popup(_indexAction[i], _optionsAction,
                GUILayout.MaxWidth(306f));
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _node.RemoveAction(_action[_indexAction[i]]);
                _indexAction.RemoveAt(i);
                _node.NameAction.RemoveAt(i);
            }

            if (i < _indexAction.Count - 1)
            {
                GUILayout.Box("", GUILayout.Width(40f));
            }
            else if (GUILayout.Button("Add", GUILayout.Width(40)))
            {
                _indexAction.Add(0);
                _node.NameAction.Add("");
            }
            
            GUILayout.EndHorizontal();
        }

        EditorGUI.indentLevel--;
        if (_indexAction.Count == 0 &&
            GUILayout.Button("Add action"))
        {
            _node.NameAction.Add("");
            _indexAction.Add(0);
        }
    }

    public static void SetMarker(Node node)
    {
        string path = AssetDatabase.GetAssetPath(node);
        var importer = AssetImporter.GetAtPath(path);
        importer.assetBundleName = "nodes";
    }

    public static void AddInEditor()
    {
        Node[] nodes = MyAssetBundle.GetNodesAsset();
        for (int i = 0; i < nodes.Length; i++)
        {
            if (Node.Nodes.ContainsKey(nodes[i].name))
            {
                Node.Nodes[nodes[i].name] = nodes[i];
            }
            else
            {
                Node.Nodes.Add(nodes[i].name, nodes[i]);
            }
        }
    }
}