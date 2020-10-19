using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotController : MonoBehaviour
{

    public Vector3 dest;
    public Image r;
    public bool mode;
    public int winplayerIndex;
    PlayerController PC;
    public RoundEndAlarmController reac;
    // Start is called before the first frame update
    void Start()
    {
        if (mode)
        {
            PC = GameManager.PlayersS.transform.GetChild(winplayerIndex).GetComponent<PlayerController>();
            reac.TrueScore-=10;
            reac.UpdateTExt();
        }

        r = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPos = !mode ? Vector3.Lerp(transform.localPosition, dest, 0.1f) : Vector3.Lerp(transform.position, dest, 0.1f);
        float dst  = Vector3.Distance(nextPos, dest);
        if (!mode) transform.localPosition = nextPos;
        else transform.position = nextPos;
        if (dst <= 60.6f)
        {
            if(!mode)
            {
                reac.TrueScore+=10;
                reac.UpdateTExt();
            }
            else
            {
                int prevtextscore = int.Parse(PC.ScoreText.text);
                if (prevtextscore < PC.WinScore) PC.ScoreText.text = (prevtextscore + 1).ToString();
            }
            Destroy(gameObject);
        }
        r.color = new Color(1, 1, 1, dst / 1000);
    }
}
