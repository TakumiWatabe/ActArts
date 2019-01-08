using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBreakScript : MonoBehaviour {

    //ガード値に関係する処理を行うスクリプト

    //ガード値取得用
    private GuardScript GScript;

    private int guard;
    private bool breakFlag = false;

    private string state;

	// Use this for initialization
	void Start ()
    {
        //スクリプト取得
        GScript = this.GetComponent<GuardScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //ガード値を取得
        guard = GScript.NowGuardVal;

        guardBreak();

        Debug.Log(this.tag + "Guard : " + guard);
    }

    //ガードブレイクを行う判定を行う関数
    private void guardBreak()
    {
        //ガード値が無くなってしまったら
        if (guard <= 0)
        {
            Debug.Log("ガードブレイクしたー！");
            //ガードブレイクを行う(モーション)
            breakFlag = true;
            //ガード値の回復
            refreshGuard();
        }
        else
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

    //ガードブレイク中操作を受け付けないようにする
    private void notControll()
    {
        //操作をできなくさせる
    }

    //コルーチン処理
    IEnumerator refresh()
    {
        //1秒待つ
        yield return new WaitForSeconds(1.0f);
        //ガード値を全回復
        Debug.Log("ガード値が回復しました");
        guard = GScript.MaxGuardVal;
        GScript.NowGuardVal = guard;
    }
}
