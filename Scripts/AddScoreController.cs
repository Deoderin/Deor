using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreController : MonoBehaviour
{
    public static bool Shower = false;
    public void StartShowScore()
    {
        Shower = true;
    }
    public void EndShowScore()
    {
        Shower = false;
        Destroy(gameObject);
    }

}
