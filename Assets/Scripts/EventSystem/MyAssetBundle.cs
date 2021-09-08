using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MyAssetBundle
{
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

            Node[] nodes = myLoadedAssetBundle.LoadAllAssets<Node>();
            myLoadedAssetBundle.Unload(false);
            return nodes;
        }

        return null;
    }
    // public static Node[] GetNodesAsset()
    // {
    //     List<Node> nodes = new List<Node>();
    //     string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Nodes"});
    //     foreach (string SOName in assetNames)
    //     {
    //         var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
    //         nodes.Add(AssetDatabase.LoadAssetAtPath<Node>(SOpath));
    //     }
    //
    //     return nodes.ToArray();
    // }
}