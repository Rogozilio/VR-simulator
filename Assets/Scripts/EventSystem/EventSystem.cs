using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    private List<Node> ActiveNodes = new List<Node>();
    private int[,] linkNode;

    private void Start()
    {
        foreach (var node in Resources.LoadAll<Node>("Nodes/"))
        {
            Node.AddInEditor(node);
        }

        SaveLoad system = new SaveLoad();
        linkNode = system.LoadData();
        for (int i = 0; i < linkNode.GetLength(0); i++)
        {
            bool isNodeFinisFree = true;
            bool isNodeStartFree = true;

            for (int j = 0; j < linkNode.GetLength(1); j++)
            {
                if (linkNode[j, i] > 0)
                {
                    isNodeFinisFree = false;
                    break;
                }

                if (linkNode[i, j] > 0)
                {
                    isNodeStartFree = false;
                }
            }

            if (isNodeFinisFree && !isNodeStartFree)
            {
                ActiveNodes.Add(Node.Nodes[i]);
            }
        }
    }

    void Update()
    {
        foreach (var node in ActiveNodes)
        {
            node.Launch();
            StartActions(node);

            if (node.NumberActiveCondition > 0)
            {
                for (int i = 0; i < linkNode.GetLength(0); i++)
                {
                    if (linkNode[node.Number, i] == node.NumberActiveCondition)
                    {
                        ActiveNodes.Add(Node.Nodes[i]);
                    }
                }

                node.NumberActiveCondition = 0;
                ActiveNodes.Remove(node);
                return;
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