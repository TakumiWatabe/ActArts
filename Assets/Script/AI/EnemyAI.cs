////////////////////////////////
// Creater : Masato Yamagishi //
// Data    : 10/30            //
////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private GameObject enemy;
    private float enemyDis = 0.0f;
    private bool isRigor = false;   //ガード硬直
    private bool isGap = false;     //後隙
    private bool isJump = false;
    private int elapsedTime = 0;
    private Animator animator;
    private Animator cAnimator;     //子のアニメーター
    private PlayerController pc;
    private PlayerController cpc;   //子のスクリプト

    [SerializeField,Header("移動系行動の判定間隔")]
    private int judgTime = 20;
    [SerializeField,Header("移動系行動の割合(％)")]
    private int neutralProbability = 5;
    [SerializeField]
    private int advanceProbability = 50;
    [SerializeField]
    private int dashProbability = 10;
    [SerializeField]
    private int backProbability = 35;
    [SerializeField,Header("ジャンプの割合(％)")]
    private int jumpProbability = 10;
    [SerializeField,Header("攻撃系行動の判定距離")]
    private float wStandAttackDis = 0.0f;
    [SerializeField]
    private float sStandAttackDis = 0.0f;
    [SerializeField]
    private float wSitAttackDis = 0.0f;
    [SerializeField]
    private float sSitAttackDis = 0.0f;
    [SerializeField]
    private float wJumpAttackDis = 0.0f;
    [SerializeField]
    private float sJumpAttackDis = 0.0f;
    [SerializeField]
    private float hadouKenDis = 0.0f;
    [SerializeField]
    private float shouryuKenDis = 0.0f;
    [SerializeField]
    private float shouryuKenHeight = 0.0f;
    [SerializeField, Header("防御行動をする相手との距離")]
    private float defendDis = 0.0f;

    // Use this for initialization
    void Start () {
        pc = gameObject.GetComponent<PlayerController>();
        cpc = gameObject.transform.GetChild(0).GetComponent<PlayerController>();
        enemy = pc.fightEnemy;
        pc.ControllerName = "AI";
        cpc.ControllerName = "AI";
        animator = GetComponent<Animator>();
        cAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    public void Initialize()
    {
        pc = gameObject.GetComponent<PlayerController>();
        enemy = pc.fightEnemy;
        pc.ControllerName = "AI";
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        //相手との距離を計算
        enemyDis = gameObject.transform.position.x - enemy.transform.position.x;
        if (enemyDis < 0.0f)
            enemyDis *= -1.0f;

        elapsedTime++;

        pc.PunchKey = false;
        cpc.PunchKey = false;
        pc.KickKey = false;
        cpc.KickKey = false;

        //ガードモーション中は動かない
        if (pc.State == "StandGuard" || pc.State == "SitGuard")
        {
            isRigor = true;
        }
        else
        {
            isRigor = false;
        }

        if(isRigor)
        {
            //ガード継続
        }
        else if(!isGap)
        {
            
            if (Random.Range(0, 2) == 0)
            {
                //攻撃系行動の決定
                if (!ChoiceAttack() && elapsedTime >= judgTime)
                {
                    //移動系行動の決定
                    MoveAI();
                }
            }
            else
            {
                //防御行動の決定
                if(!JudgGuard() && elapsedTime >= judgTime)
                {
                    //移動系行動の決定
                    MoveAI();
                }
            }
        }
	}

    /// <summary>
    /// 移動系行動の決定
    /// </summary>
    private void MoveAI()
    {
        //経過時間を0にする
        elapsedTime = Random.Range(0, 10);

        int n = Random.Range(0, 100);
        if (n < neutralProbability)
        {
            //待機
            pc.InputDKey = 5;
            cpc.InputDKey = 5;
            elapsedTime = 10;
        }
        else if (n < neutralProbability + dashProbability)
        {
            //ダッシュ
            pc.InputDKey = 6;
            cpc.InputDKey = 6;
            pc.State = "Dash";
            cpc.State = "Dash";
            elapsedTime = 10;
        }
        else if (n < neutralProbability + dashProbability + advanceProbability)
        {
            //前進
            pc.InputDKey = 6;
            cpc.InputDKey = 6;
        }
        else
        {
            //後退
            pc.InputDKey = 4;
            cpc.InputDKey = 4;
        }

        //ジャンプのT/F判定
        if (!isJump)
        {
            JumpAI();
        }
    }

    /// <summary>
    /// たまにジャンプする
    /// </summary>
    private void JumpAI()
    {
        if(Random.Range(0,100) <= 10)
        {
            switch (pc.InputDKey)
            {
                //後ジャンプ
                case 4:
                    pc.InputDKey = 7;
                    cpc.InputDKey = 7;
                    break;
                //ジャンプ
                case 5:
                    pc.InputDKey = 8;
                    cpc.InputDKey = 8;
                    break;
                case 6:
                    pc.InputDKey = 9;
                    cpc.InputDKey = 9;
                    //前ジャンプ
                    break;
            }

            isJump = true;
        }
    }
    
    /// <summary>
    /// ガードのT/F判定
    /// </summary>
    /// <returns>ガードするかしないか</returns>
    private bool JudgGuard()
    {
        //敵、または敵弾が範囲内にあるならガード
        if (enemyDis <= defendDis)
        {
            //20フレームガード継続
            //立ちかしゃがみガード
            if(Random.Range(0,2) == 0)
            {
                pc.InputDKey = 4;
                cpc.InputDKey = 4;
            }
            else
            {
                pc.InputDKey = 1;
                cpc.InputDKey = 1;
            }
            elapsedTime = 0;
        }
        return false;
    }

    /// <summary>
    /// 敵との距離によって攻撃する
    /// </summary>
    /// <returns>攻撃するかどうか</returns>
    private bool ChoiceAttack()
    {
        //ジャンプ攻撃の判定
        if (isJump){
            if (JudgWeekJumpAttack())
                return true;
            if (JudgStrongJumpAttack())
                return true;
        }

        //昇竜の判定
        if(enemy.transform.position.y >= shouryuKenDis)
        {
            if (JudgShouryuKenAttack())
                return true;
        }

        //弱攻撃の判定
        if (Random.Range(0,2) == 0){
            if (JudgWeekStandAttack())
                return true;
        }
        else {
            if (JudgWeekSitAttack())
                return true;
        }

        //強攻撃の判定
        if (Random.Range(0, 2) == 0) {
            if (JudgStrongStandAttack())
                return true;
        }
        else {
            if (JudgStrongSitAttack())
                return true;
        }

        //波動拳の判定
        if(Random.Range(0,61) == 0) {
            if (JudgHadouKenAttack())
                return true;
        }

        return false;
    }

    /// <summary>
    /// 以下、各攻撃の判定
    /// </summary>
    /// <returns>攻撃するかしないか</returns>
    private bool JudgWeekStandAttack()
    {
        if (enemyDis < wStandAttackDis)
        {
            //立ち弱
            pc.InputDKey = 5;
            pc.PunchKey = true;
            cpc.InputDKey = 5;
            cpc.PunchKey = true;
            return true;
        }
        return false;
    }
    
    private bool JudgStrongStandAttack()
    {
        if (enemyDis < sStandAttackDis)
        {
            //立ち強
            pc.InputDKey = 5;
            pc.KickKey = true;
            cpc.InputDKey = 5;
            cpc.KickKey = true;
            return true;
        }
        return false;
    }
    
    private bool JudgWeekSitAttack()
    {
        if (enemyDis < wSitAttackDis)
        {
            //しゃがみ弱
            pc.InputDKey = 2;
            pc.PunchKey = true;
            cpc.InputDKey = 2;
            cpc.PunchKey = true;
            return true;
        }
        return false;
    }
    
    private bool JudgStrongSitAttack()
    {
        if (enemyDis < sSitAttackDis)
        {
            //しゃがみ強
            pc.InputDKey = 2;
            pc.KickKey = true;
            cpc.InputDKey = 2;
            cpc.KickKey = true;
            return true;
        }
        return false;
    }

    private bool JudgWeekJumpAttack()
    {
        if (enemyDis < wJumpAttackDis)
        {
            //ジャンプ弱
            pc.InputDKey = 5;
            pc.PunchKey = true;
            cpc.InputDKey = 5;
            cpc.PunchKey = true;
            return true;
        }
        return false;
    }

    private bool JudgStrongJumpAttack()
    {
        if (enemyDis < sJumpAttackDis)
        {
            //ジャンプ強
            pc.InputDKey = 5;
            pc.KickKey = true;
            cpc.InputDKey = 5;
            cpc.KickKey = true;
            return true;
        }
        return false;
    }

    private bool JudgHadouKenAttack()
    {
        if (enemyDis < hadouKenDis)
        {
            //波動拳
            pc.InputDKey = 5;
            pc.State = "Special";
            pc.SpecialState = "Hadoken";
            cpc.InputDKey = 5;
            cpc.State = "Special";
            cpc.SpecialState = "Hadoken";
            return true;
        }
        return false;
    }

    private bool JudgShouryuKenAttack()
    {
        if (enemyDis < shouryuKenDis)
        {
            //昇竜拳
            pc.InputDKey = 5;
            pc.State = "Special";
            pc.SpecialState = "Syoryuken";
            cpc.InputDKey = 5;
            cpc.State = "Special";
            cpc.SpecialState = "Syoryuken";
            return true;
        }
        return false;
    }
}