using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUPDirector : MonoBehaviour {

    //制限時間終了時の処理を行うスクリプト
    //プレイヤー
    private GameObject player1, player2;
    //HP取得用
    private HPDirectorScript P1HP, P2HP;
    private FadeImage fade;

    //制限時間取得用
    [SerializeField]
    private GameObject ButDir;
    private TimerScript timer;

    private bool gameSet = false;
    private bool drawJudge = false;
    private int charID = 0;

    private bool flag = true;

    //制限時間超過画像
    [SerializeField]
    private Image timeUp;

    private Color white = new Color(1, 1, 1, 1);

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
        P1HP = player1.GetComponent<HPDirectorScript>();
        P2HP = player2.GetComponent<HPDirectorScript>();

        timer = ButDir.GetComponent<TimerScript>();
        fade = this.GetComponent<FadeImage>();

        timeUp.enabled = false;
        timeUp.color = white;
    }

    public void Initialize()
    {
        gameSet = false;
        drawJudge = false;
        charID = 0;

        flag = true;

        timeUp.enabled = false;
        timeUp.color = white;
    }

    public void DecisionVS()
    {
        FinishJugde();

        //対戦終了判定
        if (flag)
        {
            //対戦が終了しているなら
            if (gameSet)
            {
                if (!fade.Flag && timeUp.color.a == 1) { fade.Flag = true; }
                //TimeUp演出
                TimeUpFini();
            }
        }
        if (!fade.Flag && timeUp.color.a < 0)
        {
            flag = false;
        }

    }

    //対戦終了判定
    private void FinishJugde()
    {
        //制限時間が終了したら対戦終了
        if (timer.GetGameTimer() <= 0)
        {
            gameSet = true;

            //勝者判定
            if (P1HP.NowHPState > P2HP.NowHPState) { charID = 1; }
            if (P1HP.NowHPState < P2HP.NowHPState) { charID = 2; }
            if (P1HP.NowHPState == P2HP.NowHPState) { drawJudge = true; }
        }
    }

    //TimeUp画像表示
    private void TimeUpFini()
    {
        timeUp.enabled = true;
        fade.doFade(timeUp, 10);
    }

    //決着判定
    public bool GetFinish { get { return gameSet; } }
    //勝者判定
    public int GetWinner { get { return charID; } }
    //引き分け判定取得
    public bool GetDraw { get { return drawJudge; } }

    //TimeUp画像
    public float GetTimeUpAlpha { get { return timeUp.color.a; } }
    public bool GetFlag { get { return flag; } }
}
