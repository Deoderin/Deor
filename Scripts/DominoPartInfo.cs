using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoPartInfo : MonoBehaviour
{
    public int value;
    public bool doubleDomino;
    //1 left 2 right
    public int DirectionSide;
    public Vector3 DoubleAlternativePositon;
    public Vector3 StandartPosition;
    public Vector3 StandartRotation;
    public GameObject mainParent;
    // Start is called before the first frame update
    void Start()
    {
        StandartPosition = transform.localPosition;
        StandartRotation = transform.localRotation.eulerAngles;
    }

    public void SetAlternativePos(bool set)
    {
        if(!doubleDomino)
        {
            transform.localPosition = set ? DoubleAlternativePositon : StandartPosition;
            transform.localRotation = set ? Quaternion.Euler(StandartRotation + new Vector3(0, 0, 90)) : Quaternion.Euler(StandartRotation);
        }
       
    }
}
