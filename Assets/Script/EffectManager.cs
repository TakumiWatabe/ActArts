using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    float speed;
	// Use this for initialization
	void Start () {
        speed = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position += new Vector3(speed,0,0);
	}
}
