using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : MonoBehaviour {

    //攻撃ヒット時処理スクリプト

    //バトルディレクター
    //[SerializeField]
    GameObject dir;
    BattleDirector BtDir;
    //コライダーイベント
    ColliderEvent CEvent;
    //HPディレクター
    HPDirectorScript HPDir;

    //各種エフェクト
    [SerializeField]
    GameObject effectStr;
    [SerializeField]
    GameObject effectMid;
    [SerializeField]
    GameObject effectWeak;


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

    //時間を遅くしている時間
    [SerializeField, Range(1, 100)]
    private float slowTime = 10;
    private float elapsedTime = 0;

    private bool hitatk = false;

    private GameObject dirSys;
    private TextGenerator textGene;
    private GameObject BattleText;
    void Awake()
    {
        dir = GameObject.Find("BattleDirecter");
        BtDir = dir.GetComponent<BattleDirector>();
        dirSys = GameObject.Find("TextFactory");
        textGene = dirSys.GetComponent<TextGenerator>();
        BattleText = GameObject.Find("GameText");
    }

    // Use this for initialization
    void Start()
    {
        //ディレクタースクリプト取得
        CEvent = this.GetComponent<ColliderEvent>();
        HPDir = this.GetComponent<HPDirectorScript>();

        //コライダーのスクリプトを取得
        for (int i = 0; i < CEvent.HClid.Count; i++)
        {
            col.Add(CEvent.HClid[i]);
            react.Add(col[i].GetComponent<ColliderReact>());
        }

        charcterJudg(this.gameObject.tag);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //キャラクター番号から相手キャラクターを判別
        switch (numID)
        {
            case (int)ValueScript.FightChar.CHARA_1:
                hitJudg(1);
                break;
            case (int)ValueScript.FightChar.CHARA_2:
                hitJudg(0);
                break;
            default:
                break;
        }

        //if (textGene.gameStateNum != 1 || BattleText.activeSelf == true)
        //{
        //    Time.timeScale = 0;
        //}
        //else if (textGene.gameStateNum == 1)
        //{
        //    Time.timeScale = 1;
        //}


        if (hitatk)
        {
            elapsedTime++;
            Time.timeScale = 0;
        }

        if(slowTime<=elapsedTime)
        {
            hitatk = false;
            elapsedTime = 0;
            Time.timeScale = 1;
        }

	}

    //攻撃ヒット判定
    private void hitJudg(int charNum)
    {
        //くらい判定の数
        for (int i = 0; i < CEvent.HClid.Count; i++)
        {
            //攻撃が当たっているなら
            if (react[i].hiting)
            {
                //技の数
                for (int j = 0; j < (int)ValueScript.AtkVal.ATK_NUM; j++)
                {
                    //技の攻撃判定の数
                    for (int k = 0; k < BtDir.CCount(0, j); k++)
                    {
                        //攻撃の種類を判定
                        if (react[i].CObj.name == BtDir.Fcollider(charNum, j, k).name)
                        {
                            Debug.Log("ダメージを与えた！！！");
                            //判定した攻撃の威力分ダメージを受ける
                            HPDir.hitDmage(BtDir.Fattack(0, j));
                            react[i].hiting = false;

                            Vector3 position = new Vector3(this.transform.parent.transform.position.x, this.transform.parent.transform.position.y, this.transform.parent.transform.position.z);

                            Vector3 body = GetComponent<ColliderEvent>().GetHitBoxs[i].center + this.transform.parent.transform.position;
                            Vector3 attack = new Vector3(0, 0, 0);
                            for (int l = 0; l < GetComponent<ColliderEvent>().AClid.Count; l++)
                            {
                                if (react[i].CObj.name == GetComponent<ColliderEvent>().GetAtkBoxs[l].name)
                                {
                                    attack = GetComponent<ColliderEvent>().GetAtkBoxs[l].center + this.transform.parent.transform.position;
                                }

                            }
                            //攻撃判定の場所
                            Vector3 pos1 = BtDir.Fcollider(charNum, j, k).GetComponent<Collider>().ClosestPointOnBounds(position);

                            //エフェクト発生場所
                            Vector3 effectPos = (body + attack) / 2;

                            hitatk = true;

                            //エフェクト発生
                            if (BtDir.Fattack(0, j) > 800) Instantiate(effectStr, effectPos, Quaternion.identity);
                            else if (BtDir.Fattack(0, j) > 500) Instantiate(effectMid, effectPos, Quaternion.identity);
                            else if (BtDir.Fattack(0, j) <= 500) Instantiate(effectWeak, effectPos, Quaternion.identity);

                            Debug.Log(react[i].CObj.transform.root.GetChild(0).name);
                            Debug.Log(BtDir.Fcollider(charNum, j, k).transform.root.GetChild(0).name);

                            if (BtDir.Fcollider(charNum, j, k).layer == 16)
                            {
                                switch (numID)
                                {
                                    case (int)ValueScript.FightChar.CHARA_1:
                                        switch (react[i].CObj.transform.root.GetChild(0).name)
                                        {
                                            case "Aoi":
                                                Destroy(BtDir.Fighter(1).transform.GetChild(0).GetChild(44).gameObject);
                                                break;
                                            case "Hikari":
                                                Destroy(BtDir.Fighter(1).transform.GetChild(0).GetChild(39).gameObject);
                                                break;
                                        }
                                        break;

                                    case (int)ValueScript.FightChar.CHARA_2:
                                        switch (react[i].CObj.transform.root.GetChild(0).name)
                                        {
                                            case "Aoi":
                                                Destroy(BtDir.Fighter(0).transform.GetChild(0).GetChild(44).gameObject);
                                                break;
                                            case "Hikari":
                                                Destroy(BtDir.Fighter(0).transform.GetChild(0).GetChild(39).gameObject);
                                                break;
                                        }
                                        break;
                                }
                            }

                            GetComponent<PlayerController>().HitDamage(BtDir.Fattack(0, j));
                        }
                    }
                }
            }

            if (react[i].CObj != null)
            {
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

    private void charcterJudg(string tagname)
    {
        if (tagname == "P1")
        {
            //操作番号決定
            numID = (int)ValueScript.FightChar.CHARA_1;
        }
        else if (tagname == "P2")
        {
            //操作番号決定
            numID = (int)ValueScript.FightChar.CHARA_2;
        }
    }
}
