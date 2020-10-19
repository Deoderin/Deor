using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSizeCatsle : MonoBehaviour
{
    public float EndSize;
    RectTransform castleH;
    bool animationActive = false;
    // Start is called before the first frame update
    void Start()
    {
        castleH = transform.GetChild(0).GetComponent<RectTransform>();
        if (EndSize == 0) EndSize = castleH.sizeDelta.y;
        StartCoroutine(WaitAndPrint());

    }
    private IEnumerator WaitAndPrint()
    {

        yield return new WaitForSeconds(1.6f);
        animationActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (animationActive) castleH.sizeDelta = new Vector2(94, Mathf.Lerp(castleH.sizeDelta.y, EndSize, 0.04f));
    }
}
