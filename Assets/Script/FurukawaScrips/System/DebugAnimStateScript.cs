using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAnimStateScript : MonoBehaviour
{

    //プレイヤーのアニメーションの管理をするスクリプト

    //アニメーション判別用変数
    private string MoveAniSta = "NONE";
    private string AtkAniSta = "NONE";
    private string HitAniSta = "NONE";

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
            //case "Stand":
            //    Stand();
            //    break;
            //case "Walk":
            //    Walk();
            //    break;
            //case "Dash":
            //    Dashing();
            //    break;
            //case "Sit":
            //    Sit();
            //    break;
            //case "Jump":
            //    Jump();
            //    Jumping();
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
            //    Punch();
            //    break;
            //case "5B":
            //    Kick();
            //    break;
            //case "2A":
            //    SitPunch();
            //    break;
            //case "2B":
            //    SitKick();
            //    break;
            //case "JA":
            //    Punch();
            //    break;
            //case "JB":
            //    Kick();
            //    break;
            //case "236A":
            //    SitPunch();
            //    break;
            //case "623A":
            //    SitKick();
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
            //case "StandGuard":
            //    StandGuard();
            //    break;
            //case "SitGuard":
            //    SitGuard();
            //    break;
            //case "Damage":
            //    Damage();
            //    break;
            //case "JumpingDamage":
            //    JumpingDamage();
            //    break;
            //case "SitDamage":
            //    SitDamage();
            //    break;
            //default:
            //    break;
        }
    }

    //変数取得用
    public string StateMove
    {
        get { return MoveAniSta; }
        set { MoveAniSta = value; }
    }

    public string StateAtk
    {
        get { return AtkAniSta; }
        set { AtkAniSta = value; }
    }

    public string StateHit
    {
        get { return HitAniSta; }
        set { HitAniSta = value; }
    }
}