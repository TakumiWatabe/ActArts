using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDirector : MonoBehaviour {

    //アクションステータス
    private struct FS
    {
        //攻撃判定
        public List<GameObject> co;
        //攻撃力
        public int ac;

        //初期化関数
        public void Reset() { co = new List<GameObject>(); }
    };

    //操作キャラクター判別用
    private struct FightPlayer
    {
        //操作キャラクター
        public GameObject fightCharacter;
        //キャラクターID
        public int playNumber;
    };

    //キャラクター生成オブジェクト
    [SerializeField]
    private GameObject contl;
    private InstanceScript InScript;

    ////キャラクターオブジェクト
    //[SerializeField]
    private GameObject[] character = new GameObject[2];
    private const int act = 10;

    //プレイヤーネーム(大文字)
    //[SerializeField]
    private string player1Name,player2Name;

    //キャラクタースクリプト
    private ColliderEvent[] CEvent = new ColliderEvent[2];
    private ReadCSV csv;

    //アクションステータス
    private FS[,] f = new FS[(int)ValueScript.FightChar.CHARA_NUM, (int)ValueScript.AtkVal.ATK_NUM];
    //キャラクターステータス
    private FightPlayer[] fp = new FightPlayer[(int)ValueScript.FightChar.CHARA_NUM];

    //技データ集
    private Dictionary<string, ReadCSV.CharaData>[] artsData = new Dictionary<string, ReadCSV.CharaData>[2];

    void Awake()
    {
        InScript = contl.GetComponent<InstanceScript>();
    }

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < 2; i++)
        {
            character[i] = InScript.Fighter(i);
            switch(InScript.charName(i))
            {
                case "Aoi":
                    player1Name = "AOI";
                    ////player2Name = "AOI";
                    break;
                case "Hikari":
                    //player1Name = "HIKARI";
                    player2Name = "HIKARI";
                    break;
                default:
                    break;
            }
        }


        //スクリプト取得
        //Aoi
        CEvent[0] = character[(int)ValueScript.FightChar.CHARA_1].GetComponent<ColliderEvent>();
        //Hikari
        CEvent[1] = character[(int)ValueScript.FightChar.CHARA_2].GetComponent<ColliderEvent>();

        //CSV読み込みスクリプト取得
        csv = GetComponent<ReadCSV>();
        //キャラクターの技データ集を取得
        artsData[(int)ValueScript.FightChar.CHARA_1] = csv.readCSVData(player1Name);
        artsData[(int)ValueScript.FightChar.CHARA_2] = csv.readCSVData(player2Name);

        //配列を使うために初期化を行う
        for (int i=0;i<(int)ValueScript.FightChar.CHARA_NUM;i++)
        {
            for(int j=0;j<(int)ValueScript.AtkVal.ATK_NUM;j++)
            {
                f[i, j].Reset();
            }
        }

        //技ごとの攻撃判定設定
        Burger();
        RevolvingStake();

        //キャラクターセレクトからキャラクターを取得
        fp[(int)ValueScript.FightChar.CHARA_1].fightCharacter = character[(int)ValueScript.FightChar.CHARA_1];
        fp[(int)ValueScript.FightChar.CHARA_1].playNumber = (int)ValueScript.FightChar.CHARA_1;
        fp[(int)ValueScript.FightChar.CHARA_2].fightCharacter = character[(int)ValueScript.FightChar.CHARA_2];
        fp[(int)ValueScript.FightChar.CHARA_2].playNumber = (int)ValueScript.FightChar.CHARA_2;

        for (int i = 0; i < (int)ValueScript.FightChar.CHARA_NUM; i++)
        {
            for (int j = 0; j < (int)ValueScript.AtkVal.ATK_NUM; j++)
            {

                Debug.Log(f[i, j].co.Count);
            }
        }
    }

    //技の威力を設定する関数
    private void RevolvingStake()
    {
        for (int i = 0; i < (int)ValueScript.FightChar.CHARA_NUM; i++)
        {
            for (int j = 0; j < (int)ValueScript.AtkVal.ATK_NUM; j++)
            {
                //威力設定
                f[i, j].ac = artsData[i][csv.Skills[j]].damage;
            }
        }
    }

    //あたり判定の判別変数設定関数
    private void Burger()
    {
        //キャラの数
        for (int i = 0; i < (int)ValueScript.FightChar.CHARA_NUM; i++)
        {
            //攻撃技の数
            for (int j = 0; j < (int)ValueScript.AtkVal.ATK_NUM; j++)
            {
                //全攻撃判定の数
                for (int k = 0; k < CEvent[i].AClid.Count; k++)
                {
                    //攻撃判定のレイヤーが一致しているなら
                    if (CEvent[i].AClid[k].layer == j + act)
                    {
                        f[i, j].co.Add(CEvent[i].AClid[k]);
                    }
                }
            }
        }
    }

    //攻撃判定の取得
    public GameObject Fcollider(int ft, int at,int num) { return f[ft, at].co[num]; }
    //技威力の取得
    public int Fattack(int ft, int at) { return f[ft, at].ac; }
    //攻撃判定数の取得
    public int CCount(int ft, int at) { return f[ft, at].co.Count; }
    //キャラクターの取得
    public GameObject Fighter(int ft) { return fp[ft].fightCharacter; }
    //キャラクターIDの取得
    public int FNumber(int ft) { return fp[ft].playNumber; }
}
