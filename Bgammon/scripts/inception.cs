using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inception : MonoBehaviour
{
    public GameObject board;
    public GameObject cup;
    public GameObject[] turnIndicator;

    public Vector3 BoardPos;
    public Vector3 RotateAngle;
    public static bool opened;
    bool rotate;
    bool open;
   
    // Start is called before the first frame update
    void Start()
    {
        opened = false;
    }
    private IEnumerator StartShower(float waitTimeRotation, float waitTimeOopen)
    {
        rotate = true;
            yield return new WaitForSeconds(waitTimeRotation);
        open = true;
        turnIndicator[0].GetComponent<Animator>().enabled = true;
        turnIndicator[1].GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(waitTimeOopen);
       
        rotate = false;
        open = false;
        GetComponent<Choise>().Deactiv();

    }
    // Update is called once per frame
    void Update()
    {
        if(rotate)
        {
            board.transform.rotation = Quaternion.Lerp(board.transform.rotation, Quaternion.Euler(0, -90, 0), 3 * Time.deltaTime);
            board.transform.position = Vector3.Lerp(board.transform.position, BoardPos, 3f * Time.deltaTime);
        }
        if(open)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 17, Time.deltaTime);

            cup.transform.localRotation = Quaternion.Lerp(cup.transform.localRotation, Quaternion.Euler(RotateAngle), 1.4f * Time.deltaTime);
        }
    }
    public void Switch()
    {
        if(!opened)
        {
            if (!ResolutionControl.isFullScreen)
            {
                ResolutionControl.RC.FullscreenCall();
            }
            StartCoroutine(StartShower(1.4f, 4f));
        }
        opened = true;
    }
}
