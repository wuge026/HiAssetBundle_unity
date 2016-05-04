﻿//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;
using HiAssetBundle;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ResourceMgr
{

    private static ResourceMgr instance;
    public static ResourceMgr Instance
    {
        get
        {
            if (instance == null)
                instance = new ResourceMgr();
            return instance;
        }
    }
    /// <summary>
    /// Important: paramName must include extension, for example "tank.prefab"
    /// </summary>
    /// <param name="paramFolder"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    public Object GetObj(string paramFolder, string paramName)
    {
#if UNITY_EDITOR
        var temp = "Assets/" + paramFolder + "/" + paramName;
        return AssetDatabase.LoadAssetAtPath(temp, typeof(Object));
#endif
        paramFolder = paramFolder.Replace("/", "");
        paramName = paramName.Remove(paramName.LastIndexOf("."));
        AssetBundle tempAB = AssetBundleMgr.GetAssetBundle(paramFolder);
        if (tempAB != null && tempAB.Contains(paramName))
            return tempAB.LoadAsset(paramName);

        Debug.LogError("the things you want to load is null");
        return null;
    }

    public GameObject GetGameObject(string paramFolder, string paramName)
    {
        var temp = GetObj(paramFolder, paramName);
        if (temp != null)
            return (GameObject)MonoBehaviour.Instantiate(temp);
        return null;
    }
}