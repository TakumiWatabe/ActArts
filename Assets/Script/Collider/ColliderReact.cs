using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReact : MonoBehaviour {

    //技の性能用変数
    struct ArtsState
    {
        public string attri;        //技の属性
        public float startCorr;     //初動ダメージ
        public float comboCorr;     //コンボダメージ
        public int atkLev;          //攻撃レベル
        public int blockStun;       //ガードしたときに行動ができるようになるまでのフレーム
        public int hitStun;         //技を食らったときに行動ができるようになるまでのフレーム
    };

    //CSV読み込みオブジェクト
    //[SerializeField]
    private GameObject dir;
    private ReadCSV csv;
    private GameObject contl;
    private InstanceScript InScript;

    //プレイヤーネーム(大文字)
    [SerializeField]
    private string playerName;

    //技データ集
    private Dictionary<string, ReadCSV.CharaData> artsData;
    private const int act = 10;

    //あたり判定のタグ名
    private string colliderTag;

    //攻撃接触判定
    private bool hitAtk = false;
    //攻撃判定オブジェクト
    private GameObject collid = null;

    bool flag = false;

    private ArtsState arts;

    //あたり判定に当たったら
    void OnTriggerEnter(Collider other)
    {
        //くらい判定なら
        if (colliderTag == "Hit")
        {
            //攻撃未接触＆判定が攻撃なら＆判定登録されてないなら
            if (!hitAtk && other.gameObject.tag == "Attack" && !collid)
            {
                //攻撃がヒット
                hitAtk = true;
                //攻撃判定を記憶
                collid = other.gameObject;

                Debug.Log("攻撃が当たった！！！");
            }
        }
    }

    void Awake()
    {
        dir = GameObject.Find("BattleDirecter");
        csv = dir.GetComponent<ReadCSV>();
        contl= GameObject.Find("FighterComtrol");
        InScript = contl.GetComponent<InstanceScript>();
        colliderTag = this.gameObject.tag;
    }

    // Use this for initialization
    void Start()
    {
        //CSV読み込みスクリプト取得
        csv = dir.GetComponent<ReadCSV>();
        //Debug.Log(this.transform.root.tag);
        //キャラクターの技データ集を取得
        artsData = csv.readCSVData(playerName);
    

        if (colliderTag == "Attack")
        {

            //データ設定
            Data(this.gameObject.layer - act);
        }
        else
        {
            csv = null;
        }
    }

    //衝突判定取得
    public bool hiting
    {
        set { hitAtk = value; }
        get { return hitAtk; }
    }
    public GameObject CObj
    {
        set { collid = value; }
        get { return collid; }
    }

    private void Data(int DataID)
    {
        arts.attri = artsData[csv.Skills[DataID]].attri;
        arts.startCorr = artsData[csv.Skills[DataID]].startCorrec;
        arts.comboCorr = artsData[csv.Skills[DataID]].conboCorrec;
        arts.atkLev = artsData[csv.Skills[DataID]].attackLevel;
        arts.blockStun = artsData[csv.Skills[DataID]].blockStun;
        arts.hitStun = artsData[csv.Skills[DataID]].hitStun;
    }

    //データ取得
    public string Attri { get { return arts.attri; } }
    public float StartCorr { get { return arts.startCorr; } }
    public float CombCorr { get { return arts.comboCorr; } }
    public int AtkLev { get { return arts.atkLev; } }
    public int BlockStun { get { return arts.blockStun; } }
    public int HitStun { get { return arts.hitStun; } }
}
