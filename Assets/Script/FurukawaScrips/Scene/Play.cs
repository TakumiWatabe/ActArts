using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    //プレイシーンのシーン遷移に関係する処理を行うスクリプト
    private SceneFade fade;
    [SerializeField]
    private GameObject butDir;
    private TimerScript timer;

    private RoundDirector Round;
    private FightDirector Fight;
    private FinishScript Finish;
    private WinnerDirector Winner;
    private TimeUPDirector TimeUp;

    private GameDirector Director;
    private GetGameScript GetGame;

    private SceneMane SMane;
    [SerializeField]
    private Sprite gameImage;

    //プレイヤー
    private GameObject player1;
    private GameObject player2;

    private PlayerController PController1P;
    private PlayerController PController2P;

    private int rounds = 0;
    private bool disF = true;

    private List<AIIntention> intentions = new List<AIIntention>();

    private AudioSource audio;
    //音声
    [SerializeField]
    private AudioClip round1;
    [SerializeField]
    private AudioClip round2;
    [SerializeField]
    private AudioClip round3;

    private List<AudioClip> sounds = new List<AudioClip>();
    private bool soundPlayed = false;

    // Use this for initialization
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        fade = this.GetComponent<SceneFade>();
        timer = butDir.GetComponent<TimerScript>();

        Round = this.GetComponent<RoundDirector>();
        Fight = this.GetComponent<FightDirector>();
        Finish = this.GetComponent<FinishScript>();
        Winner = this.GetComponent<WinnerDirector>();
        TimeUp = this.GetComponent<TimeUPDirector>();

        Director = this.GetComponent<GameDirector>();
        GetGame = this.GetComponent<GetGameScript>();

        SMane = this.GetComponent<SceneMane>();

        player1 = GameObject.FindGameObjectWithTag("P1");
        player2 = GameObject.FindGameObjectWithTag("P2");

        PController1P = player1.GetComponent<PlayerController>();
        PController2P = player2.GetComponent<PlayerController>();

        intentions.Add(GameObject.Find("AoiIntentionObj").GetComponent<AIIntention>());
        intentions.Add(GameObject.Find("HikariIntentionObj").GetComponent<AIIntention>());
        intentions.Add(GameObject.Find("XionIntentionObj").GetComponent<AIIntention>());
        intentions.Add(GameObject.Find("ChloeIntentionObj").GetComponent<AIIntention>());
        intentions.Add(GameObject.Find("MariIntentionObj").GetComponent<AIIntention>());
        intentions.Add(GameObject.Find("ShiroganeIntentionObj").GetComponent<AIIntention>());

        fade.ImageAlpha = 1;

        sounds.Add(round1);
        sounds.Add(round2);
        sounds.Add(round3);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(rounds);

        //シーン開始時のフェードアウト
        if (disF)
        {
            fade.FadeOut();

            //フェードアウト終了
            if (fade.ImageAlpha <= 0 && !fade.FFlag)
            {
                //ラウンド表示&fight表示
                Round.RoundsDisplay(rounds);
                if (Round.AppearRound && Round.RoundAlpha(rounds) <= 0) { Fight.FightDisplay(); }

                if (!soundPlayed)
                {
                    audio.PlayOneShot(sounds[rounds], 1.0f);
                    soundPlayed = true;
                }

                //対戦が終了したら
                Finish.FinishVS();
            }
        }

        if (!Finish.GetF && disF)
        {
            //アイコン点灯
            GetGame.DisplayGame();
            disF = false;
        }

        if (!disF)
        {
            fade.FFlag = true;
            fade.FadeIn();

            if (fade.ImageAlpha >= 1)
            {
                //どちらかが2勝したらりざるとへ
                if (GetGame.P1win == 2 || GetGame.P2win == 2)
                {
                    for (int i = 0; i < intentions.Count; i++)
                    {
                        if (intentions[i].IsTrain)
                        {
                            GameObject.Find("LoadCanvas").SetActive(true);
                            intentions[i].LearningSpeed = 5;
                            return;
                        }
                    }

                    GameObject.Find("LoadCanvas").SetActive(false);
                    GetGame.ResetGame(gameImage);
                    for (int i = 0; i < intentions.Count; i++)
                    {
                        intentions[i].Learning(false);
                    }
                    SceneManager.LoadScene(SMane.Scenes("Result"));
                }

                Round.Initialize();
                Fight.Initialize();
                Finish.Initialize();
                Winner.Initialize();
                TimeUp.Initialize();
                Director.Initialize();

                PController1P.Initialize();
                PController2P.Initialize();

                disF = true;
                fade.FFlag = false;
                timer.ResetGameTimer();
                soundPlayed = false;
                //ラウンドを進める
                if (Finish.GetDraw) { rounds += 1; }
            }
        }
    }

    //勝利数
    public int getFinish { get { return rounds; } }
}
