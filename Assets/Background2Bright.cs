using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2Bright : MonoBehaviour {

    Material materials;
    Color col;
    float alpha = 0;
    [SerializeField]
    float alphaPlus = 0.01f;

    // Use this for initialization
    void Start () {
        materials = this.GetComponent<MeshRenderer>().materials[0];
        col = new Color(1, 1, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        col = new Color(1, 1, 1, Mathf.Cos(alpha) / 2 + 0.5f);
        //col.a = Mathf.Cos(alpha) / 2 + 0.5f;
        alpha += alphaPlus;
        materials.color = col;
    }
}
