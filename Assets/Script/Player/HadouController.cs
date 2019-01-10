using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadouController : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    float speed = 1.0f;
    public int direction = 1;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + speed * direction, transform.position.y, transform.position.z);
    }
}
