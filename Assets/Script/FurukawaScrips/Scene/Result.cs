using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GamepadInput;

public class Result : MonoBehaviour {

    //リザルトシーンうぃ行う処理のスクリプト
    //シーンマネージャー
    [SerializeField]
    private GameObject manager;

    private SceneMane SMana;
    private SceneFade SFadeScene;
    private SceneFade SFadeBack;

    //選択肢の画像
    [SerializeField]
    private Image[] Choice = new Image[3];
    [SerializeField]
    private Image[] back = new Image[2];

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
    private bool nextMenu = false;

    private AudioSource audio;
    [SerializeField]
    private AudioClip dicideSE;
    [SerializeField]
    private AudioClip cursorSE;

    // Use this for initialization
    void Start ()
    {
        if (this.GetComponent<AudioSource>() != null) audio = this.GetComponent<AudioSource>();

        SMana = manager.GetComponent<SceneMane>();
        SFadeScene = manager.GetComponent<SceneFade>();
        SFadeBack = this.GetComponent<SceneFade>();

        SFadeScene.ImageAlpha = 1;
        SFadeBack.ImageAlpha = 0;
        SFadeBack.FFlag = true;
	}

    // Update is called once per frame
    void Update()
    {
        //フェードアウト
        SFadeScene.FadeOut();
        hideImage();

        if (Input.GetButtonDown("AButton"))
        {
            nextMenu = true;
        }

        if (nextMenu)
        {
            //フェードイン
            SFadeBack.FadeIn();

            if (SFadeBack.ImageAlpha >= 1)
            {
                //選択肢移動
                selectMove();
                //選択肢画像の明暗処理
                lightAndDark();
                //シーン遷移
                changeScene();
            }
        }

        Debug.Log(nextMenu);
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
                audio.PlayOneShot(cursorSE, 1.0f);
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
                audio.PlayOneShot(cursorSE, 1.0f);
            }
        }

        //動かしていないなら
        if ((GamePad.GetAxis(stick, p1Con).y == 0 && GamePad.GetAxis(dpad, p1Con).y == 0) && moveflag)
        {
            moveflag = false;
        }
    }

    //画像を隠す関数
    private void hideImage()
    {
        if (SFadeBack.ImageAlpha >= 1)
        {
            for (int i = 0; i < Choice.Length; i++)
            {
                Choice[i].enabled = true;
            }
            for (int i = 0; i < back.Length; i++)
            {
                back[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < Choice.Length; i++)
            {
                Choice[i].enabled = false;
            }
            for (int i = 0; i < back.Length; i++)
            {
                back[i].enabled = false;
            }
        }
    }

    //画像の明暗処理
    private void lightAndDark()
    {
        for (int i = 0; i < Choice.Length; i++)
        {
            //選択肢を黒くする
            Choice[i].color = darkColor;
            //選択された選択肢を明るくする
            if (i == selectNum)
            {
                Choice[i].color = brightColor;
            }
        }
    }

    //決定ボタンを押したらシーン遷移する
    private void changeScene()
    {
        if(Input.GetButtonDown("AButton"))
        {
            fade = true;
            SFadeScene.FFlag = true;
            audio.Stop();
            audio.PlayOneShot(dicideSE, 1.0f);
        }

        if(fade)
        {
            //フェードイン
            SFadeScene.FadeIn();
            if(SFadeScene.ImageAlpha==1)
            {
                switch(selectNum)
                {
                    case 0:
                        SceneManager.LoadScene(SMana.Scenes("Play"));
                        break;
                    case 1:
                        SceneManager.LoadScene(SMana.Scenes("Select"));
                        break;
                    case 2:
                        SceneManager.LoadScene(SMana.Scenes("Menu"));
                        break;
                }
            }
        }
    }
}
