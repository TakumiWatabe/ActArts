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

    //コライダー
    BoxCollider HitBox;
    BoxCollider AtkBox;

    //あたり判定の数
    int HCnum;
    int ACnum;

    List<BoxCollider> HitBoxs = new List<BoxCollider>();
    List<BoxCollider> AtkBoxs = new List<BoxCollider>();

    private const float CSizeZ = 0.25f;

    // Use this for initialization
    void Start ()
    {
        //あたり判定の数
        HCnum = HitCollider.Count;
        ACnum = AtkCollider.Count;

        //あたり判定のコライダーを格納
        for (int i = 0; i < HCnum; i++)
        {
            HitBoxs.Add(HitCollider[i].GetComponent<BoxCollider>());
        }

        for (int i = 0; i < ACnum; i++)
        {
            AtkBoxs.Add(AtkCollider[i].GetComponent<BoxCollider>());
        }
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
        //くらい判定初期化
        HitColliderActive();

        //基本くらい判定
        HitCollider[0].SetActive(true);

        //攻撃判定初期化
        AtkColliderActive();
    }

    //-----------------------------------------
    //基本しゃがみあたり判定
    //
    //使用モーション:しゃがみ、しゃがみガード
    //-----------------------------------------
    void BasicSitCollide()
    {
        //くらい判定初期化
        HitColliderActive();

        //基本しゃがみ判定
        HitCollider[1].SetActive(true);

        //攻撃判定初期化
        AtkColliderActive();
    }

    //------------------
    //パンチあたり判定
    //
    //使用モーション:立ちパンチ　フレーム数:15
    //------------------
    void BasePunchCollider()
    {

        HitCollider[2].SetActive(true);
        AtkCollider[0].SetActive(true);

        HitBox = HitCollider[2].GetComponent<BoxCollider>();

        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.2f, 0.25f),
            new Vector3(0.25f, 0.3f, 0.35f));
    }
    void SetPunchCollider_1()
    {
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1, 0.35f),
            new Vector3(0.25f, 0.35f, 0.55f));

        AtkColliderActive();
    }
    void SetPunchCollider_2()
    {
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1, 0.25f),
            new Vector3(0.25f, 0.5f, 0.3f));
    }

    //------------------
    //キックあたり判定
    //
    //使用モーション:立ちキック フレーム数:5
    //------------------
    void BaseKickColliderAtk()
    {
        HitCollider[3].SetActive(true);
        AtkCollider[1].SetActive(true);

        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        AtkBox = AtkCollider[1].GetComponent<BoxCollider>();

        SetBoxState(
            HitBox,
            new Vector3(0, 0.85f, 0.45f),
            new Vector3(0.25f, 0.3f, 0.8f));
        SetBoxState(
            AtkBox,
            new Vector3(0, 0.95f, 0.48f),
            new Vector3(0.25f, 0.25f, 0.95f));
    }
    void SetKickCollider_1()
    {
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.9f, 0.5f),
            new Vector3(0.25f, 0.3f, 0.95f));
    }
    void SetKickCollider_2()
    {
        AtkColliderActive();
    }
    void SetKickCollider_3()
    {
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.65f, 0.4f),
            new Vector3(0.25f, 0.3f, 0.75f));
    }
    void SetKickCollider_4()
    {
        HitColliderActive();
        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.85f, 0.45f),
            new Vector3(0.25f, 0.3f, 0.8f));
    }

    //--------------------------------
    //ダウンあたり判定
    //
    //使用モーション:ダウン
    //--------------------------------
    void BasicDownCollider()
    {
        HitCollider[4].SetActive(true);
    }

    //--------------------------------
    //受け身あたり判定
    //
    //使用モーション:受け身
    //--------------------------------
    void BasicPassiveCollider()
    {
        HitCollider[5].SetActive(true);
    }

    //--------------------------------
    //昇竜コマンドあたり判定
    //
    //使用モーション:昇竜
    //--------------------------------
    void BasicShoruCollider1()
    {
        HitColliderActive();

        AtkColliderActive();

        AtkCollider[2].SetActive(true);
    }
    void BasicShoruCollider2()
    {
        HitCollider[6].SetActive(true);
        HitCollider[7].SetActive(true);
        HitCollider[8].SetActive(true);

        HitBox = HitCollider[6].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.75f, 0.4f));

        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0),
            new Vector3(0.25f, 0.8f, 0.9f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.35f, 0.35f),
            new Vector3(0.25f, 0.3f, 0.5f));

        AtkColliderActive();
    }
    void BasicShoruCollider3()
    {
        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.45f, 0.3f),
            new Vector3(0.25f, 0.4f, 0.4f));
    }
    void BasicShoruCollider4()
    {
        HitBox = HitCollider[6].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(0.25f, 0.75f, 0.4f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.35f, 0.4f),
            new Vector3(0.25f, 0.3f, 0.5f));
    }
    void BasicShoruCollider5()
    {
        HitBox = HitCollider[6].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.1f),
            new Vector3(0.25f, 0.75f, 0.4f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.5f),
            new Vector3(0.25f, 0.25f, 0.5f));
    }
    void BasicShoruCollider6()
    {
        HitBox = HitCollider[6].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(0.25f, 0.75f, 0.4f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.4f),
            new Vector3(0.25f, 0.25f, 0.5f));
    }
    void BasicShoruCollider7()
    {
        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0.35f),
            new Vector3(0.25f, 0.25f, 0.35f));
    }
    void BasicShoruCollider8()
    {
        HitColliderActive();
        HitCollider[6].SetActive(true);
        HitCollider[7].SetActive(true);
    }


    //--------------------------------
    //波動コマンドあたり判定
    //
    //使用モーション:波動
    //--------------------------------
    void BasicHadouCollider1()
    {
        HitColliderActive();
        HitCollider[9].SetActive(true);
        HitCollider[10].SetActive(true);
        HitCollider[11].SetActive(true);

        HitBox = HitCollider[9].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.88f, 0),
            new Vector3(0.25f, 1.5f, 0.5f));

        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.38f, 0.38f),
            new Vector3(0.25f, 0.5f, 0.3f));

        HitBox = HitCollider[11].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.4f),
            new Vector3(0.25f, 0.25f, 0.5f));

        AtkColliderActive();

    }
    void BasicHadouCollider2()
    {
        HitBox = HitCollider[11].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.5f, 0.3f),
            new Vector3(0.25f, 0.3f, 0.4f));

        //AtkCollider[4].SetActive(true);
        //AtkCollider[5].SetActive(true);
        //AtkCollider[6].SetActive(true);
    }
    void BasicHadouCollider3()
    {
        HitColliderActive();
        HitCollider[9].SetActive(true);
        HitCollider[10].SetActive(true);

        HitBox = HitCollider[9].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.9f, -0.05f),
            new Vector3(0.25f, 1.5f, 0.5f));

        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.42f, 0.35f),
            new Vector3(0.25f, 0.55f, 0.35f));

        AtkColliderActive();
    }
    void BasicHadouCollider4()
    {
        HitBox = HitCollider[9].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.9f, 0f),
            new Vector3(0.25f, 1.6f, 0.5f));

        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.35f, 0.35f),
            new Vector3(0.25f, 0.5f, 0.35f));
    }
    void BasicHadouCollider5()
    {
        HitBox = HitCollider[9].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.85f, 0f),
            new Vector3(0.25f, 1.55f, 0.5f));

        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.32f, 0.3f),
            new Vector3(0.25f, 0.5f, 0.3f));

        HitCollider[11].SetActive(true);
        HitBox = HitCollider[11].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
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
        HitColliderActive();
        HitCollider[12].SetActive(true);

        AtkColliderActive();
        AtkCollider[7].SetActive(true);
    }
    void SitPunchCollider_2()
    {
        AtkColliderActive();
    }

    //--------------------------------
    //しゃがみキックあたり判定
    //
    //使用モーション:しゃがみキック
    //--------------------------------
    void SitKickCollider1()
    {
        HitColliderActive();

        HitCollider[13].SetActive(true);
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.05f, 0),
            new Vector3(0.25f, 1.05f, 0.7f));

        AtkColliderActive();
    }
    void SitKickCollider2()
    {
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.02f, 0),
            new Vector3(0.25f, 0.9f, 0.65f));

        HitCollider[14].SetActive(true);
        HitBox = HitCollider[14].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.75f, 0.45f),
            new Vector3(0.25f, 0.35f, 0.5f));

    }
    void SitKickCollider3()
    {
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, -0.1f),
            new Vector3(0.25f, 0.88f, 0.65f));

        HitBox = HitCollider[14].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.75f, 0.5f),
            new Vector3(0.25f, 0.35f, 0.75f));

        AtkCollider[8].SetActive(true);
    }
    void SitKickCollider4()
    {
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, -0.15f),
            new Vector3(0.25f, 0.85f, 0.7f));

        AtkColliderActive();
    }
    void SitKickCollider5()
    {
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.98f, -0.2f),
            new Vector3(0.25f, 0.8f, 0.75f));

        HitBox = HitCollider[14].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.9f, 0.4f),
            new Vector3(0.25f, 0.25f, 0.7f));
    }
    void SitKickCollider6()
    {
        HitBox = HitCollider[14].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.8f, 0.35f),
            new Vector3(0.25f, 0.35f, 0.6f));
    }
    void SitKickCollider7()
    {
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.02f, -0.1f),
            new Vector3(0.25f, 0.9f, 0.5f));

        HitBox = HitCollider[14].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.67f, 0.3f),
            new Vector3(0.25f, 0.2f, 0.5f));
    }
    void SitKickCollider8()
    {
        HitColliderActive();

        HitCollider[13].SetActive(true);
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.08f, -0.05f),
            new Vector3(0.25f, 1f, 0.5f));
    }
    void SitKickCollider9()
    {
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.08f, -0.05f),
            new Vector3(0.25f, 1f, 0.6f));
    }

    //--------------------------------
    //ジャンプパンチあたり判定
    //
    //使用モーション:ジャンプパンチ
    //--------------------------------
    void JumpPunchCollider1()
    {
        HitColliderActive();

        HitCollider[15].SetActive(true);
        HitCollider[16].SetActive(true);

        HitBox = HitCollider[15].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.95f, 0.1f),
            new Vector3(0.25f, 1.3f, 0.5f));

        HitBox = HitCollider[16].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, -0.25f),
            new Vector3(0.25f, 0.8f, 0.4f));

        AtkColliderActive();
    }
    void JumpPunchCollider2()
    {
        HitCollider[17].SetActive(true);

        HitBox = HitCollider[15].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0.1f),
            new Vector3(0.25f, 1.2f, 0.5f));

        HitBox = HitCollider[17].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.4f),
            new Vector3(0.25f, 0.2f, 0.5f));
    }
    void JumpPunchCollider3()
    {
        HitBox = HitCollider[15].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0f),
            new Vector3(0.25f, 1.2f, 0.5f));
        HitBox = HitCollider[16].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, -0.4f),
            new Vector3(0.25f, 0.65f, 0.4f));
        HitBox = HitCollider[17].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.4f),
            new Vector3(0.25f, 0.2f, 0.5f));

        AtkCollider[9].SetActive(true);
        AtkCollider[10].SetActive(true);
    }
    void JumpPunchCollider4()
    {
        HitBox = HitCollider[17].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.2f, 0.45f),
            new Vector3(0.25f, 0.2f, 0.5f));

        AtkColliderActive();
    }
    void JumpPunchCollider5()
    {
        HitBox = HitCollider[15].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.95f, -0.02f),
            new Vector3(0.25f, 1.3f, 0.5f));
        HitBox = HitCollider[16].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.3f, -0.4f),
            new Vector3(0.25f, 0.55f, 0.35f));
        HitBox = HitCollider[17].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.15f, 0.35f),
            new Vector3(0.25f, 0.3f, 0.35f));
    }
    void JumpPunchCollider6()
    {
        HitBox = HitCollider[16].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.3f, -0.35f),
            new Vector3(0.25f, 0.55f, 0.35f));
        HitBox = HitCollider[17].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.3f),
            new Vector3(0.25f, 0.3f, 0.3f));
    }

    //--------------------------------
    //ジャンプキックあたり判定
    //
    //使用モーション:ジャンプキック
    //--------------------------------
    void JumpKickCollider1()
    {
        HitColliderActive();
        HitCollider[18].SetActive(true);
        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, -0.05f),
            new Vector3(0.25f, 1.3f, 0.45f));

        AtkColliderActive();
    }
    void JumpKickCollider2()
    {
        HitCollider[19].SetActive(true);

        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.05f, -0.05f),
            new Vector3(0.25f, 1.15f, 0.45f));

        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0.25f),
            new Vector3(0.25f, 0.35f, 0.35f));
    }
    void JumpKickCollider3()
    {
        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.05f, -0.05f),
            new Vector3(0.25f, 1.1f, 0.45f));

        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0.3f),
            new Vector3(0.25f, 0.5f, 0.5f));

        AtkCollider[11].SetActive(true);
        AtkCollider[12].SetActive(true);
    }
    void JumpKickCollider4()
    {
        AtkColliderActive();
    }
    void JumpKickCollider5()
    {
        HitCollider[19].SetActive(true);
        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.3f, 0.1f),
            new Vector3(0.25f, 0.325f, 0.25f));
    }

    //--------------------------------
    //ダッシュあたり判定
    //
    //使用モーション:ダッシュ
    //--------------------------------
    void BasicDashCollider()
    {
        HitColliderActive();
        HitCollider[20].SetActive(true);
        AtkColliderActive();
    }

    //--------------------------------
    //くらい強あたり判定
    //
    //使用モーション:くらい強
    //--------------------------------
    void BasicLDCollider()
    {
        HitColliderActive();
        HitCollider[21].SetActive(true);
        AtkColliderActive();
    }

    //--------------------------------------
    //しゃがみ系あたり判定
    //
    //使用モーション:立ち上がり、しゃがみ中
    //--------------------------------------
    void SitCollider()
    {
        HitColliderActive();
        HitCollider[22].SetActive(true);
        AtkColliderActive();
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
        HitColliderActive();

        //基本くらい判定
        HitCollider[0].SetActive(true);

        //攻撃判定初期化
        AtkColliderActive();
    }

    //------------------------------------------------------
    //基本しゃがみあたり判定
    //
    //使用モーション:しゃがみ、しゃがみガード、しゃがみダメ
    //------------------------------------------------------
    void HBasicSitCollide()
    {
        //くらい判定初期化
        HitColliderActive();

        //基本しゃがみ判定
        HitCollider[1].SetActive(true);

        //攻撃判定初期化
        AtkColliderActive();
    }

    //--------------------------------
    //ダッシュあたり判定
    //
    //使用モーション:ダッシュ
    //--------------------------------
    void HBasicDashCollider1()
    {
        //くらい判定初期化
        HitColliderActive();

        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.6f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 0.6f));

        //攻撃判定初期化
        AtkColliderActive();
    }
    void HBasicDashCollider2()
    {
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 0.8f));
    }
    void HBasicDashCollider3()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.65f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 0.95f));
    }
    void HBasicDashCollider4()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.6f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 1.25f));
    }
    void HBasicDashCollider5()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.55f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 1f));
    }
    void HBasicDashCollider6()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.55f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 0.6f));
    }
    void HBasicDashCollider7()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.55f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 1f));
    }
    void HBasicDashCollider8()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.6f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 1.2f));
    }
    void HBasicDashCollider9()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.6f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 1f));
    }
    void HBasicDashCollider10()
    {
        HitCollider[2].SetActive(true);
        HitBox = HitCollider[2].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.15f),
            new Vector3(0.25f, 0.8f, 0.5f));

        HitCollider[3].SetActive(true);
        HitBox = HitCollider[3].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, 0),
            new Vector3(0.25f, 0.8f, 0.6f));
    }

    //--------------------------------
    //波動コマンドあたり判定
    //
    //使用モーション:波動
    //--------------------------------
    void HBasicHadouCollider1()
    {
        HitCollider[4].SetActive(true);
        HitBox = HitCollider[4].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.4f),
            new Vector3(0.25f, 0.3f, 0.5f));
    }
    void HBasicHadouCollider2()
    {
        SetBoxState(
            HitBox,
            new Vector3(0, 1.2f, 0.4f),
            new Vector3(0.25f, 0.3f, 0.55f));

        //AtkCollider[0].SetActive(true);
    }
    void HBasicHadouCollider3()
    {
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0.4f),
            new Vector3(0.25f, 0.3f, 0.3f));

        AtkColliderActive();
    }

    //-----------------------------------------
    //ジャンプあたり判定
    //
    //使用モーション:ジャンプ、ジャンプ中
    //-----------------------------------------
    void HBasicJumpCollide1()
    {
        HitColliderActive();

        HitCollider[5].SetActive(true);
        HitBox = HitCollider[5].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.3f, 0),
            new Vector3(0.25f, 0.55f, 0.8f));

        HitCollider[6].SetActive(true);
        HitBox = HitCollider[6].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.6f, 0.05f),
            new Vector3(0.25f, 0.9f, 0.6f));

        AtkColliderActive();
    }
    void HBasicJumpCollide2()
    {
        HitBox = HitCollider[5].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.65f, 0.8f));

        HitBox = HitCollider[6].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.45f, 0),
            new Vector3(0.25f, 1.1f, 0.45f));
    }

    //--------------------------------
    //ジャンプキックあたり判定
    //
    //使用モーション:ジャンプキック
    //--------------------------------
    void HJumpKickCollider1()
    {
        HitColliderActive();

        HitCollider[7].SetActive(true);
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.6f, 0.75f));

        HitCollider[8].SetActive(true);
        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.48f, 0),
            new Vector3(0.25f, 1.1f, 0.45f));

        AtkColliderActive();
    }
    void HJumpKickCollider2()
    {
        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, 0.15f),
            new Vector3(0.25f, 0.6f, 0.75f));

    }
    void HJumpKickCollider3()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.3f, 0),
            new Vector3(0.25f, 0.5f, 0.75f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.8f, 0.15f),
            new Vector3(0.25f, 0.6f, 0.75f));
    }
    void HJumpKickCollider4()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.2f, 0),
            new Vector3(0.25f, 0.7f, 0.75f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, 0.25f),
            new Vector3(0.25f, 0.6f, 1));
    }
    void HJumpKickCollider5()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.2f, 0),
            new Vector3(0.25f, 0.7f, 0.75f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, 0.25f),
            new Vector3(0.25f, 0.6f, 1));

        AtkCollider[1].SetActive(true);
    }
    void HJumpKickCollider6()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.65f, 0.75f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.65f, 0.25f),
            new Vector3(0.25f, 0.6f, 1f));

        AtkColliderActive();
    }
    void HJumpKickCollider7()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.65f, 0.75f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.6f, 0.15f),
            new Vector3(0.25f, 0.7f, 0.8f));
    }
    void HJumpKickCollider8()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.65f, 0.75f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0.1f),
            new Vector3(0.25f, 0.9f, 0.65f));
    }
    void HJumpKickCollider9()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.65f, 0.75f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0.1f),
            new Vector3(0.25f, 0.9f, 0.55f));
    }
    void HJumpKickCollider10()
    {
        HitBox = HitCollider[7].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.65f, 0.8f));

        HitBox = HitCollider[8].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0),
            new Vector3(0.25f, 1.1f, 0.5f));
    }

    //--------------------------------
    //ジャンプパンチあたり判定
    //
    //使用モーション:ジャンプパンチ
    //--------------------------------
    void HJumpPunchCollider1()
    {
        HitColliderActive();

        HitCollider[9].SetActive(true);
        HitBox = HitCollider[9].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0),
            new Vector3(0.25f, 0.65f, 0.75f));

        HitCollider[10].SetActive(true);
        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0),
            new Vector3(0.25f, 1.2f, 0.5f));

        AtkColliderActive();
    }
    void HJumpPunchCollider2()
    {
        HitBox = HitCollider[9].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(0.25f, 0.65f, 0.65f));

        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, 0),
            new Vector3(0.25f, 0.9f, 0.5f));
    }
    void HJumpPunchCollider3()
    {
        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.75f, 0),
            new Vector3(0.25f, 0.7f, 0.5f));

        AtkCollider[2].SetActive(true);
    }
    void HJumpPunchCollider4()
    {
        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.65f, 0),
            new Vector3(0.25f, 0.95f, 0.5f));

        AtkColliderActive();
    }
    void HJumpPunchCollider5()
    {
        HitBox = HitCollider[9].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.25f, 0.05f),
            new Vector3(0.25f, 0.65f, 0.7f));

        HitBox = HitCollider[10].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.6f, 0),
            new Vector3(0.25f, 1.1f, 0.5f));

    }

    //--------------------------------------
    //しゃがみ中あたり判定
    //
    //使用モーション:しゃがみ中
    //--------------------------------------
    void HSitingCollider1()
    {
        HitColliderActive();

        HitCollider[11].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.75f, 0),
            new Vector3(0.25f, 1.6f, 0.5f));

        AtkColliderActive();
    }
    void HSitingCollider2()
    {
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0),
            new Vector3(0.25f, 1.25f, 0.5f));
    }

    //--------------------------------
    //しゃがみキックあたり判定
    //
    //使用モーション:しゃがみキック
    //--------------------------------
    void HSitKickCollider1()
    {
        HitColliderActive();

        HitCollider[12].SetActive(true);
        HitBox = HitCollider[12].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0),
            new Vector3(0.25f, 1.1f, 0.5f));

        AtkColliderActive();
    }
    void HSitKickCollider2()
    {
        HitBox = HitCollider[12].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, -0.1f),
            new Vector3(0.25f, 1f, 0.7f));

        HitCollider[13].SetActive(true);
        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, 0.3f),
            new Vector3(0.25f, 0.4f, 0.5f));
    }
    void HSitKickCollider3()
    {
        HitBox = HitCollider[12].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.95f, -0.15f),
            new Vector3(0.25f, 0.9f, 0.7f));

        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(0.25f, 0.35f, 0.7f));

        AtkCollider[3].SetActive(true);
    }
    void HSitKickCollider4()
    {
        HitBox = HitCollider[12].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.95f, -0.15f),
            new Vector3(0.25f, 0.9f, 0.7f));

        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, 0.45f),
            new Vector3(0.25f, 0.35f, 0.7f));

        AtkColliderActive();
    }
    void HSitKickCollider5()
    {
        HitBox = HitCollider[12].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, -0.1f),
            new Vector3(0.25f, 0.95f, 0.55f));

        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.2f, 0.2f),
            new Vector3(0.25f, 0.25f, 0.5f));
    }
    void HSitKickCollider6()
    {
        HitBox = HitCollider[12].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.95f, 0.1f),
            new Vector3(0.25f, 0.9f, 0.7f));

        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, -0.45f),
            new Vector3(0.25f, 0.35f, 0.7f));
    }
    void HSitKickCollider7()
    {
        HitBox = HitCollider[12].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0.1f),
            new Vector3(0.25f, 0.95f, 0.7f));

        HitBox = HitCollider[13].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 0.7f, -0.3f),
            new Vector3(0.25f, 0.35f, 0.45f));
    }


    //--------------------------------
    //しゃがみパンチあたり判定
    //
    //使用モーション:しゃがみパンチ
    //--------------------------------
    void HSitPunchCollider1()
    {
        HitColliderActive();

        HitCollider[14].SetActive(true);
        HitBox = HitCollider[14].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0),
            new Vector3(0.25f, 1.1f, 0.55f));

        AtkColliderActive();
    }
    void HSitPunchCollider2()
    {
        AtkCollider[4].SetActive(true);
    }
    void HSitPunchCollider3()
    {
        AtkColliderActive();
    }
    void HSitPunchCollider4()
    {
        HitCollider[15].SetActive(true);
        HitBox = HitCollider[15].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0.3f),
            new Vector3(0.25f, 0.25f, 0.5f));
    }


    //-----------------------------------------
    //立ち上がりあたり判定
    //
    //使用モーション:立ち上がり
    //-----------------------------------------
    void HStandUpCollider1()
    {
        HitColliderActive();

        HitCollider[16].SetActive(true);
        HitBox = HitCollider[16].GetComponent<BoxCollider>();
        SetBoxState(
            HitBox,
            new Vector3(0, 1.18f, 0),
            new Vector3(0.25f, 1.15f, 0.6f));

        AtkColliderActive();
    }
    void HStandUpCollider2()
    {
        SetBoxState(
            HitBox,
            new Vector3(0, 1.1f, 0),
            new Vector3(0.25f, 1.3f, 0.65f));
    }
    void HStandUpCollider3()
    {
        SetBoxState(
            HitBox,
            new Vector3(0, 0.95f, 0),
            new Vector3(0.25f, 1.65f, 0.65f));
    }

    //------------------
    //強攻撃あたり判定
    //
    //使用モーション:強攻撃
    //------------------
    void HBaseKickCollider1()
    {
        HitColliderActive();

        HitCollider[17].SetActive(true);
        HitBox = HitCollider[17].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.85f, 0f),
            new Vector3(0.25f, 1.4f, 0.5f));

        AtkColliderActive();
    }
    void HBaseKickCollider2()
    {
        HitBox = HitCollider[17].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.15f, 0.2f),
            new Vector3(0.25f, 0.6f, 0.5f));

        HitCollider[18].SetActive(true);
        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.55f, 0f),
            new Vector3(0.25f, 0.8f, 1f));
    }
    void HBaseKickCollider3()
    {
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.15f, 0.3f),
            new Vector3(0.25f, 0.6f, 0.5f));

        HitCollider[18].SetActive(true);
        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.55f, -0.1f),
            new Vector3(0.25f, 0.8f, 0.9f));

        AtkCollider[5].SetActive(true);
    }
    void HBaseKickCollider4()
    {
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.15f, 0.3f),
            new Vector3(0.25f, 0.6f, 0.5f));

        HitCollider[18].SetActive(true);
        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.55f, -0.1f),
            new Vector3(0.25f, 0.8f, 0.9f));

        AtkColliderActive();
    }
    void HBaseKickCollider5()
    {
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.15f, 0.2f),
            new Vector3(0.25f, 0.6f, 0.5f));

        HitCollider[18].SetActive(true);
        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.55f, 0f),
            new Vector3(0.25f, 0.85f, 0.9f));
    }


    void HBaseKickCollider6()
    {
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.15f, 0.2f),
            new Vector3(0.25f, 0.6f, 0.5f));

        HitCollider[18].SetActive(true);
        HitBox = HitCollider[18].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.55f, 0f),
            new Vector3(0.25f, 0.85f, 1f));


    }

    //--------------------------------
    //ダウンあたり判定
    //
    //使用モーション:ダウン
    //--------------------------------
    void HDownCollider1()
    {
        HitColliderActive();

        HitCollider[19].SetActive(true);
        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.78f, 0.05f),
            new Vector3(0.25f, 1.5f, 0.6f));

        AtkColliderActive();
    }
    void HDownCollider2()
    {
        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.78f, 0.05f),
            new Vector3(0.25f, 1.55f, 0.6f));
    }
    void HDownCollider3()
    {
        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.65f, -0.1f),
            new Vector3(0.25f, 1.35f, 0.6f));
    }
    void HDownCollider4()
    {
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.75f, -0.2f),
            new Vector3(0.25f, 0.5f, 0.9f));

        HitCollider[20].SetActive(true);
        HitBox = HitCollider[20].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0.35f),
            new Vector3(0.25f, 0.6f, 0.5f));
    }
    void HDownCollider5()
    {
        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, -0.2f),
            new Vector3(0.25f, 0.45f, 0.9f));

        HitBox = HitCollider[20].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.5f, 0.35f),
            new Vector3(0.25f, 0.6f, 0.5f));
    }
    void HDownCollider6()
    {
        HitBox = HitCollider[19].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, -0.6f, -0.2f),
            new Vector3(0.25f, 0.45f, 1f));

        HitBox = HitCollider[20].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.6f, 0.45f),
            new Vector3(0.25f, 0.6f, 0.6f));
    }

    //--------------------------------
    //昇竜コマンドあたり判定
    //
    //使用モーション:昇竜
    //--------------------------------
    void HBasicShoruCollider1()
    {
        HitColliderActive();

        AtkColliderActive();
        AtkCollider[6].SetActive(true);
    }
    void HBasicShoruCollider2()
    {
        HitCollider[21].SetActive(true);
        HitBox = HitCollider[21].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1f, 0.05f),
            new Vector3(0.25f, 1.3f, 0.5f));

        HitCollider[22].SetActive(true);
        HitBox = HitCollider[22].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.4f, -0.35f),
            new Vector3(0.25f, 0.75f, 0.35f));

        AtkColliderActive();
    }
    void HBasicShoruCollider3()
    {
        HitBox = HitCollider[21].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.85f, 0.05f),
            new Vector3(0.25f, 1.5f, 0.5f));

        HitCollider[22].SetActive(false);
    }
    void HBasicShoruCollider4()
    {
        HitBox = HitCollider[21].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.75f, 0.05f),
            new Vector3(0.25f, 1.5f, 0.55f));

        HitCollider[22].SetActive(true);
        HitBox = HitCollider[22].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 1.4f, 0.35f),
            new Vector3(0.25f, 0.5f, 0.2f));
    }

    void HBasicShoruCollider5()
    {
        HitBox = HitCollider[21].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.65f, 0.05f),
            new Vector3(0.25f, 1.4f, 0.55f));

        HitCollider[22].SetActive(false);
    }
    void HBasicShoruCollider6()
    {
        HitBox = HitCollider[21].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.6f, 0f),
            new Vector3(0.25f, 1.3f, 0.55f));
    }
    void HBasicShoruCollider7()
    {
        HitBox = HitCollider[21].GetComponent<BoxCollider>();
        //コライダー設定
        SetBoxState(
            HitBox,
            new Vector3(0, 0.6f, 0),
            new Vector3(0.25f, 1.3f, 0.5f));
    }

    //------------------
    //パンチあたり判定
    //
    //使用モーション:立ちパンチ　フレーム数:15
    //------------------
    void HBasePunchCollider1()
    {
        HitColliderActive();

        HitCollider[23].SetActive(true);
        AtkCollider[7].SetActive(true);
    }
    void HBasePunchCollider2()
    {
        AtkColliderActive();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //-------------------------------------------------------------------------------------------------------------------

    //くらい判定初期化関数
    private void HitColliderActive()
    {
        for (int i = 0; i < HCnum; i++)
        {
            //判定非動作
            HitCollider[i].SetActive(false);
        }

    }

    //攻撃判定初期化関数
    private void AtkColliderActive()
    {
        for (int i = 0; i < ACnum; i++)
        {
            //判定非動作
            AtkCollider[i].SetActive(false);
            if(AtkCollider[i].layer==16)
            {
                AtkCollider[i].SetActive(true);
            }
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
    public List<BoxCollider> GetHitBoxs { get { return HitBoxs; } }
    public List<BoxCollider> GetAtkBoxs { get { return AtkBoxs; } }
}
