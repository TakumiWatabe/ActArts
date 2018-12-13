using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAnimStateScript : MonoBehaviour
{

    //プレイヤーのアニメーションの管理をするスクリプト

    //アニメーション判別用変数
    private string AniState = "NONE";

    //各種アニメーション時の処理を行うスクリプト

    void Awake()
    {
        //スクリプト取得
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
        //switch (state)
        //{
        //    case "StandGuard":
        //        StandGuardPro();
        //        StandGuardAnim();
        //        break;
        //    case "SitGuard":
        //        SitGuardPro();
        //        SitGuardAnim();
        //        break;
        //    case "Damage":
        //        DamagePro();
        //        DamageAnim();
        //        break;
        //    case "JumpingDamage":
        //        JumpingDamagePro();
        //        JumpingDamageAnim();
        //        break;
        //    case "SitDamage":
        //        SitDamagePro();
        //        SitDamageAnim();
        //        break;
        //    default:
        //        break;
        //}
    }

    //特殊アニメーション群
    public void SpecialAnimProces()
    {
        //switch (state)
        //{
        //    case "Dash":
        //        DashPro();
        //        DashAnim();
        //        break;
        //    case "236A":
        //        HadouPro();
        //        HadouAnim();
        //        break;
        //    case "623A":
        //        ShoryuPro();
        //        ShoryuAnim();
        //        break;
        //}
    }

    ////変数取得用
    //public string StateMove
    //{
    //    get { return MoveAniSta; }
    //    set { MoveAniSta = value; }
    //}

    //public string StateAtk
    //{
    //    get { return AtkAniSta; }
    //    set { AtkAniSta = value; }
    //}

    //public string StateHit
    //{
    //    get { return HitAniSta; }
    //    set { HitAniSta = value; }
    //}

    //public string StateSpe
    //{
    //    get { return SpeAniSta; }
    //    set { SpeAniSta = value; }
    //}
}