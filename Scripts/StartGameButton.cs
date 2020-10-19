using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public AddPlayer[] players;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if(ResolutionControl.isFullScreen)
        {
            GameManager.spawnerS.GetComponent<Spawner>().Spawn(-1);

            Destroy(gameObject);
        }
      
    }
}
