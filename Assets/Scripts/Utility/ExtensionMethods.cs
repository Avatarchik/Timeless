﻿using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

    public static RectTransform rectTransform(this Transform transform){
        return transform as RectTransform;
    }
    public static Player GetSelf(this MonoBehaviour monoBehaviour){
        if ( GameManager.instance.player == null ) Log(monoBehaviour, "Returned a null value");
        return GameManager.instance.player;
    }

    public static void Log(this MonoBehaviour monoBehaviour, string text){
        if ( GameManager.instance.debug ) Debug.Log(text);
    }

}