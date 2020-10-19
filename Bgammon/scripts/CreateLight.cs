using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class CreateLight : MonoBehaviour
{
    public GameObject[] lightPositionBlack;
    public GameObject[] lightPositionWhite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Generator gen = gameObject.GetComponent<Generator>();
        for (int i = 0; i < 24; i++)
        {
            if (gen.moveBlack == true)
            {
                if (i == gen.lightPos2Black || i == gen.lightPos1Black || i == gen.lightPosSumBlack)
                {
                    lightPositionBlack[i].SetActive(true);
                }
                if (gen.move == 4 && i == gen.lightPosSumX2Black)
                {
                    lightPositionBlack[i].SetActive(true);
                }
                if (gen.pressing == false)
                {
                    lightPositionBlack[i].SetActive(false);
                }
            }
            if (gen.moveBlack == false)
            {
                if (i == gen.lightPos2White || i == gen.lightPos1White || i == gen.lightPosSumWhite)
                {
                    lightPositionWhite[i].SetActive(true);
                }
                if (gen.move == 4 && i == gen.lightPosSumX2White)
                {
                    lightPositionWhite[i].SetActive(true);
                }
                if (gen.pressing == false)
                {
                    lightPositionWhite[i].SetActive(false);
                }
            }
        }
    }
}
