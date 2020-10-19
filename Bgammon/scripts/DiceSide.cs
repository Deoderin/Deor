using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool onGroud;
    public int sideValue;

    private void OnTriggerStay(Collider colider)
    {
        if (colider.tag == "Board")
        {
            onGroud = true; 
        }
    }
    private void OnTriggerExit(Collider colider)
    {
        if( colider.tag == "Board")
        {
            onGroud = false;
        }
    }

    public bool onGround()
    {
        return onGroud;
    }
}
