using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : MonoBehaviour {

    //攻撃ヒット時処理スクリプト

    //コライダーイベント
    ColliderEvent CEvent;
    //HPディレクター
    HPDirectorScript HPDir;
    //対戦相手取得用
    DebugGetEnemyScript DEGScript;
    //エフェクト発生用
    SetEffectScript SEScript;

    //あたり判定群
    List<GameObject> col = new List<GameObject>();
    List<ColliderReact> react = new List<ColliderReact>();

    //操作キャラクター番号
    private int numID = 0;

    //のけぞり判定時間
    [SerializeField,Range(1,60)]
    private int time = 30;
    //経過時間
    private float timecCnt = 0;

    private bool hitatk;

    //技情報取得用
    private GameObject Opponent;
    private ArtsStateScript ASScript;

    void Awake()
    {
        ASScript = Opponent.GetComponent<ArtsStateScript>();
    }

    // Use this for initialization
    void Start()
    {
        //ディレクタースクリプト取得
        CEvent = this.GetComponent<ColliderEvent>();
        HPDir = this.GetComponent<HPDirectorScript>();
        DEGScript = this.GetComponent<DebugGetEnemyScript>();
        SEScript = this.GetComponent<SetEffectScript>();

        //コライダーのスクリプトを取得
        for (int i = 0; i < CEvent.HClid.Count; i++)
        {
            col.Add(CEvent.HClid[i]);
            react.Add(col[i].GetComponent<ColliderReact>());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //攻撃くらい判定
        hitJudg();
	}

    //攻撃ヒット判定
    private void hitJudg()
    {
        //くらい判定の数
        for (int i = 0; i < CEvent.HClid.Count; i++)
        {
            //攻撃が当たっているなら
            if (react[i].hiting && react[i].CObj != null)
            {
                Debug.Log("ダメージを与えた!!");
                hitatk = true;
                react[i].hiting = false;
                //ダメージ分HPゲージを減らす
                HPDir.hitDmage(ASScript.Damage((int)CEvent.GetType));
                //飛び道具消失
                toolVoid();
                //エフェクト発生位置計算
                SEScript.caluclation(CEvent.GetHitBoxs[i], react[i].CObj.GetComponent<BoxCollider>());
                SEScript.appearEffe(ASScript.AtkLev((int)CEvent.GetType));
            }

            //攻撃を食らっているなら
            if (react[i].CObj != null)
            {
                react[i].CObj.SetActive(false);

                //のけぞり時間中ならあたり判定しない
                if (time >= timecCnt)
                {
                    timecCnt++;
                }
                else if (time < timecCnt)
                {
                    //のけぞり時間外になったらあたり判定する
                    timecCnt = 0;
                    react[i].CObj = null;
                }
            }
        }
    }

    //飛び道具消失判定
    void toolVoid()
    {
        //攻撃判定が飛び道具なら
        if (CEvent.GetType == ValueScript.AtkVal.HADOUKEN)
        {
            ////飛び道具を消す
            //Debug.Log(/*シリアライズで取ってくる予定の波動拳君をここまで引っ張ってくる*/);
            //Destroy(/*シリアライズで取ってくる予定の波動拳君をここまで引っ張ってくる*/);
        }
    }

    //変数取得関数
    public bool hitDamage { get { return hitatk; } }
}
