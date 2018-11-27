using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadoCollid : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Attack"&&other.gameObject.layer==16)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
