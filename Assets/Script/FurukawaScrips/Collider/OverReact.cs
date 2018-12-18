using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverReact : MonoBehaviour {

    //対戦相手オブジェクト
    private GameObject dir;
    private DebugGetEnemyScript DGEScript;

    //あたり判定のタグ名
    private string colliderTag;

    //重なり判定接触時
    private bool contact = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ｳﾞｪｯ⁈");
    }

    //重なり判定に当たったら
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("フッ...");
        //重なり判定なら
        if(colliderTag=="OverLap")
        {
            if (!contact && collision.gameObject.tag == "OverLap")
            {
                //重なる
                contact = true;
                Debug.Log("重なってます");
            }
        }
    }

    //重なり判定から離れたなら
    void OnCollisionExit(Collision collision)
    {
        //重なり判定なら
        if (colliderTag == "OverLap" && collision.gameObject.tag == "OverLap")
        {
            //離れる
            contact = false;
            Debug.Log("離れてます");
        }
    }

    void Awake()
    {
        colliderTag = this.gameObject.tag;
    }

    public bool contacting { get { return contact; } }
}
