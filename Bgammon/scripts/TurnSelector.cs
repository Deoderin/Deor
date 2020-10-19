using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSelector : MonoBehaviour
{
    public float alpha = 0;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.color = Color.Lerp(mat.color,new Color(mat.color.r, mat.color.g, mat.color.b, alpha),Time.deltaTime);
    }
}
