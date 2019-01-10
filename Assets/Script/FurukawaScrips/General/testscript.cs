using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour {

    float time = 1;
    float nowtime = 0;

    Vector3 start;
    Vector3 end;

    // Use this for initialization
    void Start ()
    {
        start = this.transform.position;
        end = this.transform.position - Vector3.up;
    }

    // Update is called once per frame
    void Update ()
    {
        nowtime += Time.deltaTime;


        Debug.Log(nowtime);

        if(nowtime<time)
        {
            this.transform.position = Vector3.Lerp(start, end, nowtime);
        }
        

    }
}
