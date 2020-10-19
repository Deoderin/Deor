using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Suspq : MonoBehaviour
{    
    public static void Suspend()
    {
        //Disable objects
        if(GameObject.Find("Suspend").transform.Find("Scene") != null)
        {
            GameObject.Find("Suspend").transform.Find("Scene").gameObject.SetActive(false);
        }
      
        Time.timeScale = 0;
    }
    public static void On()
    {
        //Enable objects
        if (GameObject.Find("Suspend").transform.Find("Scene") != null)
        {
            GameObject.Find("Suspend").transform.Find("Scene").gameObject.SetActive(true);
        }
        Time.timeScale = 1;   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Suspend();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            On();
        }
    }
}
