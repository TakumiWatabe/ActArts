using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GamepadInput;

public class InputInfoScript : MonoBehaviour
{
    //入力に関する情報を管理するスクリプト(移動編)

    //入力に対する受取ID
    private enum InputReID
    {
        INPUT_0,
        INPUT_1,
        INPUT_2,
        INPUT_3,
        INPUT_4,
        INPUT_5,
        INPUT_6,
        INPUT_7,
        INPUT_8,
        INPUT_9,
    }

    //入力値
    private float InputAxisX = 0;
    private float InputAxisY = 0;
    //攻撃ボタン押下判定
    private bool atkA = false;      //弱
    private bool atkB = false;      //強

    //左右上下判定制限
    private const float InputMax = 0.5f;
    private const float InputMin = -0.5f;

    //プレイヤーのタグ
    private DebugGetEnemyScript DGEScript = null;

    //入力ID
    private string inputs = ((int)InputReID.INPUT_0).ToString();

    // Use this for initialization
    void Start ()
    {
        DGEScript = this.GetComponent<DebugGetEnemyScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (DGEScript != null)
        {
            //コントローラ番号からコントローラーを判別
            switch (DGEScript.GetCont)
            {
                //入力値を取得
                case 1:
                    //1P
                    InputAxisX = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
                    InputAxisY = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
                    atkA = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One);
                    atkB = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One);
                    break;
                case 2:
                    //2P
                    InputAxisX = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).x;
                    InputAxisY = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).y;
                    atkA = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Two);
                    atkB = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.Two);
                    break;
                default:
                    break;
            }
        }
        else
        {
            //デバッグ用
            InputAxisX = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
            InputAxisY = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
            atkA = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One);
            atkB = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One);
        }

        //入力値設定
        receiveID();
    }

    //入力IDを設定する関数
    private void receiveID()
    {
        if (DGEScript != null)
        {
            //自身が右側に居るなら
            if (DGEScript.GetDir)
            {
                //入力値設定
                inputs=changeInput((int)InputReID.INPUT_4, (int)InputReID.INPUT_6,
                            (int)InputReID.INPUT_7, (int)InputReID.INPUT_9,
                            (int)InputReID.INPUT_1, (int)InputReID.INPUT_3);
            }
            //自身が左側に居るなら
            else
            {
                //入力値設定
                inputs=changeInput((int)InputReID.INPUT_6, (int)InputReID.INPUT_4,
                            (int)InputReID.INPUT_9, (int)InputReID.INPUT_7,
                            (int)InputReID.INPUT_3, (int)InputReID.INPUT_1);
            }
        }
        else
        {
            //デバッグ用
            //入力値設定
            inputs=changeInput((int)InputReID.INPUT_6, (int)InputReID.INPUT_4,
                        (int)InputReID.INPUT_9, (int)InputReID.INPUT_7,
                        (int)InputReID.INPUT_3, (int)InputReID.INPUT_1);
        }

        //攻撃判定
        if (atkA){ inputs = "A"; }
        else if (atkB){ inputs = "B"; }
    }

    //向きによって変わる移動時の入力判定
    private string changeInput(int right, int left, int rightUp, int leftUp, int rightDown, int leftDown)
    {
        //入力なしなら立ち(5)
        if (InputAxisX == 0 && InputAxisY == 0) { return ((int)InputReID.INPUT_5).ToString(); }
        //左右どちらかを入力していれば歩き
        if (InputAxisX < 0 || InputAxisX > 0)
        {
            //右入力なら6か4
            if (InputAxisX > 0 && InputAxisY < InputMax && InputAxisY > InputMin) { return right.ToString(); }
            //左入力なら6か4
            if (InputAxisX < 0 && InputAxisY < InputMax && InputAxisY > InputMin) { return left.ToString(); }
        }
        //上入力ならジャンプ(8)
        if (InputAxisY > 0 && InputAxisX < InputMax && InputAxisX > InputMin) { return ((int)InputReID.INPUT_8).ToString(); }
        //下入力なら(2)
        if (InputAxisY < 0 && InputAxisX < InputMax && InputAxisX > InputMin) { return ((int)InputReID.INPUT_2).ToString(); }

        //右上入力なら9か7
        if (InputAxisY > InputMax && InputAxisX > InputMax){ return rightUp.ToString(); }
        //左上入力なら9か7
        if (InputAxisY > InputMax && InputAxisX < InputMin){ return leftUp.ToString(); }

        //右下入力なら3か1
        if (InputAxisY < InputMin && InputAxisX > InputMax) { return rightDown.ToString(); }
        //左下入力なら3か1
        if (InputAxisY < InputMin && InputAxisX < InputMin) { return leftDown.ToString(); }

        return "";
    }

    //入力値取得
    public float inputX { get { return InputAxisX; } }
    public float inputY { get { return InputAxisY; } }

    //行動判定取得
    public string motionState { get { return inputs; } }
}