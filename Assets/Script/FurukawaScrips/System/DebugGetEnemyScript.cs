using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGetEnemyScript : MonoBehaviour
{
    //対戦相手
    private GameObject enemy;
    private PlayerController enemyScript;

    [SerializeField]
    private GameObject debugEnemy;

    //キャラクター生成オブジェクト
    private GameObject contl = null;
    private InstanceScript InScript = null;

    //向き
    bool direction = true;

    //コントローラー番号
    private int controller = 0;

    void Awake()
    {
        //オブジェクトを登録
        contl = GameObject.Find("FighterComtrol");
        if (contl != null)
        {
            //スクリプトを取得
            InScript = contl.GetComponent<InstanceScript>();
        }
    }

    // Use this for initialization
    void Start()
    {
        //初期設定
        InitState();

        Debug.Log(enemy.tag);
        //対戦相手のコントローラー取得
        enemyScript = enemy.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //キャラクターの向きを設定
        SetDirection();
    }


    //相手の位置から方向を決める関数
    private bool SetDirection()
    {
        //(自身が)右向き
        if (enemy.transform.position.x >= transform.position.x)
        {
            direction = true;
        }
        //(自身が)左向き
        else
        {
            direction = false;
        }

        return direction;
    }

    //初期設定関数
    private void InitState()
    {
        //プレイヤーが生成されているなら
        if (contl != null)
        {
            //プレイヤー１
            if (this.gameObject.tag == "P1")
            {
                enemy = InScript.Fighter(1);
                controller = (int)ValueScript.Controller.CONTROLLER_1;
                return;
            }
            //プレイヤー２
            enemy = InScript.Fighter(0);
            controller = (int)ValueScript.Controller.CONTROLLER_2;
            this.transform.position = new Vector3(1, this.transform.position.y, this.transform.position.z);
            return;
        }
        //デバック用の相手
        enemy = debugEnemy;
        return;
    }

    //対戦相手を取得
    public GameObject EObj { get { return enemy; } }
    //コントローラー取得
    public PlayerController PCscript { get { return enemyScript; } }
    //向きを取得
    public bool GetDir { get { return direction; } }
}
