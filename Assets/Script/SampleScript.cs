using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour {
	NumberImageRenderer ni = null;
	int number = 0;
	// Use this for initialization
	void Start () {
		ni = GetComponent<NumberImageRenderer> ();	
		ni.maxDigit = 9;
		ni.Draw (1973476);
	}

	// Update is called once per frame
	void Update () {
		//ni.Draw (number);
		//number++;
	}
}
