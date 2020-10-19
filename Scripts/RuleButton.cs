using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleButton : MonoBehaviour
{
    public int PlayerId;
    public void Rule(PlayerController PC)
    {
       
        GetComponent<Animator>().SetTrigger("click");
        GameManager.SpawnRules(PlayerId, PC);
    }

    public void RuleButtonActivator()
    {
        if (GameObject.Find("Rule" + PlayerId) == null)
        {
            GetComponent<Animator>().SetTrigger("click");
        }
       
        GameManager.SpawnRules(PlayerId, null);
    }
}
