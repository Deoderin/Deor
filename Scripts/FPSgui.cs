using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSgui : MonoBehaviour
{
    float deltaTime = 0.0f;
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void OnGUI()
    {
        /* fps display */
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 10, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 1 / 120;
        style.normal.textColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps) + " " + Screen.width + "-" + Screen.height;
        GUI.Label(rect, text, style);
    }
    void Update()
    {
        /* fps info */
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
}
