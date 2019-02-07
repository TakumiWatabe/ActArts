using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIcon : MonoBehaviour {

    // Update is called once per frame
    void Update () {
        gameObject.transform.localEulerAngles += new Vector3(0.0f, 0.0f, 60.0f);
	}
}
