using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GamepadInput;

public class PlayMenu : MonoBehaviour {

    //プレイメニューシーンを行う処理のスクリプト
    //シーンマネージャー
    [SerializeField]
    private GameObject manager;

    private SceneMane SMana;
    private SceneFade SFade;
    DataRetention datums;

    //選択肢の画像
    [SerializeField]
    private Image[] Choice = new Image[3];
    [SerializeField]
    private Text[] Description = new Text[3];

    //選択時の色
    private Color brightColor = new Color(1, 1, 1, 1);
    //非選択時の色
    private Color darkColor = new Color(0.5f, 0.5f, 0.5f, 1);

    //選択番号
    private int selectNum = 0;
    private bool moveflag = false;

    //コントローラーの名前
    private string controllerName = "";

    private float chackVal = 0;
    private bool fade = false;

    // Use this for initialization
    void Start()
    {
        SMana = manager.GetComponent<SceneMane>();
        SFade = manager.GetComponent<SceneFade>();
        datums = GameObject.Find("GameSystem").GetComponent<DataRetention>();

        SFade.ImageAlpha = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //フェードアウト
        SFade.FadeOut();

        //選択肢移動
        selectMove();
        //選択肢画像の明暗処理
        lightAndDark();
        //シーン遷移
        changeScene();
	}

    //コントローラー判別
    private void selectMove()
    {
        //通常のコントローラー
        if (controllerName != "Arcade Stick (MadCatz FightStick Neo)")
        {
            //上下移動
            stickVal();
        }
        //アーケードコントローラー
        else
        {
            //上下移動
            stickVal();
        }
    }

    //選択肢の移動
    private void stickVal()
    {
        //1Pコントローラー
        var p1Con = GamePad.Index.One;
        //左スティック
        var stick = GamePad.Axis.LeftStick;
        //十字キー
        var dpad = GamePad.Axis.Dpad;

        //上
        if ((GamePad.GetAxis(stick, p1Con).y > 0 || GamePad.GetAxis(dpad, p1Con).y > 0) && !moveflag)
        {
            moveflag = true;
            if (selectNum != 0)
            {
                //番号を戻す
                selectNum--;
            }
        }

        //下
        if ((GamePad.GetAxis(stick, p1Con).y < 0 || GamePad.GetAxis(dpad, p1Con).y < 0) && !moveflag)
        {
            moveflag = true;
            if (selectNum != 2)
            {
                //番号を進める
                selectNum++;
            }
        }

        //動かしていないなら
        if ((GamePad.GetAxis(stick, p1Con).y == 0 && GamePad.GetAxis(dpad, p1Con).y == 0) && moveflag)
        {
            moveflag = false;
        }
    }

    //画像の明暗処理
    private void lightAndDark()
    {
        for (int i = 0; i < Choice.Length; i++)
        {
            //選択肢を黒くする
            Choice[i].color = darkColor;
            Description[i].enabled = false;
            //選択された選択肢を明るくする
            if (i == selectNum)
            {
                Choice[i].color = brightColor;
                Description[i].enabled = true;
            }
        }
    }

    //決定したシーンへ遷移する
    private void changeScene()
    {
        if (Input.GetButtonDown("AButton"))
        {
            fade = true;
            SFade.FFlag = true;
        }

        if (fade)
        {
            //フェードイン
            SFade.FadeIn();

            if (SFade.ImageAlpha == 1)
            {
                switch (selectNum)
                {
                    case 0:
                        SceneManager.LoadScene(SMana.Scenes("Select"));
                        break;
                    case 1:
                        SceneManager.LoadScene(SMana.Scenes("Select"));
                        break;
                    case 2:
                        Debug.Log("アーケードモード");
                        break;
                }

                //モード設定
                datums.Mode = selectNum;
            }
        }
    }
}
