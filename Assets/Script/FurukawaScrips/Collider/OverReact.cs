using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverReact : MonoBehaviour {

    //対戦相手オブジェクト
    private GameObject dir;
    private DebugGetEnemyScript DGEScript;

    //重なり判定接触時
    private bool contact = false;

    //重なり判定に当たったら
    void OnCollisionEnter(Collision collision)
    {
            //重なっていないなら
            if (!contact)
            {
                //重なる
                contact = true;
            }
    }

    //重なり判定から離れたなら
    void OnCollisionExit(Collision collision)
    {
        //重なっているなら
        if (contact)
        {
            //離れる
            contact = false;
        }
    }


    public bool contacting { get { return contact; } }
}
