using System.Collections.Generic;
using UnityEngine;


public class EventSystem : MonoBehaviour
{
    private Node[] _allNodes;
    private List<Node> _activeNodes = new List<Node>();

    private void Start()
    {
        _allNodes = MyAssetBundle.Load("nodes");

        foreach (Node node in _allNodes)
        {
            if (node.PrevNode.Count == 0
                && node.NextNode.Length > 0)
            {
                _activeNodes.Add(node);
            }
        }
    }

    void Update()
    {
        foreach (var node in _activeNodes)
        {
            if (node != null)
            {
                node.Launch();
                StartActions(node);

                if (node.NumberActiveCondition > 0)
                {
                    _activeNodes.Add(node.NextNode[node.NumberActiveCondition - 1]);
                    node.NumberActiveCondition = 0;
                    _activeNodes.Remove(node);
                    return;
                }
            }
        }
    }

    private void StartActions(Node node)
    {
        node.ActionVoid?.Invoke();
        Node.DelegateActionCorutin actionCorutin
            = node.ActionCorutin;
        if (actionCorutin != null)
        {
            StartCoroutine(actionCorutin());
        }
    }
}