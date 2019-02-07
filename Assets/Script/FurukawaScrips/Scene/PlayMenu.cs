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
    private TextAnim TAnim;
    DataRetention datums;

    //選択肢の画像
    [SerializeField]
    private Image[] Choice = new Image[3];
    [SerializeField]
    private Text[] Description = new Text[3];
    [SerializeField]
    private Image select;

    //選択時の色
    private Color brightColor = new Color(1, 1, 1, 1);
    //非選択時の色
    private Color darkColor = new Color(0.5f, 0.5f, 0.5f, 1);

    //選択番号
    private int selectNum = 0;
    private bool moveflag = false;

    //コントローラーの名前
    private string controllerName = "";

    private bool fade = false;
    [SerializeField, Range(1, 10)]
    private float speedRot = 0.1f;
    private float moveSped = 90;

    AudioSource audio;

    [SerializeField]
    AudioClip dicideSE;
    [SerializeField]
    AudioClip cursorSE;

    // Use this for initialization
    void Start()
    {
        if (this.GetComponent<AudioSource>() != null) audio = this.GetComponent<AudioSource>();

        SMana = manager.GetComponent<SceneMane>();
        SFade = manager.GetComponent<SceneFade>();
        TAnim = this.GetComponent<TextAnim>();
        datums = GameObject.Find("GameSystem").GetComponent<DataRetention>();

        SFade.ImageAlpha = 1;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //フェードアウト
        SFade.FadeOut();
        select.transform.Rotate(0, 0, speedRot);

        if (!fade)
        {
            //選択肢移動
            selectMove();
            //選択肢画像の明暗処理
            lightAndDark();
        }
	}

    void Update()
    {
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
            stickVal(true);
        }
        //アーケードコントローラー
        else
        {
            //上下移動
            stickVal(false);
        }
    }

    //選択肢の移動
    private void stickVal(bool controller)
    {
        //1Pコントローラー
        var p1Con = GamePad.Index.One;
        //左スティック
        var stick = GamePad.Axis.LeftStick;
        //十字キー
        var dpad = GamePad.Axis.Dpad;

        if (controller)
        {
            //上
            if ((GamePad.GetAxis(stick, p1Con).y > 0 || GamePad.GetAxis(dpad, p1Con).y > 0) && !moveflag)
            {
                moveflag = true;
                if (selectNum != 0)
                {
                    TAnim.Initialize();
                    //番号を戻す
                    selectNum--;
                    select.transform.localPosition += new Vector3(0, moveSped, 0);
                    audio.PlayOneShot(cursorSE, 1.0f);
                }
            }

            //下
            if ((GamePad.GetAxis(stick, p1Con).y < 0 || GamePad.GetAxis(dpad, p1Con).y < 0) && !moveflag)
            {
                moveflag = true;
                if (selectNum != 2)
                {
                    TAnim.Initialize();
                    //番号を進める
                    selectNum++;
                    select.transform.localPosition += new Vector3(0, -moveSped, 0);
                    audio.PlayOneShot(cursorSE, 1.0f);
                }
            }
        }
        else
        {
            //上
            if ((GamePad.GetAxis(stick, p1Con).y < 0 || GamePad.GetAxis(dpad, p1Con).y < 0) && !moveflag)
            {
                moveflag = true;
                if (selectNum != 0)
                {
                    TAnim.Initialize();
                    //番号を戻す
                    selectNum--;
                    select.transform.localPosition += new Vector3(0, moveSped, 0);
                    audio.PlayOneShot(cursorSE, 1.0f);
                }
            }

            //下
            if ((GamePad.GetAxis(stick, p1Con).y > 0 || GamePad.GetAxis(dpad, p1Con).y > 0) && !moveflag)
            {
                moveflag = true;
                if (selectNum != 2)
                {
                    TAnim.Initialize();
                    //番号を進める
                    selectNum++;
                    select.transform.localPosition += new Vector3(0, -moveSped, 0);
                    audio.PlayOneShot(cursorSE, 1.0f);
                }
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
        TAnim.DisplayAnim(selectNum);
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
            if (SFade.ImageAlpha <= 0)
            {
                audio.Stop();
                audio.PlayOneShot(dicideSE, 1.0f);
                fade = true;
                SFade.FFlag = true;
            }
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
                        fade = false;
                        SFade.FFlag = false;
                        break;
                }

                //モード設定
                datums.Mode = selectNum;
            }
        }
    }
}
