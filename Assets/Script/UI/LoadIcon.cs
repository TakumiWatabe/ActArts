using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIcon : MonoBehaviour {

    static private LoadIcon instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.localEulerAngles += new Vector3(0.0f, 0.0f, 60.0f);
	}
}
