using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HikariEvent : MonoBehaviour {

    //--------------------------------------
    //あたり判定設定
    //--------------------------------------

    //くらい判定
    [SerializeField]
    private List<GameObject> HitCollider;
    //攻撃判定
    [SerializeField]
    private List<GameObject> AtkCollider;
    //押し合い判定
    [SerializeField]
    private List<GameObject> PushCollider;

    //コライダー
    BoxCollider HitBox;
    BoxCollider AtkBox;
    BoxCollider PushBox;

    //あたり判定の数
    int HCnum;
    int ACnum;
    int PCnum;

    private const float CSizeZ = 0.25f;

    // Use this for initialization
    void Start()
    {
        //あたり判定の数
        HCnum = HitCollider.Count;
        ACnum = AtkCollider.Count;
        PCnum = PushCollider.Count;
    }




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
        }
    }

    //押し合い判定初期化関数
    private void PushColliderActive()
    {
        for (int i = 0; i < PCnum; i++)
        {
            //判定非動作
            PushCollider[i].SetActive(false);
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
    public List<GameObject> PClid { get { return PushCollider; } }

}
