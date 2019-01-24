using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadouController : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    float speed = 1.0f;
    public int direction = 1;

    private Vector3 pos;

	void Start () {
        pos = transform.position;
        //transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        pos.x = transform.position.x + speed * direction;
        transform.position = pos;
    }
}
