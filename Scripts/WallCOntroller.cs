using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCOntroller : MonoBehaviour
{
    public List<GameObject> floor1 = new List<GameObject>();
    public List<GameObject> floor2 = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStart()
    {
        //for (int i = 0;i < floor1.Count;i++)
        //{
        //    floor1[i].transform.localPosition = new Vector3(i*0.33f,0, 0.02f);
        //    floor2[i].transform.localPosition = new Vector3(i * 0.33f, 0.2f, 0);
        //}
    }
}
