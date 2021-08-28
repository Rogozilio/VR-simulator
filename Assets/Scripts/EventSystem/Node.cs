using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Component = UnityEngine.Component;

[CreateAssetMenu(menuName = "EventSystem/Node", order = 1), ExecuteInEditMode]
public class Node : ScriptableObject
{
    public static Dictionary<int, Node> Nodes = new Dictionary<int, Node>();

    public delegate void DelegateActionVoid();

    public delegate IEnumerator DelegateActionCorutin();

    private DelegateActionVoid _actionVoid = delegate { };

    private DelegateActionCorutin _actionCorutin = delegate { return null; };

    [SerializeField, HideInInspector]
    private string _nameGameObject;

    [SerializeField, HideInInspector]
    private string _nameScript;

    [SerializeField, HideInInspector]
    private List<string> _nameProperty = new List<string>();

    [SerializeField, HideInInspector]
    private List<string> _nameAction = new List<string>();

    public string Name = "Name";

    [HideInInspector]
    public bool IsUse = false;

    [HideInInspector]
    public Node[] NextNode;

    [HideInInspector]
    public List<Node> PrevNode = new List<Node>();

    private int _numberActiveCondition = 0;

    public int Number;

    [HideInInspector]
    public Vector2 EditorPosition;

    [HideInInspector]
    public List<Condition> Conditions = new List<Condition>();

    public string NameGameObject
    {
        get => _nameGameObject;
        set => _nameGameObject = value;
    }

    public string NameScript
    {
        get => _nameScript;
        set => _nameScript = value;
    }

    public List<string> NameProperty
    {
        get => _nameProperty;
        set => _nameProperty = value;
    }

    public List<string> NameAction
    {
        get => _nameAction;
        set => _nameAction = value;
    }

    public int NumberActiveCondition
    {
        get => _numberActiveCondition;
        set => _numberActiveCondition = value;
    }
    
    private void Awake()
    {
        AddInEditor(this);
    }

    private void OnEnable()
    {
        Name = name;
    }

    public DelegateActionVoid ActionVoid =>
        FindActions(_actionVoid) ? _actionVoid : null;

    public DelegateActionCorutin ActionCorutin =>
        FindActions(_actionCorutin) ? _actionCorutin : null;

    public void SetCondition(string nameProperty, Operator op, float result = 1)
    {
        Condition condition = new Condition(nameProperty, op, result);
        Conditions.Add(condition);
    }

    private void SetAction(DelegateActionVoid action)
    {
        _actionVoid -= action;
        _actionVoid += action;
    }

    private void SetAction(DelegateActionCorutin action)
    {
        _actionCorutin -= action;
        _actionCorutin += action;
    }

    public void SetActions(MethodInfo method)
    {
        Type type = method.ReturnType;
        if (type == typeof(void))
        {
            SetAction((DelegateActionVoid) Delegate.CreateDelegate(
                typeof(DelegateActionVoid), null, method));
        }
        else if (type == typeof(IEnumerator))
        {
            SetAction((DelegateActionCorutin) Delegate.CreateDelegate(
                typeof(DelegateActionCorutin), null, method));
        }
    }

    public void RemoveAction(MethodInfo method)
    {
        Type type = method.ReturnType;
        if (type == typeof(void))
        {
            _actionVoid -= (DelegateActionVoid) Delegate.CreateDelegate(
                typeof(DelegateActionVoid), null, method);
        }
        else if (type == typeof(IEnumerable))
        {
            _actionCorutin -= (DelegateActionCorutin) Delegate.CreateDelegate(
                typeof(DelegateActionCorutin), null, method);
        }
    }

    public void LogActions()
    {
        Debug.Log(_actionVoid.GetInvocationList().Length);
        Debug.Log(_actionCorutin.GetInvocationList().Length);
        foreach (var action in _actionVoid.GetInvocationList())
        {
            Debug.Log((action.Method.Name));
        }

        foreach (var action in _actionCorutin.GetInvocationList())
        {
            Debug.Log((action.Method.Name));
        }
    }

    public static void AddInEditor(Node newNode)
    {
        foreach (var node in Nodes)
        {
            if (node.Key == newNode.Number)
            {
                Nodes[node.Key] = newNode;
                return;
            }
        }
        newNode.Number = Nodes.Count;
        Nodes.Add(newNode.Number, newNode);
    }

    private float GetValueCondition(int numberProperty)
    {
        Type type = Type.GetType(_nameScript);
        Component component = GameObject.Find(_nameGameObject).GetComponent(type);
        float result = (float) GameObject.Find(_nameGameObject).GetComponent(type).GetType()
            .GetProperty(_nameProperty[numberProperty])
            ?.GetValue(component);
        return result;
    }

    public void Launch()
    {
        if (Conditions.Count == 0)
        {
            _numberActiveCondition = 1;
        }

        for (int i = 0; i < Conditions.Count; i++)
        {
            if (Conditions[i].GetResult(GetValueCondition(i)))
            {
                _numberActiveCondition = i + 1;
                break;
            }
        }
    }

    public List<Condition> GetConditions()
    {
        return Conditions;
    }

    public List<Delegate> GetActions()
    {
        List<Delegate> actions = new List<Delegate>();
        for (int i = 1; i < _actionVoid.GetInvocationList().Length; i++)
        {
            actions.Add(_actionVoid.GetInvocationList()[i]);
        }

        for (int i = 1; i < _actionCorutin.GetInvocationList().Length; i++)
        {
            actions.Add(_actionCorutin.GetInvocationList()[i]);
        }

        return actions;
    }

    private bool FindActions(Delegate action)
    {
        if (action.GetInvocationList().Length <= 1)
        {
            bool isActionSet = false;
            Type typeComponent = Type.GetType(_nameScript);
            MethodInfo[] methods = GameObject.Find(_nameGameObject).GetComponent(typeComponent)
                .GetType()
                .GetMethods();

            foreach (var nameAction in _nameAction)
            {
                foreach (var method in methods)
                {
                    if (method.Name == nameAction
                        && method.ReturnType == action.Method.ReturnType)
                    {
                        SetActions(method);
                        isActionSet = true;
                        break;
                    }
                }
            }

            return isActionSet;
        }
        else
        {
            return false;
        }
    }
}