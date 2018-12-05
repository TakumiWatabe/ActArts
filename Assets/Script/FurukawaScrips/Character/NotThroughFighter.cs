using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotThroughFighter : MonoBehaviour {

    //重なり判定処理(PlayerControllerの仕様によって変更あり)

    private GameObject dir;
    private DebugGetEnemyScript DGEScript;
    private OverlapScript OScript;
    private List<OverReact> over = new List<OverReact>();
    //自分コントローラー
    private PlayerController PContr;
    //相手コントローラー
    private PlayerController EContr;

    //壁と認識する範囲
    [SerializeField]
    private float rightWall;
    [SerializeField]
    private float leftWall;

    //速度補正
    private const float correctionSpd = 0.9f;
    //速度変換用
    private const float conversion = -1.0f;

    void Awake()
    {
        dir = GameObject.Find("BattleDirecter");
        DGEScript = dir.GetComponent<DebugGetEnemyScript>();
        OScript = this.GetComponent<OverlapScript>();
    }

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < OScript.PClid.Count; i++)
        {
            over.Add(OScript.PClid[i].GetComponent<OverReact>());
        }
        PContr = this.GetComponent<PlayerController>();
        EContr = DGEScript.EObj.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //画面外に出ないようにする
        Mathf.Clamp(this.transform.position.x, rightWall, leftWall);

        for (int i = 0; i < OScript.PClid.Count; i++)
        {
            //重なり判定に触れているなら
            if (over[i].contacting)
            {
                //押し出す
                //お互いが触れたときに速度を90%に下げる
                castumSpeed(PContr, EContr);

                //ジャンプ時に触れているなら左右に落とす
                if (PContr.animState == "Jump")
                {
                    //落とす
                    contactMove();
                }
            }
        }
    }

    //ジャンプ時に押し出す処理
    private void contactMove()
    {
        if (this.transform.position.x >= DGEScript.EObj.transform.position.x)
        {
            //左に落とす
            PContr.thisSpeed = PContr.thisGravity;
        }
        else if (this.transform.position.x <= DGEScript.EObj.transform.position.x)
        {
            //右に落とす
            PContr.thisSpeed = PContr.thisGravity * conversion;
        }
    }

    //押し出す処理
    private void castumSpeed(PlayerController PcontrObj,PlayerController EcontrObj)
    {
        //速度に補正をかける
        PcontrObj.thisSpeed = PcontrObj.thisSpeed / correctionSpd;

        //相手が動いてないなら
        if (EcontrObj.thisSpeed == 0)
        {
            //相手を動かす
            EcontrObj.thisSpeed = PcontrObj.thisSpeed * conversion;
        }
    }
}
