using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComProcessScript : MonoBehaviour {

    //入力によるキャラクターの状態を設定するスクリプト
    //プレイヤーの挙動の根幹を担うように設計

    //コマンド取得用
    private CommandScript comm;
    private string inputCom;
    private string coms;

    //行動判別用
    private string state = "";

    //走り判定
    private bool dashFlag = false;

    //波動拳判定
    private bool hadouF = false;
    //昇竜拳判定
    private bool shoryuF = false;

    // Use this for initialization
    void Start ()
    {
        comm = this.GetComponent<CommandScript>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //コマンドを取得
        inputCom = comm.getCom;
        coms = comm.getincom;

        movement(inputCom);
        attackment(inputCom);

        Debug.Log(state);
        Debug.Log(shoryuF);
        Debug.Log(hadouF);
	}

    //移動系
    private void movement(string moveCom)
    {
        //しゃがみ(ガード可能)状態
        if (moveCom == "1") { state = "SitGuard"; }
        //しゃがみ(ガード不能)状態
        if (moveCom == "2") { state = "Sit"; }
        //しゃがみ(ガード不能)状態
        if (moveCom == "3") { state = "Sit"; }

        //後ろ歩き状態
        if (moveCom == "4") { state = "BackWalk"; }
        //立ち状態
        if (moveCom == "5") { state = "Neutral"; }
        //前歩き状態
        if (moveCom == "6") { state = "FrontWalk"; }

        //後ろジャンプ状態
        if (moveCom == "7") { state = "BackJump"; }
        //ジャンプ状態
        if (moveCom == "8") { state = "Jump"; }
        //前ジャンプ状態
        if (moveCom == "9") { state = "FrontJump"; }

        //走り状態
        if (coms.EndsWith("66")){ dashFlag = true; }
        //走り状態を解除
        if (dashFlag && moveCom != "6"){ dashFlag = false; }

        //波動拳状態
        if (coms.Contains("236A")) { hadouF = true; }
        //昇竜拳状態
        if (coms.Contains("623A")) { shoryuF = true; }
    }

    //攻撃系
    private void attackment(string motion)
    {
        //攻撃前の状態
        string motionstate = state;

        //弱
        if (motion == "A") { motionstate = "5A"; }
        //強
        if (motion == "B") { motionstate = "5B"; }

        //しゃがみ状態なら
        if (state == "Sit" || state == "SitGuard")
        {
            //弱
            if (motion == "A") { motionstate = "2A"; }
            //強
            if (motion == "B") { motionstate = "2B"; }
        }

        //ジャンプ状態なら
        if (state == "Jump" || state == "BackJump" || state == "FrontJump")
        {
            //弱
            if (motion == "A") { motionstate = "JA"; }
            //強
            if (motion == "B") { motionstate = "JB"; }
        }

        //状態を確定
        state = motionstate;
    }

    //画面にデバック表示
    private void OnGUI()
    {
        GUILayout.Label("\nState: " + state);
    }

    public string motionState { get { return state; } }

    public bool dashMot { get { return dashFlag; } }
    public bool hadouMot
    {
        get { return hadouF; }
        set { hadouF = value; }
    }
    public bool shoryuMot
    {
        get { return shoryuF; }
        set { shoryuF = value; }
    }
}
