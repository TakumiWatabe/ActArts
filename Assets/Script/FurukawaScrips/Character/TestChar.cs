using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : MonoBehaviour {

    //攻撃ヒット時処理スクリプト

    //ヒット時攻撃判定ステータス取得用
    private struct HitState
    {
        public int damage;
        public string attri;
        public float startCorr;
        public float comboCorr;
        public int atkLev;
        public int blockStun;
        public int hitStun; 
    };

    //敵取得用
    PlayerController Pcont;
    //コライダーイベント
    ColliderEvent CEvent;   //自身
    ColliderEvent CEventEne;   //敵
    //HPディレクター
    HPDirectorScript HPDir;
    //対戦相手取得用
    DebugGetEnemyScript DEGScript;
    //エフェクト発生用
    SetEffectScript SEScript;

    //あたり判定群
    List<GameObject> col = new List<GameObject>();
    List<ColliderReact> react = new List<ColliderReact>();

    //あたり判定ID
    private int collID;

    //操作キャラクター番号
    private int numID = 0;

    //のけぞり判定時間
    [SerializeField,Range(1,60)]
    private int time = 30;
    //経過時間
    private float timecCnt = 0;

    private bool hitatk;

    //技情報取得用
    private ArtsStateScript ASScript;
    private ArtsStateScript ASScriptEne;

    //技性能
    private HitState hitColSta;


    void Awake()
    {
        Pcont = this.GetComponent<PlayerController>();
        ASScript = this.GetComponent<ArtsStateScript>();
    }

    // Use this for initialization
    void Start()
    {
        //ディレクタースクリプト取得
        ASScriptEne = Pcont.fightEnemy.GetComponent<ArtsStateScript>();
        CEvent = this.GetComponent<ColliderEvent>();
        CEventEne = Pcont.fightEnemy.GetComponent<ColliderEvent>();
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
                HPDir.hitDmage(ASScriptEne.Damage((int)CEventEne.GetType));
                Pcont.HitDamage(ASScriptEne.Damage((int)CEventEne.GetType));
                //攻撃を食らったあたり判定のIDを取得
                collID = i;
                //飛び道具消失
                toolVoid();
                //エフェクト発生位置計算
                SEScript.caluclation(CEvent.GetHitBoxs[i], react[i].CObj.GetComponent<BoxCollider>());
                SEScript.appearEffe(ASScriptEne.AtkLev((int)CEventEne.GetType));

                //受けた攻撃のステータス
                CreateArtsSatet(ASScriptEne, (int)CEventEne.GetType);
            }

            //攻撃を食らっているなら
            if (react[i].CObj != null)
            {
                //react[i].CObj.SetActive(false);

                //のけぞり時間中ならあたり判定しない
                if (time >= timecCnt)
                {
                    timecCnt++;
                }
                else if (time < timecCnt)
                {
                    //のけぞり時間外になったらあたり判定する
                    timecCnt = 0;
                    collID = CEvent.HClid.Count;
                    react[i].CObj = null;
                }
            }
        }
    }

    //飛び道具消失判定
    void toolVoid()
    {
        //攻撃判定が飛び道具なら
        if (CEventEne.GetType == ValueScript.AtkVal.HADOUKEN)
        {
            //飛び道具を消す
            Debug.Log(Pcont.fightEnemy.GetComponent<PlayerController>().GetHadou.name);
            Destroy(Pcont.fightEnemy.GetComponent<PlayerController>().GetHadou);
        }
    }

    //攻撃時の性能を取得
    private void CreateArtsSatet(ArtsStateScript arts,int type)
    {
        hitColSta.damage = arts.Damage(type);
        hitColSta.attri = arts.Attri(type);
        hitColSta.startCorr = arts.StartCorr(type);
        hitColSta.comboCorr = arts.CombCorr(type);
        hitColSta.atkLev = arts.AtkLev(type);
        hitColSta.blockStun = arts.BlockStun(type);
        hitColSta.hitStun = arts.HitStun(type);
    }

    //変数取得関数
    public bool hitDamage
    {
        get { return hitatk; }
        set { hitatk = value; }
    }
    public int CID { get { return collID; } }

    //データ取得
    public int Damage(int ID) { return hitColSta.damage; }
    public string Attri(int ID) { return hitColSta.attri; }
    public float StartCorr(int ID) { return hitColSta.startCorr; }
    public float CombCorr(int ID) { return hitColSta.comboCorr; }
    public int AtkLev(int ID) { return hitColSta.atkLev; }
    public int BlockStun(int ID) { return hitColSta.blockStun; }
    public int HitStun(int ID) { return hitColSta.hitStun; }
}
