using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{

    public void Click()
    {
        if (!Application.isEditor)
        {
            if (GetComponent<Button>().enabled && GetComponent<Button>().interactable)
            {
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }

}
