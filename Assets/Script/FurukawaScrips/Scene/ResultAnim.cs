using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultAnim : MonoBehaviour {

    [SerializeField]
    private Image back;
    [SerializeField]
    private GameObject barU1, barU2;
    [SerializeField]
    private GameObject barD1, barD2;

    private bool fade = false;

    private Color initColor;

    private float red;
    private float green;
    private float blue;
    private float alpha = 0;

    private float speedFade = 0.01f;
    private float speedPos = 1f;

    private Vector3 initPos = new Vector3(-1000, -210, 0);

    private float overLine = 600;

	// Use this for initialization
	void Start ()
    {
        initColor = back.color;

        red = initColor.r;
        green = initColor.g;
        blue = initColor.b;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (alpha > 1) { fade = false; }
        else if (alpha < 0) { fade = true; }

        fadeAnim();
        back.color = new Color(red, green, blue, alpha);

        barU1.transform.localPosition += new Vector3(speedPos, 0, 0);
        barU2.transform.localPosition += new Vector3(speedPos, 0, 0);
        barD1.transform.localPosition += new Vector3(speedPos, 0, 0);
        barD2.transform.localPosition += new Vector3(speedPos, 0, 0);

        backAnim(barU1);
        backAnim(barU2);
        backAnim(barD1);
        backAnim(barD2);
    }

    //フェードアニメ
    private void fadeAnim()
    {
        if(fade){ alpha += speedFade; }
        else { alpha -= speedFade; }
    }

    //背景アニメーション
    private void backAnim(GameObject img)
    {
        if (img.transform.localPosition.x >= overLine) 
        {
            img.transform.localPosition = initPos;
        }
    }
}
