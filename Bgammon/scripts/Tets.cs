using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Tets : MonoBehaviour
{
    public GameObject chase;
    public GameObject chips;
    // Start is called before the first frame update
    void Start()
    {
        chase.transform.position = Vector3.Lerp(chase.transform.position, chips.transform.position, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //chase.transform.position = Vector3.Lerp(chase.transform.position, chips.transform.position, 0.2f);
    }
    
}
