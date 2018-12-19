using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotThroughFighter : MonoBehaviour {

    //重なり判定処理(PlayerControllerの仕様によって変更あり)

    private OverlapScript OScript;
    private OverReact over;
    //自分コントローラー
    private PlayerController PContr;
    private GameObject enemyF;

    //壁と認識する範囲
    [SerializeField]
    private float rightWall;
    [SerializeField]
    private float leftWall;

    //速度補正
    private const float correctionSpd = 9f;
    //速度変換用
    private const float conversion = -1.0f;

    [SerializeField, Range(1, 0)]
    private float contactArea = 0.5f;


    void Awake()
    {
        OScript = this.GetComponent<OverlapScript>();
    }

    // Use this for initialization
    void Start ()
    {
        over = OScript.PClid.GetComponent<OverReact>();
        
        PContr = this.GetComponent<PlayerController>();
        enemyF = PContr.fightEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        //画面外に出ないようにする
        Mathf.Clamp(this.transform.position.x, leftWall, -rightWall);
        inStage();
        //Debug.Log(EContr.transform.position.x);

        //重なり判定に触れているなら
        if (over.contacting)
        {
            //押し出す
            //お互いが触れたときに速度を90%に下げる
            castumSpeed(PContr);

            //ジャンプ時に触れているなら左右に落とす
            if (PContr.animState == "Jump")
            {
                //落とす
                contactMove();
            }

            //貫通しない処理
            noThrough(PContr);
        }

        //めり込んだ場合押し返す
        noCross();
    }

    //ジャンプ時に押し出す処理
    private void contactMove()
    {
        //消しちゃダメ
        //if (this.transform.position.x >= DGEScript.EObj.transform.position.x)
        if (this.transform.position.x >= enemyF.transform.position.x)
        {
            //左に落とす
            PContr.thisSpeed = PContr.thisGravity;
        }
        //else if (this.transform.position.x <= DGEScript.EObj.transform.position.x)
        else if (this.transform.position.x <= enemyF.transform.position.x)
        {
            //右に落とす
            PContr.thisSpeed = PContr.thisGravity * conversion;
        }
    }

    //画面外に行かない処理
    private void inStage()
    {
        if (this.transform.position.x >= leftWall || this.transform.position.x <= -rightWall)
        {
            if (this.transform.position.x >= leftWall)
            {
                this.transform.position = new Vector3(2.95f, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x <= -rightWall)
            {
                this.transform.position = new Vector3(-2.95f, this.transform.position.y, this.transform.position.z);
            }
            Debug.Log("ダメです");
        }
    }

    //押し出す処理
    private void castumSpeed(PlayerController PcontrObj)
    {
        //速度に補正をかける
        PcontrObj.thisSpeed = PcontrObj.thisSpeed * correctionSpd;
    }

    //ダッシュ中に貫通しない処理
    private void noThrough(PlayerController PcontrObj)
    {
        if (PcontrObj.thisSpeed != 0)
        {
            //若干の速度対抗
            enemyF.GetComponent<PlayerController>().thisSpeed = PcontrObj.thisSpeed / correctionSpd;
            Debug.Log("ﾌｫｯ?!");
        }
    }

    //キャラ同士がめり込んだ時の処理
    private void noCross()
    {
        //相対距離を算出
        float distance = Mathf.Abs(this.transform.position.x - enemyF.transform.position.x);

        //相対距離が近かったら
        if (distance < contactArea)
        {
            //壁に接しているのなら
            if (this.transform.position.x >= leftWall || this.transform.position.x <= -rightWall)
            {
                if (this.transform.position.x >= leftWall)
                {
                    //相手を押し戻す
                    enemyF.transform.position = new Vector3(enemyF.transform.position.x - (contactArea - distance), enemyF.transform.position.y, enemyF.transform.position.z);
                }
                if (this.transform.position.x <= -rightWall)
                {
                    //相手を押し戻す
                    enemyF.transform.position = new Vector3(enemyF.transform.position.x + (contactArea - distance), enemyF.transform.position.y, enemyF.transform.position.z);
                }
            }
            //壁に接していないなら
            else
            {
                //右側なら
                if (PContr.Direction == 1)
                {
                    //お互いを少し戻す
                    this.transform.position = new Vector3(this.transform.position.x + (contactArea - distance) / 2, this.transform.position.y, this.transform.position.z);
                }
                //左側なら
                else
                {
                    //お互いを少し戻す
                    this.transform.position = new Vector3(this.transform.position.x - (contactArea - distance) / 2, this.transform.position.y, this.transform.position.z);
                }
            }
        }
    }
}
