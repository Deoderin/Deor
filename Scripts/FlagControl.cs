using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagControl : MonoBehaviour
{
    public GameObject flag;
    public Image flagImage;
    public float Color = 0;
    // Start is called before the first frame update
    void Start()
    {
        flag = transform.GetChild(1).gameObject;
        flagImage = flag.GetComponent<Image>();
        StartCoroutine(StartAnimation(Random.Range(0,20)/80+1.5f));
    }

    private IEnumerator StartAnimation(float waitTime)
    {
            yield return new WaitForSeconds(waitTime);
        flag.GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        flagImage.color = new Color(flagImage.color.r, flagImage.color.g, flagImage.color.b, Color);
    }
}
