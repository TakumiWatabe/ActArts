using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    //フェードイン/アウトを行うメソッド
    //原色
    private Color imageColors;

    //色
    private float red;
    private float green;
    private float blue;
    private float alpha;
    private float a;

    private bool fadeFlag = true;
    private bool f = false;

    //変化量
    [SerializeField, Range(0, 1)]
    private float change = 0.02f;

    private float count = 0;
    private float timer;

    //フェードを行う
    public void doFade(Image image, float time)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

        if (fadeFlag){ FadeIn(); }
        if (!fadeFlag)
        {
            if (count < time) { count += 0.1f; }
            else if (count > time){ FadeOut(); }
        }
    }
    public void doFadeSp(Image image, float time,bool fp)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

        if (fadeFlag) { FadeIn(); }
        if (!fadeFlag&&fp)
        {
            if (count < time) { count += 0.001f; }
            else if (count > time) { FadeOut(); }
        }
    }


    //フェードイン
    public void FadeIn()
    {
        if (alpha < 1) { alpha += change; }
        if (alpha > 1 && fadeFlag) { fadeFlag = false; }
    }

    //フェードアウト
    public void FadeOut()
    {
        if (alpha > 0) { alpha -= change; }
        if (alpha < 0) { count = 0; f = true;}
    }

    public Color GetColor { get { return imageColors; } }  

    //状態の初期化
    public bool Flag
    {
        get{ return fadeFlag; }
        set { fadeFlag = value; }
    }

    public float Counts
    {
        get { return count; }
        set { count = value; }
    }

    public void reState() { count = 0; }
    public bool fs { get { return f; } }

}
