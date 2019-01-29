﻿using System.Collections;
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

    // Use this for initialization
    void Start ()
    {
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

        fade.ImageAlpha = 1;
	}
	
    private void Initialize()
    {

    }

	// Update is called once per frame
	void Update ()
    {
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

        if(!disF)
        {
            fade.FFlag = true;
            fade.FadeIn();


            if (fade.ImageAlpha > 1)
            {
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
                //ラウンドを進める
                if (!Finish.GetDraw){ rounds += 1; }
            //どちらかが2勝したらりざるとへ
            if (GetGame.P1win == 2 || GetGame.P2win == 2)
            {
                GetGame.ResetGame(gameImage);
                GameObject.Find("AoiIntentionObj").GetComponent<AIIntention>().Learning(false);
                GameObject.Find("HikariIntentionObj").GetComponent<AIIntention>().Learning(false);
                SceneManager.LoadScene(SMane.Scenes("Result"));
            }
            }
        }
    }

    //勝利数
    public int getFinish { get { return rounds; } }
}
