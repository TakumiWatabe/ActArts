using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneFade : MonoBehaviour {

    //フェードイン/アウトを行う処理
    //イベントシステム
    [SerializeField]
    private GameObject eve;
    private EventSystem eveSystem;

    [SerializeField]
    private Image fadeImage;

    private Color imageCol;

    //色
    private float red;
    private float green;
    private float blue;
    private float alpha;

    //変化量
    [SerializeField, Range(0, 0.5f)]
    private float change = 0.0f;

    private bool fadeFlag = false;

    // Use this for initialization
    void Start ()
    {
        //Object.FindObjectOfTypeは重いので極力使わない
        eveSystem = eve.GetComponent<EventSystem>();

        //フェード用画像の色を取得
        imageCol = fadeImage.color;

        red = imageCol.r;
        green = imageCol.g;
        blue = imageCol.b;
	}
	
	// Update is called once per frame
	void Update ()
    {
        eveSystem.enabled = true;
        //アルファ値を取得
        imageCol.a = alpha;
        //画像を変化
        fadeImage.color = new Color(red, green, blue, imageCol.a);
        lockVal();
    }

    //フェードインを行う処理
    public void FadeIn()
    {
        if (alpha < 1 && fadeFlag)
        {
            //操作を受け付けなくする
            eveSystem.enabled = false;
            alpha += change;
        }
    }

    //フェードアウトを行う処理
    public void FadeOut()
    {
        if (alpha > 0 && !fadeFlag)
        {
            //操作を受け付けなくする
            eveSystem.enabled = false;
            alpha -= change;
        }
    }

    //値を固定する
    private void lockVal()
    {
        if (alpha > 1 && fadeFlag) alpha = 1;
        if (alpha < 0 && !fadeFlag) alpha = 0;
    }

    //アルファ値取得
    public float ImageAlpha
    {
        get { return alpha; }
        set { alpha = value; }
    }

    public bool FFlag
    {
        get { return fadeFlag; }
        set { fadeFlag = value; }
    }
}
