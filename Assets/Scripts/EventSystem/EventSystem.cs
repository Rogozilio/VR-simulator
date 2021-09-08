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
            foreach (var next in node.NextNode)
            {
                if (next != "" && node.PrevNode.Count == 0)
                {
                    _activeNodes.Add(node);
                    break;
                }
            }
        }
    }

    private void Update()
    {
        foreach (var nodeActive in _activeNodes)
        {
            if (nodeActive != null)
            {
                nodeActive.Launch();
                StartActions(nodeActive);

                if (nodeActive.NumberActiveCondition > 0)
                {
                    foreach (var node in _allNodes)
                    {
                        if (node.name ==
                            nodeActive.NextNode[nodeActive.NumberActiveCondition - 1])
                        {
                            _activeNodes.Add(node);
                            break;
                        }
                    }

                    nodeActive.NumberActiveCondition = 0;
                    _activeNodes.Remove(nodeActive);
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