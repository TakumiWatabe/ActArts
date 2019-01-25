using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundDirector : MonoBehaviour
{
    //ラウンド数から画像表示を行うスクリプト
    private FadeImage fade;

    // ラウンドを表示したか
    private bool ARound;
    private bool flag = true;

    //画像
    [SerializeField]
    private Image[] round = new Image[3];
    private Color clear = new Color(1, 1, 1, 0);

    // Use this for initialization
    void Start ()
    {
        fade = this.GetComponent<FadeImage>();        
        ARound = false;

        //設定
        round[0].color = clear;
        round[1].color = clear;
        round[2].color = clear;
    }

    public void Initialize()
    {
        flag = true;
        ARound = false;

        //設定
        round[0].color = clear;
        round[1].color = clear;
        round[2].color = clear;

    }

    public void RoundsDisplay(int rounds)
    {
        //ラウンド数の表示
        if (flag)
        {
            if (!fade.Flag && round[rounds].color.a <= 0) { fade.Flag = true; }
            RoundFade(rounds);
        }
        //アニメーションが終了したら稼働させない
        if (!fade.Flag && round[rounds].color.a < 0)
        {
            flag = false;
        }
    }

    //ラウンド画像のフェード
    private void RoundFade(int num)
    {
        //フェードイン/アウトを行う
        fade.doFade(round[num], 9);
        //表示判定
        if (round[num].color.a >= 1) { ARound = true; }
    }

    public float RoundAlpha(int num) { return round[num].color.a; }
    public bool AppearRound { get { return ARound; } }
}
