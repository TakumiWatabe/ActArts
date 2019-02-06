using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerDirector : MonoBehaviour {

    //勝敗決定時処理を行うスクリプト
    //プレイヤー
    private GameObject player1, player2;
    //HP取得用
    private HPDirectorScript P1HP, P2HP;
    private FadeImage fade;

    // 戦闘開始表示用
    private float baseTime = 1;
    private float timer = 0;
    private float counts = 0.05f;

    private bool gameSet = false;
    private bool drawJugde = false;
    private int charID = 0;
    //遷移用時間
    private float time = 0;
    private float count = 0.05f;
    private float t = 0;
    private float c = 0.05f;

    private bool flag = true;

    //KO画像
    [SerializeField]
    private GameObject[] KO = new GameObject[2];
    [SerializeField]
    private Image[] KOImage = new Image[2];
    [SerializeField]
    private Image[] PerfectKO = new Image[2];

    private Vector3[] PosKO = new Vector3[2];

    private Color clear = new Color(1, 1, 1, 0);
    private Color white = new Color(1, 1, 1, 1);
    private Vector3 endScle = new Vector3(10, 10, 10);
    private Vector3 strScle = new Vector3(1, 1, 1);

    //キャラクター生成オブジェクト
    private GameObject contl;
    private InstanceScript InScript;
    private GameObject dir;

    private AudioSource audio;
    [SerializeField]
    private AudioClip koSE;

    void Awake()
    {
        contl = GameObject.Find("FighterComtrol");
        InScript = contl.GetComponent<InstanceScript>();
        dir = GameObject.Find("BattleDirecter");
    }

    // Use this for initialization
    void Start ()
    {
        audio = this.GetComponent<AudioSource>();
        player1 = GameObject.FindGameObjectWithTag("P1");
        player2 = GameObject.FindGameObjectWithTag("P2");
        P1HP = player1.GetComponent<HPDirectorScript>();
        P2HP = player2.GetComponent<HPDirectorScript>();

        fade = this.GetComponent<FadeImage>();
        fade.Flag = false;

        for (int i = 0; i < KO.Length; i++)
        {
            PosKO[i] = KO[i].transform.localPosition;
            PerfectKO[i].enabled = false;
            PerfectKO[i].transform.localScale = strScle;
        }

        PerfectKO[0].color = white;
        PerfectKO[1].color = white;
        KOImage[0].color = white;
        KOImage[1].color = white;
    }

    // Update is called once per frame
    public void Initialize()
    {
        // 戦闘開始表示用
        baseTime = 1;
        timer = 0;
        counts = 0.05f;

        gameSet = false;
        drawJugde = false;
        charID = 0;
        //遷移用時間
        time = 0;
        count = 0.05f;
        t = 0;
        c = 0.05f;

        flag = true;

        for (int i = 0; i < KO.Length; i++)
        {
            KO[i].transform.localPosition = PosKO[i];
            PosKO[i] = KO[i].transform.localPosition;
            PerfectKO[i].transform.localScale = strScle;

            PerfectKO[i].enabled = false;
        }

        PerfectKO[0].color = white;
        PerfectKO[1].color = white;
        KOImage[0].color = white;
        KOImage[1].color = white;
    }

    public void DecisionVS()
    {
        //対戦終了判定
        FinishJugde();

        //対戦が終了しているなら
        if (gameSet)
        {
            //勝利時HPが満タンならPerfect演出
            if (P1HP.NowHPState == P1HP.MaxHPState ||
                P2HP.NowHPState == P2HP.MaxHPState)
            {
                if (flag)
                {
                    if (!fade.Flag && (PerfectKO[0].color.a == 1 && PerfectKO[1].color.a == 1))
                    {
                        fade.Flag = true;
                        audio.PlayOneShot(koSE, 1.0f);
                    }
                    PerfectKOAnim();
                }
                if (!fade.Flag && (PerfectKO[0].color.a < 0 && PerfectKO[1].color.a < 0))
                {
                    flag = false;
                }
            }
            //そうでないなら通常演出
            else if (P1HP.NowHPState < P1HP.MaxHPState ||
                P2HP.NowHPState < P2HP.MaxHPState)
            {
                if (flag)
                {
                    if (!fade.Flag && (KOImage[0].color.a == 1 || KOImage[1].color.a == 1))
                    {
                        fade.Flag = true;
                        audio.PlayOneShot(koSE, 1.0f);
                    }
                    KOAnim();
                }
                if (!fade.Flag && (KOImage[0].color.a < 0 || KOImage[0].color.a < 0))
                {
                    flag = false;
                }
            }
        }
    }

    //対戦終了判定
    private void FinishJugde()
    {
        //どちらかのHPが0になったら対戦終了
        if (P1HP.NowHPState <= 0 || P2HP.NowHPState <= 0)
        {
            //終了判定
            gameSet = true;
            //プレイヤー2の勝利
            if(P1HP.NowHPState <= 0){ charID = 2; }
            //プレイヤー1の勝利
            if (P2HP.NowHPState <= 0) { charID = 1; }
            //引き分け
            if(P1HP.NowHPState <= 0 && P2HP.NowHPState <= 0) { drawJugde = true; }
        }
    }

    //KO画像表示
    private void KOAnim()
    {
        if (time < 1) { time += count; }
        //左右から出現
        KO[0].transform.localPosition = Vector3.Lerp(PosKO[0], new Vector3(-200, 0, 0), time);
        KO[1].transform.localPosition = Vector3.Lerp(PosKO[1], new Vector3(200, 0, 0), time);

        fade.doFade(KOImage[0],15f);
        fade.doFade(KOImage[1],15f);
    }

    //PerfectKO画像表示
    private void PerfectKOAnim()
    {
        //原色画像を出現＆フェードアウト(ディレイ)
        //白色画像を出現＆フェードアウト拡大
        for (int i = 0; i < PerfectKO.Length; i++){ PerfectKO[i].enabled = true; }
        //fade.Flag = false;
        fade.doFade(PerfectKO[1], 1);
        if (PerfectKO[1].color.a > 0)
        {
            PerfectKO[0].color = white;
        }
        if (PerfectKO[1].color.a <= 0)
        {
            if (t < 1) { t += c; }
            else if (t > 1){ fade.doFadeSp(PerfectKO[0], 15, true); }
        }

        expansion(PerfectKO[1]);
    }

    //拡大
    private void expansion(Image image)
    {
        Vector3 vec;
        if (timer <= baseTime) { timer += 0.01f; }

        vec = Vector3.Lerp(strScle, endScle, timer);
        image.transform.localScale = vec;
    }

    //決着判定
    public bool GetFinish { get { return gameSet; } }
    //勝者取得
    public int GetWinner { get { return charID; } }
    //引き分け判定取得
    public bool GetDraw { get { return drawJugde; } }

    //KO画像
    public float GetKOAlpha(int id) { return KOImage[id].color.a; }
    //Perfect画像
    public float GetPerfectKOAlpha(int id) { return PerfectKO[id].color.a; }
    public bool GetFlag { get { return flag; } }
}
