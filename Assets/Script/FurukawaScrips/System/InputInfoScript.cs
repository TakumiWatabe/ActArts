using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using System;

public class InputInfoScript : MonoBehaviour
{
    //入力に関する情報を管理するスクリプト

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

    //左右上下判定制限
    private const float InputMax = 0.5f;
    private const float InputMin = -0.5f;
    //斜め判定制限
    private const float InputDiaUMax = 0.75f;
    private const float InputDiaUMin = 0.25f;
    private const float InputDiaDMax = -0.25f;
    private const float InputDiaDMin = -0.75f;

    //プレイヤーのタグ
    private DebugGetEnemyScript DGEScript = null;

    //入力ID
    private int inputID = (int)InputReID.INPUT_0;

    //次入力判定の猶予
    [SerializeField, Range(0, 60)]
    private int graceInput = 15;
    private int graceTime = 0;
    //入力履歴
    private string command = "";
    //コマンド記憶変数
    int lastComm = 0;

    //走り判定
    private bool dashFlag = false;

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
                    InputAxisX = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
                    InputAxisY = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
                    break;
                case 2:
                    InputAxisX = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).x;
                    InputAxisY = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).y;
                    break;
                default:
                    break;
            }
        }
        else
        {
            InputAxisX = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
            InputAxisY = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
        }

        //入力値設定
        receiveID();

        //走り状態を解除
        if (dashFlag && inputID != (int)InputReID.INPUT_6)
        {
            dashFlag = false;
        }
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
                changeInput((int)InputReID.INPUT_4, (int)InputReID.INPUT_6,
                            (int)InputReID.INPUT_7, (int)InputReID.INPUT_9,
                            (int)InputReID.INPUT_1, (int)InputReID.INPUT_3);
            }
            //自身が左側に居るなら
            else
            {
                //入力値設定
                changeInput((int)InputReID.INPUT_6, (int)InputReID.INPUT_4,
                            (int)InputReID.INPUT_9, (int)InputReID.INPUT_7,
                            (int)InputReID.INPUT_3, (int)InputReID.INPUT_1);
            }
        }
        else
        {
            //デバッグ用
            //入力値設定
            changeInput((int)InputReID.INPUT_6, (int)InputReID.INPUT_4,
                        (int)InputReID.INPUT_9, (int)InputReID.INPUT_7,
                        (int)InputReID.INPUT_3, (int)InputReID.INPUT_1);
        }

        //コマンド入力履歴記憶
        createCommand();

        //コマンドが入力されているなら走り  
        if(command.Contains("656"))
        {
            dashFlag = true;
        }
        Debug.Log(dashFlag);
    }

    //コマンド取得関数
    public void createCommand()
    {
        //フレーム加算
        graceTime++;

        //コマンド入力猶予内なら
        if (graceTime < graceInput)
        {
            //前回のコマンドと入力したコマンドが異なっていれば
            if (lastComm != inputID)
            {
                //入力値を記憶
                lastComm = inputID;
                //コマンド履歴に文字列として追加
                command += inputID.ToString();
                graceTime = 0;
            }
        }
        if (graceTime >= graceInput)
        {
            //変数初期化
            lastComm = 0;
            command = "";
            graceTime = 0;
        }
    }

    //向きによって変わる移動時の入力判定
    private void changeInput(int right, int left, int rightUp, int leftUp, int rightDown, int leftDown)
    {
        //入力なしなら立ち(5)
        if (InputAxisX == 0 && InputAxisY == 0) { inputID = (int)InputReID.INPUT_5; }
        //左右どちらかを入力していれば歩き
        if (InputAxisX < 0 || InputAxisX > 0)
        {
            //右入力なら6か4
            if (InputAxisX > 0 && InputAxisY < InputMax && InputAxisY > InputMin) { inputID = right; }
            //左入力なら6か4
            if (InputAxisX < 0 && InputAxisY < InputMax && InputAxisY > InputMin) { inputID = left; }
        }
        //上入力ならジャンプ(8)
        if (InputAxisY > 0 && InputAxisX < InputMax && InputAxisX > InputMin) { inputID = (int)InputReID.INPUT_8; }
        //下入力なら(2)
        if (InputAxisY < 0 && InputAxisX < InputMax && InputAxisX > InputMin) { inputID = (int)InputReID.INPUT_2; }

        //右上入力なら9か7
        if (InputAxisY > 0 && InputAxisX > InputDiaUMin && InputAxisX < InputDiaUMax){ inputID = rightUp; }
        //左上入力なら9か7
        if (InputAxisY > 0 && InputAxisX > InputDiaDMin && InputAxisX < InputDiaDMax){ inputID = leftUp; }

        //右下入力なら3か1
        if (InputAxisY < 0 && InputAxisX > InputDiaUMin && InputAxisX < InputDiaUMax) { inputID = rightDown; }
        //左下入力なら3か1
        if (InputAxisY < 0 && InputAxisX > InputDiaDMin && InputAxisX < InputDiaDMax) { inputID = leftDown; }
    }
    private void OnGUI()
    {
        GUILayout.Label("Command: " + command);
    }

    //入力値取得
    public float inputX { get { return InputAxisX; } }
    public float inputY { get { return InputAxisY; } }

    //行動判定取得
    public int motionState { get { return inputID; } }
}