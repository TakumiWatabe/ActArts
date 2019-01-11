﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour {

    //--------------------------------------
    //あたり判定設定
    //--------------------------------------

    //くらい判定
    [SerializeField]
    private List<GameObject> HitCollider;
    //攻撃判定
    [SerializeField]
    private List<GameObject> AtkCollider;

    //飛び道具判定
    [SerializeField]
    private GameObject ToolCollider;

    //コライダー
    private List<BoxCollider> HitBox = new List<BoxCollider>();
    private List<BoxCollider> AtkBox = new List<BoxCollider>();

    //あたり判定の数
    int HCnum;
    int ACnum;

    //あたり判定の固定変数
    private const float CSizeZ = 0.25f;

    //攻撃種類判定用
    private ValueScript.AtkVal atkType = ValueScript.AtkVal.ATK_NUM;

    // Use this for initialization
    void Start ()
    {
        //あたり判定の数
        HCnum = HitCollider.Count;
        ACnum = AtkCollider.Count;

        //初期化
        HitColliderActive(0);
        AtkColliderActive(0);
        ToolCollider.SetActive(true);
        InitCollider();

    }

    //-------------------------------------------------------------------------------------------------------------------
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Aoiあたり判定
    //
    //-------------------------------------------------------
    //基本あたり判定
    //
    //使用モーション:立ち、前進、後進、ジャンプ、立ちガード、ダメージ
    //-------------------------------------------------------
    void BasicCollide()
    {
        //初期化
        HitColliderActive(1);
        AtkColliderActive(0);

        //基本くらい判定
        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0.05f),
            new Vector3(CSizeZ, 1.6f, 0.7f));
    }

    //-----------------------------------------
    //基本しゃがみあたり判定
    //
    //使用モーション:しゃがみ、しゃがみガード
    //-----------------------------------------
    void BasicSitCollide()
    {
        //初期化
        HitColliderActive(1);
        AtkColliderActive(0);

        //基本しゃがみ判定
        SetBoxState(HitBox[0],
            new Vector3(0, 1.08f, 0f),
            new Vector3(CSizeZ, 1.15f, 0.7f));
    }

    //------------------
    //パンチあたり判定
    //
    //使用モーション:立ちパンチ
    //------------------
    void BasePunchCollider()
    {
        //初期化
        HitColliderActive(2);
        AtkColliderActive(1);

        //コライダー設定
        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0.05f),
            new Vector3(CSizeZ, 1.6f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1f, 0.35f),
            new Vector3(CSizeZ, 0.25f, 0.4f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1, 0.4f),
            new Vector3(CSizeZ, 0.45f, 0.5f));

        //弱
        atkType = ValueScript.AtkVal.PUNCH;
    }
    void SetPunchCollider_1()
    {
        AtkColliderActive(0);

        //コライダー設定
        SetBoxState(HitBox[1],
            new Vector3(0, 1, 0.35f),
            new Vector3(CSizeZ, 0.35f, 0.55f));

    }
    void SetPunchCollider_2()
    {
        //コライダー設定
        SetBoxState(HitBox[1],
            new Vector3(0, 1, 0.25f),
            new Vector3(CSizeZ, 0.5f, 0.3f));
    }

    //------------------
    //キックあたり判定
    //
    //使用モーション:立ちキック フレーム数:5
    //------------------
    void BaseKickColliderAtk()
    {
        HitColliderActive(2);
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0.05f),
            new Vector3(CSizeZ, 1.6f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.85f, 0.45f),
            new Vector3(0.25f, 0.3f, 0.8f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.9f, 0.48f),
            new Vector3(CSizeZ, 0.15f, 0.95f));

        //強
        atkType = ValueScript.AtkVal.KICK;
    }
    void SetKickCollider_1()
    {
        SetBoxState(HitBox[1],
            new Vector3(0, 0.75f, 0.5f),
            new Vector3(CSizeZ, 0.3f, 0.9f));
    }
    void SetKickCollider_2()
    {
        AtkColliderActive(0);
    }
    void SetKickCollider_3()
    {
        SetBoxState(HitBox[1],
            new Vector3(0, 0.65f, 0.4f),
            new Vector3(CSizeZ, 0.3f, 0.75f));
    }

    //--------------------------------
    //ダウンあたり判定
    //
    //使用モーション:ダウン
    //--------------------------------
    void BasicDownCollider()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.2f),
            new Vector3(CSizeZ, 0.5f, 2f));
    }

    //--------------------------------
    //昇竜コマンドあたり判定
    //
    //使用モーション:昇竜
    //--------------------------------
    void BasicShoruCollider1()
    {
        HitColliderActive(0);
        AtkColliderActive(1);

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.6f, 0.45f),
            new Vector3(CSizeZ, 1f, 0.6f));

        //昇竜コマンド
        atkType = ValueScript.AtkVal.SYORYUKEN;
    }
    void BasicShoruCollider2()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0),
            new Vector3(CSizeZ, 1.6f, 0.45f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.2f, 0),
            new Vector3(CSizeZ, 0.3f, 0.7f));
    }
    void BasicShoruCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0),
            new Vector3(CSizeZ, 1.6f, 0.45f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.2f, 0.2f),
            new Vector3(CSizeZ, 0.3f, 0.5f));
    }
    void BasicShoruCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0),
            new Vector3(CSizeZ, 1.6f, 0.45f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.8f, 1));
    }

    //--------------------------------
    //波動コマンドあたり判定
    //
    //使用モーション:波動
    //--------------------------------
    void BasicHadouCollider1()
    {
        HitColliderActive(3);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0),
            new Vector3(CSizeZ, 1.55f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.32f, 0.3f),
            new Vector3(CSizeZ, 0.5f, 0.3f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.1f, 0.35f),
            new Vector3(CSizeZ, 0.25f, 0.4f));

        //波動コマンド
        atkType = ValueScript.AtkVal.HADOUKEN;
    }
    void BasicHadouCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0),
            new Vector3(CSizeZ, 1.55f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.32f, 0.3f),
            new Vector3(CSizeZ, 0.5f, 0.3f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.5f, 0.3f),
            new Vector3(CSizeZ, 0.3f, 0.4f));

    }
    void BasicHadouCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, -0.05f),
            new Vector3(CSizeZ, 1.5f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.42f, 0.35f),
            new Vector3(CSizeZ, 0.55f, 0.35f));
        SetBoxState(HitBox[2], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void BasicHadouCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.35f, 0.35f),
            new Vector3(CSizeZ, 0.5f, 0.35f));
    }
    void BasicHadouCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0f),
            new Vector3(0.25f, 1.55f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.32f, 0.3f),
            new Vector3(0.25f, 0.5f, 0.3f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.2f, 0.4f),
            new Vector3(0.25f, 0.25f, 0.45f));
    }

    //--------------------------------
    //しゃがみパンチあたり判定
    //
    //使用モーション:しゃがみパンチ
    //--------------------------------
    void SitPunchCollider_1()
    {
        HitColliderActive(2);
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.08f, 0),
            new Vector3(CSizeZ, 1.15f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.9f, 0.4f),
            new Vector3(CSizeZ, 0.3f, 0.3f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.8f, 0.4f),
            new Vector3(CSizeZ, 0.5f, 0.6f));

        //しゃがみ弱
        atkType = ValueScript.AtkVal.PUNCH_SIT;
    }
    void SitPunchCollider_2()
    {
        AtkColliderActive(0);
    }

    //--------------------------------
    //しゃがみキックあたり判定
    //
    //使用モーション:しゃがみキック
    //--------------------------------
    void SitKickCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.05f, 0),
            new Vector3(CSizeZ, 1.05f, 0.7f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        //しゃがみ強
        atkType = ValueScript.AtkVal.KICK_SIT;
    }
    void SitKickCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.02f, 0),
            new Vector3(CSizeZ, 0.9f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.75f, 0.45f),
            new Vector3(CSizeZ, 0.35f, 0.5f));
    }
    void SitKickCollider3()
    {
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, -0.1f),
            new Vector3(CSizeZ, 0.88f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.75f, 0.5f),
            new Vector3(CSizeZ, 0.35f, 0.75f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.7f, 0.6f),
            new Vector3(CSizeZ, 0.2f, 1.3f));

    }
    void SitKickCollider4()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, -0.15f),
            new Vector3(CSizeZ, 0.85f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.75f, 0.5f),
            new Vector3(CSizeZ, 0.35f, 0.75f));
    }
    void SitKickCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.98f, -0.2f),
            new Vector3(CSizeZ, 0.8f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.9f, 0.4f),
            new Vector3(CSizeZ, 0.25f, 0.7f));
    }
    void SitKickCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.98f, -0.2f),
            new Vector3(CSizeZ, 0.8f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0.35f),
            new Vector3(CSizeZ, 0.35f, 0.6f));
    }
    void SitKickCollider7()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.02f, -0.1f),
            new Vector3(CSizeZ, 0.9f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.67f, 0.3f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void SitKickCollider8()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.08f, -0.05f),
            new Vector3(CSizeZ, 1f, 0.5f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void SitKickCollider9()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.08f, -0.05f),
            new Vector3(CSizeZ, 1f, 0.6f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }

    //--------------------------------
    //ジャンプパンチあたり判定
    //
    //使用モーション:ジャンプパンチ
    //--------------------------------
    void JumpPunchCollider1()
    {
        HitColliderActive(3);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, 0.1f),
            new Vector3(CSizeZ, 1.3f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.25f),
            new Vector3(CSizeZ, 0.8f, 0.4f));
        SetBoxState(HitBox[2], new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        //ジャンプ弱
        atkType = ValueScript.AtkVal.PUNCH_JUMP;
    }
    void JumpPunchCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0.1f),
            new Vector3(CSizeZ, 1.2f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.25f),
            new Vector3(CSizeZ, 0.8f, 0.4f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.25f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void JumpPunchCollider3()
    {
        AtkColliderActive(2);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0),
            new Vector3(CSizeZ, 1.2f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.4f),
            new Vector3(CSizeZ, 0.65f, 0.4f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.25f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.5f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.25f, 0.5f),
            new Vector3(CSizeZ, 0.5f, 0.7f));
        SetBoxState(AtkBox[1],
            new Vector3(0, 1.25f, 0.45f),
            new Vector3(CSizeZ, 0.7f, 0.4f));
    }
    void JumpPunchCollider4()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0),
            new Vector3(CSizeZ, 1.2f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.4f),
            new Vector3(CSizeZ, 0.65f, 0.4f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.2f, 0.45f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void JumpPunchCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, -0.02f),
            new Vector3(CSizeZ, 1.3f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.3f, -0.4f),
            new Vector3(CSizeZ, 0.55f, 0.35f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.15f, 0.35f),
            new Vector3(CSizeZ, 0.3f, 0.35f));
    }
    void JumpPunchCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, -0.02f),
            new Vector3(CSizeZ, 1.3f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.3f, -0.35f),
            new Vector3(CSizeZ, 0.55f, 0.35f));
        SetBoxState(HitBox[2],
            new Vector3(0, 1.1f, 0.3f),
            new Vector3(CSizeZ, 0.3f, 0.3f));
    }

    //--------------------------------
    //ジャンプキックあたり判定
    //
    //使用モーション:ジャンプキック
    //--------------------------------
    void JumpKickCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, -0.05f),
            new Vector3(CSizeZ, 1.3f, 0.45f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        //ジャンプ強
        atkType = ValueScript.AtkVal.KICK_JUMP;
    }
    void JumpKickCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.05f, -0.05f),
            new Vector3(CSizeZ, 1.15f, 0.45f));
        SetBoxState(HitBox[1], 
            new Vector3(0, 0.5f, 0.25f), 
            new Vector3(CSizeZ, 0.35f, 0.35f));
    }
    void JumpKickCollider3()
    {
        AtkColliderActive(2);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.05f, -0.05f),
            new Vector3(CSizeZ, 1.1f, 0.45f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.3f),
            new Vector3(CSizeZ, 0.5f, 0.5f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.7f, 0.05f),
            new Vector3(CSizeZ, 0.45f, 0.7f));
        SetBoxState(AtkBox[1],
            new Vector3(0, 0.45f, 0.4f),
            new Vector3(CSizeZ, 0.5f, 0.5f));
    }
    void JumpKickCollider4()
    {
        AtkColliderActive(0);
    }
    void JumpKickCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.05f, -0.05f),
            new Vector3(CSizeZ, 1.1f, 0.45f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.3f, 0.1f),
            new Vector3(CSizeZ, 0.325f, 0.25f));
    }

    //--------------------------------
    //ダッシュあたり判定
    //
    //使用モーション:ダッシュ
    //--------------------------------
    void BasicDashCollider()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0f),
            new Vector3(CSizeZ, 1.25f, 1f));
    }

    //--------------------------------
    //くらい強あたり判定
    //
    //使用モーション:くらい強
    //--------------------------------
    void BasicLDCollider()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.78f, 0.1f),
            new Vector3(CSizeZ, 1.45f, 0.7f));
    }

    //--------------------------------------
    //しゃがみ系あたり判定
    //
    //使用モーション:立ち上がり、しゃがみ中
    //--------------------------------------
    void SitCollider()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.08f, 0f),
            new Vector3(CSizeZ, 1.15f, 0.7f));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //-------------------------------------------------------------------------------------------------------------------

    //-------------------------------------------------------------------------------------------------------------------
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Hikariあたり判定
    //
    //---------------------------------------------------------
    //基本あたり判定
    //
    //使用モーション:立ち、前進、後進、立ちガード、ダメージ
    //---------------------------------------------------------
    void HBasicCollide()
    {
        //くらい判定初期化
        HitColliderActive(1);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.78f, 0f),
            new Vector3(CSizeZ, 1.55f, 0.65f));
    }

    //------------------------------------------------------
    //基本しゃがみあたり判定
    //
    //使用モーション:しゃがみ、しゃがみガード、しゃがみダメ
    //------------------------------------------------------
    void HBasicSitCollide()
    {
        //くらい判定初期化
        HitColliderActive(1);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.6f));
    }

    //--------------------------------
    //ダッシュあたり判定
    //
    //使用モーション:ダッシュ
    //--------------------------------
    void HBasicDashCollider1()
    {
        //くらい判定初期化
        HitColliderActive(2);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
    }
    void HBasicDashCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.8f));
    }
    void HBasicDashCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.95f));
    }
    void HBasicDashCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1.25f));
    }
    void HBasicDashCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1f));
    }
    void HBasicDashCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
    }
    void HBasicDashCollider7()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1f));
    }
    void HBasicDashCollider8()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1.25f));
    }
    void HBasicDashCollider9()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1f));
    }
    void HBasicDashCollider10()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
    }

    //--------------------------------
    //波動コマンドあたり判定
    //
    //使用モーション:波動
    //--------------------------------
    void HBasicHadouCollider1()
    {
        //くらい判定初期化
        HitColliderActive(1);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.4f),
            new Vector3(CSizeZ, 0.3f, 0.5f));

        //波動コマンド
        atkType = ValueScript.AtkVal.HADOUKEN;
    }
    void HBasicHadouCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.4f),
            new Vector3(CSizeZ, 0.3f, 0.55f));
    }
    void HBasicHadouCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0.4f),
            new Vector3(CSizeZ, 0.3f, 0.3f));
    }

    //-----------------------------------------
    //ジャンプあたり判定
    //
    //使用モーション:ジャンプ、ジャンプ中
    //-----------------------------------------
    void HBasicJumpCollide1()
    {
        //くらい判定初期化
        HitColliderActive(2);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.3f, 0f),
            new Vector3(CSizeZ, 0.55f, 0.8f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.6f, 0.05f),
            new Vector3(CSizeZ, 0.9f, 0.6f));
    }
    void HBasicJumpCollide2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.8f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.45f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.45f));
    }

    //--------------------------------
    //ジャンプキックあたり判定
    //
    //使用モーション:ジャンプキック
    //--------------------------------
    void HJumpKickCollider1()
    {
        //くらい判定初期化
        HitColliderActive(2);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.6f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.48f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.45f));

        //ジャンプ強
        atkType = ValueScript.AtkVal.KICK_JUMP;
    }
    void HJumpKickCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.3f, 0f),
            new Vector3(CSizeZ, 0.55f, 0.8f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.15f),
            new Vector3(CSizeZ, 0.6f, 0.75f));
    }
    void HJumpKickCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.3f, 0f),
            new Vector3(CSizeZ, 0.5f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0.15f),
            new Vector3(CSizeZ, 0.6f, 0.75f));
    }
    void HJumpKickCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0f),
            new Vector3(CSizeZ, 0.7f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.25f),
            new Vector3(CSizeZ, 0.6f, 1f));
    }
    void HJumpKickCollider5()
    {
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0f),
            new Vector3(CSizeZ, 0.7f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.25f),
            new Vector3(CSizeZ, 0.6f, 1f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.6f, 0.5f),
            new Vector3(CSizeZ, 0.3f, 0.85f));
    }
    void HJumpKickCollider6()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.65f, 0.25f),
            new Vector3(CSizeZ, 0.6f, 1f));
    }
    void HJumpKickCollider7()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.6f, 0.15f),
            new Vector3(CSizeZ, 0.7f, 0.8f));
    }
    void HJumpKickCollider8()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.1f),
            new Vector3(CSizeZ, 0.9f, 0.65f));
    }
    void HJumpKickCollider9()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.1f),
            new Vector3(CSizeZ, 0.9f, 0.55f));
    }
    void HJumpKickCollider10()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.8f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.5f));
    }

    //--------------------------------
    //ジャンプパンチあたり判定
    //
    //使用モーション:ジャンプパンチ
    //--------------------------------
    void HJumpPunchCollider1()
    {
        //くらい判定初期化
        HitColliderActive(2);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0f),
            new Vector3(CSizeZ, 1.2f, 0.5f));

        //ジャンプ弱
        atkType = ValueScript.AtkVal.PUNCH_JUMP;
    }
    void HJumpPunchCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(CSizeZ, 0.65f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0f),
            new Vector3(CSizeZ, 0.9f, 0.5f));
    }
    void HJumpPunchCollider3()
    {
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(CSizeZ, 0.65f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.75f, 0f),
            new Vector3(CSizeZ, 0.5f, 0.5f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.18f, 0.45f),
            new Vector3(CSizeZ, 0.3f, 0.5f));
    }
    void HJumpPunchCollider4()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(CSizeZ, 0.65f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.65f, 0f),
            new Vector3(CSizeZ, 0.95f, 0.5f));
    }
    void HJumpPunchCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(CSizeZ, 0.65f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.6f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.5f));
    }

    //--------------------------------------
    //しゃがみ中あたり判定
    //
    //使用モーション:しゃがみ中
    //--------------------------------------
    void HSitingCollider1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.75f, 0f),
            new Vector3(CSizeZ, 1.6f, 0.5f));
    }
    void HSitingCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0f),
            new Vector3(CSizeZ, 1.25f, 0.5f));
    }

    //--------------------------------
    //しゃがみキックあたり判定
    //
    //使用モーション:しゃがみキック
    //--------------------------------
    void HSitKickCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.5f));
        SetBoxState(HitBox[1],new Vector3(0, 0, 0),new Vector3(0, 0, 0));

        //しゃがみ強
        atkType = ValueScript.AtkVal.KICK_SIT;
    }
    void HSitKickCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1f, -0.1f),
            new Vector3(CSizeZ, 1f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.3f),
            new Vector3(CSizeZ, 0.4f, 0.5f));
    }
    void HSitKickCollider3()
    {
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, -0.15f),
            new Vector3(CSizeZ, 0.9f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(CSizeZ, 0.35f, 0.7f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.65f, 0.5f),
            new Vector3(CSizeZ, 0.25f, 0.8f));
    }
    void HSitKickCollider4()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, -0.15f),
            new Vector3(CSizeZ, 0.9f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(CSizeZ, 0.35f, 0.7f));
    }
    void HSitKickCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1f, -0.1f),
            new Vector3(CSizeZ, 0.95f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.2f, 0.2f),
            new Vector3(CSizeZ, 0.25f, 0.5f));
    }
    void HSitKickCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, 0.1f),
            new Vector3(CSizeZ, 0.9f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, -0.45f),
            new Vector3(CSizeZ, 0.35f, 0.7f));
    }
    void HSitKickCollider7()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0.1f),
            new Vector3(CSizeZ, 0.95f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, -0.3f),
            new Vector3(CSizeZ, 0.35f, 0.45f));
    }


    //--------------------------------
    //しゃがみパンチあたり判定
    //
    //使用モーション:しゃがみパンチ
    //--------------------------------
    void HSitPunchCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.55f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        //しゃがみ弱
        atkType = ValueScript.AtkVal.PUNCH_SIT;
    }
    void HSitPunchCollider2()
    {
        AtkColliderActive(1);

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.1f, 0.4f),
            new Vector3(CSizeZ, 0.45f, 0.6f));
    }
    void HSitPunchCollider3()
    {
        AtkColliderActive(0);
    }
    void HSitPunchCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0f),
            new Vector3(CSizeZ, 1.1f, 0.55f));
        SetBoxState(HitBox[1], 
            new Vector3(0, 1.1f, 0.3f), 
            new Vector3(CSizeZ, 0.25f, 0.5f));
    }

    //-----------------------------------------
    //立ち上がりあたり判定
    //
    //使用モーション:立ち上がり
    //-----------------------------------------
    void HStandUpCollider1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.18f, 0f),
            new Vector3(CSizeZ, 1.15f, 0.6f));
    }
    void HStandUpCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0f),
            new Vector3(CSizeZ, 1.3f, 0.65f));
    }
    void HStandUpCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, 0f),
            new Vector3(CSizeZ, 1.65f, 0.65f));
    }

    //------------------
    //強攻撃あたり判定
    //
    //使用モーション:強攻撃
    //------------------
    void HBaseKickCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        SetBoxState(HitBox[1],new Vector3(0, 0, 0),new Vector3(0, 0, 0));

        //強
        atkType = ValueScript.AtkVal.KICK;
    }
    void HBaseKickCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.15f, 0.2f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.55f, 0f),
            new Vector3(CSizeZ, 0.8f, 1f));
    }
    void HBaseKickCollider3()
    {
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.15f, 0.3f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.55f, -0.1f),
            new Vector3(CSizeZ, 0.8f, 0.9f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1f, 0.6f),
            new Vector3(CSizeZ, 0.35f, 0.65f));
    }
    void HBaseKickCollider4()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.15f, 0.3f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.55f, -0.1f),
            new Vector3(CSizeZ, 0.8f, 0.9f));
    }
    void HBaseKickCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.15f, 0.2f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.55f, 0f),
            new Vector3(CSizeZ, 0.85f, 0.9f));
    }
    void HBaseKickCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.15f, 0.2f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.55f, 0),
            new Vector3(CSizeZ, 0.85f, 1f));
    }

    //--------------------------------
    //ダウンあたり判定
    //
    //使用モーション:ダウン
    //--------------------------------
    void HDownCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.78f, 0.05f),
            new Vector3(CSizeZ, 1.5f, 0.6f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void HDownCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.78f, 0.05f),
            new Vector3(CSizeZ, 1.55f, 0.6f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void HDownCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.65f, -0.1f),
            new Vector3(CSizeZ, 1.35f, 0.6f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void HDownCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.75f, -0.2f),
            new Vector3(CSizeZ, 0.5f, 0.9f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.35f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
    }
    void HDownCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, -0.2f),
            new Vector3(CSizeZ, 0.45f, 0.9f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.35f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
    }
    void HDownCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, -0.6f, -0.2f),
            new Vector3(CSizeZ, 0.45f, 1f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.6f, 0.45f),
            new Vector3(CSizeZ, 0.6f, 0.6f));
    }

    //--------------------------------
    //昇竜コマンドあたり判定
    //
    //使用モーション:昇竜
    //--------------------------------
    void HBasicShoruCollider1()
    {
        HitColliderActive(0);
        AtkColliderActive(1);

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(CSizeZ, 1.2f, 0.5f));

        //昇竜コマンド
        atkType = ValueScript.AtkVal.SYORYUKEN;
    }
    void HBasicShoruCollider2()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0.05f),
            new Vector3(CSizeZ, 1.3f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, -0.35f),
            new Vector3(CSizeZ, 0.75f, 0.35f));
    }
    void HBasicShoruCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.85f, 0.05f),
            new Vector3(CSizeZ, 1.5f, 0.5f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void HBasicShoruCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.75f, 0.05f),
            new Vector3(CSizeZ, 1.5f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.4f, 0.35f),
            new Vector3(CSizeZ, 0.5f, 0.2f));
    }
    void HBasicShoruCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.65f, 0.05f),
            new Vector3(CSizeZ, 1.4f, 0.55f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void HBasicShoruCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0f),
            new Vector3(CSizeZ, 1.3f, 0.55f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void HBasicShoruCollider7()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0f),
            new Vector3(CSizeZ, 1.3f, 0.5f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }

    //------------------
    //パンチあたり判定
    //
    //使用モーション:立ちパンチ　フレーム数:15
    //------------------
    void HBasePunchCollider1()
    {
        HitColliderActive(1);
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.4f),
            new Vector3(CSizeZ, 0.3f, 0.5f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 1.15f, 0.45f),
            new Vector3(CSizeZ, 0.4f, 0.6f));

        //弱
        atkType = ValueScript.AtkVal.PUNCH;
    }
    void HBasePunchCollider2()
    {
        AtkColliderActive(0);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //-------------------------------------------------------------------------------------------------------------------

    //初期化関数
    private void InitCollider()
    {
        //あたり判定のコライダーを格納
        for (int i = 0; i < HCnum; i++)
        {
            HitBox.Add(HitCollider[i].GetComponent<BoxCollider>());
        }

        for (int i = 0; i < ACnum; i++)
        {
            AtkBox.Add(AtkCollider[i].GetComponent<BoxCollider>());
        }
    }

    //くらい判定初期化関数
    private void HitColliderActive(int val)
    {
        for (int i = 0; i < HCnum; i++)
        {
            //判定非動作
            HitCollider[i].SetActive(false);
        }

        for (int i = 0; i < val; i++)
        {
            //判定非動作
            HitCollider[i].SetActive(true);
        }
    }

    //攻撃判定初期化関数
    private void AtkColliderActive(int val)
    {
        for (int i = 0; i < ACnum; i++)
        {
            //判定非動作
            AtkCollider[i].SetActive(false);
        }

        for (int i = 0; i < val; i++)
        {
            //判定非動作
            AtkCollider[i].SetActive(true);
        }
    }

    //あたり判定設定関数
    private void SetBoxState(BoxCollider box, Vector3 pos, Vector3 size)
    {
        box.center = pos;
        box.size = size;
    }

    //変数取得
    public List<GameObject> HClid { get { return HitCollider; } }
    public List<GameObject> AClid { get { return AtkCollider; } }
    public GameObject TClid { get { return ToolCollider; } }
    public List<BoxCollider> GetHitBoxs { get { return HitBox; } }
    public List<BoxCollider> GetAtkBoxs { get { return AtkBox; } }
    public new ValueScript.AtkVal GetType { get { return atkType; } }
}