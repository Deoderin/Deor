using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndAlarmController : MonoBehaviour
{
    public PlayerController pc;
    public bool winner;
    public bool east;
    public int Score;
    public int TrueScore;
    public int EndScore;
    public Text text;
    public int WinplayerIndex;
    public GameObject Dots;
    public bool looser;
    public GameManager GM;
    public PlayerController PC;
    public string LooserText = "";
    Transform spawncontainer;
    // Start is called before the first frame update
    void Start()
    {
        spawncontainer = GameObject.Find("InvisibleContainer").transform;
        PC = GameManager.PlayersS.transform.GetChild(WinplayerIndex).GetComponent<PlayerController>();
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        TrueScore = 0;
        UpdateTExt();
    }
    public void UpdateTExt()
    {
        text.text = Localization.InyourHand + " " + TrueScore + " " + Localization.Scores + "\n";
        text.text += winner ? (east ? Localization.WinText2 : Localization.WinText) : pc.looserAlertROundFinall;
    }
    public IEnumerator Highlight()
    {
        for (int i = 0; i < (EndScore/10); i++)
        {
            CellDragging CD = pc.transform.GetChild(Random.Range(0, pc.transform.childCount-2) + 1).GetComponent<CellDragging>();
           
                GameObject dot = Instantiate(Dots, Camera.main.WorldToScreenPoint(CD.transform.position) + new Vector3(Random.Range(-30, 30), Random.Range(-70, 70), 0), Quaternion.identity, this.transform);
                dot.GetComponent<DotController>().dest = new Vector3(100, 100, 0);
                dot.GetComponent<DotController>().reac = this;
                yield return new WaitForSeconds(0.02f);
          
        }
        if(winner) GetComponent<Animator>().SetTrigger("winner");

        if (looser) StartCoroutine(Breakime());
    }

    public IEnumerator Breakime()
    {
        yield return new WaitForSeconds(2.5f);

        foreach (GameObject gm in GameObject.FindGameObjectsWithTag("alertROund"))
        {
            RoundEndAlarmController rac = gm.GetComponent<RoundEndAlarmController>();
            rac.GetComponent<Animator>().SetTrigger("end");
        }
    }
    public void StartSpawnDots()
    {
        if (pc.transform.childCount - 1 > 0)
        {
            StartCoroutine(Highlight());
        }
    }

    //public IEnumerator HighlightEnd()
    //{
    //    Vector3 winnerpos = PC.HUD.transform.GetChild(0).GetChild(0).GetChild(1).transform.position;
    //    EndScore = TrueScore + 0;
    //    for (int j = 0; j < EndScore; j++)
    //    {
    //        GameObject dot = Instantiate(Dots, transform.position + new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0), Quaternion.identity, spawncontainer);
    //        DotController Dc = dot.GetComponent<DotController>();
    //        Dc.reac = this;
    //        Dc.mode = true;
    //        Dc.winplayerIndex = WinplayerIndex;
    //        Dc.dest = winnerpos;
    //        yield return new WaitForSeconds(0.04f);
    //    }
    //    yield return new WaitForSeconds(0.5f);
    //    GetComponent<Animator>().SetTrigger("end");
    //}

    public void DestroyAlarm()
    {
        for (int i = 0; i < GameManager.PlayersS.transform.childCount; i++)
        {
            GameManager.PlayersS.transform.GetChild(i).GetComponent<PlayerController>().StartNewROund();
        }
        if (looser)
        {
            if (GameManager.Round == 8)
            {
                GM.GameEndAnimation();
            }
            else
            {
                GameObject.Find("Mahjong(Clone)").GetComponent<MahjongAnimation>().EndAnimation();
                GM.RoundLoader.GetComponent<Animator>().SetTrigger("LoadRound");
            }
        }
        Destroy(gameObject);
    }
}
