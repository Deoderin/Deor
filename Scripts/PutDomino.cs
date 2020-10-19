using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutDomino : MonoBehaviour
{

    public void PutOneDomino()
    {
        if(GameObject.Find("StartDarkRound").GetComponent<Image>().color.a == 0)
        {
            if (transform.parent.parent.parent.GetComponent<HudAlpha>().playerID == GameManager.PlayersS.transform.GetChild(GameManager.ActivePlayerIndex).GetComponent<PlayerController>().id)
            {
               // GameObject.Find("Spawner").GetComponent<Spawner>().AddDominoToPlayer(transform.parent.parent.parent.GetComponent<HudAlpha>().playerID);
            }
        }
        for (int i = 0; i < GameManager.PlayersS.transform.childCount; i++)
        {
            GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().DominoCountText.text = GameObject.Find("Spawner").GetComponent<Spawner>().CellsCountSpawn.Count.ToString();
        }
    }
}
