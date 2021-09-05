using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MyAssetBundle
{
    public static void Save()
    {
        string assetBundleDirectory = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
    }

    public static Node[] Load(string nameAsset)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "AssetBundles", nameAsset);
        if (File.Exists(path))
        {
            AssetBundle myLoadedAssetBundle
                = AssetBundle.LoadFromFile(path);

            if (myLoadedAssetBundle == null)
            {
                myLoadedAssetBundle.Unload(false);
                throw new Exception("Failed to load AssetBundle!");
            }

            Node[] nodesAssetBundle = myLoadedAssetBundle.LoadAllAssets<Node>();
            myLoadedAssetBundle.Unload(false);
            if (nodesAssetBundle != null)
            {
                foreach (var nodeAB in nodesAssetBundle)
                {
                    bool isNodeExist = false;
                    foreach (Node node in GetNodesAsset())
                    {
                        if (node.name == nodeAB.name)
                        {
                            isNodeExist = true;
                            node.NextNode = nodeAB.NextNode;
                            node.PrevNode = nodeAB.PrevNode;
                            EditorUtility.SetDirty(node);
                            break;
                        }
                    }

                    if (!isNodeExist)
                    {
                        Node newScriptableObject = new Node();
                        EditorUtility.CopySerialized(nodeAB, newScriptableObject);
                        AssetDatabase.CreateAsset(newScriptableObject,
                            Path.Combine("Assets/Nodes", nodeAB.name + ".asset"));
                        AssetDatabase.SaveAssets();
                        NodeEditor.SetMarker(newScriptableObject);
                    }
                    NodeEditor.AddInEditor();
                }
            }

            return nodesAssetBundle;
        }

        return null;
    }

    public static Node[] GetNodesAsset()
    {
        List<Node> nodes = new List<Node>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Nodes"});
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            nodes.Add(AssetDatabase.LoadAssetAtPath<Node>(SOpath));
        }

        return nodes.ToArray();
    }
}