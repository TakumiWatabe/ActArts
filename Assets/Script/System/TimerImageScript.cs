using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerImageScript : MonoBehaviour {

    //プレイ中の制限時間を画像に反映させる
    [SerializeField]
    private Image[] limit = new Image[2];

    private int limitTime = 0;
    private int nowTime = 0;

    TimerScript TScript;

	// Use this for initialization
	void Start ()
    {
        TScript = this.GetComponent<TimerScript>();
        TScript.SwithGameTimer();

        limitTime = (int)TScript.GetGameTimer();
        nowTime = limitTime;

        limit[0].transform.localPosition = new Vector3((limitTime % 10) * -25, limit[0].transform.localPosition.y, limit[0].transform.localPosition.z);
        limit[1].transform.localPosition = new Vector3((limitTime / 10) * -25, limit[1].transform.localPosition.y, limit[1].transform.localPosition.z);
    }

    // Update is called once per frame
    void Update ()
    {
        limitTime = (int)TScript.GetGameTimer();

        if (nowTime != limitTime)
        {
            limit[0].transform.localPosition = new Vector3((limitTime % 10) * -25, limit[0].transform.localPosition.y, limit[0].transform.localPosition.z);
            limit[1].transform.localPosition = new Vector3((limitTime / 10) * -25, limit[1].transform.localPosition.y, limit[1].transform.localPosition.z);
            nowTime = limitTime;
        }
    }
}
