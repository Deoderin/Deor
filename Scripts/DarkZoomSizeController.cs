using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkZoomSizeController : MonoBehaviour
{
    public AnimationCurve Scale;

    // Update is called once per frame
    void Update()
    {
        if(Screen.width > 2500 && Camera.main != null)
        {
            float scale = Scale.Evaluate(Camera.main.orthographicSize);
            transform.localScale = new Vector3(scale, scale, 1);
        }
       
    }
}
