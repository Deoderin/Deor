using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    int x = 0;
    Quaternion end;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, end, 2f * Time.deltaTime);
    }
    public void Switch()
    {
        x += 90;
        end = Quaternion.Euler(90, 90 + x, 0); 
    }
}
