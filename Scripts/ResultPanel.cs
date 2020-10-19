using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{

    public GameObject[] resultPlayers;
    [HideInInspector]
    public Text[] resultPlayersScores;
    private const int maxSizeHeigth = 460;
    public Image[] AlphaControl;
    public float alpha = 0;
    public int indexPlayer = -1;
    public Vector2[] place;
    public Sprite crown;

    // Start is called before the first frame update
    void Start()
    {
        resultPlayersScores = new Text[resultPlayers.Length];
        for (int i =0;i < resultPlayersScores.Length;i++)   
        {
            resultPlayersScores[i] = resultPlayers[i].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        }
            SetResultPanelInfo(place);
    }

    public void StartLoadNewGame()
    {
        transform.parent.parent.GetComponent<Animator>().enabled = true;
    }

    public void SetResultPanelInfo(Vector2[] place)
    {
        float Scale = maxSizeHeigth / (Mathf.Max(place[0].y));
        if ((Mathf.Max(place[0].y)) == 0) Scale = 1;  

        for (int i =0;i < place.Length;i++)
        {
            int id = GameManager.PlayersS.transform.GetChild((int)place[i].x).GetComponent<PlayerController>().id;

            if (place[i].x == indexPlayer) resultPlayersScores[id].transform.parent.GetComponent<Animator>().enabled = true;
            if (i == 0) resultPlayersScores[id].transform.parent.GetComponent<Image>().sprite = crown;


            if (place[i].x >= 0)
            {
                resultPlayers[id].SetActive(true);
                resultPlayersScores[id].text = place[i].y.ToString();
                resultPlayers[id].transform.SetSiblingIndex(i);
                resultPlayers[id].GetComponent<ResultSizeCatsle>().EndSize = place[i].y * Scale;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Text score in resultPlayersScores)
        {
            score.color = new Color(score.color.r, score.color.g, score.color.b, alpha);
        }
        foreach (Image img in AlphaControl)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }
    }
}
