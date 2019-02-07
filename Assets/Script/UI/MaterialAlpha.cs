using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialAlpha : MonoBehaviour {
    MeshRenderer render;
    Color color;
    float alpha = 0f;
    bool alphaFlag = true;
    // Use this for initialization
    void Start () {
        render = GetComponent<MeshRenderer>();
        color = render.material.color;
    }

    // Update is called once per frame
    void Update () {
        if (alphaFlag)
            color.a -= Time.deltaTime;
        else
            color.a += Time.deltaTime;
        if (color.a >= 1)
            alphaFlag = true;
        else if (color.a <= 0)
            alphaFlag = false; 
        render.material.color = color;
    }
}
