using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageJudgeScript : MonoBehaviour {

    //攻撃を受けたときの挙動を制御するスクリプト

    //キャラクタースクリプト
    private TestChar TChar;
    private ComProcessScript CPScript;

	// Use this for initialization
	void Start ()
    {
        TChar = this.GetComponentInChildren<TestChar>();
        CPScript = this.GetComponent<ComProcessScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //攻撃を受けたときの挙動を制御する
    private void JudgeDama()
    {
        if(TChar.hitDamage)
        {
            if(CPScript.motionState=="")
            {

            }
        }
    }
}
