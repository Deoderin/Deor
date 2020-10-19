using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRound : MonoBehaviour
{
    public int startindex;
 
    // Update is called once per frame
    public void LoadNewRound()
    {
        int z = Random.Range(0, GameManager.PlayersS.transform.childCount);
        Debug.LogError("z - " + z);
        Loader(z, startindex);

    }

    public void LoadNewRoundSave(int index, int east)
    {
        Loader(index, east);
    }


    public void Loader(int index,int east)
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("Domino");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        //по стороне света в зависимости от победы менять или нет

        GameManager.spawnerS.GetComponent<Spawner>().Spawn(east);
        for (int i = 0; i < GameManager.PlayersS.transform.childCount; i++)
        {
            GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().ScoreUpdate(GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().WinScore);
        }


        GameManager.StartGame(index, east);
        var counters = GameObject.FindGameObjectsWithTag("Counter");
        for (int i = 0; i < counters.Length; i++)
        {
            Destroy(counters[i]);
        }
        Saveloader.SaveFile.RoundLoad = false;
        
        GameManager.GM.UpdateScoreSticks();
    }
}
