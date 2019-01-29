using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour {

    //対戦終了時勝者表示を行うスクリプト
    //プレイヤー
    private GameObject player1;
    private GameObject player2;

    private FadeImage fade;
    private WinnerDirector WDir;
    private TimeUPDirector TUDir;

    [SerializeField]
    private GameObject ButDir;
    private TimerScript timer;

    private int charID = 0;

    //キャラクター名前
    [SerializeField]
    private List<Sprite> names;
    [SerializeField]
    private Image winner;
    [SerializeField]
    private Image win;

    //引き分け画像
    [SerializeField]
    private Image draw;

    private Color white = new Color(1, 1, 1, 1);
    private bool flag = true;
    private bool finishDraw = false;

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;
    private GameObject dir;


    void Awake()
    {
        contl = GameObject.Find("FighterComtrol");
        InScript = contl.GetComponent<InstanceScript>();
        dir = GameObject.Find("BattleDirecter");
    }

    // Use this for initialization
    void Start ()
    {
        player1 = GameObject.FindGameObjectWithTag("P1");
        player2 = GameObject.FindGameObjectWithTag("P2");

        timer = ButDir.GetComponent<TimerScript>();
        fade = this.GetComponent<FadeImage>();
        
        WDir = this.GetComponent<WinnerDirector>();
        TUDir = this.GetComponent<TimeUPDirector>();

        winner.enabled = false;
        win.enabled = false;
        draw.enabled = false;

        winner.color = white;
        win.color = white;
        draw.color = white;
    }

    public void Initialize()
    {
        charID = 0;
        flag = true;
        finishDraw = false;

        winner.enabled = false;
        win.enabled = false;
        draw.enabled = false;

        winner.color = white;
        win.color = white;
        draw.color = white;
    }

    public void FinishVS()
    {
        //どちらかが倒れた
        WDir.DecisionVS();
        //制限時間になった
        TUDir.DecisionVS();

        //決着がついた
        if (WDir.GetFinish || TUDir.GetFinish)
        {
            //時間を止める
            timer.SwithGameTimer = false;
            if (WDir.GetFinish) { charID = WDir.GetWinner; }
            else if (TUDir.GetFinish) { charID = TUDir.GetWinner; }

            if (!TUDir.GetFlag || !WDir.GetFlag)
            {
                //勝者表示
                if (!WDir.GetDraw && !TUDir.GetDraw)
                {
                    if (flag)
                    {
                        if (!fade.Flag && win.color.a == 1) { fade.Flag = true; }
                        DisplayWin(charID);
                    }
                    if (!fade.Flag && win.color.a < 0)
                    {
                        flag = false;
                    }

                }
                //引き分け判定
                if (WDir.GetDraw || TUDir.GetDraw)
                {
                    if (flag)
                    {
                        if (!fade.Flag && draw.color.a == 1) { fade.Flag = true; }
                        finishDraw = true;
                        DrawFini();
                    }
                    if(!fade.Flag&& draw.color.a < 0)
                    {
                        flag = false;
                    }
                }
            }
        }
    }

    //勝者表示
    private void DisplayWin(int id)
    {
        //勝者の名前を画像を設定
        SetWinner(id);

        //勝利画像表示
        winner.enabled = true;
        win.enabled = true;

        fade.doFade(winner,9);
        fade.doFade(win,9);
    }

    //勝者の名前を設定
    private void SetWinner(int id)
    {
        if (id == 1){ WinnerName(player1.name); }
        else{ WinnerName(player2.name); }
    }

    //名前画像を登録する
    private void WinnerName(string name)
    {
        switch(name)
        {
            case "Aoi":
                winner.sprite = names[0];
                break;
            case "Hikari":
                winner.sprite = names[1];
                break;
            case "Shirogane":
                winner.sprite = names[2];
                break;
            case "Xion":
                winner.sprite = names[3];
                break;
            case "Chloe":
                winner.sprite = names[4];
                break;
            case "Mari":
                winner.sprite = names[5];
                break;
        }
    }

    //引き分け表示
    private void DrawFini()
    {
        draw.enabled = true;
        //フェードアニメ
        fade.doFade(draw, 10);
    }

    //勝者判定
    public int GetCharID { get { return charID; } }

    public bool GetF { get { return flag; } }

    public bool GetDraw { get { return finishDraw; } }
}
