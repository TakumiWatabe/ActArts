using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProInfoScript : MonoBehaviour {

    //コライダーイベント
    ColliderEvent CEvent;
    //エフェクト発生用
    SetEffectScript SEScript;
    //HPディレクター
    HPDirectorScript HPDir;

    //技情報取得用
    private GameObject Opponent;
    private ArtsStateScript ASScript;

    private const int nonDamage = 0;

    void Awake()
    {
        //スクリプト取得
        HPDir = this.GetComponent<HPDirectorScript>();
        ASScript = Opponent.GetComponent<ArtsStateScript>();
        CEvent = this.GetComponent<ColliderEvent>();
        SEScript = this.GetComponent<SetEffectScript>();
    }

    void Start()
    {
        //コライダーのスクリプトを取得
        for (int i = 0; i < CEvent.HClid.Count; i++)
        {
            //col.Add(CEvent.HClid[i]);
            //react.Add(col[i].GetComponent<ColliderReact>());
        }
    }

    //立ちガード処理
    public void StandGuardPro()
    {
        Debug.Log("ガードした！");
        //ダメージ分HPゲージを減らす
        HPDir.hitDmage(nonDamage);
        ////エフェクト発生位置計算
        //SEScript.caluclation(CEvent.GetHitBoxs[i], react[i].CObj.GetComponent<BoxCollider>());
        //SEScript.appearEffe(ASScript.AtkLev((int)CEvent.GetType));
    }

    //しゃがみガード処理
    public void SitGuardPro()
    {
        Debug.Log("しゃがみガードした！");

    }

    //通常のけぞり
    public void DamagePro()
    {
        Debug.Log("ダメージを受けた！");
    }

    //しゃがみのけぞり
    public void SitDamagePro()
    {
        Debug.Log("しゃがんでダメージ受けた！");
    }
}
