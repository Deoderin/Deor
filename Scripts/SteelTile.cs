using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelTile : MonoBehaviour
{
    public int id;
    Animator AM;
    // Start is called before the first frame update
    void Start()
    {
        AM = GetComponent<Animator>();
    }

    public void Steel()
    {
        GameManager.GState = GameManager.GameState.Steel;
        GameManager.GM.DiscardPlayerId = id;
        GameObject.FindGameObjectWithTag("Discard").GetComponent<Collider>().enabled = false;
        GameObject.FindGameObjectWithTag("Timer").GetComponent<Animator>().SetTrigger("end");
        AM.SetBool("Success", true);
        AM.SetBool("Show",false);
    }

    public void DisableSBool()
    {
        AM.SetBool("Success", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
