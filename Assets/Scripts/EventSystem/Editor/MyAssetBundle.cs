using System;
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

            Node[] nodes = myLoadedAssetBundle.LoadAllAssets<Node>();
            myLoadedAssetBundle.Unload(false);
            return nodes;
        }

        return null;
    }
}