using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardTimer : MonoBehaviour
{
    public GameObject tile;
    public bool MahjongSteel;
    // Start is called before the first frame update
    void Start()
    {

    }
    

    public void NextTurn()
    {

        for (int i = 0; i < GameManager.PlayersS.transform.childCount; i++)
        {
            int id = GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().id;
            Animator AM = GameObject.Find("PB" + id).transform.GetChild(0).GetComponent<Animator>();
            if (!AM.GetBool("Success"))
            {
              
                AM.SetBool("Show", false);
                AM.SetBool("Success", false);
            }
           
        }
        if(!MahjongSteel)
        {
            if (GameManager.GState != GameManager.GameState.Steel)
            {
                tile.GetComponent<CellDragging>().State = CellDragging.DominoState.DiscardAbsolute;
                Saveloader.SaveFile.DiscardTiles.Add(tile.GetComponent<CellDragging>().PosInWall);
                GetComponent<Animator>().SetTrigger("end");
                Debug.LogWarning("rew1");
                tile.transform.GetChild(0).GetComponent<Animator>().SetBool("active", false);
                GameManager.nextTurn(true, -1);
            }
        }
        else
        {
            tile.transform.GetChild(0).GetComponent<Animator>().SetBool("active", false);
            Debug.LogWarning("rew2");
            GetComponent<Animator>().SetTrigger("end");
            tile.GetComponent<CellDragging>().OpenSet = false;
           tile.GetComponent<CellDragging>().State = CellDragging.DominoState.InHand;
            GameManager.spawnerS.GetComponent<Spawner>().AddTile(GameManager.ActivePlayerIndex);
           GameManager.GM.StartHCoroutine(GameManager.ActivePlayerIndex, -1);
        }
       

    }
    public void DestroyObj()
    {
        tile.GetComponent<Rigidbody>().isKinematic = true;
        
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = GameManager.GM.MainCam.GetComponent<Camera>().WorldToScreenPoint(tile.transform.position);
    }
}
