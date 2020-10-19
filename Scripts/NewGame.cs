using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    Transform button;
    // Start is called before the first frame update
    void Start()
    {
        button = transform.Find("Button");
    }
    public void LoadNewGame()
    {
        GameManager.LoadNewGame();
    }
    // Update is called once per frame
   public void LoadAnimator()
    {
        GetComponent<Animator>().SetTrigger("endgame");
    }

    public void LoadAnimatorResult()
    {
        for(int i =0; i < transform.GetChild(0).childCount;i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<Animator>().enabled = true;
        } 
    }
    private void Update()
    {
        if(button != null) button.Rotate(new Vector3(0, 0, 0.5f), Space.Self);
    }
}
