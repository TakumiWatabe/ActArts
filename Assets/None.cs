using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : MonoBehaviour {

    private DataRetention datas;

	// Use this for initialization
	void Start ()
    {
        datas = GameObject.Find("GameSystem").GetComponent<DataRetention>();
    }

    // Update is called once per frame
    void Update ()
    {
    }
}
