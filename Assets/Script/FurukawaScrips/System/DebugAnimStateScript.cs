using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAnimStateScript : MonoBehaviour
{

    //プレイヤーのアニメーションの管理をするスクリプト

    //アニメーション判別用変数
    private string AniState = "NONE";

    //各種アニメーション時の処理を行うスクリプト
    private ComProcessScript CPScript;

    private DamageProInfoScript DPInfo;

    void Awake()
    {
        //スクリプト取得
        CPScript = this.GetComponent<ComProcessScript>();

        DPInfo = this.GetComponentInChildren<DamageProInfoScript>();
    }

    void Update()
    {
        //状態の取得
        AniState = CPScript.motionState;

        //移動関係
        MoveAnimProces(AniState);
        //攻撃関係
        AttackAnimProces(AniState);
        //食らい関係
        HitAnimProces(AniState);
        //特殊関係
        SpecialAnimProces();
    }

    //移動アニメーション群
    public void MoveAnimProces(string state)
    {
        switch (state)
        {
            //case "Neutral":
            //    StandPro();
            //    StandAnim();
            //    break;
            //case "FrontWalk":
            //    FrontWalkPro();
            //    FrontWalkAnim();
            //    break;
            //case "BackWalk":
            //    BackWalkPro();
            //    BackWalkAnim();
            //    break;
            //case "Sit":
            //    SitPro();
            //    SitAnim();
            //    break;
            //case "Jump":
            //    JumpPro();
            //    JumpingPro();
            //    JumpAnim();
            //    break;
            //case "BackJump":
            //    JumpPro();
            //    JumpingPro();
            //    JumpAnim();
            //    break;
            //case "FrontJump":
            //    JumpPro();
            //    JumpingPro();
            //    JumpAnim();
            //    break;
            //default:
            //    break;
        }
    }

    //攻撃アニメーション群
    public void AttackAnimProces(string state)
    {
        switch (state)
        {
            //case "5A":
            //    PunchPro();
            //    PunchAnim();
            //    break;
            //case "5B":
            //    KickPro();
            //    KickAnim();
            //    break;
            //case "2A":
            //    SitPunchPro();
            //    SitPunchAnim();
            //    break;
            //case "2B":
            //    SitKickPro();
            //    SitKickAnim();
            //    break;
            //case "JA":
            //    JumpPunchPro();
            //    JumpPunchAnim();
            //    break;
            //case "JB":
            //    JumpKickPro();
            //    JumpKickAnim();
            //    break;
            //default:
            //    break;
        }
    }

    //食らいアニメーション群
    public void HitAnimProces(string state)
    {
        switch (state)
        {
            case "StandGuard":
                DPInfo.StandGuardPro();
                //    StandGuardAnim();
                break;
            case "SitGuard":
                DPInfo.SitGuardPro();
                //    SitGuardAnim();
                break;
            case "Damage":
                DPInfo.DamagePro();
                //    DamageAnim();
                break;
            //case "JumpingDamage":
            //    JumpingDamagePro();
            //    JumpingDamageAnim();
            //    break;
            case "SitDamage":
                DPInfo.SitDamagePro();
                //    SitDamageAnim();
                break;
            default:
                break;
        }
    }

    //特殊アニメーション群
    public void SpecialAnimProces()
    {
        ////走り状態
        //if (CPScript.dashMot)
        //{
        //    DashPro();
        //    DashAnim();
        //}
        ////波動拳状態
        //if (CPScript.hadouMot)
        //{
        //    HadouPro();
        //    HadouAnim();
        //    CPScript.hadouMot = false;
        //}
        ////昇竜拳状態
        //if (CPScript.shoryuMot)
        //{
        //    ShoryuPro();
        //    ShoryuAnim();
        //    CPScript.shoryuMot = false;
        //}
    }
}