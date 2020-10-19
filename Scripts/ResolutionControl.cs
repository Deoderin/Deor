using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionControl : MonoBehaviour
{
    public static bool isFullScreen;
    public Camera main;
    public Transform Canvas;
    public GameObject miniB;
    public static int startload;
    public static ResolutionControl RC;
    public inception RotateBoard;
    // Start is called before the first frame update
    void Start()
    {
        RC = this;
           startload = 0;
       if (!Application.isEditor)
        {
           
        }
        main.orthographicSize = 11;
        isFullScreen = false;
    }

    public void FullscreenCall()
    {
        startload = 1;
    }
    void Update()
    {
        if (Screen.width > 2500)
        {
            //GameObject spawner = GameObject.Find("Spawner");
            if (!isFullScreen)
            {
                if (!inception.opened)
                {
                    main.orthographicSize = 20;
                    isFullScreen = true;
                    RotateBoard.Switch();
                }
                else main.orthographicSize = 17;
                // if (spawner != null) spawner.GetComponent<Spawner>().enabled = true;
                // miniB.SetActive(false);
            }
            isFullScreen = true;
      
        }
        else
        {
            if (isFullScreen)
            {
                main.orthographicSize = 11;
                // miniB.SetActive(true);
            }
          //  miniB.transform.SetAsLastSibling();
            isFullScreen = false;
          
        }
    }
}
