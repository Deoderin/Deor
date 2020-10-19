using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleController : MonoBehaviour
{
    public int currentPage;
    public Text numberPageText;
    public Button Next;
    public Button Back;
    public int nextLoad;

    public Text info1;
    public Text info2;

    public Image ImageRules;

    public GameObject DotPrefab;
    public GameObject DotsContainers;

    public GameObject[] DotsDown;

    public PlayerController PC;

    public string[] TitlePages;
    public string[] InfoPages;
    public Sprite[] ImagePages;

    // Start is called before the first frame update
    void Start()
    {
        DotsDown = new GameObject[TitlePages.Length];
        for (int i =0;i < TitlePages.Length;i++)
        {
            DotsDown[i] = Instantiate(DotPrefab, DotsContainers.transform.GetChild(0));
        }

        TitlePages[0] = Localization.Title1;
        TitlePages[1] = Localization.Title2;
        TitlePages[2] = Localization.Title3;
        TitlePages[3] = Localization.Title4;
        TitlePages[4] = Localization.Title5;
        TitlePages[5] = Localization.Title6;
        TitlePages[6] = Localization.Title7;
        TitlePages[7] = Localization.Title8;
        TitlePages[8] = Localization.Title9;
        TitlePages[9] = Localization.Title10;

        InfoPages[0] = Localization.RulePage1;
        InfoPages[1] = Localization.RulePage2;
        InfoPages[2] = Localization.RulePage3;
        InfoPages[3] = Localization.RulePage4;
        InfoPages[4] = Localization.RulePage5;
        InfoPages[5] = Localization.RulePage6;
        InfoPages[6] = Localization.RulePage7;
        InfoPages[7] = Localization.RulePage8;
        InfoPages[8] = Localization.RulePage9;
        InfoPages[9] = Localization.RulePage10;

        currentPage = 1;
        numberPageText.text = currentPage.ToString();
        SetActivePagesButton();
        setActiveDot();
        SetPage(0);
    }

    private void setActiveDot()
    {

        for (int i = TitlePages.Length - 1; i >= 0; i--)
        {
            DotsDown[i].GetComponent<Animator>().SetBool("active", (i == (currentPage - 1)));
        }
    }

    public void SetPage(int nextPage)
    {
        currentPage += nextPage;
        currentPage = Mathf.Clamp(currentPage,1, ImagePages.Length);
        GetComponent<Animator>().SetTrigger((nextPage == 1) ? "next" : "back");
        setActiveDot();
        nextLoad = currentPage;
        GetComponent<Animator>().SetTrigger("SwapPage");
        SetActivePagesButton();
    }

    public void SetActivePagesButton()
    {
        Back.interactable = !(currentPage <= 1);
        Next.interactable = !(currentPage >= ImagePages.Length);
    }

    public void SetTextPage()
    {
        string title ="<b>" + TitlePages[currentPage - 1] + "</b> ";
        info1.text = title+ InfoPages[currentPage - 1];
        ImageRules.sprite = ImagePages[currentPage - 1];
        if(currentPage == 8)
        {
            ImageRules.GetComponent<RectTransform>().sizeDelta = new Vector2(978, 572);
            info2.text = "";
        }
        else if(currentPage == 9 || currentPage == 10)
        {
            ImageRules.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            info1.text = "";
            info2.lineSpacing = 2;
            info2.text = title + InfoPages[currentPage - 1];
        }
        else
        {
            ImageRules.GetComponent<RectTransform>().sizeDelta = new Vector2(795, 572);
            info2.text = "";
        }
        numberPageText.text = currentPage.ToString();
    }

    public void DestroyRules()
    {
        Destroy(gameObject);
        if(PC != null && PC.firstplayer) GameManager.StartAlertLoadGame(PC.id);    
    }
    public void CloseRules()
    {
        GetComponent<Animator>().SetTrigger("End");
    }
}
