using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBreakScript : MonoBehaviour {

    //ガード値に関係する処理を行うスクリプト

    //ガード値取得用
    private GuardScript GScript;

    private PlayerController playerController;

    private int guard;
    private bool local = false;
    private bool breakFlag = false;

    private string state;

	// Use this for initialization
	void Start ()
    {
        //スクリプト取得
        GScript = this.GetComponent<GuardScript>();
        playerController = this.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //ガード値を取得
        guard = GScript.NowGuardVal;
        guardBreak();

        //Debug.Log(this.tag + "Guard : " + guard);
    }

    //ガードブレイクを行う判定を行う関数
    private void guardBreak()
    {
        //ガード値が無くなってしまったら
        if (guard <= 0 && !local)
        {
            Debug.Log("ガードブレイクしたー！");
            //ガードブレイクを行う(モーション)
            breakFlag = true;
            local = true;

            playerController.State = "GuardCrash";

            //ガード値の回復
            refreshGuard();
        }
        else if(guard > 0)
        {
            breakFlag = false;
        }
    }

    //ガード値を全回復させる関数
    private void refreshGuard()
    {
        //コルーチンを実行
        StartCoroutine("refresh");
    }

    //コルーチン処理
    IEnumerator refresh()
    {
        //1秒待つ
        yield return new WaitForSeconds(1.0f);
        local = false;
        //ガード値を全回復
        Debug.Log("ガード値が回復しました");
        guard = GScript.MaxGuardVal;
        GScript.NowGuardVal = guard;
    }

    //デバッグ表示
    void OnGUI()
    {
        if(this.tag=="P1")
        GUI.Label(new Rect(50, 50, 50, 50), GScript.NowGuardVal.ToString());
    }
}
