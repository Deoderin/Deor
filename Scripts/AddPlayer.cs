using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPlayer : MonoBehaviour
{
    public int playerId;
    public Color miplecolors;
    public GameObject HUD;
    public GameObject PlayerPrefab;
    public Color PlayerColor;
    public Vector3 HUdPosition;
    public Vector3 ScorePosition;
    public Vector3 PlayerPostion;
    public int HudRotation;
    public float HUdAlpha = 0.8f;
    public bool created;
    public bool rot;
    public bool canStart;
  
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("rot", rot);
        GetComponent<Image>().color = PlayerColor;
    }

    public void UpdateText()
    {
        foreach (GameObject addbut in GameObject.FindGameObjectsWithTag("startbut"))
        {
            if (addbut.GetComponent<AddPlayer>().created)
            {
                if(addbut == gameObject)
                {
                    if (GameManager.PlayersS.transform.childCount <= 1)
                    {
                       // addbut.transform.GetChild(0).GetComponent<Text>().text = Localization.WaitSecondEnemies;
                    }
                    else
                    {
                        canStart = true;
                        addbut.transform.GetChild(0).GetComponent<Text>().text = Localization.StartT;
                    }
                }
            else
                {
                    addbut.GetComponent<Animator>().SetTrigger("changeText");
                }
            }
        }
    }
 
    public void withoutAnimationTextChange()
    {
        if (GameManager.PlayersS.transform.childCount <= 1)
        {
           // transform.GetChild(0).GetComponent<Text>().text = Localization.WaitSecondEnemies;
        }
        else
        {
            canStart = true;
            transform.GetChild(0).GetComponent<Text>().text = Localization.StartT;
        }
    }

    public static void DisableStartButtons()
    {
        foreach (GameObject addbut in GameObject.FindGameObjectsWithTag("startbut"))
        {
            addbut.GetComponent<Animator>().SetTrigger("start");
        }
    }

    public void AddPlayers(bool save)
    {
        if(!created)
        {
            created = true;
            GetComponent<Animator>().SetTrigger("add");
            GameObject hud = Instantiate(HUD, HUdPosition, Quaternion.Euler(0, 0, HudRotation), transform.parent.parent);
            GameObject.Find("StartDarkRound").transform.SetAsLastSibling();
            hud.GetComponent<HudAlpha>().playerID = playerId;
            GameObject player = Instantiate(PlayerPrefab, PlayerPostion, Quaternion.Euler(0, HudRotation, 0), GameObject.Find("Players").transform);
            player.name = "Player" + playerId;
            PlayerController PC = player.GetComponent<PlayerController>();
            PC.MeepleRot = HudRotation;
            PC.HUdPosition = HUdPosition;
            PC.id = playerId;
            PC.playerColor = PlayerColor;
            PC.meepleColor = miplecolors;
            PC.HUD = hud;
            PC.WinScore = (int)Mathf.Ceil(Random.Range(1000, 3000) / 10f) * 10;
            //PC.WinScore = 2000;
            PC.loadSave = save;
            PC.HUD.transform.GetChild(0).GetChild(0).GetChild(1).transform.localPosition = ScorePosition;


            PC.HUD.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<RuleButton>().PlayerId = playerId;
            hud.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = miplecolors;
            PC.turnIndicator = hud.transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
            if (playerId == 1 || playerId == 3)
            {
                PC.turnIndicator.transform.localPosition = new Vector3(-PC.turnIndicator.transform.localPosition.x, PC.turnIndicator.transform.localPosition.y, PC.turnIndicator.transform.localPosition.z);
            }
       
            if (GameManager.ActivePlayerID == -1)
            {
                PC.firstplayer = true;
                GameManager.ActivePlayerID = playerId;
                hud.GetComponent<Animator>().SetBool("select", true);
            }
           // Saveloader.SaveFile.PlayerCreated[playerId] = true;
         //   Saveloader.SaveFile.PlayerCreatedindex[playerId] = (GameManager.PlayersS.transform.childCount - 1);
        }
        //else if(canStart)
        //{
        //    //GetComponent<Button>().enabled = false;
        //    //GameManager.StartGame();
        //    //DisableStartButtons();
        //}
    }
}
