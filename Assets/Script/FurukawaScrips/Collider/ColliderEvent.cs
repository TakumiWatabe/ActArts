using System.Collections;
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
    //汎用あたり判定
    //
    //-------------------------------------------------------
    //勝利・敗北あたり判定
    //
    //使用モーション:勝利・敗北
    //-------------------------------------------------------
    void BasicEventCollide()
    {
        //初期化
        HitColliderActive(0);
        AtkColliderActive(0);
    }

    //-------------------------------------------------------
    //Shiroganeあたり判定
    //-------------------------------------------------------
    //昇竜
    void S_ShoryuCollidH1()
    {
        HitColliderActive(0);
        AtkColliderActive(0);

        atkType = ValueScript.AtkVal.SYORYUKEN;
    }
    void S_ShoryuCollidH2()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.21f, 0.0f),
            new Vector3(0.43f, 1.0f, 0.25f),
            new Vector3(0.0f, -180.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.0f, -0.09f, 0.0f),
            new Vector3(1.51f, 0.35f, 0.25f),
            new Vector3(0.0f, -180.0f, 0.0f));
    }
    void S_ShoryuCollidH3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.21f, 0.0f),
            new Vector3(0.43f, 1.0f, 0.25f),
            new Vector3(0.0f, -180.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.25f, -0.4f, 0.0f),
            new Vector3(0.69f, 0.59f, 0.25f),
            new Vector3(0.0f, -105.0f, 0.0f));
    }
    void S_ShoryuCollidH4()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.21f, 0.0f),
            new Vector3(0.43f, 1.0f, 0.25f),
            new Vector3(0.0f, -180.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.22f, -0.1f, 0.0f),
            new Vector3(0.69f, 0.59f, 0.25f),
            new Vector3(0.0f, -155.0f, 0.0f));
    }
    void S_ShoryuCollidH5()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.39f, 0.0f, 0.0f),
            new Vector3(-0.93f, 0.35f, 0.25f),
            new Vector3(0.0f, -105.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.39f, 0.0f, 0.0f),
            new Vector3(-0.93f, 0.35f, 0.25f),
            new Vector3(0.0f, -105.0f, 0.0f));
    }
    void S_ShoryuCollidH6()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.02f, 0.22f, 0.0f),
            new Vector3(0.31f, 0.84f, 0.25f),
            new Vector3(0.0f, -175.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.39f, 0.0f, 0.0f),
            new Vector3(-0.93f, 0.35f, 0.25f),
            new Vector3(0.0f, -80.0f, 0.0f));
    }
    void S_ShoryuCollidH7()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, -0.12f, 0.0f),
            new Vector3(0.46f, 1.6f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }
    void S_ShoryuCollidA()
    {
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0.08f, -0.19f, 0.0f),
            new Vector3(1.9f, 0.52f, 0.25f),
            new Vector3(0.0f, -180.0f, 0.0f));
    }
    //ジャンプ強
    void S_StrongJumpAttackCollidH1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, -0.12f, 0.0f),
            new Vector3(0.53f, 1.57f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));

        atkType = ValueScript.AtkVal.KICK_JUMP;
    }
    void S_StrongJumpAttackCollidH2()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.09f, 0.12f, 0.0f),
            new Vector3(0.46f, 1.08f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }
    void S_StrongJumpAttackCollidA()
    {
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0.21f, -0.59f, 0.0f),
            new Vector3(0.48f, 0.74f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }

    //ジャンプ弱
    void S_WeekJumpAttackCollidH1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, -0.13f, 0.0f),
            new Vector3(0.53f, 1.6f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));

        atkType = ValueScript.AtkVal.PUNCH_JUMP;
    }
    void S_WeekJumpAttackCollidH2()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.15f, 0.0f),
            new Vector3(0.53f, 1.02f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(-0.03f, -0.29f, 0.0f),
            new Vector3(0.68f, 0.35f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }
    void S_WeekJumpAttackCollidA()
    {
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0.57f, -0.4f, 0.0f),
            new Vector3(0.89f, 0.58f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //しゃがみ強
    void S_StrongSitAttackCollidH1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.1f, 0.37f, 0.0f),
            new Vector3(0.37f, 0.7f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.07f, -0.09f, 0.0f),
            new Vector3(0.75f, 0.59f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));

        atkType = ValueScript.AtkVal.KICK_SIT;
    }
    void S_StrongSitAttackCollidH2()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.16f, -0.01f, 0.0f),
            new Vector3(0.86f, 0.62f, 0.25f),
            new Vector3(0.0f, 35.0f, 0.0f));
    }
    void S_StrongSitAttackCollidH3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.16f, -0.01f, 0.0f),
            new Vector3(0.86f, 0.62f, 0.25f),
            new Vector3(0.0f, 35.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(-0.04f, -0.39f, 0.0f),
            new Vector3(1.26f, 0.35f, 0.25f),
            new Vector3(0.0f, 35.0f, 0.0f));
    }
    void S_StrongSitAttackCollidA()
    {
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0.57f, -0.15f, 0.0f),
            new Vector3(0.89f, 0.37f, 0.25f),
            new Vector3(0.0f, 35.0f, 0.0f));
    }

    //しゃがみ弱
    void S_WeekSitAttackCollidH()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.1f, 0.37f, 0.0f),
            new Vector3(0.37f, 0.7f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.07f, -0.09f, 0.0f),
            new Vector3(0.75f, 0.59f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));

        atkType = ValueScript.AtkVal.PUNCH_SIT;
    }
    void S_WeekSitAttackCollidA()
    {
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0.51f, -0.15f, 0.0f),
            new Vector3(0.89f, 0.44f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //立ち強
    void S_StrongAttackCollidH1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.01f, 0.33f, 0.0f),
            new Vector3(0.37f, 0.7f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.0f, -0.49f, 0.0f),
            new Vector3(0.63f, 0.92f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));

        atkType = ValueScript.AtkVal.KICK;
    }
    void S_StrongAttackCollidH2()
    {
        HitColliderActive(3);
        SetBoxState(HitBox[0],
            new Vector3(-0.14f, 0.33f, 0.0f),
            new Vector3(0.37f, 0.7f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.0f, -0.49f, 0.0f),
            new Vector3(0.32f, 0.92f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
        SetBoxState(HitBox[2],
            new Vector3(0.21f, 0.4f, 0.0f),
            new Vector3(0.37f, 0.94f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }
    void S_StrongAttackCollidH3()
    {
        HitColliderActive(3);
        SetBoxState(HitBox[0],
            new Vector3(-0.14f, 0.33f, 0.0f),
            new Vector3(0.37f, 0.7f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.0f, -0.49f, 0.0f),
            new Vector3(0.32f, 0.92f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
        SetBoxState(HitBox[2],
            new Vector3(0.37f, 0.22f, 0.0f),
            new Vector3(0.64f, 0.64f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }
    void S_StrongAttackCollidH4()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.14f, 0.33f, 0.0f),
            new Vector3(0.37f, 0.7f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.17f, -0.49f, 0.0f),
            new Vector3(0.71f, 0.92f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }
    void S_StrongAttackCollidA1()
    {
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0.51f, -0.15f, 0.0f),
            new Vector3(0.89f, 1.37f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }

    //立ち弱
    void S_WeekAttackCollid1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.4f, 0.0f),
            new Vector3(0.46f, 0.61f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.03f, -0.39f, 0.0f),
            new Vector3(0.68f, 0.99f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));

        atkType = ValueScript.AtkVal.PUNCH;
    }
    void S_WeekAttackCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0.07f, 0.4f, 0.0f),
            new Vector3(0.37f, 0.61f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.12f, -0.39f, 0.0f),
            new Vector3(0.48f, 0.99f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
        SetBoxState(AtkBox[0],
            new Vector3(0.121f, -0.14f, 0.0f),
            new Vector3(0.48f, 1.0f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }
    void S_WeekAttackCollid3()
    {
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0.121f, -0.14f, 0.0f),
            new Vector3(0.48f, 1.0f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }

    //しゃがみくらい
    void S_SitDamageCollid()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.4f, 0.0f),
            new Vector3(0.46f, 0.61f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.03f, -0.09f, 0.0f),
            new Vector3(0.8f, 0.51f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //立くらい
    void S_StandWeekDamageCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, -0.09f, 0.0f),
            new Vector3(0.7f, 1.6f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //しゃがみガード
    void S_SitGuardCollid()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.09f, 0.37f, 0.0f),
            new Vector3(0.46f, 0.59f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.03f, -0.09f, 0.0f),
            new Vector3(0.8f, 0.51f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //立ガード
    void S_StandGuardCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.09f, -0.1f, 0.0f),
            new Vector3(0.46f, 1.6f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }

    //立ち→しゃがみ
    void S_ShtDown1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.3f, 0.0f),
            new Vector3(0.46f, 0.76f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(-0.02f, -0.46f, 0.0f),
            new Vector3(0.95f, 0.78f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }
    void S_ShtDown2()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, 0.3f, 0.0f),
            new Vector3(0.46f, 0.76f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.03f, -0.27f, 0.0f),
            new Vector3(0.86f, 0.9f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //前進
    void S_WalkCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, -0.1f, 0.0f),
            new Vector3(0.62f, 1.6f, 0.25f),
            new Vector3(0.0f, -90.0f, 0.0f));
    }

    //しゃがみ
    void S_SitCollid()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.05f, -0.1f, 0.0f),
            new Vector3(0.83f, 0.5f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.05f, 0.41f, 0.0f),
            new Vector3(0.41f, 0.55f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //ジャンプ
    void S_JumpCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.0f, -0.08f, 0.0f),
            new Vector3(0.65f, 1.6f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //波動
    void S_HadouCollid1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.06f, -0.1f, 0.0f),
            new Vector3(0.75f, 1.6f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }
    void S_HadouCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.25f, -0.1f, 0.0f),
            new Vector3(0.8f, 1.2f, 0.25f),
            new Vector3(0.0f, -85.0f, 0.0f));
    }
    void S_HadouCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0.11f, -0.08f, 0.0f),
            new Vector3(0.69f, 1.47f, 0.25f),
            new Vector3(0.0f, -85.0f, -25.0f));
    }

    //ガークラ
    void S_GuardCrushCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.06f, -0.1f, 0.0f),
            new Vector3(0.75f, 1.6f, 0.25f),
            new Vector3(0.0f, -145.0f, 0.0f));
    }

    //ダッシュ
    void S_DushCollid()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(-0.21f, -0.4f, 0.0f),
            new Vector3(1.2f, 0.68f, 0.25f),
            new Vector3(0.0f, -85.0f, 0.0f));
        SetBoxState(HitBox[1],
            new Vector3(0.15f, 0.24f, 0.0f),
            new Vector3(0.65f, 0.68f, 0.25f),
            new Vector3(0.0f, -85.0f, 0.0f));
    }

    //後退
    void S_BackWalkCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, -0.08f, 0.0f),
            new Vector3(0.37f, 1.6f, 0.3f),
            new Vector3(0.0f, -175.0f, 0.0f));
    }

    //立ち
    void S_StandCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, -0.08f,0.0f),
            new Vector3(0.56f, 1.6f, 0.25f),
            new Vector3(0.0f,-145.0f,0.0f));
    }

    //-------------------------------------------------------------------------------------------------------------------
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Mariあたり判定
    //
    //-------------------------------------------------------
    //基本あたり判定
    //
    //使用モーション:立ち、前進、後進、ジャンプ、立ちガード、ダメージ
    //-------------------------------------------------------
    void M_StandCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }
    //ダッシュ
    void M_DashCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0),
            new Vector3(CSizeZ, 1, 1.1f));
    }
    //波動
    void M_HadouCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(AtkBox[0],
        new Vector3(0, 0.6f, -0.1f),
        new Vector3(CSizeZ, 1, 0.6f));
        atkType = ValueScript.AtkVal.HADOUKEN;
    }

    void M_HadouCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.4f, 0),
            new Vector3(CSizeZ, 0.5f, 1));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0.2f),
            new Vector3(CSizeZ, 1, 0.6f));
    }
    void M_HadouCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }
    //ジャンプ強攻撃
    void M_JumpStCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.02f),
            new Vector3(CSizeZ, 0.9f, 0.4f));
        atkType = ValueScript.AtkVal.KICK_JUMP;
    }
    void M_JumpStCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.02f),
            new Vector3(CSizeZ, 0.9f, 0.4f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0.5f),
            new Vector3(CSizeZ, 0.2f, 0.6f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.8f, 0.5f),
            new Vector3(CSizeZ, 0.2f, 0.6f));
    }
    void M_JumpStCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.02f),
            new Vector3(CSizeZ, 0.9f, 0.4f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0.5f),
            new Vector3(CSizeZ, 0.2f, 0.6f));
    }

    void M_JumpStCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.02f),
            new Vector3(CSizeZ, 0.9f, 0.4f));
    }

    //ジャンプ弱攻撃
    void M_JumpWeCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
        atkType = ValueScript.AtkVal.PUNCH_JUMP;
    }
    void M_JumpWeCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.2f, 0.3f),
            new Vector3(CSizeZ, 0.2f, 0.4f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 1.2f, 0.3f),
            new Vector3(CSizeZ, 0.2f, 0.4f));
    }
    void M_JumpWeCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.2f, 0.3f),
        new Vector3(CSizeZ, 0.2f, 0.4f));
    }
    void M_JumpWeCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
    }

    //座った状態
    void M_SitCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
    }

    //座り
    void M_SitDownCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }
    void M_SitDownCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
    }


    //座りガード
    void M_SitGCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
    }

    //座り受け
    void M_SitRCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
    }

    //座り強攻撃
    void M_SitStCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
        atkType = ValueScript.AtkVal.KICK_SIT;
    }
    void M_SitStCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.6f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.2f, 0.5f),
            new Vector3(CSizeZ, 0.3f, 0.7f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.2f, 0.5f),
            new Vector3(CSizeZ, 0.3f, 0.7f));
    }
    void M_SitStCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.6f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.2f, 0.5f),
            new Vector3(CSizeZ, 0.3f, 0.7f));
    }
    void M_SitStCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
    }


    //座り弱攻撃
    void M_SitWeCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
        atkType = ValueScript.AtkVal.PUNCH_SIT;
    }
    void M_SitWeCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(CSizeZ, 0.2f, 0.4f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(CSizeZ, 0.2f, 0.4f));
    }
    void M_SitWeCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(CSizeZ, 0.2f, 0.4f));
    }
    void M_SitWeCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0),
            new Vector3(CSizeZ, 0.9f, 0.6f));
    }

    //強攻撃
    void M_StrengthCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.4f));
        atkType = ValueScript.AtkVal.KICK;
    }
    void M_StrengthCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.4f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.96f, 0.8f),
            new Vector3(CSizeZ, 0.3f, 0.7f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.96f, 0.8f),
            new Vector3(CSizeZ, 0.3f, 0.7f));
    }
    void M_StrengthCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //昇竜
    void M_SyouryuuCollid()
    {
        HitColliderActive(0);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.4f));
        atkType = ValueScript.AtkVal.SYORYUKEN;
    }
    void M_SyouryuuCollid2()
    {
        HitColliderActive(0);
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0, 1, 0.3f),
            new Vector3(CSizeZ, 1.5f, 0.2f));
    }
    void M_SyouryuuCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
    }

    //弱攻撃
    void M_WeakCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        atkType = ValueScript.AtkVal.PUNCH;
    }
    void M_WeakCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.3f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 1.3f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void M_WeakCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.3f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void M_WeakCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0.06f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
    }


    //-------------------------------------------------------------------------------------------------------------------
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Chloeあたり判定
    //
    //-------------------------------------------------------
    //基本あたり判定
    //
    //使用モーション:立ち、前進、後進、ジャンプ、立ちガード、ダメージ
    //-------------------------------------------------------

    //後退
    void C_BackCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //ダッシュ

    void C_DashCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0),
            new Vector3(CSizeZ, 1, 1));
    }

    //ガード
    void C_GuardCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));

    }

    //波動
    void C_HadouCollid1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0),
            new Vector3(CSizeZ, 1, 1));
        atkType = ValueScript.AtkVal.HADOUKEN;
    }
    void C_HadouCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, -0.2f, -0.3f),
            new Vector3(CSizeZ, 0.6f, 1.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, -0.35f, -0.2f),
            new Vector3(CSizeZ, 1, 0.7f));

        SetBoxState(AtkBox[0],
        new Vector3(0, 1, 0.5f),
        new Vector3(CSizeZ, 0.2f, 0.7f));
    }

    void C_HadouCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.4f, 0),
            new Vector3(CSizeZ, 0.5f, 1));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0.2f),
            new Vector3(CSizeZ, 1, 0.6f));
    }
    void C_HadouCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }


    //ジャンプ
    void C_JumpCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //ジャンプ強攻撃
    void C_JumpStCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
        atkType = ValueScript.AtkVal.KICK_JUMP;
    }
    void C_JumpStCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.3f, 0.6f),
            new Vector3(CSizeZ, 0.2f, 0.6f));
    }
    void C_JumpStCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
    }

    //ジャンプ弱攻撃
    void C_JumpWeCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
        atkType = ValueScript.AtkVal.PUNCH_JUMP;
    }
    void C_JumpWeCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.6f, 0.3f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void C_JumpWeCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0),
            new Vector3(CSizeZ, 1, 0.6f));
    }

    //基本立ち（neutral)
    void C_NeutralCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //座った状態
    void C_SitCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
    }

    //座り
    void C_SitDownCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }
    void C_SitDownCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
    }


    //座りガード
    void C_SitGCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
    }

    //座り受け
    void C_SitRCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
    }

    //座り強攻撃
    void C_SitStCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
        atkType = ValueScript.AtkVal.KICK_SIT;
    }
    void C_SitStCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 0.4f, 0.6f),
            new Vector3(CSizeZ, 0.3f, 0.7f));
    }
    void C_SitStCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0.6f),
            new Vector3(CSizeZ, 0.3f, 0.7f));
    }
    void C_SitStCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
    }


    //座り弱攻撃
    void C_SitWeCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
        atkType = ValueScript.AtkVal.PUNCH_SIT;
    }
    void C_SitWeCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 0.9f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.6f));
    }
    void C_SitWeCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.9f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.6f));
    }
    void C_SitWeCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
    }

    //強受け
    void C_SSRCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //弱受け
    void C_SWRCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //立ち上がり
    void C_StandUpCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1, 0.6f));
    }
    void C_StandUpCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //強攻撃
    void C_StrengthCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        atkType = ValueScript.AtkVal.KICK;
    }
    void C_StrengthCollid2()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.9f, 0.6f),
            new Vector3(CSizeZ, 0.5f, 0.5f));
    }
    void C_StrengthCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0.2f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 0.85f, 0.9f),
            new Vector3(CSizeZ, 0.2f, 0.9f));
    }
    void C_StrengthCollid4()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0.2f),
            new Vector3(CSizeZ, 1.4f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.85f, 0.9f),
            new Vector3(CSizeZ, 0.2f, 0.9f));
    }
    void C_StrengthCollid5()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0),
            new Vector3(CSizeZ, 1.4f, 0.5f));
    }

    //昇竜
    void C_SyouryuuCollid()
    {
        HitColliderActive(0);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
        atkType = ValueScript.AtkVal.SYORYUKEN;
    }
    void C_SyouryuuCollid2()
    {
        HitColliderActive(0);
        AtkColliderActive(1);
        SetBoxState(AtkBox[0],
            new Vector3(0, 0.9f, 0.5f),
            new Vector3(CSizeZ, 1, 0.6f));
    }
    void C_SyouryuuCollid3()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.5f));
    }


    //歩く
    void C_WalkCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
    }

    //弱攻撃
    void C_WeakCollid()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
        atkType = ValueScript.AtkVal.PUNCH;
    }
    void C_WeakCollid2()
    {
        HitColliderActive(1);
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 1.3f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void C_WeakCollid3()
    {
        HitColliderActive(2);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.3f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.5f));
    }
    void C_WeakCollid4()
    {
        HitColliderActive(1);
        AtkColliderActive(0);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.9f, 0),
            new Vector3(CSizeZ, 1.4f, 0.4f));
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

    //-------------------------------------------------------------------------------------------------------------------
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Xionあたり判定
    //
    //---------------------------------------------------------
    //基本あたり判定
    //
    //使用モーション:立ち、前進、後進、立ちガード、ダメージ
    //---------------------------------------------------------
    void XBasicCollide()
    {
        //くらい判定初期化
        HitColliderActive(1);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0f),
            new Vector3(CSizeZ, 1.6f, 0.55f));
    }

    //------------------------------------------------------
    //基本しゃがみあたり判定
    //
    //使用モーション:しゃがみ、しゃがみガード、しゃがみダメ
    //------------------------------------------------------
    void XBasicSitCollide()
    {
        //くらい判定初期化
        HitColliderActive(1);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1f, 0f),
            new Vector3(CSizeZ, 1f, 0.55f));
    }

    //--------------------------------
    //ダッシュあたり判定
    //
    //使用モーション:ダッシュ
    //--------------------------------
    void XBasicDashCollider1()
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
            new Vector3(0, 0, 0));
    }
    void XBasicDashCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.8f));
    }
    void XBasicDashCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.95f));
    }
    void XBasicDashCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1.25f));
    }
    void XBasicDashCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1f));
    }
    void XBasicDashCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
    }
    void XBasicDashCollider7()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1f));
    }
    void XBasicDashCollider8()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1.25f));
    }
    void XBasicDashCollider9()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.6f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.4f, 0f),
            new Vector3(CSizeZ, 0.8f, 1f));
    }
    void XBasicDashCollider10()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(CSizeZ, 0.8f, 0.5f));
        //SetBoxState(HitBox[1],
        //    new Vector3(0, 0.4f, 0f),
        //    new Vector3(CSizeZ, 0.8f, 0.6f));
    }

    //--------------------------------
    //波動コマンドあたり判定
    //
    //使用モーション:波動
    //--------------------------------
    void XBasicHadouCollider1()
    {
        //くらい判定初期化
        HitColliderActive(1);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0),
            new Vector3(CSizeZ, 1.5f, 0.55f));
    }
    void XBasicHadouCollider2()
    {
        //攻撃判定初期化
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0),
            new Vector3(CSizeZ, 0.3f, 0.55f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.2f, 0.56f),
            new Vector3(CSizeZ, 0.6f, 0.6f));
    }
    void XBasicHadouCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0f),
            new Vector3(CSizeZ, 0.3f, 0.55f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.2f, 0.7f),
            new Vector3(CSizeZ, 0.6f, 0.9f));
    }

    //-----------------------------------------
    //ジャンプあたり判定
    //
    //使用モーション:ジャンプ、ジャンプ中
    //-----------------------------------------
    void XBasicJumpCollide1()
    {
        //くらい判定初期化
        HitColliderActive(1);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0f),
            new Vector3(CSizeZ, 1f, 1f));
    }
    void XBasicJumpCollide2()
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
    void XJumpKickCollider1()
    {
        //くらい判定初期化
        HitColliderActive(2);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0f),
            new Vector3(CSizeZ, 1f, 1f));

        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0f),
            new Vector3(0, 0, 0));

        //ジャンプ強
        atkType = ValueScript.AtkVal.KICK_JUMP;
    }
    void XJumpKickCollider2()
    {
        //攻撃判定初期化
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0f),
            new Vector3(CSizeZ, 0.7f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0f),
            new Vector3(0, 0, 0));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.2f, 0.4f),
            new Vector3(CSizeZ, 0.7f, 0.6f));
    }
    void XJumpKickCollider3()
    {
        //攻撃判定初期化
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0f),
            new Vector3(CSizeZ, 0.7f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.8f, 0f),
            new Vector3(0, 0, 0));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.6f, 0.4f),
            new Vector3(CSizeZ, 0.7f, 0.7f));
    }
    void XJumpKickCollider4()
    {
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.1f),
            new Vector3(CSizeZ, 0.7f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.2f),
            new Vector3(CSizeZ, 0.8f, 0.4f));

        //SetBoxState(AtkBox[0],
        //    new Vector3(0, 0.6f, 0.4f),
        //    new Vector3(0, 0, 0));
    }
    void XJumpKickCollider5()
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
    void XJumpKickCollider6()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.65f, 0.25f),
            new Vector3(CSizeZ, 0.6f, 1f));
    }
    void XJumpKickCollider7()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.6f, 0.15f),
            new Vector3(CSizeZ, 0.7f, 0.8f));
    }
    void XJumpKickCollider8()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.1f),
            new Vector3(CSizeZ, 0.9f, 0.65f));
    }
    void XJumpKickCollider9()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0f),
            new Vector3(CSizeZ, 0.65f, 0.75f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.1f),
            new Vector3(CSizeZ, 0.9f, 0.55f));
    }
    void XJumpKickCollider10()
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
    void XJumpPunchCollider1()
    {
        //くらい判定初期化
        HitColliderActive(2);
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.1f),
            new Vector3(CSizeZ, 0.7f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.2f),
            new Vector3(0, 0, 0));

        //SetBoxState(AtkBox[0],
        //    new Vector3(0, 0.6f, 0.4f),
        //    new Vector3(0, 0, 0));

        //ジャンプ弱
        atkType = ValueScript.AtkVal.PUNCH_JUMP;
    }
    void XJumpPunchCollider2()
    {
        //攻撃判定初期化
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(CSizeZ, 0.65f, 0.65f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, 0f),
            new Vector3(CSizeZ, 0.9f, 0.5f));
    }
    void XJumpPunchCollider3()
    {
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.1f),
            new Vector3(CSizeZ, 0.7f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.2f),
            new Vector3(CSizeZ, 0.8f, 0.4f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.6f, 0.4f),
            new Vector3(0, 0, 0));
    }
    void XJumpPunchCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.2f, 0.1f),
            new Vector3(CSizeZ, 0.7f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, -0.2f),
            new Vector3(CSizeZ, 0.8f, 0.4f));

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.1f, 0.4f),
            new Vector3(CSizeZ, 0.2f, 0.7f));
    }
    void XJumpPunchCollider5()
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
    void XSitingCollider1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.75f, 0f),
            new Vector3(CSizeZ, 1.6f, 0.5f));
    }
    void XSitingCollider2()
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
    void XSitKickCollider1()
    {
        HitColliderActive(1);
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1.2f, 0.5f));
        SetBoxState(AtkBox[1], 
            new Vector3(0, 1.1f, 0.3f), 
            new Vector3(CSizeZ, 0.6f, 0.5f));

        //しゃがみ強
        atkType = ValueScript.AtkVal.KICK_SIT;
    }
    void XSitKickCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0.1f),
            new Vector3(CSizeZ, 1.3f, 0.5f));
        SetBoxState(AtkBox[1],
            new Vector3(0, 1.2f, 0.2f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
    }
    void XSitKickCollider3()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.7f, 0.1f),
            new Vector3(CSizeZ, 1.3f, 0.5f));
    }
    void XSitKickCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1.2f, 0.5f));
    }
    void XSitKickCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1f, -0.1f),
            new Vector3(CSizeZ, 0.95f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.2f, 0.2f),
            new Vector3(CSizeZ, 0.25f, 0.5f));
    }
    void XSitKickCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.95f, 0.1f),
            new Vector3(CSizeZ, 0.9f, 0.7f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.7f, -0.45f),
            new Vector3(CSizeZ, 0.35f, 0.7f));
    }
    void XSitKickCollider7()
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
    void XSitPunchCollider1()
    {
        HitColliderActive(1);
        AtkColliderActive(1);

        SetBoxState(AtkBox[0],
            new Vector3(0, 0.6f, 0.4f),
            new Vector3(CSizeZ, 0.3f, 0.5f));

        //しゃがみ弱
        atkType = ValueScript.AtkVal.PUNCH_SIT;
    }
    void XSitPunchCollider2()
    {
        AtkColliderActive(1);

        SetBoxState(AtkBox[0],
            new Vector3(0, 1.1f, 0.4f),
            new Vector3(CSizeZ, 0.45f, 0.6f));
    }
    void XSitPunchCollider3()
    {
        AtkColliderActive(0);
    }
    void XSitPunchCollider4()
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
    void XStandUpCollider1()
    {
        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, 0.1f),
            new Vector3(CSizeZ, 1f, 0.5f));
    }
    void XStandUpCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1.2f, 0.5f));
    }
    void XStandUpCollider3()
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
    void XBaseKickCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0f),
            new Vector3(CSizeZ, 1.6f, 0.55f));

        //強
        atkType = ValueScript.AtkVal.KICK;
    }
    void XBaseKickCollider2()
    {
        AtkColliderActive(1);
        SetBoxState(HitBox[0],
            new Vector3(0, 0.8f, 0f),
            new Vector3(CSizeZ, 1.6f, 0.55f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 1.1f, 0.6f),
            new Vector3(CSizeZ, 0.6f, 0.6f));
    }
    void XBaseKickCollider3()
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
    void XBaseKickCollider4()
    {
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 1.15f, 0.3f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.55f, -0.1f),
            new Vector3(CSizeZ, 0.8f, 0.9f));
    }
    void XBaseKickCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 1.15f, 0.2f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.55f, 0f),
            new Vector3(CSizeZ, 0.85f, 0.9f));
    }
    void XBaseKickCollider6()
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
    void XDownCollider1()
    {
        HitColliderActive(2);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.78f, 0.05f),
            new Vector3(CSizeZ, 1.5f, 0.6f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void XDownCollider2()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.78f, 0.05f),
            new Vector3(CSizeZ, 1.55f, 0.6f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void XDownCollider3()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.65f, -0.1f),
            new Vector3(CSizeZ, 1.35f, 0.6f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void XDownCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.75f, -0.2f),
            new Vector3(CSizeZ, 0.5f, 0.9f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.35f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
    }
    void XDownCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.5f, -0.2f),
            new Vector3(CSizeZ, 0.45f, 0.9f));
        SetBoxState(HitBox[1],
            new Vector3(0, 0.5f, 0.35f),
            new Vector3(CSizeZ, 0.6f, 0.5f));
    }
    void XDownCollider6()
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
    void XBasicShoruCollider1()
    {
        HitColliderActive(0);
        AtkColliderActive(0);

        //SetBoxState(AtkBox[0],
        //    new Vector3(0, 0.7f, 0.45f),
        //    new Vector3(CSizeZ, 1.2f, 0.5f));

        //昇竜コマンド
        atkType = ValueScript.AtkVal.SYORYUKEN;
    }
    void XBasicShoruCollider2()
    {
        HitColliderActive(1);
        AtkColliderActive(1);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.4f, 0.1f),
            new Vector3(CSizeZ, 0.8f, 0.5f));
        SetBoxState(AtkBox[0],
            new Vector3(0, 1.2f, 0.6f),
            new Vector3(CSizeZ, 0.3f, 0.7f));
    }
    void XBasicShoruCollider3()
    {

        HitColliderActive(1);
        AtkColliderActive(0);

        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0.1f),
            new Vector3(CSizeZ, 1.2f, 0.5f));
    }
    void XBasicShoruCollider4()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.75f, 0.05f),
            new Vector3(CSizeZ, 1.5f, 0.55f));
        SetBoxState(HitBox[1],
            new Vector3(0, 1.4f, 0.35f),
            new Vector3(CSizeZ, 0.5f, 0.2f));
    }
    void XBasicShoruCollider5()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.65f, 0.05f),
            new Vector3(CSizeZ, 1.4f, 0.55f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void XBasicShoruCollider6()
    {
        SetBoxState(HitBox[0],
            new Vector3(0, 0.6f, 0f),
            new Vector3(CSizeZ, 1.3f, 0.55f));
        SetBoxState(HitBox[1], new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }
    void XBasicShoruCollider7()
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
    void XBasePunchCollider1()
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
    void XBasePunchCollider2()
    {
        AtkColliderActive(0);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



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
    private void SetBoxState(BoxCollider box, Vector3 pos, Vector3 size, Vector3? rot = null)
    {
        Vector3 rotation = rot ?? Vector3.zero;
        //rotation = new Vector3();

        box.center = pos;
        box.size = size;
        box.transform.localEulerAngles = rotation;
    }

    //変数取得
    public List<GameObject> HClid { get { return HitCollider; } }
    public List<GameObject> AClid { get { return AtkCollider; } }
    public GameObject TClid { get { return ToolCollider; } }
    public List<BoxCollider> GetHitBoxs { get { return HitBox; } }
    public List<BoxCollider> GetAtkBoxs { get { return AtkBox; } }
    public new ValueScript.AtkVal GetType { get { return atkType; } }
}
