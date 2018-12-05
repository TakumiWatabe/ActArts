using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotThroughFighter : MonoBehaviour {

    private OverlapScript over;

	// Use this for initialization
	void Start ()
    {
        over = this.GetComponent<OverlapScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//お互いが触れたときに速度を90%に下げる
        //相手が壁に振れているときに移動状態＋お互いが触れているとお互い速度ゼロ
	}
}
