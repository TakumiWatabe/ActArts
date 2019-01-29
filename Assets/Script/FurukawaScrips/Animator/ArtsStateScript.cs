using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsStateScript : MonoBehaviour {

    //技情報設定スクリプト

    //技の性能用変数//public
    struct ArtsStatus
    {
        public int damage;          //技のダメージ
        public string attri;        //技の属性
        public float startCorr;     //初動ダメージ
        public float comboCorr;     //コンボダメージ
        public int atkLev;          //攻撃レベル
        public int blockStun;       //ガードしたときに行動ができるようになるまでのフレーム
        public int hitStun;         //技を食らったときに行動ができるようになるまでのフレーム
    };

    //CSV読み込みオブジェクト
    private GameObject dir;
    private ReadCSV csv;
    private ValueScript VScript;

    //プレイヤーネーム(大文字)
    [SerializeField]
    private string playerName;

    //技データ集
    private Dictionary<string, ReadCSV.CharaData> artsData;
    private ArtsStatus[] arts = new ArtsStatus[(int)ValueScript.AtkVal.ATK_NUM];

    void Awake()
    {
        dir = GameObject.Find("BattleDirecter");
        csv = dir.GetComponent<ReadCSV>();
        VScript = dir.GetComponent<ValueScript>();
    }

    // Use this for initialization
    void Start ()
    {
        //CSV読み込みスクリプト取得
        csv = dir.GetComponent<ReadCSV>();
        //キャラクターの技データ集を取得
        artsData = csv.readCSVData(playerName);

        //データ設定
        Data(arts);
    }

    //データ設定関数
    private void Data(ArtsStatus[] AS)
    {
        for (int i = 0; i < AS.Length; i++)
        {
            AS[i].damage = artsData[csv.Skills[i]].damage;
            AS[i].attri = artsData[csv.Skills[i]].attri;
            AS[i].startCorr = artsData[csv.Skills[i]].startCorrec;
            AS[i].comboCorr = artsData[csv.Skills[i]].conboCorrec;
            AS[i].atkLev = artsData[csv.Skills[i]].attackLevel;
            AS[i].blockStun = artsData[csv.Skills[i]].blockStun;
            AS[i].hitStun = artsData[csv.Skills[i]].hitStun;
        }
    }

    //データ取得
    public int Damage(int ID) { return VScript.ArtsDame(arts[ID].damage, arts[ID].startCorr); }
    public string Attri(int ID) { return arts[ID].attri; }
    public float StartCorr(int ID) { return arts[ID].startCorr; }
    public float CombCorr(int ID) { return arts[ID].comboCorr; }
    public int AtkLev(int ID) { return arts[ID].atkLev; }
    public int BlockStun(int ID) { return arts[ID].blockStun; }
    public int HitStun(int ID) { return arts[ID].hitStun; }
}
